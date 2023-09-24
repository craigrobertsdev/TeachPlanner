import React, { useState } from "react";
import useAuth from "../contexts/AuthContext";
import Button from "../components/common/Button";

function RegisterPage() {
  const { loading, error, register } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmedPassword, setConfirmedPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  function onEmailChange(e: React.ChangeEvent<HTMLInputElement>) {
    setEmail(e.target.value);
  }

  function onPasswordChange(e: React.ChangeEvent<HTMLInputElement>) {
    setPassword(e.target.value);
  }

  function onConfirmedPasswordChange(e: React.ChangeEvent<HTMLInputElement>) {
    setConfirmedPassword(e.target.value);
  }

  function onFirstNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    setFirstName(e.target.value);
  }

  function onLastNameChange(e: React.ChangeEvent<HTMLInputElement>) {
    setLastName(e.target.value);
  }

  async function onSubmit(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();

    await register(email, firstName, lastName, password, confirmedPassword);
  }
  return (
    <form className="flex flex-col items-center justify-center p-3 border border-black">
      <h2>Register</h2>
      <div className="lg:w-1/4">
        <div className="pb-3">
          <label className="block" htmlFor="email">
            Email
          </label>
          <input className="w-full p-1 rounded-xl" type="text" name="email" value={email} onChange={onEmailChange} />
        </div>
        <div className="pb-3">
          <label className="block" htmlFor="firstName">
            First Name
          </label>
          <input className="w-full p-1 rounded-xl" type="text" name="firstName" value={firstName} onChange={onFirstNameChange} />
        </div>
        <div className="pb-3">
          <label className="block" htmlFor="lastName">
            Last Name
          </label>
          <input className="w-full p-1 rounded-xl" type="text" name="lastName" value={lastName} onChange={onLastNameChange} />
        </div>
        <div className="pb-3">
          <label className="block" htmlFor="password">
            Password
          </label>
          <input className="w-full p-1 rounded-xl" type="password" name="password" value={password} onChange={onPasswordChange} />
        </div>
        <div className="pb-3">
          <label className="block" htmlFor="confirmPassword">
            Confirm Password
          </label>
          <input
            className="w-full p-1 rounded-xl"
            type="password"
            name="confirmPassword"
            value={confirmedPassword}
            onChange={onConfirmedPasswordChange}
          />
        </div>
        <Button disabled={loading} variant="submit" onClick={onSubmit}>
          Register
        </Button>
      </div>
    </form>
  );
}

export default RegisterPage;
