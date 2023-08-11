import { Alert, LinearProgress } from "@mui/material";
import { AuthProvider } from "../contexts/AuthContext";
import { useOutlet, useLoaderData, Await } from "react-router-dom";
import { Suspense } from "react";

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

export default AuthLayout;
