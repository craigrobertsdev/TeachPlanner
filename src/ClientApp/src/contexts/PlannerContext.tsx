import { createContext, useContext, useState, ReactNode, useEffect } from "react";
import { baseUrl } from "../utils/constants";
import useAuth from "./AuthContext";

type PlannerContextType = {
  currentWeekPlanner: WeekPlanner;
  dayPlanPattern: DayPlanPattern;
  subjects: Subject[];
};

type PlannerProviderProps = {
  children: ReactNode;
};

const PlannerContext = createContext<PlannerContextType>({} as PlannerContextType);

export function PlannerProvider({ children }: PlannerProviderProps) {
  const [currentWeekPlanner, setCurrentWeekPlanner] = useState<WeekPlanner>({} as WeekPlanner);
  const [dayPlanPattern, setDayPlanPattern] = useState<DayPlanPattern>({} as DayPlanPattern);
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const { user } = useAuth();

  useEffect(() => {
    fetch(`${baseUrl}/${user!.id}/lesson-planner`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ teacherId: user!.id }),
    })
      .then((response) => response.json())
      .then((data) => {
        // setCurrentWeekPlanner(data.currentWeekPlanner);
        // setDayPlanPattern(data.dayPlanPattern);
        setSubjects(data.subjects);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, []);

  return <PlannerContext.Provider value={{ currentWeekPlanner, dayPlanPattern, subjects }}>{children}</PlannerContext.Provider>;
}

export function usePlannerContext() {
  return useContext(PlannerContext);
}
