using System;
using Domain.Entities;

namespace Domain;

public interface IUserRepository
{
    Task<User> GetUserWithRoleAsync(long Id);
    Task<User?> GetUserByIdAsync(long userId);


}
