import Button from "./Button";
import React, { useEffect, useState } from "react";

type DayPlanTemplateCreatorProps = {
  dayPlan: DayPlan;
  setDayPlan: React.Dispatch<React.SetStateAction<DayPlan>>;
};

type HeaderData = {
  name?: string;
  startTime: string;
  endTime: string;
};

type LessonTemplate = {
  startTime: string;
  endTime: string;
};

type BreakTemplate = {
  name: string;
  startTime: string;
  endTime: string;
};

type TemplateHeaderProps = {
  index: number;
  value: HeaderData;
  onChange: (value: HeaderData, index: number) => void;
};

function DayPlanTemplateCreator({ dayPlan, setDayPlan }: DayPlanTemplateCreatorProps) {
  const [numberOfLessons, setNumberOfLessons] = useState<number>(6);
  const [numberOfBreaks, setNumberOfBreaks] = useState<number>(2);
  const [lessonTemplates, setLessonTemplates] = useState<LessonTemplate[]>([]);
  const [breakTemplates, setBreakTemplates] = useState<BreakTemplate[]>([]);

  useEffect(() => {
    setLessonTemplates(generateInitialLessonTemplateValues());
    setBreakTemplates(generateInitialBreakTemplateValues());
  }, []);

  function generateInitialLessonTemplateValues() {
    const lessonValues = [
      {
        startTime: "09:10",
        endTime: "10:00",
      },
      {
        startTime: "10:00",
        endTime: "10:50",
      },
      {
        startTime: "11:20",
        endTime: "12:10",
      },
      {
        startTime: "12:10",
        endTime: "13:00",
      },
      {
        startTime: "13:30",
        endTime: "14:20",
      },
      {
        startTime: "14:20",
        endTime: "15:10",
      },
    ] as LessonTemplate[];

    return lessonValues;
  }

  function generateInitialBreakTemplateValues() {
    const breakValues = [
      {
        name: "Recess",
        startTime: "10:50",
        endTime: "11:20",
      },
      {
        name: "Lunch",
        startTime: "13:00",
        endTime: "13:30",
      },
    ] as BreakTemplate[];

    return breakValues;
  }

  function onValueChange(value: HeaderData, index: number) {
    if (isBreakTemplate(value)) {
      console.log("here");
      setBreakTemplates((breakTemplates) => {
        console.log(breakTemplates);
        const updatedBreakTemplates = [...breakTemplates];
        updatedBreakTemplates[index] = value;
        return updatedBreakTemplates;
      });

      return;
    }

    setLessonTemplates((lessonTemplates) => {
      const updatedLessonTemplates = [...lessonTemplates];
      updatedLessonTemplates[index] = value;
      return updatedLessonTemplates;
    });
  }

  function onNumberOfLessonsChange(value: number) {
    if (value > lessonTemplates.length) {
      const updatedLessonTemplates = [...lessonTemplates];
      for (let i = lessonTemplates.length; i < value; i++) {
        updatedLessonTemplates.push({
          startTime: "00:00",
          endTime: "00:00",
        });
      }
      setLessonTemplates(updatedLessonTemplates);
    }

    if (value < lessonTemplates.length) {
      const updatedLessonTemplates = [...lessonTemplates];
      updatedLessonTemplates.splice(value, lessonTemplates.length - value);
      setLessonTemplates(updatedLessonTemplates);
    }
  }

  function onNumberOfBreaksChange(value: number) {
    if (breakTemplates.length < value) {
      const updatedBreakTemplates = [...breakTemplates];
      for (let i = breakTemplates.length; i < value; i++) {
        updatedBreakTemplates.push({
          name: "",
          startTime: "00:00",
          endTime: "00:00",
        });
      }
      setBreakTemplates(updatedBreakTemplates);
    }

    if (value < breakTemplates.length) {
      const updatedBreakTemplates = [...breakTemplates];
      updatedBreakTemplates.splice(value, breakTemplates.length - value);
      setBreakTemplates(updatedBreakTemplates);
    }
    setNumberOfBreaks(value);
  }

  function isBreakTemplate(value: HeaderData): value is BreakTemplate {
    return (value as BreakTemplate).name !== undefined;
  }

  return (
    <>
      <div className="flex flex-grow justify-center p-4">
        <div className="px-5">
          <h5 className="text-lg pb-2">How many lessons are in a day?</h5>
          <select
            className="w-full text-center border border-darkGreen"
            value={numberOfLessons}
            onChange={(e) => onNumberOfLessonsChange(+e.target.value)}>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
          </select>
        </div>
        <div className="px-5">
          <h5 className="text-lg pb-2">How many breaks do you have a day?</h5>
          <select
            className="w-full text-center border border-darkGreen"
            value={numberOfBreaks}
            onChange={(e) => onNumberOfBreaksChange(+e.target.value)}>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
          </select>
        </div>
      </div>
      <div className="pl-5 pb-2 my-auto">
        {/* <Button variant="submit" onClick={generateTemplateHeaders}>
          Generate Template
        </Button> */}
      </div>
      <div className="flex-grow max-w-5xl m-auto flex-col gap-x-6 ">
        <div className="flex flex-grow justify-between w-full">
          <div>
            <h5 className="text-lg pb-2">Lessons</h5>
            <div className="grid grid-cols-3 auto-rows-auto pb-2">
              {lessonTemplates.map((template, i) => (
                <TemplateLessonHeader key={`lesson${i}`} value={template} onChange={() => onValueChange(template, i)} index={i} />
              ))}
            </div>
          </div>
          <div>
            <h5 className="text-lg pb-2">Breaks</h5>
            {breakTemplates.map((template, i) => (
              <TemplateBreakHeader key={`break${i}`} value={template} onChange={(e) => onValueChange(e, i)} index={i} />
            ))}
          </div>
        </div>
      </div>
      <Button variant="submit" onClick={() => setDayPlan({ lessonTemplates, breakTemplates })}>
        Save
      </Button>
    </>
  );

  function TemplateLessonHeader({ index, value, onChange }: TemplateHeaderProps) {
    return (
      <div id={`lesson-${index}`} className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold p-2`}>
        <div>
          <h3>Lesson {index + 1}</h3>
          <div className="flex gap-y-5">
            <div className="p-2">
              <p>Start Time</p>
              <input
                id="startTime"
                className="text-center"
                type="time"
                value={value.startTime}
                onChange={(e) => onChange({ ...value, startTime: e.target.value }, index)}
              />
            </div>
            <div className="p-2">
              <p>End Time</p>
              <input
                id="endTime"
                className="text-center"
                type="time"
                value={value.endTime}
                onChange={(e) => onChange({ ...value, endTime: e.target.value }, index)}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }

  function TemplateBreakHeader({ index, value, onChange }: TemplateHeaderProps) {
    return (
      <div id={`break-${index}`} className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold`}>
        <div>
          <input
            className="text-center"
            value={value.name}
            placeholder="Break name"
            onChange={(e) => onChange({ ...value, name: e.target.value }, index)}
          />
          <div className="flex gap-y-5">
            <div className="p-2">
              <p>Start Time</p>
              <input
                id="startTime"
                className="text-center"
                type="time"
                value={value.startTime}
                onChange={(e) => onChange({ ...value, startTime: e.target.value }, index)}
              />
            </div>
            <div className="p-2">
              <p>End Time</p>
              <input
                id="endTime"
                className="text-center"
                type="time"
                value={value.endTime}
                onChange={(e) => onChange({ ...value, endTime: e.target.value }, index)}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
export default DayPlanTemplateCreator;
