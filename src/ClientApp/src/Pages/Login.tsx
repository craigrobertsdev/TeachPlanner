import React, { useState } from "react";
import { login } from "../services/UserService";
import { useLocalStorage } from "../hooks/useLocalStorage";
import useAuth from "../contexts/AuthContext";

export default function Login() {
  const { loading, error, login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { setItem } = useLocalStorage();

  function onEmailChange(e: React.ChangeEvent<HTMLInputElement>) {
    setEmail(e.target.value);
  }

  function onPasswordChange(e: React.ChangeEvent<HTMLInputElement>) {
    setPassword(e.target.value);
  }

  async function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    console.log("Form submitted");

    const formData = new FormData(e.currentTarget);

    await login(
      formData.get("email") as string,
      formData.get("password") as string
    );

    if (error) {
      setPassword("");
    }
  }

  return (
    <>
      <form onSubmit={onSubmit}>
        <label htmlFor="email">Email</label>
        <input
          type="text"
          name="email"
          value={email}
          onChange={onEmailChange}
        />
        <label htmlFor="password">Password</label>
        <input
          type="password"
          name="password"
          value={password}
          onChange={onPasswordChange}
        />
        <button disabled={loading} type="submit">
          Login
        </button>

        {error && (
          <p className="text-ceramic text-lg">Incorrect username or password</p>
        )}
      </form>
    </>
  );
}
