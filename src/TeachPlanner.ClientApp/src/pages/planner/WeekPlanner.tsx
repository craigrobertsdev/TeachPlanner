import React, { useEffect, useRef, useState } from "react";
import useAuth from "../../contexts/AuthContext";
import { getCalendarDate, getCalendarTime, getDayName } from "../../utils/dateUtils";
import { v1 as uuidv1 } from "uuid";
import LessonPlanCalendarEntry from "../../components/planner/LessonPlanCalendarEntry";
import BreakCalendarEntry from "../../components/planner/BreakCalendarEntry";
import EventCalendarEntry from "../../components/planner/EventCalendarEntry";
import AfterSchoolCalendarEntry from "../../components/planner/AfterSchoolCalendarEntry";
import LessonHeader from "../../components/planner/LessonHeader";
import BreakHeader from "../../components/planner/BreakHeader";
import { useLoaderData, useNavigate } from "react-router-dom";
import { isLessonPlan, isSchoolEvent } from "../../utils/typeGuards";
import { baseUrl } from "../../utils/constants";

function WeekPlanner() {
  const { teacher } = useAuth();
  const [gridRows, setGridRows] = useState<string>("");
  const [selectedLessonEntryIndex, setSelectedLessonEntryIndex] = useState<GridCellLocation>({ row: -1, column: -1 });
  const loaderData = useLoaderData() as LessonPlanData;
  const navigate = useNavigate();

  // need lesson plans for each day of the week
  // also need the lesson plan pattern. this should be a uniform pattern throughout the year
  const { numLessons, numBreaks, weekNumber, dayPlans, dayPlanPattern } = loaderData;
  const numRows = numLessons + numBreaks + 2; // +2 for header row and after school

  useEffect(() => {
    const sortedCalendarEntries = sortCalendarEntries(dayPlans);

    setGridDimensions(sortedCalendarEntries);
  }, []);

  function sortCalendarEntries(dayPlans: DayPlan[]): CalendarEntry[][] {
    const entries: CalendarEntry[][] = [];

    for (const dayPlan of dayPlans) {
      let unsortedEntries: CalendarEntry[] = dayPlan.lessonPlans;
      unsortedEntries = unsortedEntries.concat(dayPlan.breaks).concat(dayPlan.events);

      const sortedEntries = unsortedEntries.sort((a, b) => {
        if (a.periodNumber < b.periodNumber) {
          return -1;
        }

        if (a.periodNumber > b.periodNumber) {
          return 1;
        }

        return 0;
      });

      entries.push(sortedEntries);
    }

    return entries;
  }

  function setGridDimensions(sortedCalendarEntries: CalendarEntry[][]): void {
    let rows = "0.5fr "; // week and day header row
    let entries = sortedCalendarEntries[0]; // the grid is uniform for each column so we only work out dimensions for the first one

    for (let i = 0; i < entries.length; i++) {
      const entry = entries[i];
      if (isLessonPlan(entry) || isSchoolEvent(entry)) {
        for (let j = 0; j < entry.numberOfPeriods; j++) {
          rows += "fit-content(max-content) ";
        }
      } else {
        rows += "0.3fr";
      }
      dayPlans[0].lessonPlans.reduce((acc, curr) => acc + curr.numberOfPeriods, 0);

      if (i === entries.length - 1) {
        rows += " 0.3fr"; // after school row
      } else {
        rows += " ";
      }
    }

    setGridRows(rows);
  }

  function renderCalendarHeaders(dayPlans: DayPlan[]): React.ReactNode[] {
    const renderedCalendarHeaders = [] as React.ReactNode[];
    const numberOfLessons = getNumberOfPeriods(dayPlanPattern);
    const iterations = numberOfLessons + dayPlans[0].breaks.length + dayPlans[0].events.length;

    let lessonNumber = 1;
    let breakNumber = 1;

    renderedCalendarHeaders.push(
      <div
        key={`calendarHeader1`}
        className="row-start-1 col-start-1 flex items-center justify-center border-r-2 border-b-2 border-darkGreen text-center text-lg font-bold">
        Week {weekNumber}
      </div>
    );

    for (let i = 0; i < iterations; i++) {
      if (dayPlanPattern.pattern[i].type === "LessonPlan") {
        renderedCalendarHeaders.push(
          <LessonHeader
            key={`lessonHeader${lessonNumber}`}
            lessonNumber={lessonNumber}
            rowIndex={i + 2}
            startTime={getCalendarTime(dayPlanPattern.pattern[i].startTime)}
            endTime={getCalendarTime(dayPlanPattern.pattern[i].endTime)}
          />
        );

        lessonNumber++;
      } else if (dayPlanPattern.pattern[i].type === "Break") {
        renderedCalendarHeaders.push(
          <BreakHeader
            key={`breakHeader${breakNumber}`}
            breakNumber={breakNumber}
            rowIndex={i + 2}
            startTime={getCalendarTime(dayPlanPattern.pattern[i].startTime)}
            endTime={getCalendarTime(dayPlanPattern.pattern[i].endTime)}
          />
        );
        breakNumber++;
      }
    }

    renderedCalendarHeaders.push(
      <div
        key={`afterSchoolHeader`}
        className="row-start-[10] col-start-1 flex items-center justify-center font-semibold border-r-2 border-b-2 border-darkGreen text-center text-lg">
        After School
      </div>
    );

    return renderedCalendarHeaders;
  }

  function renderDayPlans(dayPlans: DayPlan[]): React.ReactNode[][] {
    const renderedDayPlansList = dayPlans.map((dayPlan, colIdx) => {
      const renderedDayPlans = [] as React.ReactNode[];

      let lessonPlanPos = 0;
      let breakPos = 0;
      let schoolEventPos = 0;

      renderedDayPlans.push(
        <div key={uuidv1()} className="row-start-1 border-r-2 border-b-2 border-darkGreen">
          <p className="text-center">{getDayName(dayPlan.startTime)}</p>
          <p className="text-center">{getCalendarDate(dayPlan.startTime)}</p>
        </div>
      );

      for (let i = 0; i < numRows - 2; i++) {
        if (lessonPlanPos < dayPlan.lessonPlans.length && dayPlan.lessonPlans[lessonPlanPos].periodNumber === i + 1) {
          const rowIdx = lessonPlanPos;
          renderedDayPlans.push(
            <LessonPlanCalendarEntry
              key={uuidv1()}
              lessonPlan={dayPlan.lessonPlans[lessonPlanPos]}
              columnIndex={colIdx}
              selectLessonEntry={() => handleLessonPlanEntryClicked({ row: rowIdx, column: colIdx })}
              isSelected={selectedLessonEntryIndex.row === rowIdx && selectedLessonEntryIndex.column === colIdx}
              viewLessonPlan={() => handleViewLessonPlan({ row: rowIdx, column: colIdx })}
            />
          );

          if (dayPlan.lessonPlans[lessonPlanPos].numberOfPeriods !== 1) {
            i += dayPlan.lessonPlans[lessonPlanPos].numberOfPeriods - 1;
          }

          lessonPlanPos++;
        } else if (dayPlan.events.length && dayPlan.events[schoolEventPos].periodNumber === i + 1) {
          renderedDayPlans.push(
            <EventCalendarEntry key={uuidv1()} schoolEvent={dayPlan.events[schoolEventPos]} columnIndex={colIdx} rowIndex={i + 2} />
          );

          if (dayPlan.events[schoolEventPos].numberOfPeriods !== 1) {
            i += dayPlan.events[schoolEventPos].numberOfPeriods - 1;
          }

          schoolEventPos++;
        } else if (breakPos < dayPlan.breaks.length) {
          renderedDayPlans.push(<BreakCalendarEntry key={uuidv1()} lessonBreak={dayPlan.breaks[breakPos]} columnIndex={colIdx} rowIndex={i + 2} />);
          breakPos++;
        }
      }

      renderedDayPlans.push(<AfterSchoolCalendarEntry key={uuidv1()} rowIndex={numRows} afterSchoolActivity="Crossing duty" />);

      return renderedDayPlans;
    });

    return renderedDayPlansList;
  }

  function handleLessonPlanEntryClicked(cell: GridCellLocation) {
    cell.column === selectedLessonEntryIndex.column && cell.row === selectedLessonEntryIndex.row
      ? setSelectedLessonEntryIndex({ row: -1, column: -1 })
      : setSelectedLessonEntryIndex(cell);
  }

  function handleViewLessonPlan(cell: GridCellLocation) {
    const lessonPlan = dayPlans[cell.column].lessonPlans[cell.row];
    navigate(`/teacher/lesson-plans/${lessonPlan.id}`);
  }

  return (
    <div className="flex flex-col h-full">
      <h1 className="text-4xl text-center p-3">Hi {teacher!.firstName}!</h1>
      <div
        style={{ gridAutoRows: gridRows }}
        className={`grid grid-cols-[minmax(7rem,_0.5fr),_repeat(5,_1fr)] border-l-2 border-t-2 border-darkGreen m-3 flex-grow`}>
        {renderCalendarHeaders(dayPlans)}
        {renderDayPlans(dayPlans)}
      </div>
    </div>
  );
}

export default WeekPlanner;

type LessonPlannerProps = {
  numLessons: number;
  numBreaks: number;
  lessonLength: number; // in mintues
  weekNumber: number;
  dayPlans: DayPlan[];
  dayPlanPattern: DayPlanPattern;
};

type DayPlanData = {
  lessonPlanPos: number;
  breakPos: number;
  schoolEventPos: number;
  nextLesson: LessonPlan | null;
  nextBreak: Break | null;
  nextSchoolEvent: SchoolEvent | null;
};

type GridCellLocation = {
  row: number;
  column: number;
};

type LessonPlanData = {
  numLessons: number;
  numBreaks: number;
  weekNumber: number;
  dayPlans: DayPlan[];
  dayPlanPattern: DayPlanPattern;
};

type WeekPlannerData = {
  weekPlanPattern: DayPlanPattern;
  dayPlans: DayPlan[];
  weekNumber: number;
  termNumber: number;
  year: number;
};

export async function weekPlannerLoader(): Promise<WeekPlannerData> {
  const abortController = new AbortController();
  const teacher = JSON.parse(localStorage.getItem("teacher")!);
  const token = JSON.parse(localStorage.getItem("token")!);

  const weekNumber = 1;
  const termNumber = 1;
  const year = 2023;

  const response = await fetch(`${baseUrl}/${teacher!.id}/week-planner/?week=${weekNumber}&term=${termNumber}&year=${year}`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
    signal: abortController.signal,
  });

  return response.json() as Promise<WeekPlannerData>;
}
