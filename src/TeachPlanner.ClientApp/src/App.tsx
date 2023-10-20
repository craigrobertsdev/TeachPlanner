import { Route, createBrowserRouter, createRoutesFromElements, defer } from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import ProtectedLayout from "./components/layouts/ProtectedLayout.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import WeekPlannerPage, { weekPlannerLoader } from "./pages/planner/WeekPlannerPage.tsx";
import ReportsPage from "./pages/ReportsPage.tsx";
import ResourcesPage from "./pages/ResourcesPage.tsx";
import TermPlannerPage from "./pages/planner/TermPlannerPage.tsx";
import YearPlannerPage from "./pages/planner/YearPlannerPage.tsx";
import AuthLayout, { getTeacherData } from "./components/layouts/AuthLayout.tsx";
import HomeLayout from "./components/layouts/HomeLayout.tsx";
import LessonPlanPage, { lessonPlanLoader } from "./pages/planner/LessonPlanPage.tsx";
import LessonPlansPage from "./pages/planner/LessonPlansPage.tsx";
import SettingsPage from "./pages/SettingsPage.tsx";
import RegisterPage from "./pages/RegisterPage.tsx";
import AccountPage from "./pages/AccountPage.tsx";

export const router = createBrowserRouter(
  createRoutesFromElements(
    <Route element={<AuthLayout />} loader={() => defer({ teacherPromise: getTeacherData() })}>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Route>

      <Route path="/teacher" element={<ProtectedLayout />}>
        <Route path="account" element={<AccountPage />} />
        <Route path="week-planner" loader={weekPlannerLoader} element={<WeekPlannerPage />} />
        <Route path="lesson-plans">
          <Route element={<LessonPlansPage />} index />
          <Route path=":lessonPlanId" element={<LessonPlanPage />} loader={lessonPlanLoader} />
        </Route>
        <Route path="term-planner" element={<TermPlannerPage />} />
        <Route path="year-planner" element={<YearPlannerPage />} />
        <Route path="resources" element={<ResourcesPage />} />
        <Route path="reports" element={<ReportsPage />} />
        <Route path="settings" element={<SettingsPage />} />
      </Route>
      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);
