import { useLoaderData } from "react-router-dom";
import { getCalendarTime } from "../../utils/dateUtils";
import { usePlannerContext } from "../../contexts/PlannerContext";
import { useState } from "react";
import Button from "../../components/common/Button";

function EditLessonPlanPage() {
  const lessonPlan = useLoaderData() as LessonPlan;
  // const { lessonPlanTemplate } = usePlannerContext();
  const [planningNotes, setPlanningNotes] = useState<string>(lessonPlan.planningNotes.join("\n\n"));

  function handlePlanningNotesChange(event: React.ChangeEvent<HTMLTextAreaElement>) {
    setPlanningNotes(event.target.value);
  }

  function handleUpdateLesson() {}

  function handleDeleteLesson() {}

  return (
    <div className="text-darkGreen">
      <h1 className="text-xl font-semibold ">Edit Lesson Plan</h1>
      <form className="flex flex-col p-2 justify-center items-center">
        <p>Subject: {lessonPlan.subject.name}</p>
        <p>Start Time: {getCalendarTime(lessonPlan.startTime)}</p>
        <p>End Time: {getCalendarTime(lessonPlan.endTime)}</p>
        <p>Period Number: {lessonPlan.periodNumber}</p>
        <textarea value={planningNotes} onChange={handlePlanningNotesChange}></textarea>
        <Button variant="submit" onClick={handleUpdateLesson}>
          Update Lesson Plan
        </Button>
        <Button variant="cancel" onClick={handleDeleteLesson}>
          Delete Lesson Plan
        </Button>
      </form>
    </div>
  );
}

export default EditLessonPlanPage;

export async function lessonPlanLoader(): Promise<LessonPlan> {
  // const response = await fetch(`${baseUrl}/lesson-plans/${params.lessonPlanId}`);
  // return response.json();

  return new Promise((resolve) => {
    const lessonPlanPromise = {
      numberOfPeriods: 2,
      planningNotes: ["Exploring rounding to the nearest 10 and 100"],
      resources: [
        {
          name: "Rounding to the nearest 10 and 100",
          url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
        },
        {
          name: "Quadratics",
          url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
        },
      ] as Resource[],
      subject: {
        name: "Maths",
      } as PlannerSubject,
      startTime: new Date(2023, 8, 7, 9, 10, 0),
      endTime: new Date(2023, 8, 7, 10, 50, 0),
      periodNumber: 1,
      id: "1",
    } as LessonPlan;

    resolve(lessonPlanPromise);
  });
}
