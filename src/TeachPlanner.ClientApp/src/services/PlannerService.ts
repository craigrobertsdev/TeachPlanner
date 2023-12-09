import { baseUrl } from "../utils/constants";

class PlannerService {
    async getBlankLessonPlanData(teacher: Teacher, token: string, calendarYear: string) {
        const abortController = new AbortController();

        const response = await fetch(`${baseUrl}/${teacher.id}/lesson-plans/data?calendarYear=${calendarYear}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
            signal: abortController.signal,
        });

        const data = await response.json();
        console.log(JSON.stringify(data, null, 2));
        return data;
    }

}

export default new PlannerService();