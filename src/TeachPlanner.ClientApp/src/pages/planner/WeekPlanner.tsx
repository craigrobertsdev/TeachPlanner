import React, { useEffect, useState } from "react";
import useAuth from "../../contexts/AuthContext";
import { getCalendarDate, getCalendarTime, getDayName } from "../../utils/dateUtils";
import { v1 as uuidv1 } from "uuid";
import LessonPlanCalendarEntry from "../../components/planner/LessonPlanCalendarEntry";
import BreakCalendarEntry from "../../components/planner/BreakCalendarEntry";
import EventCalendarEntry from "../../components/planner/EventCalendarEntry";
import AfterSchoolCalendarEntry from "../../components/planner/AfterSchoolCalendarEntry";
import LessonHeader from "../../components/planner/LessonHeader";
import BreakHeader from "../../components/planner/BreakHeader";
import { resolvePath, useLoaderData, useNavigate } from "react-router-dom";
import { baseUrl } from "../../utils/constants";
import { usePlannerContext } from "../../contexts/PlannerContext";
import { dayPlanHelper } from "../../utils/helpers"

function WeekPlanner() {
	const { teacher } = useAuth();
	const { availablePlannerYears } = usePlannerContext();
	const [currentYear, setCurrentYear] = useState<number>(); 
	const [gridRows, setGridRows] = useState<string>("");
	const [selectedLessonEntryIndex, setSelectedLessonEntryIndex] = useState<GridCellLocation>({ row: -1, column: -1 });
	const loaderData = useLoaderData() as LessonPlanData;
	const navigate = useNavigate();

	const { weekNumber, dayPlans, dayPlanPattern, weekStart } = loaderData;
	const numRows = dayPlanPattern.pattern.length + 2; // +2 for header row and after school

	useEffect(() => {
		async function getWeekPlanner() {
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

			// if (!response.ok) {
			//   return new Promise((resolve, _) => resolve({ dayPlanPattern: { pattern: [] }, dayPlans: [], weekStart, weekNumber, termNumber, year } as WeekPlannerData));
			// }

			return response.json() as Promise<WeekPlannerData>;
		}
	})

	useEffect(() => {
		setGridDimensions();
		setCurrentYear(availablePlannerYears[0]);
	}, []);

	// function sortCalendarEntries(dayPlans: DayPlan[]): CalendarEntry[][] {
	//   const entries: CalendarEntry[][] = [];

	//   for (const dayPlan of dayPlans) {
	//     let unsortedEntries: CalendarEntry[] = dayPlan.lessonPlans;
	//     unsortedEntries = unsortedEntries.concat(dayPlan.breaks).concat(dayPlan.events);

	//     const sortedEntries = unsortedEntries.sort((a, b) => {
	//       if (a.periodNumber < b.periodNumber) {
	//         return -1;
	//       }

	//       if (a.periodNumber > b.periodNumber) {
	//         return 1;
	//       }

	//       return 0;
	//     });

	//     entries.push(sortedEntries);
	//   }

	//   return entries;
	// }

	function setGridDimensions(): void {
		let rows = "0.5fr "; // week and day header row
		var pattern = dayPlanPattern.pattern;

		for (let i = 0; i < pattern.length; i++) {
			const entry = pattern[i];
			if (isLessonHeader(entry)) {
				rows += "minmax(100px, max-content)";
			} else {
				rows += "minmax(auto, 0.3fr)";
			}

			if (i === pattern.length - 1) {
				rows += " 0.3fr"; // after school row
			} else {
				rows += " ";
			}

			setGridRows(rows);
		}
	}

	function renderCalendarHeaders(): React.ReactNode[] {
		const renderedCalendarHeaders = [] as React.ReactNode[];
		const numberOfLessons = getNumberOfPeriods(dayPlanPattern);

		let lessonNumber = 1;
		let breakNumber = 1;

		renderedCalendarHeaders.push(
			<div
				key={`calendarHeader1`}
				className="row-start-1 col-start-1 flex items-center justify-center border-l-2 border-r-2 border-b-2 border-darkGreen text-center text-lg font-bold">
				Week {weekNumber}
			</div>
		);

		for (let i = 0; i < numberOfLessons; i++) {
			if (dayPlanPattern.pattern[i].periodType === "Lesson") {
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
			} else if (dayPlanPattern.pattern[i].periodType === "Break") {
				const breakPeriod = dayPlanPattern.pattern[i];
				renderedCalendarHeaders.push(
					<BreakHeader
						key={`breakHeader${breakNumber}`}
						breakNumber={breakNumber}
						rowIndex={i + 2}
						breakName={i === 0 ? "Recess" : "Lunch"}
						startTime={getCalendarTime(breakPeriod.startTime)}
						endTime={getCalendarTime(breakPeriod.endTime)}
					/>
				);
				breakNumber++;
			}
		}

		renderedCalendarHeaders.push(
			<div
				key={`afterSchoolHeader`}
				className="row-start-10 col-start-1 flex items-center justify-center font-semibold border-l-2 border-r-2 border-b-2 border-darkGreen text-center text-lg">
				After School
			</div>
		);

		return renderedCalendarHeaders;
	}

	function getNumberOfPeriods(dayPlanPattern: DayPlanPattern) {
		return dayPlanPattern.pattern.length;
	}

	function renderDayPlans(dayPlans: DayPlan[]): React.ReactNode[][] {
		// this function now should go through each dayPlan in dayPlan
		// if there is a day plan whose period == i + 1 then renderDayPlanWithEntry(), otherwise renderDayPlanPlaceholder()
		return dayPlans.map((dayPlan, colIdx) => {
			const renderedDayPlans = [] as React.ReactNode[];

			let lessonPlanPos = 0;
			let breakPos = 0;
			let schoolEventPos = 0;

			renderedDayPlans.push(
				<div key={uuidv1()} className={`row-start-1 border-r-2 border-b-2 border-darkGreen`}>
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
						<EventCalendarEntry 
							key={uuidv1()} 
							schoolEvent={dayPlan.events[schoolEventPos]} 
							columnIndex={colIdx} 
							rowIndex={i + 2} />
					);

					if (dayPlan.events[schoolEventPos].numberOfPeriods !== 1) {
						i += dayPlan.events[schoolEventPos].numberOfPeriods - 1;
					}

					schoolEventPos++;
				} else if (breakPos < dayPlan.breaks.length) {
					renderedDayPlans.push(
						<BreakCalendarEntry 
							key={uuidv1()} 
							lessonBreak={dayPlan.breaks[breakPos]} 
							columnIndex={colIdx} 
							rowIndex={i + 2} />);
					breakPos++;
				} else {
					renderedDayPlans.push(renderDayPlanPlaceholder(colIdx, i));
				}
			}

			renderedDayPlans.push(
				<AfterSchoolCalendarEntry 
					key={uuidv1()} 
					rowIndex={numRows} 
					columnIndex={colIdx} 
					afterSchoolActivity="Crossing duty" />);

			return renderedDayPlans;
		});
	}

	function renderDayPlanPlaceholder(colIdx: number, periodNumber: number) {
		const dayPlanPlaceholders = [] as React.ReactNode[][];
		const pattern = dayPlanPattern.pattern;

		if (pattern[periodNumber].periodType === "Lesson") {
		return <LessonPlanCalendarEntry
							key={uuidv1()}
							lessonPlan={{ periodNumber: periodNumber + 1, numberOfPeriods: 1, subject: { name: "Add a lesson plan" }, planningNotes: "" } as LessonPlan}
							columnIndex={colIdx}
							selectLessonEntry={() => handleLessonPlanEntryClicked({ row: periodNumber, column: colIdx })}
							isSelected={selectedLessonEntryIndex.row === periodNumber && selectedLessonEntryIndex.column === colIdx}
							viewLessonPlan={() => handleViewLessonPlan({ row: periodNumber, column: colIdx })}
						/>
		} else {
			return <BreakCalendarEntry 
				key={uuidv1()} 
				lessonBreak={{ name: pattern[periodNumber].name, periodNumber: periodNumber + 1 } as Break} 
				columnIndex={colIdx} 
				rowIndex={periodNumber} />
		}
	}

	function handleLessonPlanEntryClicked(cell: GridCellLocation) {
		cell.column === selectedLessonEntryIndex.column && cell.row === selectedLessonEntryIndex.row
			? setSelectedLessonEntryIndex({ row: -1, column: -1 })
			: setSelectedLessonEntryIndex(cell);
	}

	function handleViewLessonPlan(cell: GridCellLocation) {
		let lessonPlan = dayPlans[cell.column]?.lessonPlans[cell.row]as LessonPlan;
		if (!lessonPlan) {
			const periodNumber = calculatePeriodNumber(cell.row);
			// const numberofPeriods = calculateNumberOfPeriods(cell.row);
			// navigate(`/teacher/lesson-plans/create?calendarYear=${currentYear ?? availablePlannerYears[0]}&numberOfLessons=${lessonPlan.}`);
		}
		navigate(`/teacher/lesson-plans/${lessonPlan.id}`);
	}

	function calculatePeriodNumber(row: number) {
		let periodNumber = 0;
		for (let i = 0; i < row; i++) {
			if (dayPlanPattern.pattern[i].periodType === "Lesson") {
				periodNumber++;
			}
		}
		return periodNumber + 1;
	}

	function handleCalendarYearChange(event: React.ChangeEvent<HTMLSelectElement>) {
		setCurrentYear(parseInt(event.target.value));
	}

	function isLessonHeader(entry: CalendarHeader) {
		return entry.periodType === "Lesson";
	}

	return (
		<div className="flex w-full h-full">
			<div>
				<span>Year: </span>
				<select onChange={handleCalendarYearChange} className="border border-darkGreen rounded-sm">
					{availablePlannerYears.map((year) => (
						<option key={year} value={year}>
							{year}
						</option>
					))}
				</select>
			</div>
			<div
				style={{ gridAutoRows: gridRows }}
				className={`grid grid-cols-[minmax(7rem,_0.3fr),_repeat(5,_1fr)] border-t-2 border-darkGreen m-3 flex-grow`}>
				{renderCalendarHeaders()}
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
	weekStart: Date;
	dayPlans: DayPlan[];
	dayPlanPattern: DayPlanPattern;
};

type WeekPlannerData = {
	dayPlanPattern: DayPlanPattern;
	dayPlans: DayPlan[];
	weekStart: Date;
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

	// const response = await fetch(`${baseUrl}/${teacher!.id}/week-planner/?week=${weekNumber}&term=${termNumber}&year=${year}`, {
	// 	headers: {
	// 		Authorization: `Bearer ${token}`,
	// 	},
	// 	signal: abortController.signal,
	// });

	// if (!response.ok) {
	//   return new Promise((resolve, _) => resolve({ dayPlanPattern: { pattern: [] }, dayPlans: [], weekStart, weekNumber, termNumber, year } as WeekPlannerData));
	// }
	//
	return Promise.resolve({
		year: 2023,
		weekStart: new Date(2023, 12, 11),
		termNumber: 4,
		weekNumber: 9,
		dayPlanPattern: dayPlanHelper.dayPlanPattern,
		dayPlans: dayPlanHelper.dayPlans
	} as WeekPlannerData)
	// return response.json() as Promise<WeekPlannerData>;
}
