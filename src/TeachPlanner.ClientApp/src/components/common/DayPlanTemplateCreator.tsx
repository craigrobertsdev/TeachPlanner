import { DndContext } from "@dnd-kit/core";

type DayPlanTemplateCreatorProps = {
  dayPlanTemplate: DayPlanTemplate;
  setDayPlanTemplate: React.Dispatch<React.SetStateAction<DayPlanTemplate>>;
};
function DayPlanTemplateCreator({ dayPlanTemplate, setDayPlanTemplate }: DayPlanTemplateCreatorProps) {
  return <DndContext></DndContext>;
}

export default DayPlanTemplateCreator;
