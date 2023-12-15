import { createContext, useContext, useState, ReactNode, useEffect } from "react";
import useAuth from "./AuthContext";
import { baseUrl } from "../utils/constants";
import { dayPlanHelper } from "../utils/helpers";

type PlannerContextType = {
	currentWeekPlanner: WeekPlanner;
	setCurrentWeekPlanner: React.Dispatch<React.SetStateAction<WeekPlanner>>;
	dayPlanTemplate: DayPlanTemplate;
	setDayPlanPattern: React.Dispatch<React.SetStateAction<DayPlanTemplate>>;
	subjects: Subject[];
	availablePlannerYears: number[];
};

type PlannerProviderProps = {
	children: ReactNode;
};

const PlannerContext = createContext<PlannerContextType>({} as PlannerContextType);

export function PlannerProvider({ children }: PlannerProviderProps) {
	const [currentWeekPlanner, setCurrentWeekPlanner] = useState<WeekPlanner>({} as WeekPlanner);
	const [dayPlanTemplate, setDayPlanTemplate] = useState<DayPlanTemplate>(dayPlanHelper.dayPlanPattern);
	const [subjects, setSubjects] = useState<Subject[]>([]);
	const [availablePlannerYears, setAvailablePlannerYears] = useState<number[]>([]);
	const { teacher } = useAuth();

	// TODO: remove hard coded data and create endpoint
	useEffect(() => {
		// fetch(`${baseUrl}/${teacher!.id}/lesson-planner`, {
		//   method: "GET",
		//   headers: {
		//     "Content-Type": "application/json",
		//     Authorization: `Bearer ${teacher!.token}`,
		//   },
		// })
		//   .then((response) => response.json())
		//   .then((data) => {
		//     // setCurrentWeekPlanner(data.currentWeekPlanner);
		//     // setDayPlanPattern(data.dayPlanPattern);
		//     setSubjects(data.subjects);
		//   })
		//   .catch((error) => {
		//     console.error("Error:", error);
		//   });
		setAvailablePlannerYears([2023, 2024])
		setDayPlanTemplate()
	}, []);

	return <PlannerContext.Provider value={{ currentWeekPlanner, setCurrentWeekPlanner, dayPlanTemplate, setDayPlanPattern: setDayPlanTemplate, subjects, availablePlannerYears }}>
		{children}
	</PlannerContext.Provider>;
}

export function usePlannerContext() {
	return useContext(PlannerContext);
}
