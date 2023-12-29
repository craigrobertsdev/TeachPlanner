using TeachPlanner.Shared.Contracts.Teachers.AccountSetup;

namespace TeachPlanner.Shared.Contracts.Services;

public record SetTermDatesRequest(List<TermDateDto> TermDateDtos);
