using Microsoft.AspNetCore.Identity;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Blazor.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<UserId> {
    public TeacherId TeacherId { get; set; }
}

