using Application.Common.Interfaces.Authentication;
using Domain.UserAggregate;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        throw new NotImplementedException();
    }
}
