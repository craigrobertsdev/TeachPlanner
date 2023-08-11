import React from "react";
import Navbar from "../components/Navbar.tsx";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
  const navigate = useNavigate();
  function handleLogin() {
    navigate("/login");
  }

  return (
    <div className="flex flex-col flex-auto items-center justify-center">
      <h1 className="text-5xl text-darkGreen mb-11">
        Welcome to Teach Planner!
      </h1>
      <button
        className="bg-sage px-3 py-2 m-3 rounded-xl text-lg text-primary font-semibold"
        onClick={handleLogin}>
        Login
      </button>
    </div>
  );
};

export default HomePage;
