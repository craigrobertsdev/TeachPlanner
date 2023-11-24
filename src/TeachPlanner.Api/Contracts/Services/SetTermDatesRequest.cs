using TeachPlanner.Api.Contracts.Teachers.AccountSetup;

namespace TeachPlanner.Api.Contracts.Services;

public record SetTermDatesRequest(List<TermDateDto> TermDateDtos);
