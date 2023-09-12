import { baseUrl } from "../utils/constants";

type TeacherResponse = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
  subjectsTaught: Subject[];
};

type SubjectsTaughtResponse = {
  subjectsTaught: Subject[];
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

  async register(email: string, firstName: string, lastName: string, password: string) {
    const response = await fetch(`${baseUrl}/auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, firstName, lastName, password }),
    });

    if (!response.ok) {
      throw new Error(`Http request failed with status ${response.status}: ${response.statusText}`);
    }

    const data: TeacherResponse = await response.json();

    return data;
  }

  async setSubjectsTaught(teacher: Teacher, subjectNames: string[]) {
    try {
      console.log(JSON.stringify({ teacherId: teacher.id, subjectNames }));
      const request = new Request(`${baseUrl}/teacher/${teacher.id}/subjects`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${teacher!.token}`,
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
}

export default new TeacherService();
