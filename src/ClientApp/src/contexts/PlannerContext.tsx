import { createContext, useState } from "react";

type PlannerContextType = {
  currentWeekPlanner: WeekPlanner;
};

const PlannerContext = createContext<PlannerContextType>(
  {} as PlannerContextType
);

export function PlannerProvider({ children }: { children: React.ReactNode }) {
  const [currentWeekPlanner, setCurrentWeekPlanner] = useState<WeekPlanner>(
    {} as WeekPlanner
  );
}
