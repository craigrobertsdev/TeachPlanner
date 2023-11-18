import { Link } from "react-router-dom";

const tiles = [
  { name: "Week Planner", link: "/weekplanner" },
  { name: "Term Planner", link: "/termplanner" },
  { name: "Year Planner", link: "/yearplanner" },
  { name: "Settings", link: "/settings" },
  { name: "Resources", link: "/resources" },
  { name: "Reports", link: "/reports" },
];

const LaunchPad = () => (
  <div className="launchpad flex flex-wrap justify-around">
    {tiles.map((tile) => (
      <div
        className="tile w-64 h-64 m-4 bg-blue-500 text-white flex items-center justify-center hover:bg-blue-700 transition-colors duration-200"
        key={tile.name}>
        <Link to={tile.link} className="text-2xl">
          {tile.name}
        </Link>
      </div>
    ))}
  </div>
);

export default LaunchPad;
