using System;
using Domain.Entities;

namespace Domain;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(long Id);
    Task<User> GetUserWithRoleAsync(long Id);

}
