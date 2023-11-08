import Button from "./Button";
import React, { useEffect, useState } from "react";
import TimePicker from "./TimePicker";

type DayPlanTemplateCreatorProps = {
  dayPlan: DayPlan;
  setDayPlan: React.Dispatch<React.SetStateAction<DayPlan>>;
};

type LessonTemplate = {
  startTime: PeriodTime;
  endTime: PeriodTime;
};

type BreakTemplate = {
  name: string;
  startTime: PeriodTime;
  endTime: PeriodTime;
};

type PeriodTime = {
  hour: number;
  minute: number;
  period: string;
};

type LessonHeaderProps = {
  index: number;
  value: LessonTemplate;
  onChange: (value: LessonTemplate, index: number, isStartTime: boolean) => void;
};

type BreakHeaderProps = {
  index: number;
  value: BreakTemplate;
  onChange: (value: BreakTemplate, index: number, isStartTime: boolean) => void;
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
        startTime: {
          hour: 9,
          minute: 10,
          period: "AM",
        },
        endTime: {
          hour: 10,
          minute: 0,
          period: "AM",
        },
      },
      {
        startTime: {
          hour: 10,
          minute: 0,
          period: "AM",
        },
        endTime: {
          hour: 10,
          minute: 50,
          period: "AM",
        },
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
        startTime: {
          hour: 10,
          minute: 50,
          period: "AM",
        },
        endTime: {
          hour: 11,
          minute: 20,
          period: "AM",
        },
      },
      {
        name: "Lunch",
        startTime: {
          hour: 13,
          minute: 0,
          period: "PM",
        },
        endTime: {
          hour: 13,
          minute: 30,
          period: "PM",
        },
      },
    ] as BreakTemplate[];

    return breakValues;
  }

  function onLessonChange(value: LessonTemplate | BreakTemplate, index: number) {
    setLessonTemplates((lessonTemplates) => {
      const updatedLessonTemplates = [...lessonTemplates];
      updatedLessonTemplates[index] = value;
      return updatedLessonTemplates;
    });
  }

  function onBreakChange(value: BreakTemplate, index: number, isStartTime: boolean) {
    console.log(value);
    const updatedBreakTemplates = [...breakTemplates];
    isStartTime ? (updatedBreakTemplates[index].startTime = value.startTime) : (updatedBreakTemplates[index].endTime = value.endTime);
    setBreakTemplates(updatedBreakTemplates);
  }

  function onNumberOfLessonsChange(value: number) {
    if (value > lessonTemplates.length) {
      const updatedLessonTemplates = [...lessonTemplates];
      for (let i = lessonTemplates.length; i < value; i++) {
        // updatedLessonTemplates.push({
        //   startHour: 0,
        //   startMinute: 0,
        //   endHour: 0,
        //   endMinute: 0,
        // });
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
        // updatedBreakTemplates.push({
        //   name: "",
        //   startHour: 0,
        //   startMinute: 0,
        //   endHour: 0,
        //   endMinute: 0,
        // });
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
      <div className="flex-grow max-w-5xl m-auto flex-col gap-x-6 ">
        <div className="flex flex-grow justify-between w-full">
          <div>
            <h5 className="text-lg pb-2">Lessons</h5>
            <div className="grid grid-cols-3 auto-rows-auto pb-2">
              {/* {lessonTemplates.map((template, i) => (
                <TemplateLessonHeader key={`lesson${i}`} value={template} onChange={(e) => onLessonChange(e, i)} index={i} />
              ))} */}
            </div>
          </div>
          <div>
            <h5 className="text-lg pb-2">Breaks</h5>
            {breakTemplates.map((template, i) => (
              <TemplateBreakHeader key={`break${i}`} value={template} onChange={onBreakChange} index={i} />
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
    function onStartTimeChange(hours: number, minutes: number, period: string) {
      let newStartHour = hours;
      if (period === "PM" && hours < 12) {
        newStartHour = hours + 12;
      } else if (period === "AM" && hours === 12) {
        newStartHour = 0;
      } else if (period === "AM" && hours > 12) {
        newStartHour = hours - 12;
      }

      const updatedValue = { ...value, startHour: newStartHour, startMinute: minutes };
      onChange(updatedValue, index, true);
    }

    function onEndTimeChange(hours: number, minutes: number, period: string) {
      let newEndHour = hours;
      if (period === "PM" && hours < 12) {
        newEndHour = hours + 12;
      } else if (period === "AM" && hours === 12) {
        newEndHour = 0;
      } else if (period === "AM" && hours > 12) {
        newEndHour = hours - 12;
      }

      const updatedValue = { ...value, endTime: { hour: newEndHour, minute: minutes } } as BreakTemplate;
      onChange(updatedValue, index, false);
    }

    function determinePeriod(hour: number) {
      if (hour < 12) {
        return "AM";
      }
      return "PM";
    }

    return (
      <div id={`lesson-${index}`} className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold p-2`}>
        <div>
          <h3>Lesson {index + 1}</h3>
          <div className="flex flex-col">
            <div className="p-2 flex justify-between">
              <p>Start Time:</p>
              <TimePicker value={value.startTime} setValue={onStartTimeChange} />
            </div>
            <div className="flex p-2 justify-between">
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
      onChange(
        {
          ...value,
          name: e.target.value,
        },
        index,
        true
      );
    }

    function onStartTimeChange(hours: number, minutes: number, period: string) {
      value.startTime.hour = hours;
      value.startTime.minute = minutes;
      value.startTime.period = period;

      onChange(value, index, true);
    }

    function onEndTimeChange(hours: number, minutes: number, period: string) {
      value.endTime.hour = hours;
      value.endTime.minute = minutes;
      value.endTime.period = period;

      onChange(value, index, false);
    }

    return (
      <div id={`break-${index}`} className={`flex items-center justify-center border-2 m-1 border-darkGreen text-center text-lg font-semibold`}>
        <div>
          <input className="text-center" value={value.name} placeholder="Break name" onChange={onNameChange} />
          <div className="flex gap-y-5">
            <div className="p-2">
              <p>Start Time:</p>
              <TimePicker value={value.startTime} setValue={onStartTimeChange} />
            </div>
            <div className="p-2">
              <p>End Time:</p>
              <TimePicker value={value.endTime} setValue={onEndTimeChange} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
export default DayPlanTemplateCreator;
