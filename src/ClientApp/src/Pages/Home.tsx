import React from "react";
import Navbar from "../components/Navbar.tsx";

type HomePageProps = {};

const Home = (props: HomePageProps) => {
  return (
    <div className="p-5">
      <table className="border border-darkGreen">
        <thead className="border border-darkGreen">
          <tr className="p-2">
            <th className="border border-darkGreen p-5">Home</th>
            <th className="border border-darkGreen p-5">Reports</th>
            <th className="border border-darkGreen p-5">Year Planner</th>
            <th className="border border-darkGreen p-5">Term Planner</th>
            <th className="border border-darkGreen p-5">Week Planner</th>
            <th className="border border-darkGreen p-5">Lesson Planner</th>
          </tr>
        </thead>
        <tbody>
          <tr className="border border-darkGreen">
            <td className="border border-darkGreen p-5">Home</td>
            <td className="border border-darkGreen p-5">Reports</td>
            <td className="border border-darkGreen p-5">Year Planner</td>
            <td className="border border-darkGreen p-5">Term Planner</td>
            <td className="border border-darkGreen p-5">Week Planner</td>
            <td className="border border-darkGreen p-5">Lesson Planner</td>
          </tr>
          <tr className="border border-darkGreen">
            <td className="border border-darkGreen p-5">Home</td>
            <td className="border border-darkGreen p-5">Reports</td>
            <td className="border border-darkGreen p-5">Year Planner</td>
            <td className="border border-darkGreen p-5">Term Planner</td>
            <td className="border border-darkGreen p-5">Week Planner</td>
            <td className="border border-darkGreen p-5">Lesson Planner</td>
          </tr>
          <tr>
            <td className="border border-darkGreen p-5">Home</td>
            <td className="border border-darkGreen p-5">Reports</td>
            <td className="border border-darkGreen p-5">Year Planner</td>
            <td className="border border-darkGreen p-5">Term Planner</td>
            <td className="border border-darkGreen p-5">Week Planner</td>
            <td className="border border-darkGreen p-5">Lesson Planner</td>
          </tr>
        </tbody>
      </table>

      <button className="bg-sage px-3 py-2 m-3 rounded-xl text-lg text-primary font-semibold">Lesson Planner</button>
      <button className="bg-darkGreen px-3 py-2 m-3 rounded-lg text-primary font-semibold">Confirm</button>
      <button className="bg-ceramic px-3 py-1 m-3 rounded-xl text-primary font-semibold">Cancel</button>
    </div>
  );
};

export default Home;
