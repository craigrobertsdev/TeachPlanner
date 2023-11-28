type BreakHeaderProps = {
  breakNumber: number;
  breakName: string;
  rowIndex: number;
  startTime: string;
  endTime: string;
  classList?: string;
};

function BreakHeader({ breakNumber, breakName, rowIndex, startTime, endTime, classList }: BreakHeaderProps) {
  return (
    <div
      key={`breakHeader${breakNumber}`}
      className={`row-start-${rowIndex} col-start-1 flex items-center justify-center border-r-2 border-b-2 border-darkGreen text-center text-lg font-semibold ${
        classList ? classList : ""
      }`}>
      <div>
        <p>{breakName}</p>
        <span className="text-sm">{startTime}</span>
      </div>
    </div>
  );
}

export default BreakHeader;
