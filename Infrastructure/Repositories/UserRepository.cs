using System;
using Domain;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthorizationDbContext _context;
    public UserRepository (AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(long id)
    {    
        User user =
        await _context.Users.FirstOrDefaultAsync(u=> u.Id == id);

        return user;
    }

    public async Task<User> GetUserWithRoleAsync(long Id)
    {
    
            return await _context.Set<User?>()
                .Include(u => u.UserRoles)  
                .FirstOrDefaultAsync(u => u.Id == Id);

    }
}
