import useAuth from "../../contexts/AuthContext";
import { Navigate, Outlet } from "react-router-dom";
import Navbar from "../common/Navbar";
import Footer from "../common/Footer";
import { PlannerProvider } from "../../contexts/PlannerContext";
import ThemeProvider from "../../contexts/ThemeContext";

function ProtectedLayout() {
  const { teacher } = useAuth();

  if (!teacher) {
    return <Navigate to="/login" replace={true} />;
  }

  return (
    <PlannerProvider>
      <ThemeProvider>
        <Navbar />
        <div className="flex-auto items-center justify-center bg-inherit">
          <Outlet />
        </div>
        <Footer />
      </ThemeProvider>
    </PlannerProvider>
  );
}

export default ProtectedLayout;
