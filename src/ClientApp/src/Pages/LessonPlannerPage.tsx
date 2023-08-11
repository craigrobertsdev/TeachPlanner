import React from "react";
import useAuth from "../contexts/AuthContext";
import { getDayName } from "../utils/dateUtils";

type LessonPlannerProps = {
  numLessons: number;
  numBreaks: number;
  lessonLength: number; // in mintues
  weekNumber: number;
  dayPlans: DayPlan[];
};

const LessonPlannerPage = ({
  numLessons,
  numBreaks,
  lessonLength,
  weekNumber,
  dayPlans,
}: LessonPlannerProps) => {
  const { user } = useAuth();
  // TODO: get these values from the database
  numLessons = 7;
  numBreaks = 2;
  lessonLength = 30;
  weekNumber = 1;
  dayPlans = [
    {
      date: new Date(),
      lessonPlans: [
        {
          numberOfPeriods: 1,
          planningNotes: "This is a test",
          subject: {
            name: "Maths",
          } as PlannerSubject,
          startTime: new Date(2023, 8, 7, 9, 0, 0),
          endTime: new Date(2023, 8, 7, 9, 30, 0),
          id: "1",
        } as LessonPlan,
      ],
      events: [],
    },
  ];

  const numRows = numLessons + numBreaks + 2; // +2 for header row and after school
  return (
    <>
      <h1 className="text-4xl text-center p-3 mb-3">Hi {user!.firstName}!</h1>
      <div
        className={`grid grid-cols-6 grid-rows-${numRows} border border-darkGreen`}>
        <div className="row-start-1 border border-darkGreen">
          Week {weekNumber}
        </div>
        <div className="row-start-2 border border-darkGreen">Lesson 1</div>
        <div className="row-start-3 border border-darkGreen">Lesson 2</div>
        <div className="row-start-4 border border-darkGreen">Break 1</div>
        <div className="row-start-5 border border-darkGreen">Lesson 3</div>
        <div className="row-start-6 border border-darkGreen">Lesson 4</div>
        <div className="row-start-[7] border border-darkGreen">Break 2</div>
        <div className="row-start-[8] border border-darkGreen">Lesson 5</div>
        <div className="row-start-[9] border border-darkGreen">Lesson 6</div>
        <div className="row-start-[10] border border-darkGreen">Lesson 7</div>
        <div className="row-start-[11] border border-darkGreen">
          After School
        </div>
      </div>
      {dayPlans.map((dayPlan) => {
        <div className="col-start-2 border border-darkGreen">
          {getDayName(dayPlan.date)}
        </div>;
      })}
    </>
  );
};

export default LessonPlannerPage;
