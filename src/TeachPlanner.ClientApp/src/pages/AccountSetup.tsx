import { useEffect, useState } from "react";
import useAuth from "../contexts/AuthContext";
import curriculumService from "../services/CurriculumService";
import DayPlanTemplateCreator from "../components/common/DayPlanTemplateCreator";
import Button from "../components/common/Button";
import TermDatesCreator from "../components/common/TermDatesCreator";
import ValidationError from "../components/common/ValidationError";

type DayPlanPattern = {
  lessons: LessonTemplate[];
  breaks: BreakTemplate[];
  beforeSchool: PeriodTime;
  afterSchool: PeriodTime;
};

type LessonTemplate = {
  startTime: PeriodTime;
  endTime: PeriodTime;
};

type BreakTemplate = LessonTemplate & {
  name: string;
};

type PeriodTime = {
  hour: number;
  minute: number;
  period: string;
};

type TermDates = {
  startDate: Date;
  endDate: Date;
};

function AccountSetup() {
  const { teacher, token } = useAuth();
  const [subjectsTaught, setSubjectsTaught] = useState<string[]>([]);
  const [subjects, setSubjects] = useState<string[]>([]);
  const [termDates, setTermDates] = useState<TermDates[]>(
    Array.from({ length: 4 }, () => ({ startDate: new Date(), endDate: new Date() }) as TermDates)
  ); // [startDate, endDate]
  const [lessonTemplates, setLessonTemplates] = useState<LessonTemplate[]>([]);
  const [breakTemplates, setBreakTemplates] = useState<BreakTemplate[]>([]);
  const [validationErrors, setValidationErrors] = useState<string[]>([]);

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

  function setupAccount() {
    clearValidationErrors();
    /* need to create objects that the server understands
      periodType: "lesson" | "break" | "beforeSchool" | "afterSchool"
      name: string
      startTime: DateTime
      endTime: DateTime
    */

    const dayPlanPattern: DayPlanPattern = {
      lessons: [],
      breaks: [],
      beforeSchool: { hour: 8, minute: 0, period: "AM" },
      afterSchool: { hour: 3, minute: 0, period: "PM" },
    };
    validatePeriodTimes();
  }

  function clearValidationErrors() {
    setValidationErrors((prev) => []);
  }

  function validatePeriodTimes() {
    lessonTemplates.forEach((lessonTemplate, index) => {
      if (periodsOverlap(lessonTemplate, index)) {
        setValidationErrors((prev) => [...prev, `Lesson times overlap between periods ${index + 1} and ${index + 2}`]);
      }
    });
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

    return prevTime.hour < nextTime.hour || (prevTime.hour === nextTime.hour && prevTime.minute < nextTime.minute);
  }

  return (
    <div className="w-full px-4 text-center">
      <h1 className="text-3xl p-4">Welcome {teacher.firstName}!</h1>
      <h2 className="text-2xl">To get you started, we need to set up a few things first</h2>
      <br />
      <br />
      <div className="w-full flex justify-center p-2 pb-4 border-b border-darkGreen">
        <div className="">
          <h3 className="text-xl pb-2">What subjects do you teach?</h3>
          {/* <Dropdown placeholder="Select all subjects you teach" onChange={handleSubjectChange} options={subjects} multiSelect /> */}
          <div className="border border-darkGreen rounded-md p-2">
            {subjects.map((subject) => (
              <p
                key={subject}
                className={`hover:bg-primaryHover hover:cursor-default ${isSelected(subject) && "bg-primaryFocus"} text-lg my-1 px-2`}
                onClick={() => handleSubjectChange(subject)}>
                {subject}
              </p>
            ))}
          </div>
        </div>
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
      <div className="w-1/2 m-auto flex-col items-center mb-2 p-2 pb-4">
        <TermDatesCreator termDates={termDates} setTermDates={setTermDates} />
      </div>
      <ValidationError errors={validationErrors} />
      <Button onClick={setupAccount} variant="submit" classList="mb-2">
        Complete Account Setup
      </Button>
    </div>
  );
}

export default AccountSetup;
