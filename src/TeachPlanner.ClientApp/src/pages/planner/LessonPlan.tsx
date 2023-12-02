import { useLoaderData, useNavigate, useParams } from "react-router-dom";
import { getCalendarDate, getCalendarTime } from "../../utils/dateUtils";
import { useRef, useState } from "react";
import Button from "../../components/common/Button";
import CancelButton from "../../components/common/CancelButton";
import AddContentDescriptionDialogContent from "../../components/planner/AddContentDescriptionDialogContent";
import AddResourcesDialogContent from "../../components/planner/AddResourcesDialogContent";
import { baseUrl } from "../../utils/constants";

function LessonPlan() {
  const lessonPlan = useLoaderData() as LessonPlan;
  const [planningNotes, setPlanningNotes] = useState<string>(lessonPlan.planningNotes.join("\n\n"));
  const [contentDescriptions, setContentDescriptions] = useState<ContentDescription[]>(lessonPlan.contentDescriptions);
  const [resources, setResources] = useState<Resource[]>(lessonPlan.resources);
  const [assessments, setAssessments] = useState<Assessment[]>(lessonPlan.assessments);
  const [originalPlanningNotes] = useState<string>(lessonPlan.planningNotes.join("\n\n"));
  const [unsavedChanges, setUnsavedChanges] = useState<boolean>(false);
  const unsavedChangesDialog = useRef<HTMLDialogElement>(null);
  const addContentDescriptionsDialog = useRef<HTMLDialogElement>(null);
  const addResourcesDialog = useRef<HTMLDialogElement>(null);
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

  function handleAddResource() {
    addResourcesDialog!.current!.showModal();
  }

  function handleAddContentDescriptions() {
    addContentDescriptionsDialog!.current!.showModal();
  }

  function handleRemoveContentDescription(curriculumCode: string) {
    setContentDescriptions(contentDescriptions.filter((cd) => cd.curriculumCode !== curriculumCode));
  }

  function handleCancel() {
    if (unsavedChanges) {
      // Show a modal asking the user if they want to save their changes before navigating away
      unsavedChangesDialog!.current!.showModal();
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
      <div className={`flex justify-between items-center mb-3 border border-darkGreen p-2 bg-${lessonPlan.subject.name.toLowerCase()}`}>
        <p className="text-center">Subject: {lessonPlan.subject.name}</p>
        <p className="text-center">Start Time: {getCalendarTime(lessonPlan.startTime)}</p> {/*TODO: displays NAN:10am */}
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
          <dialog ref={addResourcesDialog} className="p-3 text-lg border border-darkGreen max-w-xl">
            <AddResourcesDialogContent
              subjectId={lessonPlan.subject.id}
              dialogRef={addContentDescriptionsDialog}
              initialSelectedResources={resources}
              setResources={setResources}
            />
          </dialog>
          <div className="flex flex-col items-center w-full flex-grow h-1/2 ">
            <div className="flex w-full items-center justify-between px-2 py-1">
              <h2 className="text-lg font-semibold">Resources and Assessments</h2>
              <Button variant="add" classList="self-end" onClick={handleAddResource}>
                Add
              </Button>
            </div>
            <div className="flex-grow w-full border border-darkGreen p-2 flex flex-col">
              {resources && <h3 className="text-lg text-center underline">Resources</h3>}
              {resources && (
                <ul>
                  {resources.map((resource) => (
                    <li key={resource.url} className="text-center">
                      <a target="_blank" href={resource.url} className="text-peach underline">
                        {resource.name}
                      </a>
                    </li>
                  ))}
                </ul>
              )}
              {assessments && <h3 className="text-lg text-center underline">Assessments</h3>}
              {assessments && (
                <ul>
                  {assessments.map((assessment) => (
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
          {/* Content descriptions */}
          <dialog ref={addContentDescriptionsDialog} className="p-3 text-lg border border-darkGreen max-w-xl">
            <AddContentDescriptionDialogContent
              dialogRef={addContentDescriptionsDialog}
              initialSelectedContentDescriptions={contentDescriptions}
              setContentDescriptions={setContentDescriptions}
              availableContentDescriptions={lessonPlan.subject.contentDescriptions}
            />
          </dialog>
          <div className="flex flex-col items-center w-full flex-grow h-1/2">
            <div className="flex w-full items-center justify-between px-2 py-1">
              <h2 className="text-lg font-semibold">Content Descriptions</h2>
              <Button variant="add" classList="self-end" onClick={() => handleAddContentDescriptions()}>
                Add
              </Button>
            </div>
            <div className="flex-grow w-full border border-darkGreen flex flex-col">
              <ul>
                {contentDescriptions &&
                  contentDescriptions.map((cd) => (
                    <li key={cd.curriculumCode} className="p-2 mb-2 hover:bg-sageHover group relative">
                      <div className="invisible group-hover:visible">
                        <CancelButton onClick={() => handleRemoveContentDescription(cd.curriculumCode)} />
                      </div>
                      <span className="underline">{cd.curriculumCode}:</span> {cd.description}
                    </li>
                  ))}
              </ul>
            </div>
          </div>
        </div>
      </div>
      <div className="flex justify-around mb-4 w-1/2 m-auto">
        <Button variant="submit" disabled={unsavedChanges} onClick={handleUpdateLesson}>
          Save
        </Button>
        <Button variant="cancel" onClick={handleCancel}>
          Go Back
        </Button>
      </div>
      <dialog ref={unsavedChangesDialog} className="p-3 text-lg border border-darkGreen">
        <p>You have unsaved changes. Continue without saving?</p>
        <div className="flex justify-around">
          <Button variant="submit" onClick={handleDiscardChanges}>
            Yes
          </Button>
          <Button variant="cancel" onClick={() => unsavedChangesDialog.current!.close()}>
            Cancel
          </Button>
        </div>
      </dialog>
    </section>
  );
}

export default LessonPlan;

// this function is called both when there is a new lesson plan to be created, and one that needs to be viewed or edited.
export async function lessonPlanLoader(): Promise<LessonPlan> {
  const isNewLessonPlan = window.location.pathname.includes("create");

  // if (isNewLessonPlan) {
  //   const { period, day, week } = useParams();
  //   const response = await fetch(`${baseUrl}/lesson-plans/data`);
  //   return response.json();
  // }

  // const params = useParams();
  // const response = await fetch(`${baseUrl}/lesson-plans/${params.lessonPlanId}`);
  // return response.json();

  return new Promise((resolve) => {
    const lessonPlanPromise = {
      numberOfPeriods: 2,
      planningNotes: ["Exploring rounding to the nearest 10 and 100"],
      resources: [
        {
          name: "Rounding to the nearest 10 and 100",
          url: "https://www.youtube.com/watch?v=qjxu6J6g",
        },
        {
          name: "Quadratics",
          url: "https://www.youtube.com/watch?v=9zXqjxJ6g",
        },
      ] as Resource[],
      assessments: [
        {
          name: "Algebra assessment",
          url: "https://www.youtube.com/watch?v=9zXu6J6g",
        },
      ] as Assessment[],
      contentDescriptions: [
        {
          curriculumCode: "AC9EFLA03",
          description: "Understand that texts can take many forms such as signs, books and digital texts'",
        },
        {
          curriculumCode: "AC9EFLA04",
          description: "Understand conventions of print and screen, including how books and simple digital texts are usually organised",
        },
      ] as ContentDescription[],
      subject: {
        id: "1",
        name: "Maths",
        contentDescriptions: [
          {
            curriculumCode: "AC9EFLA03",
            description: "Understand that texts can take many forms such as signs, books and digital texts'",
          },
          {
            curriculumCode: "AC9EFLA04",
            description: "Understand conventions of print and screen, including how books and simple digital texts are usually organised",
          },
          {
            curriculumCode: "AC9EFLA05",
            description: "Recognise that sentences are key units for expressing ideas",
          },
        ] as ContentDescription[],
      } as PlannerSubject,
      startTime: new Date(2023, 8, 7, 9, 10, 0),
      endTime: new Date(2023, 8, 7, 10, 50, 0),
      periodNumber: 1,
      id: "1",
    } as LessonPlan;

    resolve(lessonPlanPromise);
  });
}
