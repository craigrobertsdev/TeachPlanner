import { useEffect, useState } from "react";
import ContentDescriptionSearchBox from "../../components/planner/ContentDescriptionSearchBox";
import curriculumService from "../../services/CurriculumService";
import useAuth from "../../contexts/AuthContext";
import TermPlan from "../../components/planner/TermPlan";

// TODO: Add ability to send term plans via email
const TermPlanner = () => {
  const [terms, setTerms] = useState<TermPlan[]>([]);
  const [addingContentDescriptions, setAddingContentDescriptions] = useState<boolean>(false);
  const [subjectData, setSubjectData] = useState<Subject[] | undefined>(undefined);
  const [year, setYear] = useState<number>(new Date().getFullYear());
  const [currentTerm, setCurrentTerm] = useState<number>(getCurrentTerm());
  const { teacher, token } = useAuth();

  useEffect(() => {
    const getSubjectsTaught = async () => {
      const abortController = new AbortController();
      try {
        const response = await curriculumService.getSubjects({ taughtSubjectsOnly: true }, teacher!, token!, abortController);
        setSubjectData(response.subjects);
        setInitialTerms(response.subjects);
      } catch (error) {
        console.log(error);
      }
    };

    getSubjectsTaught();
  }, []);

  function setInitialTerms(subjects: Subject[]) {
    const initialTerms: TermPlan[] = [];

    for (let i = 0; i < 4; i++) {
      initialTerms.push({ termNumber: i + 1, subjects: subjects });
    }

    setTerms(initialTerms);
  }

  function getCurrentTerm() {
    // current term to be obtained from the server
    return 1;
  }

  useEffect(() => {
    if (subjectData) {
      terms.forEach((term) => {
        term.subjects = [...subjectData];
      });
    }

    setTerms([...terms]);
  }, [subjectData]);

  // TODO: this useEffect needs to update some list of contentDescriptions that are available to add to the term planner,
  useEffect(() => {}, [terms]);

  function handleAddContentDescriptions(termNumber: number) {
    setCurrentTerm(termNumber);
    setAddingContentDescriptions(true);
  }

  // useEffect(() => {
  //   if (terms !== undefined && terms.length > 0) {
  //     const newTerms = [...terms];
  //     const termWithMostSubjects = terms[terms.findIndex((term) => term.subjects.length === Math.max(...terms.map((term) => term.subjects.length)))];

  //     newTerms.forEach((term) => {
  //       if (term.termNumber !== termWithMostSubjects.termNumber && term.subjects.length !== termWithMostSubjects.subjects.length) {
  //         termWithMostSubjects.subjects.forEach((subject) => {
  //           if (!term.subjects.includes(subject)) {
  //             term.subjects.push({ name: subject.name, yearLevels: [] as SubjectYearLevel[] } as Subject);
  //           }
  //         });
  //       }
  //     });
  //     setTerms(newTerms);
  //   }
  // }, [addingContentDescriptions]);

  return (
    <div className="flex flex-col flex-grow">
      {/* Content Descriptions */}
      <div className="flex flex-col items-center w-full flex-grow p-2">
        <div className="flex-grow w-full p-2 flex flex-col">
          <div className="flex flex-col flex-grow w-full overflow-hidden">
            <div className="grid grid-cols-1 auto-cols-auto">
              {terms !== undefined && terms.length > 0 ? (
                terms.map((term) => (
                  <TermPlan
                    key={"Term" + term.termNumber}
                    termNumber={term.termNumber}
                    terms={terms}
                    yearLevel={"Foundation"}
                    handleAddContentDescriptions={handleAddContentDescriptions}
                  />
                ))
              ) : (
                <>
                  <TermPlan termNumber={1} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={2} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={3} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={4} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                </>
              )}
            </div>
            {/* <table className="border border-darkGreen flex-grow w-full mb-2">
              {terms !== undefined && terms.length > 0 ? (
                <tbody>
                  {terms.map((term) => (
                    <TermPlan
                      termNumber={term.termNumber}
                      terms={terms}
                      yearLevel={"Foundation"}
                      handleAddContentDescriptions={handleAddContentDescriptions}
                    />
                  ))}
                </tbody>
              ) : (
                // render empty term planner if no subjects have been added
                <>
                  <TermPlan termNumber={1} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={2} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={3} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                  <TermPlan termNumber={4} terms={[]} yearLevel={"Year 1"} handleAddContentDescriptions={handleAddContentDescriptions} />
                </>
              )}
            </table> */}
          </div>
        </div>
      </div>
      {/* Content description search box */}

      {addingContentDescriptions && (
        <ContentDescriptionSearchBox
          setAddingContentDescription={setAddingContentDescriptions}
          subjects={subjectData}
          setSubjectData={setSubjectData}
          termSubjects={terms ? terms[currentTerm - 1]?.subjects : undefined}
          termNumber={currentTerm}
          setTermSubjects={setTerms}
        />
      )}
    </div>
  );
};

export default TermPlanner;
