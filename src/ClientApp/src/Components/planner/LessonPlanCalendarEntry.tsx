import { useState } from "react";
import { v1 as uuidv1 } from "uuid";

type LessonPlanCalendarEntryProps = {
  lessonPlan: LessonPlan;
  columnIndex: number;
  selectLessonEntry: (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => void;
  isSelected: boolean;
};

export default function LessonPlanCalendarEntry({ lessonPlan, columnIndex, selectLessonEntry, isSelected }: LessonPlanCalendarEntryProps) {
  const style = {
    gridRowStart: lessonPlan.periodNumber + 1,
    gridRowEnd: lessonPlan.periodNumber + lessonPlan.numberOfPeriods + 1,
    rowSpan: lessonPlan.numberOfPeriods,
  };

  const selectedClassNames = "bg-primaryFocus";

  function openLessonEditor(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {}

  return (
    <div
      style={style}
      className={`col-start-${columnIndex + 2} col-end-${columnIndex + 2} border-r-2 border-b-2 border-darkGreen flex flex-col p-2 select-none ${
        isSelected ? selectedClassNames : " "
      }`}
      onClick={selectLessonEntry}>
      <div className="flex mb-2 justify-between items-center">
        <p className="text-lg font-semibold">{lessonPlan.subject.name}</p>
        {isSelected && (
          <button className="text-sm font-semibold text-center text-primary bg-darkGreen px-2 py-1 rounded-md" onClick={openLessonEditor}>
            Edit
          </button>
        )}
      </div>
      {lessonPlan.planningNotes.map((note, i) => (
        <p key={`lessonPlannerNote-${i}`} className="pb-4">
          {note}
        </p>
      ))}
      {lessonPlan.resources && (
        <>
          <h5 className="text-md font-semibold">Resources</h5>
          <ul>
            {lessonPlan.resources.map((resource) => (
              <li key={uuidv1()}>
                <a href={resource.url} target="_blank" className="text-peach underline">
                  {resource.name}
                </a>
              </li>
            ))}
          </ul>
        </>
      )}
    </div>
  );
}
