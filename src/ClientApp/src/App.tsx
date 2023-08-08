import { useState } from "react";
import Navbar from "./Components/Navbar.tsx";
import Footer from "./Components/Footer.tsx";

function App() {
  return (
    <>
      <Navbar />
      <div className="flex flex-col flex-auto items-center justify-center text-darkGreen">Content goes here</div>
      <Footer />
    </>
  );
}

export default App;
