namespace TeachPlanner.Domain.Common.Exceptions;
public class StrandCreationException : BaseException
{
    public StrandCreationException() : base("Either substrands or contentDescriptions must be provided", 400, "Subject.NeitherStrandNorSubstrand")
    {
    }
}
