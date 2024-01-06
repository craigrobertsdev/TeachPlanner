using FluentValidation;
using Mapster;
using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Authentication;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.Authentication;
using TeachPlanner.Shared.Contracts.Teachers;
using TeachPlanner.Api.Services.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Identity;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Api.Features.Authentication;

public static class Login {
}