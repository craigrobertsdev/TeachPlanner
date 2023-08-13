import { v1 as uuidv1 } from "uuid";

type AfterSchoolCalendarEntryProps = {
  rowIndex: number;
  columnIndex: number;
  afterSchoolActivity?: string;
};

export default function AfterSchoolCalendarEntry({ columnIndex, rowIndex, afterSchoolActivity }: AfterSchoolCalendarEntryProps) {
  const style = {
    gridRowStart: rowIndex,
  }
  return (
    <div style={style} className="border-r border-b border-darkGreen">
      {afterSchoolActivity ? afterSchoolActivity : " "}
    </div>
  )
}
