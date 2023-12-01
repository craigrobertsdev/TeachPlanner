type AfterSchoolCalendarEntryProps = {
  rowIndex: number;
  columnIndex: number;
  afterSchoolActivity?: string;
};

export default function AfterSchoolCalendarEntry({ rowIndex, columnIndex, afterSchoolActivity }: AfterSchoolCalendarEntryProps) {
  const style = {
    gridRowStart: rowIndex,
  };
  return (
    <div
      style={style}
      className={`border-r-2 border-b-2 flex items-center justify-center ${columnIndex % 2 === 1 && "bg-lightSage"} border-darkGreen`}>
      {afterSchoolActivity ? afterSchoolActivity : " "}
    </div>
  );
}
