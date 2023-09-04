import React from "react";

type WeekPlannerProps = {
  startDate: Date;
  weekNumber: number;
  termNumber: number;
  breaks: Break[];
  lessonPlans: LessonPlan[];
  events?: SchoolEvent[];
};

const WeekPlannerPage = () => {
  return (
    <div>
      <h1>Week Planner</h1>
    </div>
  );
};

export default WeekPlannerPage;
