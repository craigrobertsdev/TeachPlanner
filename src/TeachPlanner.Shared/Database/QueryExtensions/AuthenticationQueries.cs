﻿using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.QueryExtensions;

public static class AuthenticationQueries {
    public static async Task<User?> GetUserByEmail(this ApplicationDbContext context, string email,
        CancellationToken cancellationToken) {
        //return await context.Users
        //    .Where(u => u.Email == email)
        //    .FirstOrDefaultAsync(cancellationToken);
        throw new NotImplementedException();
    }
}