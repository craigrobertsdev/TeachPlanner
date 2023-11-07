import Button from "./Button";
import React, { useEffect, useState } from "react";
import TimePicker from "./TimePicker";

type DayPlanTemplateCreatorProps = {
  dayPlan: DayPlan;
  setDayPlan: React.Dispatch<React.SetStateAction<DayPlan>>;
};

type LessonTemplate = {
  startHour: number;
  startMinute: number;
  endHour: number;
  endMinute: number;
};

type BreakTemplate = {
  name: string;
  startHour: number;
  startMinute: number;
  endHour: number;
  endMinute: number;
};

type LessonHeaderProps = {
  index: number;
  value: LessonTemplate;
  onChange: (value: LessonTemplate | BreakTemplate, index: number) => void;
};

type BreakHeaderProps = {
  index: number;
  value: BreakTemplate;
  onChange: (value: BreakTemplate, index: number) => void;
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
        startHour: 9,
        startMinute: 10,
        endHour: 10,
        endMinute: 0,
      },
      {
        startHour: 10,
        startMinute: 0,
        endHour: 10,
        endMinute: 50,
      },
      {
        startHour: 11,
        startMinute: 20,
        endHour: 12,
        endMinute: 10,
      },
      {
        startHour: 12,
        startMinute: 10,
        endHour: 13,
        endMinute: 0,
      },
      {
        startHour: 13,
        startMinute: 30,
        endHour: 14,
        endMinute: 20,
      },
      {
        startHour: 14,
        startMinute: 20,
        endHour: 15,
        endMinute: 10,
      },
    ] as LessonTemplate[];

    return lessonValues;
  }

  function generateInitialBreakTemplateValues() {
    const breakValues = [
      {
        name: "Recess",
        startHour: 10,
        startMinute: 50,
        endHour: 11,
        endMinute: 20,
      },
      {
        name: "Lunch",
        startHour: 13,
        startMinute: 0,
        endHour: 13,
        endMinute: 30,
      },
    ] as BreakTemplate[];

    return breakValues;
  }

  function onValueChange(value: LessonTemplate | BreakTemplate, index: number) {
    console.log("here");
    console.log(value);
    if (isBreakTemplate(value)) {
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
          startHour: 0,
          startMinute: 0,
          endHour: 0,
          endMinute: 0,
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
          startHour: 0,
          startMinute: 0,
          endHour: 0,
          endMinute: 0,
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

  function isBreakTemplate(value: LessonTemplate | BreakTemplate): value is BreakTemplate {
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
                <TemplateLessonHeader key={`lesson${i}`} value={template} onChange={(e) => onValueChange(e, i)} index={i} />
              ))}
            </div>
          </div>
          <div>
            <h5 className="text-lg pb-2">Breaks</h5>
            {breakTemplates.map((template, i) => (
              <TemplateBreakHeader key={`break${i}`} value={template} onChange={onValueChange} index={i} />
            ))}
          </div>
        </div>
      </div>
      <Button variant="submit" onClick={() => setDayPlan({ lessonTemplates, breakTemplates })}>
        Save
      </Button>
    </>
  );

  function TemplateLessonHeader({ index, value, onChange }: LessonHeaderProps) {
    function onStartTimeChange(hours: string, minutes: string, period: string) {
      if (period == "AM" && +hours == 12) {
        value.startHour = 0;
      } else if (period == "PM") {
        value.startHour = +hours + 12;
      } else {
        value.startHour = +hours;
      }
      value.startMinute = +minutes;
      onChange(value, index);
    }

    function onEndTimeChange(hours: string, minutes: string, period: string) {
      if (period == "AM" && +hours == 12) {
        value.startHour = 0;
      } else if (period == "PM") {
        value.startHour = +hours + 12;
      } else {
        value.startHour = +hours;
      }
      value.startMinute = +minutes;
      console.log(value);
      onChange(value, index);
    }

    return (
      <div id={`lesson-${index}`} className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold p-2`}>
        <div>
          <h3>Lesson {index + 1}</h3>
          <div className="flex flex-col">
            <div className="p-2 flex justify-between">
              <p>Start Time:</p>
              <TimePicker onChange={onStartTimeChange} />
            </div>
            <div className="flex p-2 justify-between">
              <p>End Time:</p>
              <TimePicker onChange={onEndTimeChange} />
            </div>
          </div>
        </div>
      </div>
    );
  }

  function TemplateBreakHeader({ index, value, onChange }: BreakHeaderProps) {
    function onNameChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
      onChange({ ...value, name: e.target.value }, index);
    }

    function onStartTimeChange(hours: string, minutes: string, period: string) {
      if (period == "AM" && +hours == 12) {
        value.startHour = 0;
      } else if (period == "PM") {
        value.startHour = +hours + 12;
      } else {
        value.startHour = +hours;
      }
      value.startMinute = +minutes;

      onChange(value, index);
    }

    function onEndTimeChange(hours: string, minutes: string, period: string) {
      if (period == "AM" && +hours == 12) {
        value.startHour = 0;
      } else if (period == "PM") {
        value.startHour = +hours + 12;
      } else {
        value.startHour = +hours;
      }
      value.startMinute = +minutes;
      console.log(value);
      onChange(value, index);
    }

    return (
      <div
        key={`break-${index}`}
        id={`break-${index}`}
        className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold`}>
        <div>
          <input className="text-center" value={value.name} placeholder="Break name" onChange={(e) => onNameChange(e, index)} />
          <div className="flex gap-y-5">
            <div className="p-2">
              <p>Start Time:</p>
              <TimePicker onChange={onStartTimeChange} />
            </div>
            <div className="p-2">
              <p>End Time:</p>
              <TimePicker onChange={onEndTimeChange} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
export default DayPlanTemplateCreator;
