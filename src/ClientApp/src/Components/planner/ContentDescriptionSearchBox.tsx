import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "../common/Button";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import Dropdown from "../common/Dropdown";
import { baseUrl } from "../../utils/constants";
import useAuth from "../../contexts/AuthContext";
import { v1 as uuidv1 } from "uuid";

type ContentDescriptionSearchBoxProps = {
  setAddingContentDescription: React.Dispatch<React.SetStateAction<boolean>>;
  subjects: Subject[] | undefined;
  setSubjectData: React.Dispatch<React.SetStateAction<Subject[] | undefined>>;
  setSubjectsForTerm: React.Dispatch<React.SetStateAction<Subject[]>>;
};

type Topic = Strand | Substrand;

// this function needs to work out the yearlevel, topic and content descriptions to add to the termplanner
function ContentDescriptionSearchBox({ setAddingContentDescription, subjects, setSubjectData }: ContentDescriptionSearchBoxProps) {
  const [termSubjects, setTermSubjects] = useState<Subject[]>([]); // this is the array of subjects that will be added to the termplanner
  const [currentSubjectId, setCurrentSubjectId] = useState<string | undefined>(undefined);
  const [currentYearLevelName, setCurrentYearLevelName] = useState<string | undefined>(undefined);
  const [currentTopicName, setCurrentTopicName] = useState<string | undefined>(undefined);
  const [selectedContentDescriptionIds, setSelectedContentDescriptionIds] = useState<string[]>([]);
  let currentSubject: Subject | undefined = subjects?.find((subject) => subject.id === currentSubjectId);
  const currentYearLevel: SubjectYearLevel | undefined = getCurrentYearLevel();
  const currentTopic: Topic | undefined = getCurrentTopic();
  const topics: Topic[] | undefined = getTopics();
  const contentDescriptions: ContentDescription[] | undefined = getContentDescriptions();
  const { teacher } = useAuth();

  useEffect(() => {
    console.log("rendered");
  });

  useEffect(() => {
    if (subjects === undefined) {
      const controller = new AbortController();

      fetch(`${baseUrl}/curriculum?elaborations=false`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${teacher!.token}`,
        },
        signal: controller.signal,
      })
        .then((response) => response.json())
        .then((data) => {
          setSubjectData(data.subjects);
          currentSubject = data.subjects[0];
        });

      return () => controller.abort();
    }
    return; // to suppress compiler warning about all paths not returning
  }, []);

  function getCurrentYearLevel(): SubjectYearLevel | undefined {
    const subject = subjects?.find((subject) => subject.id === currentSubjectId);
    console.log("Setting currentYearLevel to: ", subject?.yearLevels.find((yl) => yl.name === currentYearLevelName));
    return subject?.yearLevels.find((yl) => yl.name === currentYearLevelName);
  }

  function getCurrentTopic(): Topic | undefined {
    if (!currentYearLevel) {
      return;
    }

    if (currentYearLevel.strands?.length! > 0) {
      return currentTopicName ? currentYearLevel.strands?.find((strand) => strand.name === currentTopicName) : currentYearLevel.strands![0];
    } else {
      return currentTopicName
        ? currentYearLevel.substrands?.find((substrand) => substrand.name === currentTopicName)
        : currentYearLevel.substrands![0];
    }
  }

  // should check whether the clicked subject is already in the termSubjects array
  // if so, set the current subject to that subject
  // if not, add the subject to the termSubjects array and set the current subject to that subject
  function handleSubjectChange(subjectName: string): void {
    termSubjects.forEach((subject) => {
      if (subject.name === subjectName) {
        setCurrentSubjectId(subject.id);
        return;
      }
    });

    const subject = subjects?.find((subject) => subject.name === subjectName)!;

    setTermSubjects([...termSubjects, subject]);
    setCurrentSubjectId(subject.id);
  }

  function getYearLevels(): string[] {
    if (!currentSubject) {
      return [];
    }

    const subject = subjects?.find((subject) => subject.name === currentSubject!.name);

    return subject ? subject.yearLevels.map((yearLevel) => yearLevel.name) : [];
  }

  function handleYearLevelChange(yearLevelName: string): void {
    const subject = subjects?.find((subject) => subject.name === currentSubject!.name);
    const yearLevel = subject?.yearLevels.find((yl) => yl.name === yearLevelName);
    setCurrentYearLevelName(yearLevel?.name);
  }

  function getTopics(): Topic[] {
    if (!currentYearLevelName || !currentYearLevelName) {
      return [];
    }

    const currentYearLevel = getCurrentYearLevel();

    const topics: Topic[] = [];
    if (currentYearLevel!.strands) {
      currentYearLevel!.strands.forEach((strand) => topics.push(strand));
    } else {
      currentYearLevel!.substrands?.forEach((substrand) => topics.push(substrand));
    }

    return topics;
  }

  function handleTopicChange(topicDescription: string): void {
    if (!currentYearLevelName || !topicDescription) {
      return;
    }

    const currentYearLevel = getCurrentYearLevel();

    const topic = currentYearLevel!.strands
      ? currentYearLevel!.strands.find((strand) => strand.name === topicDescription)!
      : currentYearLevel!.substrands!.find((substrand) => substrand.name === topicDescription)!;

    setCurrentTopicName(topic.name);
  }

  function getContentDescriptions(): ContentDescription[] {
    if (!currentSubject || !currentYearLevelName || !currentTopic) {
      return [];
    }

    const contentDescriptions: ContentDescription[] = [];
    // const currentTopic = getCurrentTopic();

    if (isStrand(currentTopic)) {
      currentTopic.substrands?.forEach((substrand) => {
        substrand.contentDescriptions?.forEach((contentDescription) => contentDescriptions.push(contentDescription));
      });
    } else {
      currentTopic.contentDescriptions?.forEach((contentDescription) => contentDescriptions.push(contentDescription));
    }

    return contentDescriptions;
  }

  function isSelectedContentDescription(contentDescription: ContentDescription): boolean {
    for (const cd of selectedContentDescriptionIds) {
      if (cd === contentDescription.curriculumCode) {
        return true;
      }
    }

    return false;
  }

  function handleContentDescriptionClick(contentDescription: ContentDescription): void {
    if (selectedContentDescriptionIds.length === 0) {
      setSelectedContentDescriptionIds([contentDescription.curriculumCode]);
      return;
    }

    for (const cd of selectedContentDescriptionIds) {
      if (cd === contentDescription.curriculumCode) {
        setSelectedContentDescriptionIds(selectedContentDescriptionIds.filter((cd) => cd !== contentDescription.curriculumCode));
      } else {
        setSelectedContentDescriptionIds([...selectedContentDescriptionIds, contentDescription.curriculumCode]);
      }
    }
  }

  function handleAddContentDescriptions(): void {}

  function handleCloseSearchBox(): void {
    setAddingContentDescription(false);
  }

  //#region type guards
  function isStrand(topic: Topic): topic is Strand {
    return (topic as Strand).substrands !== undefined;
  }
  //#endregion

  return (
    <dialog className="flex flex-col z-10 flex-grow border border-darkGreen w-[60vw] top-6 h-[75vh] px-2 bg-slate-300">
      <div className="justify-center w-full p-1">
        <h1 className="text-lg text-center font-semibold">Add Content Descriptions</h1>
        <Button variant="close" classList="absolute top-1 right-1" onClick={handleCloseSearchBox}>
          <FontAwesomeIcon icon={faXmark} />
        </Button>
      </div>
      {subjects === undefined ? (
        <div>Loading...</div>
      ) : (
        <>
          <div className="grid grid-cols-2 mb-3">
            <div className="flex flex-col items-center">
              <div className="text-left">
                <label className="block mb-1 font-semibold">Subjects</label>
                <Dropdown
                  options={subjects.map((subject) => subject.name)}
                  defaultValue={currentSubject?.name}
                  onChange={handleSubjectChange}
                  placeholder="Choose a subject"
                />
              </div>
            </div>
            <div className="flex flex-col items-center">
              <div className="text-left">
                <label className="block mb-1 font-semibold">Year Levels</label>
                <Dropdown
                  options={getYearLevels()}
                  defaultValue={currentYearLevel?.name}
                  onChange={handleYearLevelChange}
                  placeholder="Select a year level"
                  disabled={currentSubject === undefined}
                />
              </div>
            </div>
          </div>
          <div className="flex gap-3 overflow-hidden mb-2">
            {/* List of topics for each subject */}
            <div className="w-1/5 h-full">
              <h5 className="text-lg text-center">Topics</h5>
              {currentYearLevel && (
                <ul className="">
                  {topics?.map((topic) => (
                    <li
                      key={topic.name}
                      className={`border border-darkGreen hover:bg-sageHover p-2 ${topic.name === currentTopic?.name && "bg-sageFocus"} select-none`}
                      onClick={() => handleTopicChange(topic.name)}>
                      {topic.name}
                    </li>
                  ))}
                </ul>
              )}
            </div>

            {/* List of content descriptions for each topic */}
            <div className="w-4/5 flex flex-col">
              <h5 className="text-lg text-center">Content Descriptions</h5>
              <ul className="overflow-scroll">
                {contentDescriptions?.map((contentDescription) => (
                  <li
                    key={contentDescription.curriculumCode}
                    className={`border border-darkGreen hover:bg-sageHover p-2 ${
                      isSelectedContentDescription(contentDescription) && "bg-sageFocus"
                    } select-none`}
                    onClick={() => handleContentDescriptionClick(contentDescription)}>
                    {contentDescription.description}
                  </li>
                ))}
              </ul>
            </div>
          </div>
          <Button variant="add" classList="self-end mt-auto mb-2 mr-2" onClick={handleAddContentDescriptions}>
            Add Content Descriptions
          </Button>
        </>
      )}
    </dialog>
  );
}

export default ContentDescriptionSearchBox;

// copied from https://plainenglish.io/blog/deep-clone-an-object-and-preserve-its-type-with-typescript-d488c35e5574#summary
function deepCopy<T>(source: T): T {
  return Array.isArray(source)
    ? source.map((item) => deepCopy(item))
    : source instanceof Date
    ? new Date(source.getTime())
    : source && typeof source === "object"
    ? Object.getOwnPropertyNames(source).reduce(
        (o, prop) => {
          Object.defineProperty(o, prop, Object.getOwnPropertyDescriptor(source, prop)!);
          o[prop] = deepCopy((source as { [key: string]: any })[prop]);
          return o;
        },
        Object.create(Object.getPrototypeOf(source))
      )
    : (source as T);
}
