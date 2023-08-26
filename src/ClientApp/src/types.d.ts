//#region Teacher
declare type Teacher = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
  subjectsTaught: Subject[];
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
  resources?: Resource[];
  assessments?: Assessment[];
  lessonComments?: LessonComment[];
  targetContentDescriptions?: ContentDescription[];
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

declare type PlannerSubject = {
  name: string;
};

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
  id: string;
  name: string;
  strands?: Strand[];
  substrands?: Substrand[];
};

declare type Strand = {
  name: string;
  substrands?: Substrand[];
  contentDescriptions?: ContentDescription[];
};

declare type Substrand = {
  name: string;
  contentDescriptions?: ContentDescription[];
};

declare type ContentDescription = {
  id: string;
  description: string;
  curriculumCode: string;
  elaborations: string[];
};

declare type Elaboration = {
  id: string;
  description: string;
};

declare type Resource = {
  id: string;
  name: string;
  description: string;
  url: string;
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
