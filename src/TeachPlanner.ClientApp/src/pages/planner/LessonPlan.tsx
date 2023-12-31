import { useNavigate, useSearchParams } from "react-router-dom";
import { getCalendarDate, getCalendarTime } from "../../utils/dateUtils";
import { useEffect, useRef, useState } from "react";
import Button from "../../components/common/Button";
import CancelButton from "../../components/common/CancelButton";
import AddContentDescriptionDialogContent from "../../components/planner/AddContentDescriptionDialogContent";
import AddResourcesDialogContent from "../../components/planner/AddResourcesDialogContent";
import useAuth from "../../contexts/AuthContext";
import PlannerService from "../../services/PlannerService";
import { createCssClassString } from "../../utils/helpers";
import { usePlannerContext } from "../../contexts/PlannerContext";
import { getLessonEnd, getLessonStart } from "../../utils/plannerHelpers";
import RichTextEditor from "../../components/common/RichTextEditor";
import { LexicalEditor, EditorState } from "lexical";

function LessonPlan() {
  const [lessonPlan, setLessonPlan] = useState<LessonPlan>({} as LessonPlan);
  const [subjects, setSubjects] = useState([] as Subject[]);
  const [planningNotes, setPlanningNotes] = useState<EditorState>({} as EditorState);
  const [contentDescriptions, setContentDescriptions] = useState<ContentDescription[]>(lessonPlan.contentDescriptions ?? []);
  const [resources, setResources] = useState<Resource[]>(lessonPlan.resources ?? []);
  const [assessments, setAssessments] = useState<Assessment[]>(lessonPlan.assessments ?? []);
  const [originalPlanningNotes] = useState<string>(lessonPlan.planningNotes ?? "");
  const [unsavedChanges, setUnsavedChanges] = useState<boolean>(false);
  const [currentSubject, setCurrentSubject] = useState<string>(lessonPlan.subject?.name ?? "");
  const [numberOfPeriods, setNumberOfPeriods] = useState(1);
  const [lessonOverlapExists, setLessonOverlapExists] = useState(false);
  const unsavedChangesDialog = useRef<HTMLDialogElement>(null);
  const addContentDescriptionsDialog = useRef<HTMLDialogElement>(null);
  const addResourcesDialog = useRef<HTMLDialogElement>(null);
  const lessonOverlapDialog = useRef<HTMLDialogElement>(null);
  const navigate = useNavigate();
  const { teacher, token } = useAuth();
  const [searchParams] = useSearchParams();
  const { currentWeekPlanner, dayPlanTemplate } = usePlannerContext();

  useEffect(() => {
    const getLessonPlanData = async () => {
      const isNewLessonPlan = window.location.pathname.includes("create");

      if (isNewLessonPlan) {
        const response = await PlannerService.getBlankLessonPlanData(teacher!, token!, searchParams.get("calendarYear")!);
        const periodNumber = +searchParams.get("periodNumber")!;
        const lessonPlan = {
          id: "",
          periodNumber: periodNumber,
          subject: {} as PlannerSubject,
          planningNotes: "",
          contentDescriptions: [] as ContentDescription[],
          assessments: [] as Assessment[],
          resources: [] as Resource[],
          lessonComments: [],
          numberOfPeriods: 1,
          startTime: getCalendarTime(getLessonStart(dayPlanTemplate, periodNumber)),
          endTime: getCalendarTime(getLessonEnd(dayPlanTemplate, periodNumber)),
        } as LessonPlan;
        setLessonPlan(lessonPlan);
        setSubjects(response.curriculumSubjects);
        setCurrentSubject(response.curriculumSubjects[0].name);
        setNumberOfPeriods(lessonPlan.numberOfPeriods);
      } else {
        // TODO: fetch the lesson plan data from the server
      }
    };

    getLessonPlanData();
  }, []);

  function handleSubjectChange(event: React.ChangeEvent<HTMLSelectElement>) {
    if (event.target.value !== currentSubject) {
      setCurrentSubject(event.target.value);
      const yearLevels = subjects
        .find((s) => s.name === event.target.value)
        ?.yearLevels.map((yl) => {
          return { name: event.target.value, contentDescriptions: yl.contentDescriptions } as PlannerSubjectYearLevel;
        });

      lessonPlan.subject = {
        name: event.target.value,
        yearLevels,
      } as PlannerSubject;
    }
  }

  async function handleSave(overlapOverride: boolean) {
    const overlapExists = overlapOverride ? false : checkLessonOverlap();
    if (overlapExists) {
      lessonOverlapDialog.current!.showModal();
      return;
    }

    const newLessonPlan = createLessonPlanFromState();
    if (!newLessonPlan) {
      return;
    }

    const isNewLessonPlan = window.location.pathname.includes("create");

    if (isNewLessonPlan) {
      const response = await PlannerService.createNewLessonPlan(newLessonPlan, teacher!, token!);

      if (response.status.isOk) {
        navigate("/teacher/week-planner");
      }
    }

    // TODO: save updates to the lesson plan
  }

  function getPlanningNotesText() {
    for (const [key, value] of planningNotes._nodeMap.entries()) {
      // unless node tree changes for rich text editor, this is the key of the node with the planning notes.
      if (key === "3") {
        return value.__text;
      }
    }
  }

  function createLessonPlanFromState() {
    return {
      planningNotes: getPlanningNotesText(),
      subject: { name: currentSubject },
      resources: resources,
      assessments: assessments,
      contentDescriptions: contentDescriptions,
      numberOfPeriods,
    } as LessonPlan;
  }

  function checkLessonOverlap() {
    const dayPlan = currentWeekPlanner.dayPlans[+searchParams.get("day")!];
    for (let i = lessonPlan.periodNumber; i < numberOfPeriods; i++) {
      if (dayPlan.lessonPlans.find((lp) => lp.periodNumber === i + 1)) {
        return true;
      } else if (dayPlan.events.find((e) => e.periodNumber === i + 1)) {
        return true;
      }
    }

    return false;
  }

  function handleNumberOfPeriodsChange(e: React.ChangeEvent<HTMLSelectElement>) {
    setNumberOfPeriods(+e.target.value);
  }

  function handleAddResource() {
    addResourcesDialog!.current!.showModal();
  }

  function handleAddContentDescriptions() {
    addContentDescriptionsDialog!.current!.showModal();
  }

  function handleRemoveContentDescription(curriculumCode: string) {
    setContentDescriptions(contentDescriptions.filter((cd) => cd.curriculumCode !== curriculumCode));
  }

  function getContentDescriptions(subjectName: string) {
    const subject = subjects.find((s) => s.name === subjectName);

    if (!subject) {
      return;
    }
    const contentDescriptions = subject.yearLevels.map((yl) => yl.contentDescriptions);

    return contentDescriptions;
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

  function onPlanningNotesChange(editorState: EditorState) {
    setPlanningNotes(editorState);
  }

  return (
    <section className="flex flex-col text-darkGreen max-w-7xl flex-grow relative">
      {lessonPlan.startTime && (
        <>
          <div className={`flex justify-between items-center mb-3 border border-darkGreen p-2 bg-${createCssClassString(currentSubject)}`}>
            <p className="text-center">
              <span className="mx-2">Subject: </span>
              <select
                className={`text-center rounded border border-darkGreen font-bold p-1 bg-${createCssClassString(currentSubject)}`}
                onChange={handleSubjectChange}
                value={currentSubject}>
                {subjects?.map((s) => (
                  <option key={s.id} value={s.id}>
                    {s.name}
                  </option>
                ))}
              </select>
            </p>
            <p className="text-center">Start Time: {getCalendarTime(lessonPlan.startTime)}</p>
            <p className="text-center">Period Number: {lessonPlan.periodNumber}</p>
            <div>
              <label>Number of Periods: </label>
              <select
                className="text-center bg-transparent border border-darkGreen rounded-md px-2 font-semibold"
                value={numberOfPeriods}
                onChange={handleNumberOfPeriodsChange}>
                <option value="1">1</option>
                <option value="2">2</option>
              </select>
            </div>
            <p className="text-center">Date: {getCalendarDate(lessonPlan.startTime)}</p>
          </div>
          <div className="flex flex-grow gap-3 items-between mb-4">
            {/* Planning notes form */}
            <form className="flex flex-col flex-grow items-center w-2/3">
              <label htmlFor="planning-notes" className="text-lg font-semibold">
                Planning Notes
              </label>
              <div className="w-full flex flex-col flex-grow border border-darkGreen">
                <RichTextEditor onChange={onPlanningNotesChange} />
              </div>
            </form>
            {/* Resources and content descriptions contianer*/}
            <div className="flex flex-col gap-3 flex-grow w-1/3 items-center">
              {/* Resources */}
              <dialog ref={addResourcesDialog} className="p-3 text-lg border border-darkGreen max-w-xl">
                <AddResourcesDialogContent
                  subjectId={lessonPlan.subject.id}
                  dialogRef={AddResourcesDialogContent}
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
                <div className="flex-grow w-full border border-darkGreen p-2 flex flex-col overflow-hidden">
                  {resources && <h3 className="text-lg text-center underline">Resources</h3>}
                  {resources && (
                    <ul className="overflow-auto">
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
                    <ul className="overflow-auto">
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
              <dialog ref={addContentDescriptionsDialog} className="p-3 text-lg border border-darkGreen h-80% max-w-xl">
                <AddContentDescriptionDialogContent
                  dialogRef={addContentDescriptionsDialog}
                  initialSelectedContentDescriptions={contentDescriptions}
                  setContentDescriptions={setContentDescriptions}
                  availableContentDescriptions={subjects
                    .find((s) => s.name === currentSubject)!
                    .yearLevels.map((yl) => yl.contentDescriptions)
                    .flat()}
                />
              </dialog>
              <div className="flex flex-col items-center w-full flex-grow h-1/2">
                <div className="flex w-full items-center justify-between px-2 py-1">
                  <h2 className="text-lg font-semibold">Content Descriptions</h2>
                  <Button variant="add" classList="self-end" onClick={() => handleAddContentDescriptions()}>
                    Add
                  </Button>
                </div>
                <div className="flex-grow w-full border overflow-hidden border-darkGreen flex flex-col">
                  <ul className="overflow-auto">
                    {contentDescriptions &&
                      contentDescriptions.map((cd) => (
                        <li key={cd.curriculumCode} className="p-2 mb-2 hover:bg-sageHover group relative">
                          <div className="invisible group-hover:visible">
                            <CancelButton onClick={() => handleRemoveContentDescription(cd.curriculumCode)} />
                          </div>
                          <span className="underline">{cd.curriculumCode}:</span> {cd.contentDescription}
                        </li>
                      ))}
                  </ul>
                </div>
              </div>
            </div>
          </div>
          <div className="flex justify-around mb-4 w-1/2 m-auto">
            <Button variant="submit" disabled={unsavedChanges} onClick={() => handleSave(false)}>
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
          <dialog ref={lessonOverlapDialog} className="w-96 p-3 text-lg text-center border border-darkGreen">
            <p className="p-2">
              The number of periods for this lesson creates an overlap with another lesson. Continuing will cause that lesson to be overwritten and
              that data will be lost.
            </p>
            <p className="p-2">Do you want to continue?</p>
            <div className="flex justify-around">
              <Button variant="submit" onClick={() => handleSave(true)}>
                Yes
              </Button>
              <Button variant="cancel" onClick={() => lessonOverlapDialog.current!.close()}>
                Cancel
              </Button>
            </div>
          </dialog>
        </>
      )}
    </section>
  );
}

export default LessonPlan;
