namespace TeachPlanner.Api.Common.Exceptions;
public class TermPlannerCreationException : BaseException
{
    public TermPlannerCreationException() : base("TermPlanners must have either one year level or a list of year levels", 400, "TermPlanner.NoYearLevel") { }
}
public class TooManyYearLevelsException : BaseException
{
    public TooManyYearLevelsException() : base("A maximum of two year levels are allowed", 400, "TermPlanner.MoreThanTwoYearLevels") { }
}

public class DuplicateTermPlanException : BaseException
{
    public DuplicateTermPlanException() : base("Cannot add a duplicate term plan", 400, "TermPlanner.AlreadyExists") { }
}

public class TooManyTermPlansException : BaseException
{
    public TooManyTermPlansException() : base("A maximum of four term plans are allowed", 400, "TermPlanner.MoreThanFour") { }
}

public class DuplicateTermNumberException : BaseException
{
    public DuplicateTermNumberException() : base("Cannot add a duplicate term number", 400, "TermPlanner.DuplicateTermNumber") { }
}

public class TermPlannerDoesNotBelongToTeacherException : BaseException
{
    public TermPlannerDoesNotBelongToTeacherException() : base("Term planner does not belong to teacher", 401, "TermPlanner.NotBelongToTeacher") { }
}

public class TermPlannerNotFoundException : BaseException
{
    public TermPlannerNotFoundException() : base("Term planner not found", 404, "TermPlanner.NotFound") { }
}

public class TermPlanSubjectsAlreadySetException : BaseException
{
    public TermPlanSubjectsAlreadySetException() : base("CurriculumSubjects have already been set for TermPlan", 404, "TermPlanner.SubjectsAlreadySet") { }
}
