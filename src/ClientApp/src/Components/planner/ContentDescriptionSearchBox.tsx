import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "../common/Button";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import Dropdown from "../common/Dropdown";
import { baseUrl } from "../../utils/constants";
import useAuth from "../../contexts/AuthContext";

type ContentDescriptionSearchBoxProps = {
  setAddingContentDescription: React.Dispatch<React.SetStateAction<boolean>>;
  subjects: Subject[] | null;
  setSubjectData: React.Dispatch<React.SetStateAction<Subject[] | null>>;
  setSubjectsForTerm: React.Dispatch<React.SetStateAction<Subject[]>>;
};

type Topic = Strand | Substrand;

// this function needs to work out the yearlevel, topic and content descriptions to add to the termplanner
function ContentDescriptionSearchBox({ setAddingContentDescription, subjects, setSubjectData }: ContentDescriptionSearchBoxProps) {
  const [termSubjects, setTermSubjects] = useState<Subject[]>([]);
  const [currentSubject, setCurrentSubject] = useState<Subject | null>(null);
  const [currentYearLevel, setCurrentYearLevel] = useState<SubjectYearLevel | null>(null);
  const [currentTopic, setCurrentTopic] = useState<Topic | null>(null);
  const [selectedContentDescriptions, setSelectedContentDescriptions] = useState<ContentDescription[]>([]);
  const [topics, setTopics] = useState<Topic[] | null>(null);
  const [contentDescriptions, setContentDescriptions] = useState<ContentDescription[] | null>(null);
  const { teacher } = useAuth();
  const [dummyState, setDummyState] = useState<boolean>(false);

  useEffect(() => {
    if (subjects === null) {
      const fetchSubjects = async () => {
        const response = await fetch(`${baseUrl}/curriculum?elaborations=false`, {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${teacher!.token}`,
          },
        });
        const data = await response.json();

        setSubjectData(data.subjects);
        setCurrentSubject(data.subjects[0]);
      };

      fetchSubjects();
    }
  }, []);

  useEffect(() => {
    if (!currentYearLevel) {
      return;
    }
    const topic: Topic = currentYearLevel.strands?.length! > 0 ? currentYearLevel.strands![0] : currentYearLevel.substrands![0];
    setCurrentTopic(topic);
  }, [currentSubject]);

  useEffect(() => {
    setTopics(getTopics());
    setContentDescriptions(getContentDescriptions());
  }, [currentYearLevel, currentTopic]);

  function handleCloseSearchBox(): void {
    setAddingContentDescription(false);
  }

  function handleSubjectChange(subjectName: string): void {
    termSubjects.forEach((subject) => {
      if (subject.name === subjectName) {
        setCurrentSubject(subject);
        return;
      }
    });

    const newSubject = {
      name: subjectName,
      yearLevels: [] as SubjectYearLevel[],
    } as Subject;

    setTermSubjects([...termSubjects, newSubject]);
    setCurrentSubject(newSubject);
  }

  function getYearLevels(): string[] {
    if (!currentSubject) {
      return [];
    }

    const subject = subjects?.find((subject) => subject.name === currentSubject.name)!;

    return subject.yearLevels.map((yearLevel) => yearLevel.name);
  }

  function handleYearLevelChange(yearLevel: string): void {
    const subject = subjects?.find((subject) => subject.name === currentSubject!.name);

    setCurrentYearLevel(subject?.yearLevels.find((yl) => yl.name === yearLevel) as SubjectYearLevel);
  }

  function getTopics(): Topic[] {
    if (!currentYearLevel || !currentYearLevel) {
      return [];
    }

    const topics: Topic[] = [];
    if (currentYearLevel.strands) {
      currentYearLevel.strands.forEach((strand) => topics.push(strand));
    } else {
      currentYearLevel.substrands?.forEach((substrand) => topics.push(substrand));
    }

    return topics;
  }

  function handleTopicChange(topicDescription: string): void {
    if (!currentYearLevel || !topicDescription) {
      return;
    }

    const topic = currentYearLevel.strands
      ? currentYearLevel.strands.find((strand) => strand.name === topicDescription)!
      : currentYearLevel.substrands!.find((substrand) => substrand.name === topicDescription)!;

    setCurrentTopic(topic);
  }

  function getContentDescriptions(): ContentDescription[] {
    if (!currentSubject || !currentYearLevel || !currentTopic) {
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
    for (const cd of selectedContentDescriptions) {
      if (cd.curriculumCode === contentDescription.curriculumCode) {
        return true;
      }
    }

    return false;
  }

  function handleContentDescriptionClick(contentDescription: ContentDescription): void {
    if (selectedContentDescriptions.length === 0) {
      setSelectedContentDescriptions([contentDescription]);
      return;
    }

    for (const cd of selectedContentDescriptions) {
      if (cd.curriculumCode === contentDescription.curriculumCode) {
        setSelectedContentDescriptions(selectedContentDescriptions.filter((cd) => cd.curriculumCode !== contentDescription.curriculumCode));
      } else {
        setSelectedContentDescriptions([...selectedContentDescriptions, contentDescription]);
      }
    }
  }

  function handleAddContentDescriptions(): void {}

  function isStrand(topic: Topic): topic is Strand {
    return (topic as Strand).substrands !== undefined;
  }

  return (
    <div className="flex flex-col z-10 flex-grow border border-darkGreen max-w-4xl">
      <div className="flex justify-between p-1">
        <h1 className="text-lg font-semibold">Add Content Description</h1>
        <Button variant="add" classList="mb-2" onClick={handleCloseSearchBox}>
          <FontAwesomeIcon icon={faXmark} />
        </Button>
      </div>
      {subjects === null ? (
        <div>Loading...</div>
      ) : (
        <>
          <div className="flex">
            <div>
              <label>Subjects</label>
              <Dropdown
                options={subjects.map((subject) => subject.name)}
                defaultValue={currentSubject?.name}
                onChange={handleSubjectChange}
                placeholder="Choose a subject"
                isSearchable={true}
              />
            </div>
            <div>
              <label>Year Levels</label>
              <Dropdown
                options={getYearLevels()}
                defaultValue={currentYearLevel?.name}
                onChange={handleYearLevelChange}
                placeholder="Select a year level"
                disabled={currentSubject === null}
              />
            </div>
          </div>
          <div className="flex gap-3 ">
            {/* List of topics for each subject */}
            <div className="w-1/5">
              <h3>Topics</h3>
              <div className="border border-darkGreen">
                {currentYearLevel && (
                  <ul className="">
                    {topics?.map((topic) => (
                      <li
                        key={topic.id}
                        className={`border border-darkGreen hover:bg-sageHover ${topic.name === currentTopic?.name && "bg-sageFocus"} select-none`}
                        onClick={() => handleTopicChange(topic.name)}>
                        {topic.name}
                      </li>
                    ))}
                  </ul>
                )}
              </div>
            </div>

            {/* List of content descriptions for each topic */}
            <div className="w-4/5">
              <h3>Content Descriptions</h3>
              <div className="border border-darkGreen">
                <ul className="">
                  {contentDescriptions?.map((contentDescription) => (
                    <li
                      key={contentDescription.id}
                      className={`border border-darkGreen hover:bg-sageHover ${
                        isSelectedContentDescription(contentDescription) && "bg-sageFocus"
                      } select-none`}
                      onClick={() => handleContentDescriptionClick(contentDescription)}>
                      {contentDescription.description}
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          </div>
          <Button variant="add" classList="self-end mb-2" onClick={handleAddContentDescriptions}>
            Add Content Descriptions
          </Button>
        </>
      )}
    </div>
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
