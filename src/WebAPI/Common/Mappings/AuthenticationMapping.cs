using Mapster;
using TeachPlanner.Application.Authentication.Queries.Login;
using TeachPlanner.Contracts.Authentication;
using TeachPlanner.Application.Authentication.Common;
using TeachPlanner.Application.Authentication.Commands.Register;

namespace TeachPlanner.Api.Common.Mappings;

public class AuthenticationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config
            .NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.Teacher);
    }
}
