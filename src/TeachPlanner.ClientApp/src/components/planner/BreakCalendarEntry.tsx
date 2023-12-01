type BreakCalendarEntryProps = {
  lessonBreak: Break;
  columnIndex: number;
  rowIndex: number;
};

export default function BreakCalendarEntry({ lessonBreak, columnIndex }: BreakCalendarEntryProps) {
  const style = {
    gridRowStart: lessonBreak.periodNumber + 1,
  };
  return (
    <div
      style={style}
      className={`col-start-${columnIndex + 2} flex flex-col items-center justify-center col-span-1 ${
        columnIndex % 2 === 1 && "bg-lightSage"
      } border-r-2 border-b-2 border-darkGreen`}>
      <p>{lessonBreak.duty ? lessonBreak.duty : " "}</p>
    </div>
  );
}
