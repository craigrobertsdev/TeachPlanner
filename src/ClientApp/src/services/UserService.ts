import { useLocalStorage } from "../hooks/useLocalStorage";
import { baseUrl } from "../utils/constants";

type UserResponse = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  token: string;
};

export async function login(email: string, password: string): Promise<User> {
  const response = await fetch(`${baseUrl}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    throw new Error(
      `Http request failed with status ${response.status}: ${response.statusText}`
    );
  }

  const data: UserResponse = await response.json();

  return data as User;
}

export async function register(
  email: string,
  firstName: string,
  lastName: string,
  password: string
): Promise<User> {
  const response = await fetch(`${baseUrl}/auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, firstName, lastName, password }),
  });

  if (!response.ok) {
    throw new Error(
      `Http request failed with status ${response.status}: ${response.statusText}`
    );
  }

  const data: UserResponse = await response.json();

  return data as User;
}

// export function isAuthenticated(): User | null {
//   const user = getUser("user");
//   if (!user) {
//     return null;
//   }

//   return JSON.parse(user);
// }
