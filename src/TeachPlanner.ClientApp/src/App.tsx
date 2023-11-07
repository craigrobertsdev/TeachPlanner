import { Route, createBrowserRouter, createRoutesFromElements, defer } from "react-router-dom";
import Home from "./pages/Home.tsx";
import Login from "./pages/Login.tsx";
import ProtectedLayout from "./components/layouts/ProtectedLayout.tsx";
import Error from "./pages/Error.tsx";
import WeekPlanner, { weekPlannerLoader } from "./pages/planner/WeekPlanner.tsx";
import Reports from "./pages/Reports.tsx";
import Resources from "./pages/Resources.tsx";
import TermPlanner from "./pages/planner/TermPlanner.tsx";
import YearPlanner from "./pages/planner/YearPlanner.tsx";
import AuthLayout, { getTeacherData } from "./components/layouts/AuthLayout.tsx";
import HomeLayout from "./components/layouts/HomeLayout.tsx";
import LessonPlan, { lessonPlanLoader } from "./pages/planner/LessonPlan.tsx";
import LessonPlans from "./pages/planner/LessonPlans.tsx";
import Settings from "./pages/Settings.tsx";
import Register from "./pages/Register.tsx";
import AccountSetup from "./pages/AccountSetup.tsx";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <Route element={<AuthLayout />} loader={() => defer({ teacherPromise: getTeacherData() })}>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Route>

      <Route path="/teacher" element={<ProtectedLayout />}>
        <Route path="account" element={<AccountSetup />} />
        <Route path="week-planner" loader={weekPlannerLoader} element={<WeekPlanner />} />
        <Route path="lesson-plans">
          <Route element={<LessonPlans />} index />
          <Route path=":lessonPlanId" element={<LessonPlan />} loader={lessonPlanLoader} />
        </Route>
        <Route path="term-planner" element={<TermPlanner />} />
        <Route path="year-planner" element={<YearPlanner />} />
        <Route path="resources" element={<Resources />} />
        <Route path="reports" element={<Reports />} />
        <Route path="settings" element={<Settings />} />
      </Route>
      <Route path="*" element={<Error />} />
    </Route>
  )
);
