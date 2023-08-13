import { Alert, LinearProgress } from "@mui/material";
import { AuthProvider } from "../contexts/AuthContext";
import { useOutlet, useLoaderData, Await } from "react-router-dom";
import { Suspense } from "react";
import authService from "../services/AuthService";

function AuthLayout() {
  const outlet = useOutlet();
  const { userPromise } = useLoaderData() as { userPromise: Promise<User> };

  return (
    <Suspense fallback={<LinearProgress />}>
      <Await
        resolve={userPromise}
        errorElement={<Alert severity="error">Something went wrong!</Alert>}
        children={(user) => (
          <AuthProvider userData={user}>{outlet}</AuthProvider>
        )}
      />
    </Suspense>
  );
}

export function getUserData() {
  const userData = localStorage.getItem("user");

  if (!userData) {
    return new Promise((resolve) => resolve(null));
  }

  const user = JSON.parse(userData) as User;

  if (!authService.loggedIn()) {
    authService.logout();
    return new Promise((resolve) => resolve(null));
  }

  return new Promise((resolve) => resolve(user));
}

export default AuthLayout;
