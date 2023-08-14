import { v1 as uuidv1 } from "uuid";

type BreakCalendarEntryProps = {
  lessonBreak: Break;
  columnIndex: number;
  rowIndex: number;
};

export default function BreakCalendarEntry({ lessonBreak, columnIndex }: BreakCalendarEntryProps) {
  const style = {
    gridRowStart: lessonBreak.periodNumber + 1,
  }
  return (
    <div style={style} className={`col-start-${columnIndex + 2} col-span-1 border-r border-b border-darkGreen h-12`}>
      <p>{lessonBreak.duty ? lessonBreak.duty : " "}</p>
    </div>
  );
}
