import React, { ClassicComponent, useEffect, useState } from "react";
import useAuth from "../contexts/AuthContext";
import { dayPlansSeed } from "../seed/lessonPlannerSeed";
import { getDayName } from "../utils/dateUtils";
import { v1 as uuidv1 } from "uuid";
import LessonPlanCalendarEntry from "../components/planner/LessonPlanCalendarEntry";
import BreakCalendarEntry from "../components/planner/BreakCalendarEntry";
import EventCalendarEntry from "../components/planner/EventCalendarEntry";
import AfterSchoolCalendarEntry from "../components/planner/AfterSchoolCalendarEntry";

const LessonPlannerPage = ({ numLessons, numBreaks, lessonLength, weekNumber, dayPlans }: LessonPlannerProps) => {
  const { user } = useAuth();
  const [gridRows, setGridRows] = useState<string>("");
  //#region seed data
  // TODO: get these values from the database
  numLessons = 6;
  numBreaks = 2;
  lessonLength = 30;
  weekNumber = 1;
  dayPlans = dayPlansSeed;
  const [calendarEntryLists, setCalendarEntryLists] = useState<CalendarEntry[][]>([]);
  const numRows = numLessons + numBreaks + 2; // +2 for header row and after school
  //#endregion

  useEffect(() => {
    let rows = "0.5fr "; // week and day header row
    let entries: CalendarEntry[] = dayPlans[0].lessonPlans;
    entries = entries.concat(dayPlans[0].breaks);

    if (dayPlans[0].events.length) {
      entries = entries.concat(dayPlans[0].events[0]);
    }

    const sortedEntries = sortEntries(entries);

    for (let i = 0; i < sortedEntries.length; i++) {
      const entry = sortedEntries[i];
      if (isLessonPlan(entry) || isSchoolEvent(entry)) {
        rows += `repeat(${entry.numberOfPeriods}, minmax(0, 1fr))`;
      } else {
        rows += "0.5fr";
      }

      if (i === sortedEntries.length - 1) {
        rows += " 0.5fr"; // after school row
      } else {
        rows += " ";
      }
    }

    setGridRows(rows);
  }, []);

  function sortEntries(entries: CalendarEntry[]): CalendarEntry[] {

    const sortedEntries = entries.sort((a, b) => {
      if (a.periodNumber < b.periodNumber) {
        return -1;
      } else if (a.periodNumber > b.periodNumber) {
        return 1;
      } else {
        return 0;
      }
    });

    return sortedEntries;
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

  return (
    <div className="flex flex-col h-full">
      <h1 className="text-4xl text-center p-3">Hi {user!.firstName}!</h1>
      <div style={{ gridTemplateRows: gridRows }} className={`grid grid-cols-6 border-l border-t border-darkGreen m-3 flex-grow`}>
        <div className="row-start-1 col-start-1 border-r border-b border-darkGreen text-lg font-bold">Week {weekNumber}</div>
        <div className="row-start-2 col-start-1 border-r border-b border-darkGreen text-lg">Lesson 1</div>
        <div className="row-start-3 col-start-1 border-r border-b border-darkGreen text-lg">Lesson 2</div>
        <div className="row-start-4 col-start-1 border-r border-b border-darkGreen h-12 text-lg">Break 1</div>
        <div className="row-start-5 col-start-1 border-r border-b border-darkGreen text-lg">Lesson 3</div>
        <div className="row-start-6 col-start-1 border-r border-b border-darkGreen text-lg">Lesson 4</div>
        <div className="row-start-[7] col-start-1 border-r border-b border-darkGreen h-12 text-lg">Break 2</div>
        <div className="row-start-[8] col-start-1 border-r border-b border-darkGreen text-lg">Lesson 5</div>
        <div className="row-start-[9] col-start-1 border-r border-b border-darkGreen text-lg">Lesson 6</div>
        <div className="row-start-[10] col-start-1 border-r border-b border-darkGreen h-12 text-lg">After School</div>
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
