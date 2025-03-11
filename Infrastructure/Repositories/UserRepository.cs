using System;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext dbContext;

    public UserRepository(AuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User?> GetUserByIdAsync(long userId)
    {
        return await dbContext.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<User> GetUserWithRoleAsync(long Id)
    {
        throw new NotImplementedException();
    }
}
