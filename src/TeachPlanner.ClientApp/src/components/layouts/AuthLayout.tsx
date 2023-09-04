import { Alert, LinearProgress } from "@mui/material";
import { AuthProvider } from "../../contexts/AuthContext";
import { useOutlet, useLoaderData, Await } from "react-router-dom";
import { Suspense } from "react";
import authService from "../../services/AuthService";

function AuthLayout() {
  const outlet = useOutlet();
  const { teacherPromise } = useLoaderData() as { teacherPromise: Promise<Teacher> };

  return (
    <Suspense fallback={<LinearProgress />}>
      <Await
        resolve={teacherPromise}
        errorElement={<Alert severity="error">Something went wrong!</Alert>}
        children={(teacher: Teacher) => <AuthProvider teacherData={teacher}>{outlet}</AuthProvider>}
      />
    </Suspense>
  );
}

export function getTeacherData() {
  const teacherData = localStorage.getItem("teacher");

  if (!teacherData) {
    return new Promise((resolve) => resolve(null));
  }

  const teacher = JSON.parse(teacherData) as Teacher;

  if (!authService.loggedIn()) {
    authService.logout();
    return new Promise((resolve) => resolve(null));
  }

  return new Promise((resolve) => resolve(teacher));
}

export default AuthLayout;
