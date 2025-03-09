using System;
using Domain.Entities;

namespace Domain;

public interface IRolesRepository
{
    Task<Role> GetRoleByRoleId (long roleId);
}
