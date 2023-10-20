//#region Teacher
declare type Teacher = {
  id: string;
  firstName: string;
  lastName: string;
  lessonPlans: LessonPlan[];
  reports: Report[];
  resources: Resource[];
  studentsForYear: number & Student[];
  subjectsTaughtIds: Subject[];
  summativeAssessments: Assessment[];
  termPlanners: TermPlanner[];
  weekPlanners: WeekPlanner[];
};

//#region Student
declare type Student = {
  id: string;
  firstName: string;
  lastName: string;
};

//#region Planner
declare type DayPlanPattern = {
  pattern: CalendarHeader[];
};

declare type CalendarHeader = {
  startTime: Date;
  endTime: Date;
  type: "LessonPlan" | "Break";
};

declare type LessonPlan = {
  id: string;
  subject: PlannerSubject;
  planningNotes: string[];
  startTime: Date;
  endTime: Date;
  numberOfPeriods: number;
  periodNumber: number;
  resources: Resource[];
  assessments: Assessment[];
  lessonComments: LessonComment[];
  contentDescriptions: ContentDescription[];
};

declare type DayPlan = {
  startTime: Date;
  endTime: Date;
  numberOfPeriods: number;
  lessonPlans: LessonPlan[];
  events: SchoolEvent[];
  breaks: Break[];
};

declare type WeekPlanner = {
  lessonPlans: LessonPlan[];
  events: SchoolEvent[];
  weekNumber: number;
  weekStart: Date;
};

declare type Break = {
  startTime: Date;
  endTime: Date;
  periodNumber: number;
  name: string;
  duty: string;
};

declare type SchoolEvent = {
  startTime: Date;
  endTime: Date;
  name: string;
  description: string;
  location: string;
  numberOfPeriods: number;
  periodNumber: number;
};

declare type LessonComment = {
  id: string;
  content: string;
  date: Date;
};

declare type CalendarEntry = LessonPlan | Break | SchoolEvent;

declare type PlannerPeriod = {
  type: "lesson" | "break" | "afterschool";
  startTime: Date;
  endTime: Date;
};

//#region Curriculum
declare type Subject = {
  id: string;
  name: string;
  yearLevels: SubjectYearLevel[];
};

declare type SubjectYearLevel = {
  name: string;
  subject: Subject;
  strands: Strand[];
};

declare type Strand = {
  name: string;
  yearLevel: SubjectYearLevel;
  substrands?: Substrand[];
  contentDescriptions?: ContentDescription[];
};

declare type Substrand = {
  name: string;
  strand: Strand;
  contentDescriptions?: ContentDescription[];
};

declare type ContentDescription = {
  description: string;
  strand?: Strand;
  substrand?: Substrand;
  curriculumCode: string;
  elaborations: string[];
};

declare type Elaboration = {
  contentDescription: ContentDescription;
  description: string;
};

declare type Resource = {
  id: string;
  name: string;
  description: string;
  url: string;
};

type YearLevelBandNames = {
  foundation: "Foundation";
  years1To2: "Years1To2";
  years3To4: "Years3To4";
  years5To6: "Years5To6";
};

declare type YearLevelBand = SubjectYearLevel & {
  bandLevelValue: YearLevelBandNames[keyof YearLevelBandNames];
};

//#region Assessments
declare type Assessment = {
  id: string;
  name: string;
  description: string;
  student: Student;
  subjectName: string;
  url: string;
  dateConducted: Date;
} & (
  | {
      type: "summative";
      planningNotes?: string;
      dateScheduled: Date;
      grade: number | string;
    }
  | {
      type: "formative";
      comments: string;
    }
);

//#region TermPlanner
declare type TermPlanner = {
  calendarYear: number;
  termPlans: TermPlan[];
  yearLevels: YearLevelValue[];
};

declare type TermPlan = {
  termNumber: number;
  subjects: Subject[];
};

declare type DayPlanTemplate = {
  calendarEntries: Period[];
};

declare type Period = {
  type: "lesson" | "break" | "afterschool" | "beforeschool";
  startTime: Date;
  endTime: Date;
};
