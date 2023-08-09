import React, { useState } from "react";
import { login } from "../assets/services/UserService";

export default function Login() {
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
    try {
      await login(email, password);
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <>
      <form onSubmit={onSubmit}>
        <label htmlFor="username">Username</label>
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
        <button type="submit">Login</button>
      </form>
    </>
  );
}
