const lessonPlanSeed = [
  {
    numberOfPeriods: 2,
    planningNotes: "This is a test",
    subject: {
      name: "Maths",
    } as PlannerSubject,
    date: new Date(),
    periodNumber: 1,
    id: "1",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: "This is a test",
    subject: {
      name: "English",
    } as PlannerSubject,
    date: new Date(),
    periodNumber: 4,
    id: "2",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: "This is a test",
    subject: {
      name: "Health",
    } as PlannerSubject,
    date: new Date(),
    periodNumber: 7,
    id: "3",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: "This is a test",
    subject: {
      name: "NIT",
    } as PlannerSubject,
    date: new Date(),
    periodNumber: 8,
    id: "4",
  } as LessonPlan,
];

const lessonBreakSeed = [
  {
    startTime: new Date(2023, 8, 7, 10, 50, 0),
    endTime: new Date(2023, 8, 7, 11, 20, 0),
    periodNumber: 3,
    name: "Lunch",
    duty: "Yard Duty",
  },
  {
    startTime: new Date(2023, 8, 7, 13, 0, 0),
    endTime: new Date(2023, 8, 7, 13, 30, 0),
    periodNumber: 6,
    name: "Recess",
    duty: null,
  },
] as Break[];

const lessonEventSeed = [] as SchoolEvent[];

export const dayPlansSeed = [
  {
    startTime: new Date(2023, 7, 7, 9, 0, 0),
    endTime: new Date(2023, 7, 7, 15, 10, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed,
  },
  {
    startTime: new Date(2023, 7, 8, 9, 0, 0),
    endTime: new Date(2023, 7, 8, 15, 10, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed,
  },
  {
    startTime: new Date(2023, 7, 9, 9, 0, 0),
    endTime: new Date(2023, 7, 9, 15, 10, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed,
  },
  {
    startTime: new Date(2023, 7, 10, 9, 0, 0),
    endTime: new Date(2023, 7, 10, 15, 10, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed,
  },
  {
    startTime: new Date(2023, 7, 11, 9, 0, 0),
    endTime: new Date(2023, 7, 11, 15, 10, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed,
  },
] as DayPlan[];
