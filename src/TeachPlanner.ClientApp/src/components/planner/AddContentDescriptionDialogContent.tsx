import { useState } from "react";
import Button from "../common/Button";
import CancelButton from "../common/CancelButton";

type Props = {
  dialogRef: React.RefObject<HTMLDialogElement>;
  initialSelectedContentDescriptions: ContentDescription[];
  setContentDescriptions: React.Dispatch<React.SetStateAction<ContentDescription[]>>;
  availableContentDescriptions: ContentDescription[];
};

function AddContentDescriptionDialogContent({
  dialogRef,
  initialSelectedContentDescriptions,
  setContentDescriptions,
  availableContentDescriptions,
}: Props) {
  const [selectedContentDescriptions, setSelectedContentDescriptions] = useState<ContentDescription[]>(initialSelectedContentDescriptions);

  function handleSelectContentDescriptions() {
    setContentDescriptions(selectedContentDescriptions);
    dialogRef.current?.close();
  }

  function handleClick(curriculumCode: string) {
    if (cdNotSelected(curriculumCode)) {
      setSelectedContentDescriptions([
        ...selectedContentDescriptions,
        availableContentDescriptions.find((cd) => cd.curriculumCode === curriculumCode)!,
      ]);
      return;
    }

    setSelectedContentDescriptions(selectedContentDescriptions.filter((cd) => cd.curriculumCode !== curriculumCode));
  }

  function cdNotSelected(curriculumCode: string) {
    return !selectedContentDescriptions.find((cd) => cd.curriculumCode === curriculumCode);
  }

  function isSelected(curriculumCode: string) {
    return selectedContentDescriptions.find((scd) => scd.curriculumCode === curriculumCode);
  }

  return (
    <div className="p-2 flex flex-col relative">
      <div>
        <CancelButton onClick={() => dialogRef.current?.close()} />
        <h3 className="text-lg font-semibold">Select the content descriptions to add to this lesson</h3>
      </div>
      <div className="border border-darkGreen p-2">
        <ul>
          {availableContentDescriptions.map((cd) => (
            <li
              key={cd.curriculumCode}
              className={`select-none hover:bg-primaryHover hover:cursor-pointer mb-1
              ${isSelected(cd.curriculumCode) && "bg-primaryFocus hover:bg-primaryFocus"}`}
              onClick={() => handleClick(cd.curriculumCode)}>
              {cd.curriculumCode}: {cd.description}
            </li>
          ))}
        </ul>
      </div>
      <Button variant="add" onClick={handleSelectContentDescriptions} classList="mt-2">
        Add
      </Button>
    </div>
  );
}

export default AddContentDescriptionDialogContent;
