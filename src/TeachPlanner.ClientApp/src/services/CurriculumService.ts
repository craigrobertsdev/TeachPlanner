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
  async getSubjects(
    { elaborations = false, taughtSubjectsOnly = false }: CurriculumVariables,
    teacher: Teacher,
    token: string,
    controller: AbortController
  ) {
    const request = new Request(
      `${baseUrl}/${teacher.id}/subjects/curriculum/?includeElaborations=${elaborations}&taughtSubjectsOnly=${taughtSubjectsOnly}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        signal: controller.signal,
      }
    );

    const response = await fetch(request);

    const data = await response.json();

    return data as Subject[];
  }

  async getTermPlannerSubjects({ year = new Date().getFullYear(), elaborations = true }: CurriculumVariables, teacher: Teacher, token: string) {
    const response = await fetch(`${baseUrl}/curriculum/${teacher.id}/term-planner?year=${year}&elaborations=${elaborations}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    const data = await response.json();

    return data as SubjectResponse;
  }

  async saveTermSubjects(subjects: TermPlan[]) {
    throw new Error("Not implemented");
  }

  async getSubjectNames(teacher: Teacher, token: string) {
    const request = new Request(`${baseUrl}/${teacher.id}/curriculum/subject-names`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    const response = await fetch(request);
    const data = await response.json();
    return data as { subjectNames: string[] };
  }
}

export default new CurriculumService();
