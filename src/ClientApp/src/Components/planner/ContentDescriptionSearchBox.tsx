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

// this function needs to work out the yearlevel, topic and content descriptions to add to the termplanner
function ContentDescriptionSearchBox({ setAddingContentDescription, subjects, setSubjectData }: ContentDescriptionSearchBoxProps) {
  const [termSubjects, setTermSubjects] = useState<Subject[]>([]);
  const [currentSubject, setCurrentSubject] = useState<Subject | null>(null);
  const [currentYearLevel, setCurrentYearLevel] = useState<SubjectYearLevel | null>(null);
  const [currentTopic, setCurrentTopic] = useState<Strand | Substrand | null>(null);
  const [selectedContentDescriptions, setSelectedContentDescriptions] = useState<ContentDescription[]>([]);
  const { teacher } = useAuth();

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

  function getTopics(): string[] {
    if (!currentYearLevel) {
      return [];
    }

    const topics: string[] = [];
    if (currentYearLevel.strands) {
      currentYearLevel.strands.forEach((strand) => topics.push(strand.name));
    } else {
      currentYearLevel.substrands!.forEach((substrand) => topics.push(substrand.name));
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
    if (!currentTopic) {
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
    return selectedContentDescriptions.includes(contentDescription);
  }

  function handleContentDescriptionClick(contentDescription: ContentDescription): void {
    if (selectedContentDescriptions.includes(contentDescription)) {
      setSelectedContentDescriptions(selectedContentDescriptions.filter((cd) => cd !== contentDescription));
    } else {
      setSelectedContentDescriptions([...selectedContentDescriptions, contentDescription]);
    }
  }

  function handleAddContentDescriptions(): void {}

  function isStrand(topic: Strand | Substrand): topic is Strand {
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
                onChange={handleSubjectChange}
                placeholder="Choose a subject"
                isSearchable={true}
              />
            </div>
            <div>
              <label>Year Levels</label>
              <Dropdown
                options={getYearLevels()}
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
                    {getTopics().map((topic) => (
                      <li
                        key={topic}
                        className={`border border-darkGreen hover:bg-sageHover ${topic === currentTopic?.name && "bg-sageFocus"} select-none`}
                        onClick={() => handleTopicChange(topic)}>
                        {topic}
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
                  {getContentDescriptions().map((contentDescription) => (
                    <li
                      key={contentDescription.id}
                      className={`border border-darkGreen hover:bg-sageHover ${
                        isSelectedContentDescription(contentDescription.description) && "bg-sageFocus"
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
