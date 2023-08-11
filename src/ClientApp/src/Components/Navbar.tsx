import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import useAuth from "../contexts/AuthContext";

const Navbar = () => {
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  return (
    <nav className="bg-sage flex flex-none items-center px-2 py-3">
      <h1 className="text-darkGreen text-3xl pr-4 mr-4 border-r-[2px]">
        Teach Planner
      </h1>
      <ul className="list-none flex flex-1 gap-6 text-darkGreen text-xl">
        <li>
          <Link to="/">Home</Link>
        </li>
        <li>
          <Link to="/lesson-planner">Lesson Planner</Link>
        </li>
        <li>
          <Link to="/week-planner">Week Planner</Link>
        </li>
        <li className="ml-auto pr-6">
          <button onClick={logout}>Logout</button>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
