using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;
public class AuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMaskRepository _maskRepository;
    private readonly IUserRoleRepository _urRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IActeeRepository _acteeRepo;
    private readonly IPermissionRepository _permissionRepo;
    private readonly IApplicationRepository _appRepo;
    private readonly IMenuRepository _menuRepo;

    public AuthorizationService(
        IMenuRepository menuRepository,
        IPermissionRepository permissionRepo,
        IRoleRepository roleRepo,
        IUserRoleRepository userRoleRepository,
        IUserRepository userRepository,
        IApplicationRepository applicationRepository,
        IActeeRepository acteeRepository,
        IMaskRepository maskRepository )

    {
        _maskRepository = maskRepository;
        _menuRepo = menuRepository;
        _permissionRepo = permissionRepo;
        _acteeRepo = acteeRepository;
        _roleRepo = roleRepo;
        _urRepo = userRoleRepository;
        _userRepository = userRepository;
        _appRepo = applicationRepository;
    }


public async Task<List<Aplication>> GetApplicationsByUserRolesAsync(long userId)
{
    // Fetch user roles
    var userRoles = await _urRepo.GetByUserIdAsync(userId);
    
    if (userRoles == null || !userRoles.Any())
    {
        throw new UnauthorizedAccessException("User has no roles assigned.");
    }

    // Extract role IDs
    var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();

    // Fetch applications that are associated with these roles directly from the database
    //var applications = await _appRepo.GetApplicationByRoleId(roleIds);

    // Ensure distinct applications
    //return applications.Distinct().ToList();
    return null;
}


public async Task<List<Mask>> GetMasksAsync(long userId, string clientId)
{
    return await _maskRepository.GetMasksByUserIdAndClientIdAsync(userId, clientId);
}

}


