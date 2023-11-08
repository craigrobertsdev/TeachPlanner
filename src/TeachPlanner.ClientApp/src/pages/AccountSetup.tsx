import { useEffect, useState } from "react";
import useAuth from "../contexts/AuthContext";
import curriculumService from "../services/CurriculumService";
import DayPlanTemplateCreator from "../components/common/DayPlanTemplateCreator";

function AccountSetup() {
  const { teacher, token } = useAuth();
  const [subjectsTaught, setSubjectsTaught] = useState<string[]>([]);
  const [subjects, setSubjects] = useState<string[]>([]);
  const [dayPlan, setDayPlan] = useState<DayPlan>({} as DayPlan);

  useEffect(() => {
    async function getSubjects() {
      const abortController = new AbortController();
      const subjectData = await curriculumService.getSubjects({}, teacher!, token!, abortController);
      setSubjects(subjectData.map((subject) => subject.name));
    }

    getSubjects();
  }, []);

  function handleSubjectChange(subjectName: string) {
    if (subjectsTaught.includes(subjectName)) {
      setSubjectsTaught(subjectsTaught.filter((subject) => subject !== subjectName));
      return;
    }

    setSubjectsTaught([...subjectsTaught, subjectName]);
  }

  function isSelected(subjectName: string) {
    return subjectsTaught.includes(subjectName);
  }

  if (!teacher) {
    return <p>loading...</p>;
  }

  return (
    <div className="w-full px-4 text-center">
      <h1 className="text-3xl p-4">Welcome {teacher.firstName}!</h1>
      <h2 className="text-2xl">To get you started, we need to set up a few things first</h2>
      <br />
      <br />
      <div className="w-full flex justify-center p-2 pb-4 border-b border-darkGreen">
        <div className="w-2/5">
          <h3 className="text-xl pb-2">What subjects do you teach?</h3>
          {/* <Dropdown placeholder="Select all subjects you teach" onChange={handleSubjectChange} options={subjects} multiSelect /> */}
          <div className="border border-darkGreen p-2">
            {subjects.map((subject) => (
              <p
                key={subject}
                className={`hover:bg-primaryHover ${isSelected(subject) && "bg-primaryFocus"} m-1`}
                onClick={() => handleSubjectChange(subject)}>
                {subject}
              </p>
            ))}
          </div>
        </div>
      </div>
      <br />
      <br />
      <div className="w-full flex-col items-center  p-2 pb-4 border-b border-darkGreen">
        <h3 className="text-xl pb-2">How is your day structured?</h3>
        <p className="text-lg">For example, how many lessons do you have a day, how many breaks, and what times do they start and finish?</p>
        <DayPlanTemplateCreator dayPlan={dayPlan} setDayPlan={setDayPlan} />
      </div>
    </div>
  );
}

export default AccountSetup;
