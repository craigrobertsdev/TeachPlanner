import { baseUrl } from "../utils/constants";

type TeacherResponse = {
  teacher: Teacher;
  token: string;
};

type TeacherSettings = {
  curriculumSubjects: Subject[];
  subjectsTaught: Subject[];
  students: Student[];
  calendarYear: number;
};

class TeacherService {
  async login(email: string, password: string) {
    const response = await fetch(`${baseUrl}/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      throw new Error(`Http request failed with status ${response.status}: ${response.statusText}`);
    }

    const data: TeacherResponse = await response.json();

    return data;
  }

  async register(email: string, firstName: string, lastName: string, password: string, confirmedPassword: string) {
    const body = JSON.stringify({ email, firstName, lastName, password, confirmedPassword });
    console.log(body);
    const response = await fetch(`${baseUrl}/auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body,
    });

    if (!response.ok) {
      throw new Error(`Http request failed with status ${response.status}: ${response.statusText}`);
    }

    const data: TeacherResponse = await response.json();

    return data;
  }

  async setSubjectsTaught(teacher: Teacher, token: string, subjectNames: string[]) {
    try {
      console.log(JSON.stringify({ teacherId: teacher.id, subjectNames }));
      const request = new Request(`${baseUrl}/teacher/${teacher.id}/subjects`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({ subjectNames }),
      });

      const response = await fetch(request);

      if (!response.ok) {
        throw new Error(`Http request failed with status ${response.status}: ${response.statusText}`);
      }
    } catch (error) {
      console.log(error);
    }
  }

  async getSettingsData(teacher: Teacher, calendarYear: number, token: string) {
    const request = new Request(`${baseUrl}/teacher/${teacher.id}/settings?calendarYear=${calendarYear}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    const response = await fetch(request);

    const data = await response.json();

    return data as TeacherSettings;
  }
}

export default new TeacherService();
