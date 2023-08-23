import { useEffect, useState } from "react";
import Button from "../../components/common/Button";
import ContentDescriptionSearchBox from "../../components/planner/ContentDescriptionSearchBox";
import { baseUrl } from "../../utils/constants";
import Dropdown from "../../components/common/Dropdown";

const TermPlannerPage = () => {
  const [addingContentDescription, setAddingContentDescription] = useState<boolean>(true);
  const [currentSubject, setCurrentSubject] = useState<string>("");
  const [subjects, setSubjects] = useState<Subject[]>([]);

  useEffect(() => {
    setSubjects([
      {
        id: "1",
        name: "Mathematics",
      } as Subject,
      {
        id: "2",
        name: "English",
      } as Subject,
      {
        id: "3",
        name: "Science",
      } as Subject,
    ]);
  }, []);

  useEffect(() => {
    console.log(currentSubject);
  }, [currentSubject]);

  useEffect(() => {
    const fetchContentDescriptions = async () => {
      const response = await fetch(`${baseUrl}/curriculum/content-descriptions`, {
        body: JSON.stringify({
          elaborations: false,
        }),
      });
      const data = await response.json();

      setSubjects(data);
    };

    fetchContentDescriptions();
  }, []);

  function handleAddContentDescription() {
    setAddingContentDescription(true);
  }

  function handleSubjectChange(value: string | string[] | undefined) {
    setCurrentSubject(value as string);
  }

  return (
    <div>
      Term Planner
      {/* Content Descriptions */}
      <div className="flex flex-col items-center w-full flex-grow h-1/2 p-2">
        <h2 className="text-lg font-semibold">Content Descriptions</h2>
        <div className="flex-grow w-full border border-darkGreen p-2 flex flex-col">
          <Button variant="add" classList="self-end mb-2" onClick={handleAddContentDescription}>
            Add Content Description
          </Button>
        </div>
      </div>
      {/* Content description search box */}
      {addingContentDescription && (
        <ContentDescriptionSearchBox subjects={subjects} setSubjects={setSubjects} setAddingContentDescription={setAddingContentDescription} />
      )}
      <Dropdown
        placeholder="Choose subject"
        options={subjects.map((subject) => subject.name)}
        onChange={handleSubjectChange}
        isSearchable={true}
        multiSelect={true}
      />
    </div>
  );
};

export default TermPlannerPage;
