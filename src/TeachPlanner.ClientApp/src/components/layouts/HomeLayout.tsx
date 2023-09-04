import { Link, Navigate, Outlet } from "react-router-dom";
import useAuth from "../../contexts/AuthContext";
import Footer from "../common/Footer";

function HomeLayout() {
  const { teacher } = useAuth();

  if (teacher) {
    return <Navigate to="/teacher/lesson-planner" replace />;
  }

  return (
    <>
      <nav className="bg-sage flex flex-none items-center px-2 py-3">
        <h1 className="text-darkGreen text-3xl pr-4 mr-4 border-r-[2px]">Teach Planner</h1>
        <ul className="list-none flex flex-1 gap-6 text-darkGreen text-xl">
          <li>
            <Link to="/">Home</Link>
          </li>
          <li className="ml-auto pr-6">
            <Link to="/login">Login</Link>
          </li>
        </ul>
      </nav>
      <div className="flex-auto items-center justify-center">
        <Outlet />
      </div>
      <Footer />
    </>
  );
}

export default HomeLayout;
