import { NavLink } from "react-router-dom";

const Navbar = () => {
  return (
    <div className="bg-sage flex flex-none items-center px-2 py-3">
      <h1 className="text-darkGreen text-3xl">Teach Planner</h1>
      <ul className="list-none">
        <li>
          <NavLink to="/">Home</NavLink>
        </li>
      </ul>
    </div>
  );
};

export default Navbar;
