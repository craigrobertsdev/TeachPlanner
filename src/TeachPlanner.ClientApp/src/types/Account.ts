export type AccountDetails = {
  subjectsTaught: string[];
  dayPlanPattern: DayPlanPattern;
  termDates: TermDates[];
};

export type TermDates = {
  startDate: Date;
  endDate: Date;
};

export type DayPlanPattern = {
  lessons: LessonTemplate[];
  breaks: BreakTemplate[];
};

export type LessonTemplate = {
  startTime: PeriodTime;
  endTime: PeriodTime;
};

export type BreakTemplate = LessonTemplate & {
  name: string;
};

export type PeriodTime = {
  hour: number;
  minute: number;
  period: string;
};
