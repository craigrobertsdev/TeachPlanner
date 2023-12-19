import { baseUrl } from "../utils/constants";

class PlannerService {
	async getBlankLessonPlanData(teacher: Teacher, token: string, calendarYear: string): Promise<LessonPlanData>  {
		const abortController = new AbortController();

		const response = await fetch(`${baseUrl}/${teacher.id}/lesson-plans/data?calendarYear=${calendarYear}`, {
			headers: {
				Authorization: `Bearer ${token}`,
			},
			signal: abortController.signal,
		});

		const data: LessonPlanData = await response.json();
		return data;
	}

	async createNewLessonPlan(lessonPlan: LessonPlan, teacher: Teacher, token: string) {
		const abortController = new AbortController();

		const response = await fetch(`${baseUrl}/${teacher.id}/lesson-plans`, {
			method: "POST",
			headers: {
				Authorization: `Bearer ${token}`,
				"Content-Type": "application/json",
			},
			signal: abortController.signal,
			body: JSON.stringify(lessonPlan),
		});

		return await response.json();
	}
}

export default new PlannerService();

type LessonPlanData = {
	curriculumSubjects: Subject[];
	resources: Resource[];
}
