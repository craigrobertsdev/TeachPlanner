using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeachPlanner.Shared.Common.Interfaces.Authentication;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator {
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(JwtSettings jwtSettings) {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(Teacher teacher) {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, teacher.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, teacher.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, teacher.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", teacher.Id.ToString())
        };

        var securityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}