import useAuth from "../../contexts/AuthContext";
import { Navigate, Outlet } from "react-router-dom";
import Navbar from "../common/Navbar";
import Footer from "../common/Footer";
import { PlannerProvider } from "../../contexts/PlannerContext";
import { useThemeContext } from "../../contexts/ThemeContext";
import { useEffect, useRef } from "react";

function ProtectedLayout() {
  const { teacher } = useAuth();
  const { cancelModalOpen, dialogOpenStyle } = useThemeContext();
  const layoutRef = useRef<HTMLDivElement>(null);

  if (!teacher) {
    return <Navigate to="/login" replace={true} />;
  }

  useEffect(() => {
    // if (cancelModalOpen) {
    //   layoutRef.current?.classList.add(...dialogOpenStyle);
    // } else {
    //   layoutRef.current?.classList.remove(...dialogOpenStyle);
    // }
  }, [cancelModalOpen]);

  console.log(dialogOpenStyle);

  return (
    <PlannerProvider>
      <Navbar />
      <main ref={layoutRef} className={`flex flex-auto justify-center bg-inherit ${cancelModalOpen ? dialogOpenStyle : ""}`}>
        <Outlet />
      </main>
      <Footer />
    </PlannerProvider>
  );
}

export default ProtectedLayout;
