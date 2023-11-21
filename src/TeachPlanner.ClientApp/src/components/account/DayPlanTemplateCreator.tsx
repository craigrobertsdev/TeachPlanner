import React, { useEffect, useState } from "react";
import TimePicker from "../common/TimePicker";

type DayPlanTemplateCreatorProps = {
  breakTemplates: BreakTemplate[];
  lessonTemplates: LessonTemplate[];
  setBreakTemplates: React.Dispatch<React.SetStateAction<BreakTemplate[]>>;
  setLessonTemplates: React.Dispatch<React.SetStateAction<LessonTemplate[]>>;
};

type LessonTemplate = {
  startTime: PeriodTime;
  endTime: PeriodTime;
};

type BreakTemplate = LessonTemplate & {
  name: string;
};

type PeriodTime = {
  hours: number;
  minutes: number;
  period: string;
};

type LessonHeaderProps = {
  index: number;
  value: LessonTemplate;
  onChange: (value: LessonTemplate, index: number) => void;
};

type BreakHeaderProps = {
  index: number;
  value: BreakTemplate;
  onChange: (value: BreakTemplate, index: number) => void;
};

function DayPlanTemplateCreator({ breakTemplates, lessonTemplates, setBreakTemplates, setLessonTemplates }: DayPlanTemplateCreatorProps) {
  const [numberOfLessons, setNumberOfLessons] = useState<number>(6);
  const [numberOfBreaks, setNumberOfBreaks] = useState<number>(2);

  useEffect(() => {
    setLessonTemplates(generateInitialLessonTemplateValues());
    setBreakTemplates(generateInitialBreakTemplateValues());
  }, []);

  function generateInitialLessonTemplateValues() {
    const lessonValues = [
      {
        startTime: {
          hours: 9,
          minutes: 10,
          period: "AM",
        },
        endTime: {
          hours: 10,
          minutes: 0,
          period: "AM",
        },
      },
      {
        startTime: {
          hours: 10,
          minutes: 0,
          period: "AM",
        },
        endTime: {
          hours: 10,
          minutes: 50,
          period: "AM",
        },
      },
      {
        startTime: {
          hours: 11,
          minutes: 20,
          period: "AM",
        },
        endTime: {
          hours: 12,
          minutes: 10,
          period: "PM",
        },
      },
      {
        startTime: {
          hours: 12,
          minutes: 10,
          period: "PM",
        },
        endTime: {
          hours: 1,
          minutes: 0,
          period: "PM",
        },
      },
      {
        startTime: {
          hours: 1,
          minutes: 30,
          period: "PM",
        },
        endTime: {
          hours: 2,
          minutes: 20,
          period: "PM",
        },
      },
      {
        startTime: {
          hours: 2,
          minutes: 20,
          period: "PM",
        },
        endTime: {
          hours: 3,
          minutes: 10,
          period: "PM",
        },
      },
    ] as LessonTemplate[];

    return lessonValues;
  }

  function generateInitialBreakTemplateValues() {
    const breakValues = [
      {
        name: "Recess",
        startTime: {
          hours: 10,
          minutes: 50,
          period: "AM",
        },
        endTime: {
          hours: 11,
          minutes: 20,
          period: "AM",
        },
      },
      {
        name: "Lunch",
        startTime: {
          hours: 1,
          minutes: 0,
          period: "PM",
        },
        endTime: {
          hours: 1,
          minutes: 30,
          period: "PM",
        },
      },
    ] as BreakTemplate[];

    return breakValues;
  }

  function onLessonChange(value: LessonTemplate, index: number) {
    setLessonTemplates((lessonTemplates) => {
      const updatedLessonTemplates = [...lessonTemplates];
      updatedLessonTemplates[index] = value;
      return updatedLessonTemplates;
    });
  }

  function onBreakChange(value: BreakTemplate, index: number) {
    const updatedBreakTemplates = [...breakTemplates];
    updatedBreakTemplates[index] = value;
    setBreakTemplates(updatedBreakTemplates);
  }

  function onNumberOfLessonsChange(value: number) {
    if (lessonTemplates.length < value) {
      const updatedLessonTemplates = [...lessonTemplates];
      for (let i = lessonTemplates.length; i < value; i++) {
        updatedLessonTemplates.push({
          startTime: {
            hours: 10,
            minutes: 0,
            period: "AM",
          },
          endTime: {
            hours: 10,
            minutes: 0,
            period: "AM",
          },
        });
      }
      setLessonTemplates(updatedLessonTemplates);
    }

    if (value < lessonTemplates.length) {
      const updatedLessonTemplates = [...lessonTemplates];
      updatedLessonTemplates.splice(value, lessonTemplates.length - value);
      setLessonTemplates(updatedLessonTemplates);
    }

    setNumberOfLessons(value);
  }

  function onNumberOfBreaksChange(value: number) {
    if (breakTemplates.length < value) {
      const updatedBreakTemplates = [...breakTemplates];
      for (let i = breakTemplates.length; i < value; i++) {
        updatedBreakTemplates.push({
          name: "",
          startTime: {
            hours: 10,
            minutes: 0,
            period: "AM",
          },
          endTime: {
            hours: 10,
            minutes: 0,
            period: "AM",
          },
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

  return (
    <>
      <div className="flex flex-grow justify-center p-4">
        <div className="px-5">
          <h4 className="text-lg pb-2">How many lessons are in a day?</h4>
          <select
            className="w-full text-center p-1 text-lg font-semibold border border-darkGreen"
            value={numberOfLessons}
            onChange={(e) => onNumberOfLessonsChange(+e.target.value)}>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
          </select>
        </div>
        <div className="px-5">
          <h4 className="text-lg pb-2">How many breaks do you have a day?</h4>
          <select
            className="w-full text-center p-1 text-lg font-semibold border border-darkGreen"
            value={numberOfBreaks}
            onChange={(e) => onNumberOfBreaksChange(+e.target.value)}>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
          </select>
        </div>
      </div>
      <div className="flex-grow max-w-5xl m-auto flex-col gap-x-6 ">
        <div className="flex flex-grow justify-around w-full">
          <div>
            <h4 className="text-lg pb-2 font-bold">Lessons</h4>
            <div className="grid grid-cols-3 auto-rows-auto pb-2">
              {lessonTemplates.map((template, i) => (
                <TemplateLessonHeader key={`lesson-${i}`} value={template} onChange={(e) => onLessonChange(e, i)} index={i} />
              ))}
            </div>
          </div>
          <div>
            <h4 className="text-lg pb-2 font-bold">Breaks</h4>
            {breakTemplates.map((template, i) => (
              <div className="flex gap-1">
                <TemplateBreakHeader key={`break-${i}`} value={template} onChange={(e) => onBreakChange(e, i)} index={i} />
              </div>
            ))}
          </div>
        </div>
      </div>
    </>
  );
}

function TemplateLessonHeader({ index, value, onChange }: LessonHeaderProps) {
  function onStartTimeChange(hours: number, minutes: number, period: string) {
    value.startTime.hours = hours;
    value.startTime.minutes = minutes;
    value.startTime.period = period;

    onChange(value, index);
  }

  function onEndTimeChange(hours: number, minutes: number, period: string) {
    value.endTime.hours = hours;
    value.endTime.minutes = minutes;
    value.endTime.period = period;

    onChange(value, index);
  }

  return (
    <div
      id={`lesson-${index}`}
      className={`flex items-center justify-center border-2 m-1 bg-main border-darkGreen rounded-md text-center text-lg font-semibold p-2`}>
      <div>
        <h3>Lesson {index + 1}</h3>
        <div className="text-center flex flex-col items-center">
          <div className="p-2 flex flex-col">
            <p>Start Time:</p>
            <TimePicker value={value.startTime} setValue={onStartTimeChange} />
          </div>
          <div className="p-2 flex flex-col">
            <p>End Time:</p>
            <TimePicker value={value.endTime} setValue={onEndTimeChange} />
          </div>
        </div>
      </div>
    </div>
  );
}

function TemplateBreakHeader({ index, value, onChange }: BreakHeaderProps) {
  function onNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    value.name = e.target.value;
    onChange(value, index);
  }

  function onStartTimeChange(hours: number, minutes: number, period: string) {
    value.startTime.hours = hours;
    value.startTime.minutes = minutes;
    value.startTime.period = period;

    onChange(value, index);
  }

  function onEndTimeChange(hours: number, minutes: number, period: string) {
    value.endTime.hours = hours;
    value.endTime.minutes = minutes;
    value.endTime.period = period;

    onChange(value, index);
  }

  return (
    <div
      id={`break-${index}`}
      className={`flex items-center justify-center border-2 m-1 bg-main border-darkGreen rounded-md text-center text-lg font-semibold p-2`}>
      <div>
        <input className="text-center border rounded-md border-darkGreen" value={value.name} placeholder="Break name" onChange={onNameChange} />
        <div className="text-center flex flex-col items-center">
          <div className="flex flex-col p-2">
            <p>Start Time:</p>
            <TimePicker value={value.startTime} setValue={onStartTimeChange} />
          </div>
          <div className="p-2 flex flex-col">
            <p>End Time:</p>
            <TimePicker value={value.endTime} setValue={onEndTimeChange} />
          </div>
        </div>
      </div>
    </div>
  );
}
export default DayPlanTemplateCreator;
