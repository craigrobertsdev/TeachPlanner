import useAuth from "../../contexts/AuthContext";
import { Navigate, Outlet } from "react-router-dom";
import Navbar from "../common/Navbar";
import Footer from "../common/Footer";
import { PlannerProvider } from "../../contexts/PlannerContext";
import { useRef } from "react";

function ProtectedLayout() {
  const { teacher } = useAuth();
  const layoutRef = useRef<HTMLDivElement>(null);

  if (!teacher) {
    return <Navigate to="/login" replace={true} />;
  }

  return (
    <PlannerProvider>
      <Navbar />
      <main ref={layoutRef} className={"flex flex-auto justify-center bg-inherit relative"}>
        <Outlet />
      </main>
      <Footer />
    </PlannerProvider>
  );
}

export default ProtectedLayout;
