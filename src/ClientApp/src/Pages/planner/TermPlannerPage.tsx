import { useEffect, useState } from "react";
import Button from "../../components/common/Button";
import ContentDescriptionSearchBox from "../../components/planner/ContentDescriptionSearchBox";
import CurriculumService from "../../services/CurriculumService";
import useAuth from "../../contexts/AuthContext";

// TODO: work out why an error occurs when reopening the search box after closing it
const TermPlannerPage = () => {
  const [subjectsForTerm, setSubjectsForTerm] = useState<Subject[]>([]);
  const [addingContentDescriptions, setAddingContentDescriptions] = useState<boolean>(false);
  const [subjectData, setSubjectData] = useState<Subject[] | undefined>(undefined);
  const [year, setYear] = useState<number>(new Date().getFullYear());
  const { teacher } = useAuth();

  useEffect(() => {
    const getSubjects = async () => {
      const data = CurriculumService.getTermPlannerSubjects({ year }, teacher!);
    };
  }, []);

  async function handleAddContentDescription() {
    setAddingContentDescriptions(true);
  }

  useEffect(() => {
    console.log(subjectsForTerm);
  }, [subjectsForTerm]);

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
      {addingContentDescriptions && (
        <ContentDescriptionSearchBox
          setAddingContentDescription={setAddingContentDescriptions}
          subjects={subjectData}
          setSubjectData={setSubjectData}
          termSubjects={subjectsForTerm}
          setTermSubjects={setSubjectsForTerm}
        />
      )}
    </div>
  );
};

export default TermPlannerPage;
