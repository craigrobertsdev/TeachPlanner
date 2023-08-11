import {
  BrowserRouter,
  Route,
  RouterProvider,
  Routes,
  createBrowserRouter,
  createRoutesFromElements,
  defer,
} from "react-router-dom";
import HomePage from "./pages/HomePage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import ProtectedLayout from "./components/ProtectedLayout.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import LessonPlannerPage from "./pages/LessonPlannerPage.tsx";
import ReportsPage from "./pages/ReportsPage.tsx";
import ResourcesPage from "./pages/ResourcesPage.tsx";
import TermPlannerPage from "./pages/TermPlannerPage.tsx";
import YearPlannerPage from "./pages/YearPlannerPage.tsx";
import AuthLayout from "./components/AuthLayout.tsx";
import HomeLayout from "./components/HomeLayout.tsx";

const getUserData = () =>
  new Promise((resolve) => {
    setTimeout(() => {
      const user = localStorage.getItem("user");
      resolve(user);
    }, 1000);
  });

// for error testing
// const getUserData = () =>
//   new Promise((resolve, reject) => {
//     setTimeout(() => {
//       const user = localStorage.getItem("token");
//       reject("Error");
//     }, 3000);
//   });

export const router = createBrowserRouter(
  createRoutesFromElements(
    <Route
      element={<AuthLayout />}
      loader={() => defer({ userPromise: getUserData() })}>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
      </Route>

      <Route path="/teacher" element={<ProtectedLayout />}>
        <Route path="lesson-planner" element={<LessonPlannerPage />} />
        {/* <Route path="/weekplanner" element={<WeekPlanner />} /> */}
        <Route path="term-planner" element={<TermPlannerPage />} />
        <Route path="year-planner" element={<YearPlannerPage />} />
        <Route path="resources" element={<ResourcesPage />} />
        <Route path="reports" element={<ReportsPage />} />
      </Route>
      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);
