import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "../common/Button";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import Dropdown from "../common/Dropdown";
import { baseUrl } from "../../utils/constants";
import useAuth from "../../contexts/AuthContext";

type ContentDescriptionSearchBoxProps = {
  setAddingContentDescription: React.Dispatch<React.SetStateAction<boolean>>;
  subjects: Subject[] | undefined;
  setSubjectData: React.Dispatch<React.SetStateAction<Subject[] | undefined>>;
  setSubjectsForTerm: React.Dispatch<React.SetStateAction<Subject[]>>;
};

type Topic = Strand | Substrand;

type YearLevelBandNames = {
  foundation: "Foundation";
  years1To2: "Years1To2";
  years3To4: "Years3To4";
  years5To6: "Years5To6";
};

type YearLevelBand = SubjectYearLevel & {
  bandLevelValue: YearLevelBandNames[keyof YearLevelBandNames];
};

// this function needs to work out the yearlevel, topic and content descriptions to add to the termplanner
function ContentDescriptionSearchBox({ setAddingContentDescription, subjects, setSubjectData }: ContentDescriptionSearchBoxProps) {
  const [termSubjects, setTermSubjects] = useState<Subject[]>([]); // this is the array of subjects that will be added to the termplanner
  const [currentSubjectId, setCurrentSubjectId] = useState<string | undefined>(undefined);
  const [currentYearLevelName, setCurrentYearLevelName] = useState<string | undefined>(undefined);
  const [currentTopicName, setCurrentTopicName] = useState<string | undefined>(undefined);
  const [selectedContentDescriptionIds, setSelectedContentDescriptionIds] = useState<string[]>([]);
  const currentSubject: Subject | undefined = subjects?.find((subject) => subject.id === currentSubjectId);
  let currentYearLevel: SubjectYearLevel | YearLevelBand | undefined = getCurrentYearLevel();
  const currentTopic: Topic | undefined = getCurrentTopic();
  const topics: Topic[] | undefined = getTopics();
  const contentDescriptions: ContentDescription[] | undefined = getContentDescriptions();
  const { teacher } = useAuth();

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
        });

      return () => controller.abort();
    }
    return; // to suppress compiler warning about all paths not returning
  }, []);

  // this function is called whenever the subject is being changed
  // needs to check whether the subject has individual year levels or bands of year levels,
  // and return the appropriate year level or band based on previous selections
  function getCurrentYearLevel(): SubjectYearLevel | YearLevelBand | undefined {
    if (!subjects) {
      return undefined;
    }

    const subject = subjects.find((subject) => subject.id === currentSubjectId);

    return subject ? subject.yearLevels.find((yl) => yl.name === currentYearLevelName) : undefined;
  }

  function getCurrentTopic(): Topic | undefined {
    console.log(currentTopicName);
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

    // if subject is Health and PE, then it has band levels. Need to get the bandLevelValue for the selected band level
    // also need to set the current year level to the band level equivalent
    // needs to set the current topic to the first one

    // if we're moving from a subject with band levels to a subject with year levels
    if (isBandLevel(currentYearLevel) && isBandLevelArray(subject.yearLevels)) {
      const yearLevelName = determineYearLevel(termSubjects);
      setCurrentYearLevelName(yearLevelName);
    }

    // if we're moving from a subject with year levels to a subject with band levels
    if (!isBandLevel(currentYearLevel) && isBandLevelArray(subject.yearLevels)) {
      const currentYearLevelNumber = currentYearLevelName?.split(" ")[1];
      if (currentYearLevelNumber) {
        const bandLevel = subject.yearLevels.find((yl) => yl.bandLevelValue.match(currentYearLevelNumber));
        setCurrentYearLevelName(bandLevel?.name);
      }
    }

    setTermSubjects([...termSubjects, subject]);
    setCurrentSubjectId(subject.id);
    setCurrentTopicName((prev) => getCurrentTopic()?.name);
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
    if (!currentYearLevelName) {
      return [];
    }

    const currentYearLevel = getCurrentYearLevel();

    if (!currentYearLevel) {
      return [];
    }

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

  // called only when the previous subject has YearLevelBands as year levels and the new subject has SubjectYearLevels
  function determineYearLevel(subjects: Subject[]): string {
    const yearLevelNameCounts = {
      Foundation: 0,
      "Year 1": 0,
      "Year 2": 0,
      "Year 3": 0,
      "Year 4": 0,
      "Year 5": 0,
      "Year 6": 0,
    };

    for (const subject of subjects) {
      for (const yl of subject.yearLevels) {
        switch (yl.name) {
          case "Foundation":
            yearLevelNameCounts["Foundation"]++;
            break;
          case "Year 1":
            yearLevelNameCounts["Year 1"]++;
            break;
          case "Year 2":
            yearLevelNameCounts["Year 2"]++;
            break;
          case "Year 3":
            yearLevelNameCounts["Year 3"]++;
            break;
          case "Year 4":
            yearLevelNameCounts["Year 4"]++;
            break;
          case "Year 5":
            yearLevelNameCounts["Year 5"]++;
            break;
          case "Year 6":
            yearLevelNameCounts["Year 6"]++;
            break;
          default:
            break;
        }
      }
    }

    const arr = Object.values(yearLevelNameCounts);
    const maxIndex = arr.indexOf(Math.max(...arr));

    return Object.keys(yearLevelNameCounts)[maxIndex];
  }

  //#region type guards
  function isStrand(topic: Topic): topic is Strand {
    return (topic as Strand).substrands !== undefined;
  }

  function isBandLevel(yearLevel: unknown): yearLevel is YearLevelBand {
    if (yearLevel === undefined) {
      return false;
    }
    return (yearLevel as YearLevelBand).bandLevelValue !== undefined;
  }

  function isBandLevelArray(yearLevels: unknown): yearLevels is YearLevelBand[] {
    if (yearLevels === undefined) {
      return false;
    }
    return Array.isArray(yearLevels) && (yearLevels as YearLevelBand[]).every((yl) => yl.bandLevelValue !== undefined);
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
          <div className="grid grid-cols-2 gap-5 mb-3">
            <div className="flex flex-col items-start">
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
            <div className="flex flex-col items-start m-auto">
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
