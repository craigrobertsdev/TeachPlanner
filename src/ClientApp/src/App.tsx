import Navbar from "./components/Navbar.tsx";
import Footer from "./components/Footer.tsx";
import { Outlet, Route, Routes } from "react-router-dom";
import Home from "./pages/Home.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import WeekPlanner from "./pages/WeekPlanner.tsx";
import TermPlanner from "./pages/TermPlanner.tsx";
import YearPlanner from "./pages/YearPlanner.tsx";
import LessonPlanner from "./pages/LessonPlanner.tsx";
import Reports from "./pages/Reports.tsx";
import Resources from "./pages/Resources.tsx";

function App() {
  return (
    <>
      <Navbar loggedIn={true} /* TODO - implement authentication in global state */ />
      <div className="flex-auto items-center justify-center text-darkGreen">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/lessonplanner" element={<LessonPlanner />} />
          <Route path="/weekplanner" element={<WeekPlanner />} />
          <Route path="/termplanner" element={<TermPlanner />} />
          <Route path="/yearplanner" element={<YearPlanner />} />
          <Route path="/resources" element={<Resources />} />
          <Route path="/reports" element={<Reports />} />
          <Route path="*" element={<ErrorPage />} />
        </Routes>
        <Outlet />
      </div>
      <Footer />
    </>
  );
}

export default App;
