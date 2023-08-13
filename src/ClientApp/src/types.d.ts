//#region User
declare type User = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
};

//#region Planner
declare type LessonPlan = {
  id: string;
  subject: PlannerSubject;
  planningNotes: string;
  date: Date;
  numberOfPeriods: number;
  periodNumber: number;
  resources?: Resource[];
  summativeAssessments?: SummativeAssessment[];
  formativeAssessments?: FormativeAssessment[];
  lessonComments?: LessonComment[];
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

declare type PlannerSubject = {
  name: string;
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
  strands: Strand[];
};

declare type Strand = {
  id: string;
  name: string;
  substrands?: Substrand[];
  contentDescriptions?: ContentDescription[];
};

declare type Substrand = {
  id: string;
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
  description: string;
  link: string;
};

//#region Assessments
declare type Assessment = {
  id: string;
  description: string;
  student: Student;
  subjectName: string;
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
