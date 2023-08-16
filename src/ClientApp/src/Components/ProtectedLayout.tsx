import useAuth from "../contexts/AuthContext";
import { Navigate, Outlet } from "react-router-dom";
import Navbar from "./Navbar";
import Footer from "./Footer";

function ProtectedLayout() {
  const { user } = useAuth();

  if (!user) {
    return <Navigate to="/login" replace={true} />;
  }

  return (
    <>
      <Navbar />
      <div className="flex-auto items-center justify-center bg-inherit">
        <Outlet />
      </div>
      <Footer />
    </>
  );
}

export default ProtectedLayout;
