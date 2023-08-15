import React, { useEffect, useState } from "react";
import useAuth from "../contexts/AuthContext";
import { dayPlansSeed, dayPlanPatternSeed } from "../seed/lessonPlannerSeed";
import { getCalendarTime, getDayName } from "../utils/dateUtils";
import { v1 as uuidv1 } from "uuid";
import LessonPlanCalendarEntry from "../components/planner/LessonPlanCalendarEntry";
import BreakCalendarEntry from "../components/planner/BreakCalendarEntry";
import EventCalendarEntry from "../components/planner/EventCalendarEntry";
import AfterSchoolCalendarEntry from "../components/planner/AfterSchoolCalendarEntry";

const LessonPlannerPage = ({ numLessons, numBreaks, lessonLength, weekNumber, dayPlans, dayPlanPattern }: LessonPlannerProps) => {
  const { user } = useAuth();
  const [gridRows, setGridRows] = useState<string>("");
  const [sortedCalendarEntries, setSortedCalendarEntries] = useState<CalendarEntry[][]>([]);

  //#region seed data
  // TODO: get these values from the database
  numLessons = 6;
  numBreaks = 2;
  lessonLength = 30;
  weekNumber = 1;
  dayPlans = dayPlansSeed;
  dayPlanPattern = dayPlanPatternSeed;
  const numRows = numLessons + numBreaks + 2; // +2 for header row and after school
  //#endregion

  useEffect(() => {
    const sortedCalendarEntries = sortCalendarEntries(dayPlans)
    setSortedCalendarEntries(sortedCalendarEntries);

    let rows = "0.5fr "; // week and day header row
    let entries = sortedCalendarEntries[0];

    for (let i = 0; i < entries.length; i++) {
      const entry = entries[i];
      if (isLessonPlan(entry) || isSchoolEvent(entry)) {
        for (let j = 0; j < entry.numberOfPeriods; j++) {
          rows += "minmax(0, 1fr) ";
        }
      } else {
        rows += "0.5fr";
      }

      if (i === entries.length - 1) {
        rows += " 0.5fr"; // after school row
      } else {
        rows += " ";
      }
    }

    setGridRows(rows);
  }, []);

  function sortCalendarEntries(dayPlans: DayPlan[]): CalendarEntry[][] {
    const entries: CalendarEntry[][] = [];

    for (const dayPlan of dayPlans) {
      let unsortedEntries: CalendarEntry[] = dayPlan.lessonPlans;
      unsortedEntries = unsortedEntries.concat(dayPlan.breaks).concat(dayPlan.events);

      const sortedEntries = unsortedEntries.sort((a, b) => {
        if (a.periodNumber < b.periodNumber) {
          return -1;
        } else if (a.periodNumber > b.periodNumber) {
          return 1;
        } else {
          return 0;
        }
      });

      entries.push(sortedEntries);
    }

    return entries;
  }

  function renderCalendarHeaders(dayPlans: DayPlan[]): React.ReactNode[] {
    const renderedCalendarHeaders = [] as React.ReactNode[];
    const numberOfLessons = dayPlans[0].lessonPlans.reduce((acc, curr) => acc + curr.numberOfPeriods, 0);
    const iterations = numberOfLessons + dayPlans[0].breaks.length + dayPlans[0].events.length;

    let lessonNumber = 1;
    let breakNumber = 1;

    renderedCalendarHeaders.push(
      <div key={`calendarHeader1`} className="row-start-1 col-start-1 border-r border-b border-darkGreen text-center text-lg font-bold">Week {weekNumber}</div>
    )

    for (let i = 0; i < iterations; i++) {
      if (dayPlanPattern.pattern[i].type === "LessonPlan") {
        renderedCalendarHeaders.push(
          <div key={`lessonHeader${lessonNumber}`} className={`row-start-[${i + 2}] col-start-1 border-r border-b border-darkGreen text-center text-lg`}>
            <h3>Lesson {lessonNumber}</h3>
            <p>{getCalendarTime(dayPlanPattern.pattern[i].startTime)} - {getCalendarTime(dayPlanPattern.pattern[i].endTime)}</p>
          </div>
        )

        lessonNumber++;
      } else if (dayPlanPattern.pattern[i].type === "Break") {
        renderedCalendarHeaders.push(
          <div key={`breakHeader${breakNumber}`} className={`row-start-[${i + 2}] col-start-1 border-r border-b border-darkGreen text-center text-lg`}>
            <h3>Break {breakNumber}</h3>
            <p>{getCalendarTime(dayPlanPattern.pattern[i].startTime)} - {getCalendarTime(dayPlanPattern.pattern[i].endTime)}</p>
          </div>
        )
        breakNumber++;
      }
    }

    renderedCalendarHeaders.push(
      <div key={`afterSchoolHeader`} className="row-start-[10] col-start-1 border-r border-b border-darkGreen text-center text-lg">After School</div>
    )

    return renderedCalendarHeaders;
  }

  function renderCalendarEntries(dayPlans: DayPlan[]): React.ReactNode[][] {
    const renderedCalenderEntriesList = dayPlans.map((dayPlan, idx) => {
      const renderedCalendarEntries = [] as React.ReactNode[];

      let lessonPlanPos = 0;
      let breakPos = 0;
      let schoolEventPos = 0;

      renderedCalendarEntries.push(
        <div key={uuidv1()} className="row-start-1 border-r border-b border-darkGreen">
          {getDayName(dayPlan.endTime)}
        </div>
      )

      for (let i = 0; i < numRows - 2; i++) {

        if (lessonPlanPos < dayPlan.lessonPlans.length && dayPlan.lessonPlans[lessonPlanPos].periodNumber === i + 1) {
          renderedCalendarEntries.push(<LessonPlanCalendarEntry key={uuidv1()} lessonPlan={dayPlan.lessonPlans[lessonPlanPos]} columnIndex={idx} />);

          if (dayPlan.lessonPlans[lessonPlanPos].numberOfPeriods !== 1) {
            i += dayPlan.lessonPlans[lessonPlanPos].numberOfPeriods - 1;
          }

          lessonPlanPos++;
        } else if (dayPlan.events.length && dayPlan.events[schoolEventPos].periodNumber === i + 1) {
          renderedCalendarEntries.push(<EventCalendarEntry key={uuidv1()} schoolEvent={dayPlan.events[schoolEventPos]} columnIndex={idx} rowIndex={i + 2} />);

          if (dayPlan.events[schoolEventPos].numberOfPeriods !== 1) {
            i += dayPlan.events[schoolEventPos].numberOfPeriods - 1;
          }

          schoolEventPos++;
        } else if (breakPos < dayPlan.breaks.length) {
          renderedCalendarEntries.push(<BreakCalendarEntry key={uuidv1()} lessonBreak={dayPlan.breaks[breakPos]} columnIndex={idx} rowIndex={i + 2} />);
          breakPos++;
        }
      }

      renderedCalendarEntries.push(
        <AfterSchoolCalendarEntry key={uuidv1()} rowIndex={numRows} afterSchoolActivity="Crossing duty" />
      )

      return renderedCalendarEntries;
    });


    return renderedCalenderEntriesList;
  }

  function isLessonPlan(entry: CalendarEntry): entry is LessonPlan {
    return (entry as LessonPlan).subject !== undefined;
  }

  function isSchoolEvent(entry: CalendarEntry): entry is SchoolEvent {
    return (entry as SchoolEvent).location !== undefined;
  }

  function isBreak(entry: CalendarEntry): entry is Break {
    return !isLessonPlan(entry) && !isSchoolEvent(entry);
  }


  return (
    <div className="flex flex-col h-full">
      <h1 className="text-4xl text-center p-3">Hi {user!.firstName}!</h1>
      <div style={{ gridAutoRows: gridRows }} className={`grid grid-cols-[minmax(7rem,_0.5fr),_repeat(5,_1fr)] border-l border-t border-darkGreen m-3 flex-grow`}>
        {renderCalendarHeaders(dayPlans)}
        {renderCalendarEntries(dayPlans)}
      </div>
    </div>
  );
};

export default LessonPlannerPage;

type LessonPlannerProps = {
  numLessons: number;
  numBreaks: number;
  lessonLength: number; // in mintues
  weekNumber: number;
  dayPlans: DayPlan[];
  dayPlanPattern: DayPlanPattern;
};

type CalendarEntry = LessonPlan | Break | SchoolEvent;

type DayPlanData = {
  lessonPlanPos: number;
  breakPos: number;
  schoolEventPos: number;
  nextLesson: LessonPlan | null;
  nextBreak: Break | null;
  nextSchoolEvent: SchoolEvent | null;
};
