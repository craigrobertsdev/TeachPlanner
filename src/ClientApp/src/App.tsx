import Navbar from "./components/Navbar.tsx";
import Footer from "./components/Footer.tsx";
import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import WeekPlannerPage from "./pages/WeekPlannerPage.tsx";
import TermPlannerPage from "./pages/TermPlannerPage.tsx";
import YearPlannerPage from "./pages/YearPlanner.tsx";
import LessonPlannerPage from "./pages/LessonPlannerPage.tsx";
import ReportsPage from "./pages/ReportsPage.tsx";
import ResourcesPage from "./pages/ResourcesPage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import useAuth from "./contexts/AuthContext.tsx";

function App() {
  const { user, loading, error } = useAuth();
  return (
    <>
      <Navbar
        loggedIn={user} /* TODO - implement authentication in global state */
      />
      <div className="flex-auto items-center justify-center text-darkGreen">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/lessonplanner" element={<LessonPlannerPage />} />
          {/* <Route path="/weekplanner" element={<WeekPlanner />} /> */}
          <Route path="/termplanner" element={<TermPlannerPage />} />
          <Route path="/yearplanner" element={<YearPlannerPage />} />
          <Route path="/resources" element={<ResourcesPage />} />
          <Route path="/reports" element={<ReportsPage />} />
          <Route path="*" element={<ErrorPage />} />
        </Routes>
      </div>
      <Footer />
    </>
  );
}

export default App;
