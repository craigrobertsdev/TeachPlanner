export const dayPlanPatternSeed: DayPlanTemplate = {
  pattern: [
    {
      startTime: "09:10:00",
      endTime: "10:00:00",
      periodType: "Lesson",
    },
    {
      startTime: "10:00:00",
      endTime: "10:50:00",
      periodType: "Lesson",
    },
    {
      startTime: "10:50:00",
      endTime: "11:20:00",
      periodType: "Break",
    },
    {
      startTime: "11:20:00",
      endTime: "12:10:00",
      periodType: "Lesson",
    },
    {
      startTime: "12:10:00",
      endTime: "13:00:00",
      periodType: "Lesson",
    },
    {
      startTime: "13:00:00",
      endTime: "13:30:00",
      periodType: "Break",
    },
    {
      startTime: "13:30:00",
      endTime: "14:20:00",
      periodType: "Lesson",
    },
    {
      startTime: "14:20:00",
      endTime: "15:10:00",
      periodType: "Lesson",
    },
  ],
};

const lessonPlanSeed1 = [
  {
    numberOfPeriods: 2,
    planningNotes: ["Exploring rounding to the nearest 10 and 100"],
    resources: [
      {
        name: "Rounding to the nearest 10 and 100",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
      {
        name: "Quadratics",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "Maths",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 9, 10, 0),
    endTime: new Date(2023, 8, 7, 10, 50, 0),
    periodNumber: 1,
    id: "1",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: [
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
    ],
    resources: [
      {
        name: "Prose Fiction",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "English",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 11, 20, 0),
    endTime: new Date(2023, 8, 7, 12, 10, 0),
    periodNumber: 4,
    id: "2",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "Health",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 13, 30, 0),
    endTime: new Date(2023, 8, 7, 14, 20, 0),
    periodNumber: 7,
    id: "3",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "NIT",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 14, 20, 0),
    endTime: new Date(2023, 8, 7, 15, 10, 0),
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

const lessonPlanSeed2 = [
  {
    numberOfPeriods: 2,
    planningNotes: ["Exploring rounding to the nearest 10 and 100"],
    resources: [
      {
        name: "Rounding to the nearest 10 and 100",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
      {
        name: "Quadratics",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "Maths",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 9, 10, 0),
    endTime: new Date(2023, 8, 7, 10, 50, 0),
    periodNumber: 1,
    id: "5",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: [
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
    ],
    resources: [
      {
        name: "Prose Fiction",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "English",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 11, 20, 0),
    endTime: new Date(2023, 8, 7, 12, 10, 0),
    periodNumber: 4,
    id: "6",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "Health",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 13, 30, 0),
    endTime: new Date(2023, 8, 7, 14, 20, 0),
    periodNumber: 7,
    id: "7",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "NIT",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 14, 20, 0),
    endTime: new Date(2023, 8, 7, 15, 10, 0),
    periodNumber: 8,
    id: "8",
  } as LessonPlan,
];

const lessonPlanSeed3 = [
  {
    numberOfPeriods: 2,
    planningNotes: ["Exploring rounding to the nearest 10 and 100"],
    resources: [
      {
        name: "Rounding to the nearest 10 and 100",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
      {
        name: "Quadratics",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "Maths",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 9, 10, 0),
    endTime: new Date(2023, 8, 7, 10, 50, 0),
    periodNumber: 1,
    id: "9",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: [
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
    ],
    resources: [
      {
        name: "Prose Fiction",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "English",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 11, 20, 0),
    endTime: new Date(2023, 8, 7, 12, 10, 0),
    periodNumber: 4,
    id: "10",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "Health",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 13, 30, 0),
    endTime: new Date(2023, 8, 7, 14, 20, 0),
    periodNumber: 7,
    id: "11",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "NIT",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 14, 20, 0),
    endTime: new Date(2023, 8, 7, 15, 10, 0),
    periodNumber: 8,
    id: "12",
  } as LessonPlan,
];
const lessonPlanSeed4 = [
  {
    numberOfPeriods: 2,
    planningNotes: ["Exploring rounding to the nearest 10 and 100"],
    resources: [
      {
        name: "Rounding to the nearest 10 and 100",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
      {
        name: "Quadratics",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "Maths",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 9, 10, 0),
    endTime: new Date(2023, 8, 7, 10, 50, 0),
    periodNumber: 1,
    id: "13",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: [
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
    ],
    resources: [
      {
        name: "Prose Fiction",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "English",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 11, 20, 0),
    endTime: new Date(2023, 8, 7, 12, 10, 0),
    periodNumber: 4,
    id: "14",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "Health",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 13, 30, 0),
    endTime: new Date(2023, 8, 7, 14, 20, 0),
    periodNumber: 7,
    id: "15",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "NIT",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 14, 20, 0),
    endTime: new Date(2023, 8, 7, 15, 10, 0),
    periodNumber: 8,
    id: "16",
  } as LessonPlan,
];
const lessonPlanSeed5 = [
  {
    numberOfPeriods: 2,
    planningNotes: ["Exploring rounding to the nearest 10 and 100"],
    resources: [
      {
        name: "Rounding to the nearest 10 and 100",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
      {
        name: "Quadratics",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "Maths",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 9, 10, 0),
    endTime: new Date(2023, 8, 7, 10, 50, 0),
    periodNumber: 1,
    id: "17",
  } as LessonPlan,
  {
    numberOfPeriods: 2,
    planningNotes: [
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
      "Spelling warm up, then reading comprehension",
      "Work on narratives",
    ],
    resources: [
      {
        name: "Prose Fiction",
        url: "https://www.youtube.com/watch?v=9zXqjxu6J6g",
      },
    ] as Resource[],
    subject: {
      name: "English",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 11, 20, 0),
    endTime: new Date(2023, 8, 7, 12, 10, 0),
    periodNumber: 4,
    id: "18",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "Health",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 13, 30, 0),
    endTime: new Date(2023, 8, 7, 14, 20, 0),
    periodNumber: 7,
    id: "19",
  } as LessonPlan,
  {
    numberOfPeriods: 1,
    planningNotes: ["This is a test"],
    subject: {
      name: "NIT",
    } as PlannerSubject,
    startTime: new Date(2023, 8, 7, 14, 20, 0),
    endTime: new Date(2023, 8, 7, 15, 10, 0),
    periodNumber: 8,
    id: "20",
  } as LessonPlan,
];

export const dayPlansSeed = [
  {
    startTime: new Date(2023, 7, 7, 9, 0, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed1,
  },
  {
    startTime: new Date(2023, 7, 8, 9, 0, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed2,
  },
  {
    startTime: new Date(2023, 7, 9, 9, 0, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed3,
  },
  {
    startTime: new Date(2023, 7, 10, 9, 0, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed4,
  },
  {
    startTime: new Date(2023, 7, 11, 9, 0, 0),
    breaks: lessonBreakSeed,
    events: [] as SchoolEvent[],
    lessonPlans: lessonPlanSeed5,
  },
] as DayPlan[];
