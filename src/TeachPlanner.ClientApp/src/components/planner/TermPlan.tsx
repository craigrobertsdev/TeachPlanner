import Button from "../common/Button";
import { useEffect } from "react";

type TermPlanProps = {
  termNumber: number;
  terms: TermPlan[];
  yearLevel: string; // Year 1 || Year 1 & 2
  handleAddContentDescriptions: (termNumber: number) => void;
};

function TermPlan({ termNumber, yearLevel, terms, handleAddContentDescriptions }: TermPlanProps) {
  let subjects: Subject[] = [];

  function getContentDescriptionsForSubject(subject: Subject) {
    const contentDescriptions: ContentDescription[] = [];
    if (subject.yearLevels[0]?.strands[0].substrands?.length! > 0) {
      subject.yearLevels.forEach((yearLevel) => {
        yearLevel.strands.forEach((strand) => {
          strand.substrands?.forEach((substrand) => {
            substrand.contentDescriptions?.forEach((contentDescription) => {
              contentDescriptions.push(contentDescription);
            });
          });
        });
      });
    } else {
      subject.yearLevels.forEach((yearLevel) => {
        yearLevel.strands.forEach((strand) => {
          strand.contentDescriptions?.forEach((contentDescription) => {
            contentDescriptions.push(contentDescription);
          });
        });
      });
    }
    return contentDescriptions;
  }

  function teachesMultipleYearLevels(yearLevel: string) {
    return yearLevel.includes("&"); // year levels are defined as "Year 1 & 2" or "Year 1"
  }

  function getSubjects() {
    return terms[termNumber - 1].subjects ?? ([] as Subject[]);
  }

  return (
    <>
      {/* need to handle the following cases when dividing content descriptions:
        - the teacher only teaches 1 year level
        - the teacher teaches 2 year levels
          - subject have year levels and year level bands
      */}
      <div className="p-0">
        <div className="flex relative items-center justify-center bg-peach">
          <h4 className="ml-2 text-2xl">Term {termNumber}</h4>
          <Button variant="add" classList="ml-auto m-2" onClick={() => handleAddContentDescriptions(termNumber)}>
            Add Content Descriptions
          </Button>
        </div>
      </div>
      {subjects.length > 0 ? (
        subjects.map((subject, idx) => (
          <div className={`border border-darkGreen ${idx % 2 === 0 && "bg-sage"} `}>
            <div className={`border-r border-darkGreen ${idx !== subjects.length - 1 && "border-b"}`}>{subject.name}</div>
            {!teachesMultipleYearLevels(yearLevel) ? ( // if the teacher only teaches 1 year level
              <>
                {subject.yearLevels.length > 0 ? ( // if the subjects array has entries
                  subject.yearLevels[0].strands.map((strand) => (
                    <div className="border-r border-darkGreen min-w-[15rem]">
                      <div className="flex flex-col p-1">
                        <h4 className="font-semibold underline text-center">{strand.name}</h4>
                        <ul className="px-4">
                          {strand.substrands && strand.substrands.length > 0
                            ? strand.substrands.map(
                                (substrand) =>
                                  substrand.contentDescriptions &&
                                  substrand.contentDescriptions.map((cd) => (
                                    <li key={cd.curriculumCode} className="mb-4 line list-disc">
                                      {cd.description}
                                    </li>
                                  ))
                              )
                            : strand.contentDescriptions &&
                              strand.contentDescriptions.map((cd) => (
                                <li key={cd.curriculumCode} className="mb-4 line list-disc">
                                  {cd.description}
                                </li>
                              ))}
                        </ul>
                      </div>
                    </div>
                  ))
                ) : (
                  <div>
                    <div className="flex flex-col p-1">
                      <h4 className="font-semibold text-center ">No content descriptions added</h4>
                    </div>
                  </div>
                )}
              </>
            ) : (
              // if the teacher has multiple year levels to teach
              <div className={`border-r border-darkGreen ${idx !== subjects.length - 1 && "border-b"}`}>
                {subject.yearLevels.map((yearLevel) => yearLevel.name).join(", ")}
              </div>
            )}
          </div>
        ))
      ) : (
        <div className="min-h-[20%]">No subjects added</div>
      )}
    </>
  );
}

export default TermPlan;
