import React from "react";

type WeekPlannerProps = {
  startDate: Date;
  weekNumber: number;
  termNumber: number;
  breaks: Break[];
  lessonPlans: LessonPlan[];
  events?: SchoolEvent[];
};

const WeekPlanner = () => {
  return (
    <div>
      <h1>Week Planner</h1>
    </div>
  );
};

export default WeekPlanner;
