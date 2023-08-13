import { v1 as uuidv1 } from "uuid";

type LessonPlanCalendarEntryProps = {
  lessonPlan: LessonPlan;
  columnIndex: number;
};

export default function LessonPlanCalendarEntry({ lessonPlan, columnIndex }: LessonPlanCalendarEntryProps) {
  const style = {
    gridRowStart: lessonPlan.periodNumber + 1,
    gridRowEnd: lessonPlan.periodNumber + lessonPlan.numberOfPeriods + 1,
    rowSpan: lessonPlan.numberOfPeriods,
  }

  return (
    <div
      style={style}
      className={`col-start-${columnIndex + 2} col-end-${columnIndex + 2} border-r border-b border-darkGreen`}>
      <p>{lessonPlan.subject.name}</p>
      <p>{lessonPlan.lessonComments}</p>
    </div>
  );
}
