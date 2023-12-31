export type AccountDetails = {
  subjectsTaught: string[];
  yearLevelsTaught: string[];
  dayPlanPattern: DayPlanPattern;
};

export type TermDates = {
  startDate: Date;
  endDate: Date;
};

export type DayPlanPattern = {
  lessonTemplates: LessonTemplate[];
  breakTemplates: BreakTemplate[];
};

export type LessonTemplate = {
  startTime: PeriodTime;
  endTime: PeriodTime;
};

export type BreakTemplate = LessonTemplate & {
  name: string;
};

export type PeriodTime = {
  hours: number;
  minutes: number;
  period: string;
};
