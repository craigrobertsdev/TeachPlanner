import { v1 as uuidv1 } from "uuid";

type AfterSchoolCalendarEntryProps = {
  rowIndex: number;
  afterSchoolActivity?: string;
};

export default function AfterSchoolCalendarEntry({ rowIndex, afterSchoolActivity }: AfterSchoolCalendarEntryProps) {
  const style = {
    gridRowStart: rowIndex,
  }
  return (
    <div style={style} className="border-r-2 border-b-2 flex items-center justify-center border-darkGreen">
      {afterSchoolActivity ? afterSchoolActivity : " "}
    </div>
  )
}
