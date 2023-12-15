export function getLessonStart(dayPlanTemplate: DayPlanTemplate, lessonNumber: number) {
	return dayPlanTemplate.pattern[lessonNumber].startTime;
}

export function getLessonEnd(dayPlanTemplate: DayPlanTemplate, lessonNumber: number) {
	return dayPlanTemplate.pattern[lessonNumber].endTime;
}
