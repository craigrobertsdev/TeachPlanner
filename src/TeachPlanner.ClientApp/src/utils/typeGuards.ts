export function isStringArray(value: unknown): value is string[] {
  return Array.isArray(value) && value.every((item) => typeof item === "string");
}

export function isString(value: unknown): value is string {
  return typeof value === "string";
}

export function isLessonPlan(entry: CalendarEntry): entry is LessonPlan {
  return (entry as LessonPlan).subject !== undefined;
}

export function isSchoolEvent(entry: CalendarEntry): entry is SchoolEvent {
  return (entry as SchoolEvent).location !== undefined;
}

export function isBreak(entry: CalendarEntry): entry is Break {
  return !isLessonPlan(entry) && !isSchoolEvent(entry);
}

export function isBandLevel(yearLevel: unknown): yearLevel is YearLevelBand {
  if (yearLevel === undefined) {
    return false;
  }
  return (yearLevel as YearLevelBand).bandLevelValue !== undefined && (yearLevel as YearLevelBand).bandLevelValue !== null;
}

export function isSubjectYearLevel(yearLevel: unknown): yearLevel is SubjectYearLevel {
  if (yearLevel === undefined) {
    return false;
  }

  return (yearLevel as SubjectYearLevel).name !== undefined && !(yearLevel as YearLevelBand).bandLevelValue;
}
