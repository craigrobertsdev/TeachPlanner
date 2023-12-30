using Microsoft.AspNetCore.Identity;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    public TeacherId TeacherId { get; set; }
}

