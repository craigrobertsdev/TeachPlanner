import { useLoaderData, useNavigate } from "react-router-dom";
import { getCalendarDate, getCalendarTime } from "../../utils/dateUtils";
import { usePlannerContext } from "../../contexts/PlannerContext";
import { useEffect, useRef, useState } from "react";
import Button from "../../components/common/Button";

function EditLessonPlanPage() {
  const lessonPlan = useLoaderData() as LessonPlan;
  const { dayPlanPattern, subjects } = usePlannerContext();
  const [planningNotes, setPlanningNotes] = useState<string>(lessonPlan.planningNotes.join("\n\n"));
  const [originalPlanningNotes] = useState<string>(lessonPlan.planningNotes.join("\n\n"));
  const [unsavedChanges, setUnsavedChanges] = useState<boolean>(false);
  const dialogRef = useRef<HTMLDialogElement>(null);
  const navigate = useNavigate();

  function handlePlanningNotesChange(event: React.ChangeEvent<HTMLTextAreaElement>) {
    setPlanningNotes(event.target.value);
    if (event.target.value !== originalPlanningNotes) {
      setUnsavedChanges(true);
    } else {
      setUnsavedChanges(false);
    }
  }

  function handleUpdateLesson() {}

  function handleAddResource() {}

  function handleCancel() {
    if (unsavedChanges) {
      // Show a modal asking the user if they want to save their changes before navigating away
      dialogRef!.current!.showModal();
    } else {
      navigate(-1);
    }
  }

  function handleDiscardChanges() {
    navigate(-1);
  }

  return (
    <section className="flex flex-col text-darkGreen max-w-7xl flex-grow border border-darkGreen relative">
      This needs to allow the user to edit just the planning notes, resources and assessments for the lesson. Editing of the lesson time and subject
      should be done in the day plan pattern in settings. On load, fetch the content descriptors applicable to the subject and year level(s) of the
      lesson. The user should be able to select the content descriptors that are applicable to the subject and add them as aims for the lesson.
      Resources should be fetched for the subject and year level(s) of the lesson. Same for assessments.
      <h1 className="text-3xl font-semibold text-center">Edit Lesson Plan</h1>
      <div className={`flex justify-between items-center mb-3 border border-darkGreen p-2 bg-${lessonPlan.subject.name.toLowerCase()}`}>
        <p className="text-center">Subject: {lessonPlan.subject.name}</p>
        <p className="text-center">Start Time: {getCalendarTime(lessonPlan.startTime)}</p>
        <p className="text-center">End Time: {getCalendarTime(lessonPlan.endTime)}</p>
        <p className="text-center">Period Number: {lessonPlan.periodNumber}</p>
        <p className="text-center">Date: {getCalendarDate(lessonPlan.startTime)}</p>
      </div>
      <div className="flex flex-grow gap-3 items-between mb-4">
        {/* Planning notes form */}
        <form className="flex flex-col flex-grow items-center w-2/3">
          <label htmlFor="planning-notes" className="text-lg font-semibold">
            Planning Notes
          </label>
          <textarea className="p-2 w-full flex-grow resize-none" name="planning-notes" value={planningNotes} onChange={handlePlanningNotesChange} />
        </form>
        {/* Resources and content descriptions contianer*/}
        <div className="flex flex-col gap-3 flex-grow w-1/3 items-center">
          {/* Resources */}
          <div className="flex flex-col items-center w-full flex-grow h-1/2 ">
            <h2 className="text-lg font-semibold">Resources and Assessments</h2>
            <div className="flex-grow w-full border border-darkGreen p-2 flex flex-col">
              <Button variant="add" classList="self-end" onClick={handleAddResource}>
                Add Resource
              </Button>
              {lessonPlan.resources && <h3 className="text-lg text-center underline">Resources</h3>}
              {lessonPlan.resources && (
                <ul>
                  {lessonPlan.resources.map((resource) => (
                    <li key={resource.url} className="text-center">
                      <a target="_blank" href={resource.url} className="text-peach underline">
                        {resource.name}
                      </a>
                    </li>
                  ))}
                </ul>
              )}
              {lessonPlan.assessments && <h3 className="text-lg text-center underline">Assessments</h3>}
              {lessonPlan.assessments && (
                <ul>
                  {lessonPlan.assessments.map((assessment) => (
                    <li key={assessment.url} className="text-center">
                      <a target="_blank" href={assessment.url} className="text-peach underline">
                        {assessment.name}
                      </a>
                    </li>
                  ))}
                </ul>
              )}
            </div>
          </div>
        </div>
      </div>
      <div className="flex justify-around mb-4 w-1/2 m-auto">
        <Button variant="submit" disabled={unsavedChanges} onClick={handleUpdateLesson}>
          Update Lesson Plan
        </Button>
        <Button variant="cancel" onClick={handleCancel}>
          Go Back
        </Button>
      </div>
      <dialog ref={dialogRef} className="p-3 text-lg border border-darkGreen">
        <p>You have unsaved changes. Continue without saving?</p>
        <div className="flex justify-around">
          <Button variant="submit" onClick={handleDiscardChanges}>
            Yes
          </Button>
          <Button variant="cancel" onClick={() => dialogRef.current!.close()}>
            Cancel
          </Button>
        </div>
      </dialog>
    </section>
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
      assessments: [
        {
          name: "Algebra assessment",
          url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
        },
      ] as Assessment[],
      targetContentDescriptions: [
        {
          id: "1",
          description:
            "Content Description 1tnhaoeutnsahoeutnsaohe utnshaoeutnshaoetnsuhaoetns uhaotnsuhaoehuaotnsehu aotnshunsaot oheutnsaoeehu nstaohu tnsaoehunst aoneuhsnt aotnsuhnsth sh aonestuhaoensthu.g2,romeausk nt',h.k gcrsak,.prkm,agrs,",
        },
      ] as ContentDescription[],
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
