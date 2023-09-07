import { baseUrl } from "../utils/constants";

type CurriculumVariables = {
  elaborations?: boolean;
  year?: number;
  taughtSubjectsOnly?: boolean;
};

type SubjectResponse = {
  subjects: Subject[];
};

class CurriculumService {
  async getSubjects({ elaborations = false, taughtSubjectsOnly = false }: CurriculumVariables, teacher: Teacher, controller: AbortController) {
    const request = new Request(
      `${baseUrl}/curriculum/?teacherId=${teacher.id}&elaborations=${elaborations}&taughtSubjectsOnly=${taughtSubjectsOnly}`,
      {
        headers: {
          Authorization: `Bearer ${teacher!.token}`,
        },
        signal: controller.signal,
      }
    );

    const response = await fetch(request);

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

  async saveTermSubjects(subjects: Term[]) {
    return subjects;
  }
}

export default new CurriculumService();
