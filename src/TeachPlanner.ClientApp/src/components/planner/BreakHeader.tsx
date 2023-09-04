type BreakHeaderProps = {
  breakNumber: number;
  rowIndex: number;
  startTime: string;
  endTime: string;
}

function BreakHeader({ breakNumber, rowIndex, startTime, endTime }: BreakHeaderProps) {
  return (
    <div key={`breakHeader${breakNumber}`} className={`row-start-[${rowIndex}] col-start-1 flex items-center justify-center border-r-2 border-b-2 border-darkGreen text-center text-lg font-semibold`}>
      <p>{startTime} - {endTime}</p>
    </div>
  )
}

export default BreakHeader
