type LessonHeaderProps = {
  lessonNumber: number;
  rowIndex: number;
  startTime: string;
  endTime: string;
  classList?: string;
};

function LessonHeader({ lessonNumber, rowIndex, startTime, endTime, classList }: LessonHeaderProps) {
  return (
    <div
      key={`lessonHeader${lessonNumber}`}
      className={`row-start-${rowIndex} col-start-1 items-center flex flex-col justify-center border-r-2 border-b-2 border-darkGreen text-center text-lg font-semibold ${
        classList ? classList : ""
      }`}>
      <h3>Lesson {lessonNumber}</h3>
      <p>
        <span className="text-sm">{startTime}</span>
      </p>
    </div>
  );
}

export default LessonHeader;
