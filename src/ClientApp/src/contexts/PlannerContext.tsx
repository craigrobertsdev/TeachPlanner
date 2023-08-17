import { createContext, useContext, useState, ReactNode } from "react";

type PlannerContextType = {
  currentWeekPlanner: WeekPlanner;
  dayPlanPattern: DayPlanPattern;
};

type PlannerProviderProps = {
  children: ReactNode;
};

const PlannerContext = createContext<PlannerContextType>({} as PlannerContextType);

export function PlannerProvider({ children }: PlannerProviderProps) {
  const [currentWeekPlanner, setCurrentWeekPlanner] = useState<WeekPlanner>({} as WeekPlanner);
  const [dayPlanPattern, setDayPlanPattern] = useState<DayPlanPattern>({} as DayPlanPattern);

  return <PlannerContext.Provider value={{ currentWeekPlanner, dayPlanPattern }}>{children}</PlannerContext.Provider>;
}

export function usePlannerContext() {
  return useContext(PlannerContext);
}
