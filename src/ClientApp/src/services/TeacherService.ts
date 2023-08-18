import { useLocalStorage } from "../hooks/useLocalStorage";
import { baseUrl } from "../utils/constants";

type TeacherResponse = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
};

export async function login(email: string, password: string): Promise<Teacher> {
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

  return data as Teacher;
}

export async function register(email: string, firstName: string, lastName: string, password: string): Promise<Teacher> {
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

  return data as Teacher;
}
