import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "../common/Button";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import Dropdown from "../common/Dropdown";
import { baseUrl } from "../../utils/constants";
import useAuth from "../../contexts/AuthContext";

// TODO: implement function to add content descriptions to the term planner
type ContentDescriptionSearchBoxProps = {
  setAddingContentDescription: React.Dispatch<React.SetStateAction<boolean>>;
  subjects: Subject[] | undefined;
  setSubjectData: React.Dispatch<React.SetStateAction<Subject[] | undefined>>;
  setSubjectsForTerm: React.Dispatch<React.SetStateAction<Subject[]>>;
};

type YearLevelBandNames = {
  foundation: "Foundation";
  years1To2: "Years1To2";
  years3To4: "Years3To4";
  years5To6: "Years5To6";
};

type YearLevelBand = SubjectYearLevel & {
  bandLevelValue: YearLevelBandNames[keyof YearLevelBandNames];
};

type SubjectState = {
  currentStrand: string;
  isCurrentSubject: boolean;
  selectedContentDescriptionIds: [string, string[]][]; // first string is the year level name, second string array is the array of content description ids
};

type SubjectStateTable = {
  currentYearLevel: string;
} & {
  [key: string]: SubjectState;
};

// this function needs to work out the yearlevel, strand and content descriptions to add to the termplanner
function ContentDescriptionSearchBox({
  setAddingContentDescription,
  subjects,
  setSubjectData,
  setSubjectsForTerm,
}: ContentDescriptionSearchBoxProps) {
  const [subjectStates, setSubjectStates] = useState<SubjectStateTable>({} as SubjectStateTable);
  const { teacher } = useAuth();
  const currentSubject = getCurrentSubject();
  const currentYearLevel = getCurrentYearLevel();
  const currentStrand = getCurrentStrand();
  const selectedContentDescriptionIds = getSelectedContentDescriptionIds();
  const strands = getStrands();
  const contentDescriptions = getContentDescriptions();

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
          setInitialSubjectStates(data.subjects);
        });

      return () => controller.abort();
    }
    return; // to suppress compiler warning about all paths not returning
  }, []);

  function setInitialSubjectStates(subjects: Subject[]) {
    const subjectStateTable: SubjectStateTable = {} as SubjectStateTable;

    subjects.forEach((subject, index) => {
      const strandName = subject.yearLevels[0].strands![0].name;
      subjectStateTable[subject.name] = {
        currentStrand: strandName,
        isCurrentSubject: index === 0 ? true : false,
        selectedContentDescriptionIds: createContentDescriptionIdsArray(subject.yearLevels),
      };
    });

    subjectStateTable.currentYearLevel = subjects[0].yearLevels[0].name;

    setSubjectStates(subjectStateTable);
  }

  function createContentDescriptionIdsArray(yearLevels: SubjectYearLevel[] | YearLevelBand[]): [string, string[]][] {
    const contentDescriptionIds: [string, string[]][] = [];

    yearLevels.forEach((yearLevel) => {
      contentDescriptionIds.push([yearLevel.name, []]);
    });

    return contentDescriptionIds;
  }

  function getCurrentSubject(): Subject | undefined {
    for (const key in subjectStates) {
      if (subjectStates[key].isCurrentSubject) {
        return subjects?.find((subject) => subject.name === key);
      }
    }

    return undefined;
  }

  // this function is called whenever the subject is being changed
  // needs to check whether the subject has individual year levels or bands of year levels,
  // and return the appropriate year level or band based on previous selections
  function getCurrentYearLevel(): SubjectYearLevel | YearLevelBand | undefined {
    if (!subjects) {
      return undefined;
    }

    const subjectName = Object.keys(subjectStates).find((subjectName) => subjectStates[subjectName].isCurrentSubject)!;

    const subject = subjects.find((subject) => subject.name === subjectName)!;
    const yearLevel = subject.yearLevels.find((yearLevel) => yearLevel.name === subjectStates.currentYearLevel);

    return isBandLevel(yearLevel) ? (yearLevel as YearLevelBand) : (yearLevel as SubjectYearLevel);
  }

  function getCurrentStrand(): Strand | undefined {
    if (!currentYearLevel) {
      return;
    }

    const currentStrandName = subjectStates[currentSubject!.name].currentStrand;

    return currentStrandName ? currentYearLevel.strands.find((strand) => strand.name === currentStrandName) : currentYearLevel.strands[0];
  }

  function getSelectedContentDescriptionIds(currentSubjectName?: string): string[] {
    if (!currentSubject) {
      return [];
    }

    if (currentSubjectName) {
      return subjectStates[currentSubjectName].selectedContentDescriptionIds.map((ylcd) => ylcd[1]).flat();
    } else {
      return subjectStates[currentSubject!.name].selectedContentDescriptionIds.map((ylcd) => ylcd[1]).flat();
    }
  }
  // should check whether the clicked subject is already in the termSubjects array
  // if so, set the current subject to that subject
  // if not, add the subject to the termSubjects array and set the current subject to that subject
  function handleSubjectChange(subjectName: string): void {
    const subject = subjects?.find((subject) => subject.name === subjectName)!;

    const newSubjectStates = deepCopy(subjectStates);

    // if subject is Health and PE, then it has band levels. Need to get the bandLevelValue for the selected band level
    // also need to set the current year level to the band level equivalent
    // needs to set the current strand to the first one

    // if we're moving from a subject with band levels to a subject with year levels
    if (isBandLevel(currentYearLevel) && !isBandLevelArray(subject.yearLevels)) {
      const yearLevelName = determineYearLevel(subjectName, currentYearLevel.name);
      newSubjectStates.currentYearLevel = yearLevelName;
    } else if (!isBandLevel(currentYearLevel) && isBandLevelArray(subject.yearLevels)) {
      // if we're moving from a subject with year levels to a subject with band levels
      const currentYearLevelNumber = subjectStates.currentYearLevel.split(" ")[1];
      const bandLevel = determineBandLevel(currentYearLevelNumber);
      newSubjectStates.currentYearLevel = bandLevel;
    }

    for (const key in newSubjectStates) {
      // only iterate over the subject names. this will need to grow if we add more properties to the SubjectStateTable
      if (key !== "currentYearLevel") {
        newSubjectStates[key].isCurrentSubject = false;
      }
    }

    newSubjectStates[subjectName].isCurrentSubject = true;
    setSubjectStates(newSubjectStates);
  }

  function getYearLevels(): string[] {
    if (!currentSubject) {
      return [];
    }

    const subject = subjects?.find((subject) => subject.name === currentSubject!.name);

    return subject ? subject.yearLevels.map((yearLevel) => yearLevel.name) : [];
  }

  function handleYearLevelChange(yearLevelName: string): void {
    const newSubjectStates = deepCopy(subjectStates);
    newSubjectStates.currentYearLevel = yearLevelName;

    setSubjectStates(newSubjectStates);
  }

  function getStrands(): Strand[] {
    if (!currentYearLevel) {
      return [];
    }

    const strands: Strand[] = [];
    if (currentYearLevel!.strands) {
      currentYearLevel!.strands.forEach((strand) => strands.push(strand));
    }

    return strands;
  }

  function handleStrandChange(strandName: string): void {
    if (!currentYearLevel || !strandName) {
      return;
    }

    const strand = currentYearLevel!.strands.find((strand) => strand.name === strandName)!;

    const newSubjectStates = deepCopy(subjectStates);
    newSubjectStates[currentSubject!.name].currentStrand = strand.name;

    setSubjectStates(newSubjectStates);
  }

  function getContentDescriptions(): ContentDescription[] {
    if (!currentSubject || !currentYearLevel || !currentStrand) {
      return [];
    }

    const contentDescriptions: ContentDescription[] = [];
    if (currentStrand.substrands && currentStrand.substrands.length > 0) {
      currentStrand.substrands?.forEach((substrand) => {
        substrand.contentDescriptions?.forEach((contentDescription) => contentDescriptions.push(contentDescription));
      });
    } else {
      currentStrand.contentDescriptions?.forEach((contentDescription) => contentDescriptions.push(contentDescription));
    }

    return contentDescriptions;
  }

  function isSelectedContentDescription(contentDescription: ContentDescription): boolean {
    // find the current subject in state
    // find the contentdescriptions for that subject in the current year level
    // check if the contentdescription is in the array of selected content descriptions
    const selectedContentDescriptionIds = getSelectedContentDescriptionIds(currentSubject!.name);
    if (selectedContentDescriptionIds === undefined) {
      return false;
    }

    for (const cd of selectedContentDescriptionIds) {
      if (cd === contentDescription.curriculumCode) {
        return true;
      }
    }

    return false;
  }

  function handleContentDescriptionClick(contentDescription: ContentDescription): void {
    if (selectedContentDescriptionIds.length === 0) {
      setSubjectStates({
        ...subjectStates,
        [currentSubject!.name]: {
          ...subjectStates[currentSubject!.name],
          selectedContentDescriptionIds: subjectStates[currentSubject!.name].selectedContentDescriptionIds.map((ylcd) => {
            if (ylcd[0] === currentYearLevel!.name) {
              return [ylcd[0], [...ylcd[1], contentDescription.curriculumCode]];
            }
            return ylcd;
          }),
        },
      });
    }

    // for (const cd of selectedContentDescriptionIds) {
    if (selectedContentDescriptionIds.includes(contentDescription.curriculumCode)) {
      setSubjectStates({
        ...subjectStates,
        [currentSubject!.name]: {
          ...subjectStates[currentSubject!.name],
          selectedContentDescriptionIds: subjectStates[currentSubject!.name].selectedContentDescriptionIds.map((ylcd) => {
            if (ylcd[0] === currentYearLevel!.name) {
              return [ylcd[0], ylcd[1].filter((cd) => cd !== contentDescription.curriculumCode)];
            }
            return ylcd;
          }),
        },
      });
    } else {
      setSubjectStates({
        ...subjectStates,
        [currentSubject!.name]: {
          ...subjectStates[currentSubject!.name],
          selectedContentDescriptionIds: subjectStates[currentSubject!.name].selectedContentDescriptionIds.map((ylcd) => {
            if (ylcd[0] === currentYearLevel!.name) {
              return [ylcd[0], [...ylcd[1], contentDescription.curriculumCode]];
            }
            return ylcd;
          }),
        },
      });
    }
  }

  // this will build the list of subjects to add to the term planner
  // will return an array of subjects with the relevant year level, strand and content descriptions
  // TODO: work out why subjects with substrands are having their contentdescriptions added as null
  // TODO: work out why subjects with yearLevelbands aren't having their contentdescriptions added
  function handleAddContentDescriptions(): void {
    const termSubjects: Subject[] = [];

    for (const subjectName in subjectStates) {
      if (subjectName === "currentYearLevel") {
        continue;
      }

      if (subjectStates[subjectName].selectedContentDescriptionIds.every((ylcd) => ylcd[1].length === 0)) {
        continue;
      }

      const subjectToAdd = {
        name: subjectName,
        yearLevels: [] as SubjectYearLevel[],
      } as Subject;

      // for each subject in state, look at the year levels and for each array, iterate over the content descriptionIds and
      // find the corresponding Strand and build the object to add to the term planner
      const subject = subjects!.find((subject) => subject.name === subjectName);
      const ylcds = subjectStates[subjectName].selectedContentDescriptionIds; // ylcd === year level content description

      for (const ylcd of ylcds) {
        if (ylcd[1].length === 0) {
          continue;
        }
        const yearLevel = subject?.yearLevels.find((yearLevel) => yearLevel.name === ylcd[0]);

        if (yearLevel === undefined) {
          continue;
        }

        let yearLevelToAdd: SubjectYearLevel = {
          name: yearLevel.name,
          strands: [] as Strand[],
        };

        // for every content description in the array, find the strand and add it to the year level
        for (const contentDescriptionId of ylcd[1]) {
          const strand = yearLevel.strands.find((s) => {
            if (s.substrands && s.substrands.length > 0) {
              return s.substrands.find((ss) => ss.contentDescriptions?.find((cd) => cd.curriculumCode === contentDescriptionId));
            } else {
              return s.contentDescriptions?.find((cd) => cd.curriculumCode === contentDescriptionId);
            }
          });

          if (strand === undefined) {
            continue;
          }

          let strandToAdd: Strand;

          if (strand.substrands && strand.substrands.length > 0) {
            strandToAdd = {
              name: strand.name,
              substrands: [] as Substrand[],
            };

            // find the substrand that contains the content description and add it to the strand
            for (const substrand of strand.substrands) {
              // if this substrand has any content descriptions that match the content description id, add it to the strand
              const cdToAdd = substrand.contentDescriptions?.find((cd) => cd.curriculumCode === contentDescriptionId);
              if (cdToAdd) {
                // check if the substrand already exists in the strand
                // if not, create the substrand and add the content description to it
                strandToAdd.substrands!.push({
                  name: substrand.name,
                  contentDescriptions: [cdToAdd],
                });
              } else {
                continue;
              }

              // add the strand to the year level
              if (yearLevelToAdd.strands.find((s) => s.name === strandToAdd.name)) {
                // if the year level already has that strand, add the substrand to that strand
                yearLevelToAdd.strands.find((s) => s.name === strandToAdd.name)!.substrands!.push(strandToAdd.substrands![0]);
              } else {
                yearLevelToAdd.strands.push(strandToAdd);
              }
              // if we reach here, we've already found the content description and don't need to go over the other substrands.
              break;
            }
          } else {
            // the strand has only content descriptions, no substrands
            strandToAdd = {
              name: strand.name,
              contentDescriptions: [] as ContentDescription[],
            };

            for (const cd of strand.contentDescriptions!) {
              if (cd.curriculumCode === contentDescriptionId) {
                strandToAdd.contentDescriptions!.push(cd);
              }
            }

            // add the strand to the year level
            if (yearLevelToAdd.strands.find((s) => s.name === strandToAdd.name)) {
              yearLevelToAdd.strands.find((s) => s.name === strandToAdd.name)!.contentDescriptions!.push(strandToAdd.contentDescriptions![0]);
            } else {
              yearLevelToAdd.strands.push(strandToAdd);
            }
          }
        }

        subjectToAdd.yearLevels.push(yearLevelToAdd);
      }
      termSubjects.push(subjectToAdd);
    }
    console.log(JSON.stringify(termSubjects, null, 2));
    setSubjectsForTerm(termSubjects);
  }

  function handleCloseSearchBox(): void {
    setAddingContentDescription(false);
  }

  // called only when the previous subject has YearLevelBands as year levels and the new subject has SubjectYearLevels
  // tries to work out the most likely year level to select based on the number of content descriptions selected
  function determineYearLevel(subjectName: string, yearLevelName: string): string {
    if (yearLevelName === "Foundation") {
      return "Foundation";
    }

    const subject = subjectStates[subjectName];
    const maxCount = subject.selectedContentDescriptionIds.reduce((max, yl) => (yl[1].length > max ? yl[1].length : max), 0);

    if (maxCount === 0) {
      return `Year ${yearLevelName.split(" ")[1]}` === undefined ? "Foundation" : `Year ${yearLevelName.split(" ")[1]}`;
    }
    const yearLevel = subject.selectedContentDescriptionIds.find((yl) => yl[1].length === maxCount)!;

    const yearLevelNumber = yearLevel[0].split(" ")[1];

    return `Year ${yearLevelNumber}`; // if there are no content descriptions selected, return the lower of the 2 year levels in the band
  }

  function determineBandLevel(yearLevelName: string | undefined): string {
    if (yearLevelName === undefined) {
      return "Foundation";
    }

    return +yearLevelName % 2 === 0 ? `Years ${+yearLevelName - 1} To ${+yearLevelName}` : `Years ${+yearLevelName} To ${+yearLevelName + 1}`;
  }

  //#region type guards
  function isBandLevel(yearLevel: unknown): yearLevel is YearLevelBand {
    if (yearLevel === undefined) {
      return false;
    }
    return (yearLevel as YearLevelBand).bandLevelValue !== undefined && (yearLevel as YearLevelBand).bandLevelValue !== null;
  }

  function isBandLevelArray(yearLevels: unknown): yearLevels is YearLevelBand[] {
    if (yearLevels === undefined) {
      return false;
    }
    return Array.isArray(yearLevels) && (yearLevels as YearLevelBand[]).every((yl) => yl.bandLevelValue !== undefined && yl.bandLevelValue !== null);
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
            {/* List of strands for each subject */}
            <div className="w-1/5 h-full">
              <h5 className="text-lg text-center">Strands</h5>
              {currentYearLevel && (
                <ul className="">
                  {strands?.map((strand) => (
                    <li
                      key={strand.name}
                      className={`border border-darkGreen hover:bg-sageHover p-2 ${
                        strand.name === currentStrand?.name && "bg-sageFocus"
                      } select-none`}
                      onClick={() => handleStrandChange(strand.name)}>
                      {strand.name}
                    </li>
                  ))}
                </ul>
              )}
            </div>

            {/* List of content descriptions for each strand */}
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
