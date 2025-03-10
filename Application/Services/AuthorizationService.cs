using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;

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
    private readonly byte[] _secretKey;

    public AuthorizationService(
        IMenuRepository menuRepository,
        IPermissionRepository permissionRepo,
        IRoleRepository roleRepo,
        IUserRoleRepository userRoleRepository,
        IUserRepository userRepository,
        IApplicationRepository applicationRepository,
        IActeeRepository acteeRepository,
        IMaskRepository maskRepository,
        string secretKey)

    {
        _maskRepository = maskRepository;
        _menuRepo = menuRepository;
        _permissionRepo = permissionRepo;
        _acteeRepo = acteeRepository;
        _roleRepo = roleRepo;
        _urRepo = userRoleRepository;
        _userRepository = userRepository;
        _appRepo = applicationRepository;
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
    }

    public long? GetUserIdFromAccessToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                return long.Parse(userIdClaim.Value);
            }

            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing token: {ex.Message}");
            return null; 
        }
    }

public async Task<List<Application>> GetApplicationsByUserRolesAsync(string token)
{
    long? userId = GetUserIdFromAccessToken(token);
    
    if (userId == null)
    {
        throw new UnauthorizedAccessException("Invalid token or user ID not found.");
    }

    var user = await _userRepository.GetUserByIdAsync(userId.Value);

    if (user == null)
    {
        throw new UnauthorizedAccessException("User not found.");
    }

    var userRoles = await _urRepo.GetByUserIdAsync(userId.Value);
    
    if (userRoles == null || !userRoles.Any())
    {
        throw new UnauthorizedAccessException("User has no roles assigned.");
    }

    var roles = userRoles.Select(ur => ur.Role).ToList();


    var applications = new List<Application>();


    foreach (var role in roles)
    {
        var roleApplications = await _appRepo.GetApplicationsWithRolesAndPackagesAsync();
        applications.AddRange(roleApplications);
    }

    return applications.Distinct().ToList();  
}

public async Task<List<Mask>> GetMasksAsync(long userId, string clientId)
{
    if (userId <= 0)
        throw new ArgumentException("Invalid User ID", nameof(userId));

    if (string.IsNullOrWhiteSpace(clientId))
        throw new ArgumentException("Client ID cannot be null or empty", nameof(clientId));

    var application = await _appRepo.GetByClientIdAsync(clientId);
    if (application == null)
        throw new KeyNotFoundException("Application with the provided Client ID not found.");

    var userRoles = await _urRepo.GetRolesByUserIdAsync(userId);
    if (!userRoles.Any())
        throw new UnauthorizedAccessException("User does not have any roles assigned.");

    var permissions = await _permissionRepo.GetPermissionsByRolesAsync(userRoles.Select(r => r.Id).ToList());
    if (!permissions.Any())
        return new List<Mask>(); 

    var masks = await _maskRepository.GetMasksByPermissionsAsync(permissions.Select(p => p.Id).ToList());

    return masks;
}

}


