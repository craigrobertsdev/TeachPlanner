import { useEffect, useState } from "react";
import Button from "../../components/common/Button";
import ContentDescriptionSearchBox from "../../components/planner/ContentDescriptionSearchBox";
import useAuth from "../../contexts/AuthContext";
import { baseUrl } from "../../utils/constants";

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

type SubjectState = {
  currentYearLevel: string;
  currentTopic: string;
  isCurrentSubject: boolean;
  selectedContentDescriptionIds: [string, string[]][];
};

type SubjectStateTable = {
  [key: string]: SubjectState;
};

const TermPlannerPage = () => {
  const [subjectsForTerm, setSubjectsForTerm] = useState<Subject[]>([]);
  const [addingContentDescription, setAddingContentDescription] = useState<boolean>(true);
  const [subjectData, setSubjectData] = useState<Subject[] | undefined>(undefined);

  function handleAddContentDescription() {
    setAddingContentDescription(true);
  }
  const [subjectStates, setSubjectStates] = useState<SubjectStateTable>({});
  const { teacher } = useAuth();

  useEffect(() => {
    if (subjectData === undefined) {
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
    const subjectStateTable: SubjectStateTable = {};

    subjects.forEach((subject, index) => {
      const yearLevelName = subject.yearLevels[0].name;
      const topicName =
        subject.yearLevels[0].strands?.length! > 0 ? subject.yearLevels[0].strands![0].name : subject.yearLevels[0].substrands![0].name;
      subjectStateTable[subject.name] = {
        currentYearLevel: yearLevelName,
        currentTopic: topicName,
        isCurrentSubject: index === 0 ? true : false,
        selectedContentDescriptionIds: createContentDescriptionIdsArray(subject.yearLevels),
      };
    });

    setSubjectStates(subjectStateTable);
  }

  function getCurrentSubject(): Subject | undefined {
    for (const key in subjectStates) {
      if (subjectStates[key].isCurrentSubject) {
        return subjectData?.find((subject) => subject.name === key);
      }
    }

    return undefined;
  }

  function getCurrentYearLevel(): SubjectYearLevel | YearLevelBand | undefined {
    if (!subjectData) {
      return undefined;
    }

    const subjectName = Object.keys(subjectStates).find((subjectName) => subjectStates[subjectName].isCurrentSubject)!;
    const yearLevelName = subjectStates[subjectName].currentYearLevel;

    const subject = subjectData.find((subject) => subject.name === subjectName)!;
    const yearLevel = subject.yearLevels.find((yearLevel) => yearLevel.name === yearLevelName);

    return isBandLevel(yearLevel) ? (yearLevel as YearLevelBand) : (yearLevel as SubjectYearLevel);
  }

  function isBandLevel(yearLevel: unknown): yearLevel is YearLevelBand {
    if (yearLevel === undefined) {
      return false;
    }
    return (yearLevel as YearLevelBand).bandLevelValue !== undefined && (yearLevel as YearLevelBand).bandLevelValue !== null;
  }

  function createContentDescriptionIdsArray(yearLevels: SubjectYearLevel[] | YearLevelBand[]): [string, string[]][] {
    const contentDescriptionIds: [string, string[]][] = [];

    yearLevels.forEach((yearLevel) => {
      contentDescriptionIds.push([yearLevel.name, []]);
    });

    return contentDescriptionIds;
  }

  console.log(subjectStates);

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
      {/* {addingContentDescription && (
        <ContentDescriptionSearchBox
          setAddingContentDescription={setAddingContentDescription}
          subjects={subjectData}
          setSubjectData={setSubjectData}
          setSubjectsForTerm={setSubjectsForTerm}
        />
      )} */}
    </div>
  );
};

export default TermPlannerPage;
