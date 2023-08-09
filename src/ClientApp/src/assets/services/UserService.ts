import { useLocalStorage } from "../../hooks/useLocalStorage";

type UserResponse = {
  user: User;
  token: string;
};

export async function login(
  email: string,
  password: string
): Promise<User | null> {
  const { setItem } = useLocalStorage();
  const response = await fetch("http://localhost:5291/api/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  const data: UserResponse = await response.json();

  const token = data.token;
  const user = data.user;

  if (token) {
    user.token = token;
    setItem("token", JSON.stringify(token));
    return user;
  }

  return null;
}

export async function register(
  email: string,
  firstName: string,
  lastName: string,
  password: string
): Promise<User | null> {
  const { setItem } = useLocalStorage();
  const response = await fetch("http://localhost:5291/api/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, firstName, lastName, password }),
  });

  const data = await response.json();

  const token = data.token;
  const user = data.user;

  if (token) {
    user.token = token;
    setItem("token", JSON.stringify(token));
    return user;
  }

  return null;
}

export function isAuthenticated(): User | null {
  const { getItem } = useLocalStorage();
  const user = getItem("user");
  if (!user) {
    return null;
  }

  return JSON.parse(user);
}
