type LessonHeaderProps = {
  lessonNumber: number;
  rowIndex: number;
  startTime: string;
  endTime: string;
}

function LessonHeader({ lessonNumber, rowIndex, startTime, endTime }: LessonHeaderProps) {
  return (
    <div key={`lessonHeader${lessonNumber}`} className={`row-start-[${rowIndex}] col-start-1 items-center flex flex-col justify-center border-r-2 border-b-2 border-darkGreen text-center text-lg font-semibold`}>
      <h3>Lesson {lessonNumber}</h3>
      <p>{startTime} - {endTime}</p>
    </div>
  )
}

export default LessonHeader
