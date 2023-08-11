import React, { useState } from "react";
import useAuth from "../contexts/AuthContext";

function RegisterPage() {
  const { loading, error, register } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  function onEmailChange(e: React.ChangeEvent<HTMLInputElement>) {
    setEmail(e.target.value);
  }

  function onPasswordChange(e: React.ChangeEvent<HTMLInputElement>) {
    setPassword(e.target.value);
  }

  function onFirstNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    setFirstName(e.target.value);
  }

  function onLastNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    setLastName(e.target.value);
  }

  async function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    await register(email, firstName, lastName, password);
  }
  return (
    <form onSubmit={onSubmit}>
      <h2>Register</h2>
      <label htmlFor="email">Email</label>
      <input type="text" name="email" value={email} onChange={onEmailChange} />
      <label htmlFor="firstName">First Name</label>
      <input
        type="text"
        name="firstName"
        value={firstName}
        onChange={onFirstNameChange}
      />
      <label htmlFor="lastName">Last Name</label>
      <input
        type="text"
        name="lastName"
        value={lastName}
        onChange={onLastNameChange}
      />
      <label htmlFor="password">Password</label>
      <input
        type="password"
        name="password"
        value={password}
        onChange={onPasswordChange}
      />
      <button disabled={loading} type="submit">
        Register
      </button>
    </form>
  );
}

export default RegisterPage;
