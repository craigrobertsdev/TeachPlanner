﻿using Domain.UserAggregate;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    //Task<User>? GetUserByEmailAsync(string email);
    User? GetUserByEmail(string email);
    List<User?> GetAllUsers();
    bool IsEmailUnique(string email);
    void Add(User user);
}
