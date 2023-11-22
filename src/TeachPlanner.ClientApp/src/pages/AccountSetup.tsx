import { useEffect, useState } from "react";
import useAuth from "../contexts/AuthContext";
import curriculumService from "../services/CurriculumService";
import DayPlanTemplateCreator from "../components/account/DayPlanTemplateCreator";
import Button from "../components/common/Button";
import ValidationError from "../components/common/ValidationError";
import TeacherService from "../services/TeacherService";
import { AccountDetails, BreakTemplate, DayPlanPattern, LessonTemplate } from "../types/Account";
import { useNavigate } from "react-router-dom";
import YearLevelPicker from "../components/account/YearLevelPicker";

function AccountSetup() {
  const { teacher, token } = useAuth();
  const [plannerYear, setPlannerYear] = useState(new Date().getFullYear());
  const [subjectsTaught, setSubjectsTaught] = useState<string[]>([]);
  const [subjects, setSubjects] = useState<string[]>([]);
  const [yearLevelsTaught, setYearLevelsTaught] = useState<string[]>([]);
  const [lessonTemplates, setLessonTemplates] = useState<LessonTemplate[]>([]);
  const [breakTemplates, setBreakTemplates] = useState<BreakTemplate[]>([]);
  const [validationErrors, setValidationErrors] = useState<string[]>([]);
  const navigate = useNavigate();
  const calendarYears = [new Date().getFullYear(), new Date().getFullYear() + 1];

  useEffect(() => {
    async function getSubjects() {
      const subjectData = await curriculumService.getSubjectNames(teacher!, token!);
      setSubjects(subjectData.subjectNames);
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

  function handleYearLevelChange(event: React.ChangeEvent<HTMLSelectElement>) {
    setPlannerYear(+event.target.value);
  }

  async function setupAccount() {
    clearValidationErrors();
    validateSubjectsTaught();
    validatePeriodTimes();
    validateYearLevelsTaught();

    const dayPlanPattern: DayPlanPattern = {
      lessonTemplates,
      breakTemplates,
    };

    if (validationErrors.length > 0) {
      return;
    }

    const accountDetails: AccountDetails = {
      subjectsTaught,
      yearLevelsTaught,
      dayPlanPattern,
    };

    try {
      console.log(accountDetails);
      const response = await TeacherService.setupAccount(accountDetails, plannerYear, teacher!, token!);
      console.log(response);
      navigate("/teacher/week-planner", { replace: true });
    } catch (error) {
      setValidationErrors((prev) => [...prev, "Something went wrong, please try again."]);
    }
  }

  function clearValidationErrors() {
    setValidationErrors(() => []);
  }

  function validateSubjectsTaught() {
    if (subjectsTaught.length === 0) {
      setValidationErrors((prev) => [...prev, "You must select at least one subject"]);
    }
  }

  function validatePeriodTimes() {
    lessonTemplates.forEach((lessonTemplate, index) => {
      if (periodsOverlap(lessonTemplate, index)) {
        setValidationErrors((prev) => [...prev, `Lesson times overlap between periods ${index + 1} and ${index + 2}`]);
      }
    });
  }

  function validateYearLevelsTaught() {
    if (yearLevelsTaught.length === 0) {
      setValidationErrors((prev) => [...prev, "You must select at least one year level"]);
    }
  }

  function periodsOverlap(lessonTemplate: LessonTemplate, index: number) {
    if (index >= lessonTemplates.length - 1) {
      return false;
    }

    const prevTime = lessonTemplates[index + 1].startTime;
    const nextTime = lessonTemplate.endTime;

    if (prevTime.period === "PM" && nextTime.period === "AM") {
      return false;
    }

    if (prevTime.period === "AM" && nextTime.period === "PM") {
      return true;
    }

    return prevTime.hours < nextTime.hours || (prevTime.hours === nextTime.hours && prevTime.minutes < nextTime.minutes);
  }

  return (
    <div className="w-full px-4 flex flex-col items-center text-center">
      <h1 className="text-3xl p-4">Welcome {teacher.firstName}!</h1>
      <h2 className="text-2xl">To get you started, we need to set up a few things first</h2>
      <br />
      <br />
      <div className="w-full mb-2 p-2 pb-4 border-b border-darkGreen">
        <h3 className="text-xl pb-2">What year are you setting your planner up for?</h3>
        <select className="text-center p-1 text-lg font-semibold border border-darkGreen" onChange={handleYearLevelChange}>
          <option value={calendarYears[0]}>{calendarYears[0]}</option>
          <option value={calendarYears[1]}>{calendarYears[1]}</option>
        </select>
      </div>
      <div className="w-full flex justify-around border-b border-darkGreen">
        <div className="w-1/3 ml-auto p-2 pb-4">
          <h3 className="text-xl pb-2">What subjects do you teach?</h3>
          <div className="flex flex-col bg-main border border-darkGreen rounded-md">
            {subjects.map((subject, idx) => (
              <p
                key={subject}
                className={`hover:bg-primaryHover hover:cursor-default ${
                  isSelected(subject) && "bg-primaryFocus outline outline-1 outline-primaryFocusBorder"
                } ${idx === 0 && "rounded-t-md"} ${idx === subjects.length - 1 && "rounded-b-md"} text-lg`}
                onClick={() => handleSubjectChange(subject)}>
                {subject}
              </p>
            ))}
          </div>
        </div>
        <div className="w-1/3 mr-auto flex-col items-center p-2 pb-4">
          <YearLevelPicker yearLevelsTaught={yearLevelsTaught} setYearLevelsTaught={setYearLevelsTaught} />
        </div>
        di
      </div>
      <br />
      <br />
      <div className="w-full flex-col items-center mb-2 p-2 pb-4 border-b border-darkGreen">
        <h3 className="text-xl pb-2">How is your day structured?</h3>
        <p className="text-lg">For example, how many lessons do you have a day, how many breaks, and what times do they start and finish?</p>
        <DayPlanTemplateCreator
          breakTemplates={breakTemplates}
          lessonTemplates={lessonTemplates}
          setBreakTemplates={setBreakTemplates}
          setLessonTemplates={setLessonTemplates}
        />
      </div>
      <ValidationError errors={validationErrors} />
      <Button onClick={setupAccount} variant="submit" classList="mb-2">
        Complete Account Setup
      </Button>
    </div>
  );
}

export default AccountSetup;
