namespace TeachPlanner.Shared.Common.Exceptions;

public class TermPlannerAlreadyAssociatedException : BaseException {
    public TermPlannerAlreadyAssociatedException()
        : base("Term planner already exists for this year.",
            400, "YearData.TermPlannerAlreadyExists") { }
}

public class YearDataNotFoundException : BaseException {
    public YearDataNotFoundException()
        : base("No YearData found", 404, "YearData.NotFound") { }
}