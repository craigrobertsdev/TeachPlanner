import { baseUrl } from "../utils/constants";

type CurriculumVariables = {
  elaborations?: boolean;
  year?: number;
};

type SubjectResponse = {
  subjects: Subject[];
};

class CurriculumService {
  async getSubjectData({ elaborations = false }: CurriculumVariables, teacher: Teacher, controller: AbortController) {
    const response = await fetch(`${baseUrl}/curriculum?elaborations=${elaborations}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${teacher!.token}`,
      },
      signal: controller.signal,
    });

    const data = await response.json();

    return data as SubjectResponse;
  }

  async getTermPlannerSubjects({ year = new Date().getFullYear(), elaborations = true }: CurriculumVariables, teacher: Teacher) {
    const response = await fetch(`${baseUrl}/curriculum/term-planner?year=${year}&elaborations=${elaborations}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${teacher!.token}`,
      },
    });

    const data = await response.json();

    return data as SubjectResponse;
  }

  async saveTermSubjects(subjects: Subject[]) {}
}

export default new CurriculumService();
