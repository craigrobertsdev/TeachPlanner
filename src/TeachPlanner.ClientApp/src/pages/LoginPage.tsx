import React, { useState } from "react";
import useAuth from "../contexts/AuthContext";

export default function LoginPage() {
  const { loading, error, login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

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

    await login(formData.get("email") as string, formData.get("password") as string);

    if (error) {
      setPassword("");
    }
  }

  return (
    <form onSubmit={onSubmit}>
      <h2>Login</h2>
      <label htmlFor="email">Email</label>
      <input type="text" name="email" value={email} onChange={onEmailChange} />
      <label htmlFor="password">Password</label>
      <input type="password" name="password" value={password} onChange={onPasswordChange} />
      <button disabled={loading} type="submit">
        Login
      </button>

      {error && <p className="text-ceramic text-lg">Incorrect username or password</p>}
    </form>
  );
}
