import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "../common/Button";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import Dropdown from "../common/Dropdown";

type ContentDescriptionSearchBoxProps = {
  subjects: Subject[] | null;
  setSubjects: React.Dispatch<React.SetStateAction<Subject[]>>;
  setAddingContentDescription: React.Dispatch<React.SetStateAction<boolean>>;
};

function ContentDescriptionSearchBox({ subjects, setSubjects, setAddingContentDescription }: ContentDescriptionSearchBoxProps) {
  const [searchValue, setSearchValue] = useState<string>("");
  const [selectedYearLevels, setSelectedYearLevels] = useState<number[]>([]);
  const [currentTopic, setCurrentTopic] = useState<Subject | null>(null);

  function handleCloseSearchBox() {
    setAddingContentDescription(false);
  }

  function handleSearch(event: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(event.target.value);

    currentTopic;
  }

  return (
    <div className="flex flex-col z-10 flex-grow border border-darkGreen">
      <Button variant="add" classList="self-end mb-2" onClick={handleCloseSearchBox}>
        <FontAwesomeIcon icon={faXmark} />
      </Button>
      {subjects === null ? (
        <p>Loading...</p>
      ) : (
        // year level selector
        // <Dropdown placeholder="Choose subject" options={subjects.map((subject) => subject.name)} />
        // topic selector
        <></>
        // search box
        // <input type="text" value={searchValue} onChange={handleSearch} placeholder="Search for a content description" className="border border-darkGreen p-2" />
      )}
    </div>
  );
}

export default ContentDescriptionSearchBox;
