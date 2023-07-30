using Application.Users.Common;
using Contracts.Users;
using Mapster;

namespace WebAPI.Common.Mappings;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<GetAllUsersResult, GetAllUsersResponse>();

    }
}
