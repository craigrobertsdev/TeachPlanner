import Navbar from "./components/Navbar.tsx";
import Footer from "./components/Footer.tsx";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/Home.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import WeekPlanner from "./pages/WeekPlanner.tsx";
import TermPlanner from "./pages/TermPlanner.tsx";
import YearPlanner from "./pages/YearPlanner.tsx";
import LessonPlanner from "./pages/LessonPlanner.tsx";
import Reports from "./pages/Reports.tsx";
import Resources from "./pages/Resources.tsx";
import Login from "./pages/Login.tsx";
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
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/lessonplanner" element={<LessonPlanner />} />
          {/* <Route path="/weekplanner" element={<WeekPlanner />} /> */}
          <Route path="/termplanner" element={<TermPlanner />} />
          <Route path="/yearplanner" element={<YearPlanner />} />
          <Route path="/resources" element={<Resources />} />
          <Route path="/reports" element={<Reports />} />
          <Route path="*" element={<ErrorPage />} />
        </Routes>
      </div>
      <Footer />
    </>
  );
}

export default App;
