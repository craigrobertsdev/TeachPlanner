import { Link } from "react-router-dom";

type NavbarProps = {
  user: User | null;
};

const Navbar = ({ user }: NavbarProps) => {
  function logout() {
    localStorage.removeItem("token");
    window.location.href = "/";
  }

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
          <Link to="/lessonplanner">Lesson Planner</Link>
        </li>
        <li>
          <Link to="/weekplanner">Week Planner</Link>
        </li>
        {user ? (
          <li className="ml-auto pr-6">
            <button onClick={logout}>Logout</button>
          </li>
        ) : (
          <li className="ml-auto">
            <Link to="/login">Login</Link>
          </li>
        )}
      </ul>
    </nav>
  );
};

export default Navbar;
