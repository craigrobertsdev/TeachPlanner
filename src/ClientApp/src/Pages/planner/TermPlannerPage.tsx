import { useState } from "react";
import Button from "../../components/common/Button";
import ContentDescriptionSearchBox from "../../components/planner/ContentDescriptionSearchBox";

const TermPlannerPage = () => {
  const [subjectsForTerm, setSubjectsForTerm] = useState<Subject[]>([]);
  const [addingContentDescription, setAddingContentDescription] = useState<boolean>(true);
  const [subjectData, setSubjectData] = useState<Subject[] | null>(null);

  function handleAddContentDescription() {
    setAddingContentDescription(true);
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
        <ContentDescriptionSearchBox
          setAddingContentDescription={setAddingContentDescription}
          subjects={subjectData}
          setSubjectData={setSubjectData}
          setSubjectsForTerm={setSubjectsForTerm}
        />
      )}
    </div>
  );
};

export default TermPlannerPage;
