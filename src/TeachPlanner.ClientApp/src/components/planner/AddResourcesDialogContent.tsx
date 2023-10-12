import { useEffect, useState } from "react";
import Button from "../common/Button";
import CancelButton from "../common/CancelButton";

type Props = {
  subject: Subject;
  dialogRef: React.RefObject<HTMLDialogElement>;
  initialSelectedResources: Resource[];
  setResources: React.Dispatch<React.SetStateAction<Resource[]>>;
};

function AddResourcesDialogContent({ subject, dialogRef, initialSelectedResources, setResources }: Props) {
  const [selectedResources, setSelectedResources] = useState<Resource[]>(initialSelectedResources);
  const [availableResources, setAvailableResources] = useState<Resource[]>([]);

  useEffect(() => {
    const getResources = async () => {
      const abortController = new AbortController();
      const resources = await resourceService.getResources(subject, teacher!, token!);
    };
  }, []);

  function handleSelectResources() {
    setResources(selectedResources);
    dialogRef.current?.close();
  }

  function handleClick(id: string) {
    if (resourceNotSelected(id)) {
      setSelectedResources([...selectedResources, availableResources.find((r) => r.id === id)!]);
      return;
    }

    setSelectedResources(selectedResources.filter((r) => r.id !== id));
  }

  function resourceNotSelected(id: string) {
    return !selectedResources.find((r) => r.id === id);
  }

  function isSelected(id: string) {
    return selectedResources.find((r) => r.id === id);
  }

  return (
    <div className="p-2 flex flex-col relative">
      <div>
        <CancelButton onClick={() => dialogRef.current?.close()} />
        <h3 className="text-lg font-semibold">Select the content descriptions to add to this lesson</h3>
      </div>
      <div className="border border-darkGreen p-2">
        <ul>
          {availableResources.map((r) => (
            <li
              key={r.id}
              className={`select-none hover:bg-primaryHover hover:cursor-pointer mb-1
              ${isSelected(r.id) && "bg-primaryFocus hover:bg-primaryFocus"}`}
              onClick={() => handleClick(r.id)}>
              {r.name}: {r.description}
            </li>
          ))}
        </ul>
      </div>
      <Button variant="add" onClick={handleSelectResources} classList="mt-2">
        Add
      </Button>
    </div>
  );
}

export default AddResourcesDialogContent;
