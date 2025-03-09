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
    private readonly IUserRoleRepository _urRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IApplicationRepository _appRepo;
    private readonly byte[] _secretKey;

    public AuthorizationService(
        IRoleRepository roleRepo,
        IUserRoleRepository userRoleRepository,
        IUserRepository userRepository,
        IApplicationRepository applicationRepository,
        string secretKey)
    {
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
    // Get user roles
    var userRoles = await _urRepo.GetByUserIdAsync(userId);
    if (userRoles == null || !userRoles.Any())
    {
        throw new UnauthorizedAccessException("User has no roles assigned.");
    }

    var permissions = new List<Permission>();
    foreach (var userRole in userRoles)
    {
        var roleWithPermissions = await _roleRepo.GetByIdAsync(userRole.Role.Id);
        if (roleWithPermissions != null)
        {
            permissions.AddRange(roleWithPermissions.Permissions);
        }
    }

    var application = await _appRepo.GetByClientIdAsync(clientId);


}


}


