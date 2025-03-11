using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;

namespace Application.Services
{
    public class UserAccessService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;
        private readonly IActeeRepository acteeRepository;
        private readonly IMenuRepository menuRepository;
        private readonly IMaskRepository maskRepository;
        private readonly IApplicationRepository applicationRepository;
        private readonly IApplicationPackageRepository applicationPackageRepository;

        public UserAccessService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IActeeRepository acteeRepository,
            IMenuRepository menuRepository,
            IMaskRepository maskRepository,
            IApplicationRepository applicationRepository,
            IApplicationPackageRepository applicationPackageRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
            this.acteeRepository = acteeRepository;
            this.menuRepository = menuRepository;
            this.maskRepository = maskRepository;
            this.applicationRepository = applicationRepository;
            this.applicationPackageRepository = applicationPackageRepository;
        }

        public async Task<List<Aplication>> GetUserApplicationsAsync(long userId)
{
    // Step 1: Validate User
    if (userId <= 0)
    {
        throw new ArgumentException("Invalid userId.");
    }

    // Fetch the user
    var user = await userRepository.GetUserByIdAsync(userId);
    if (user == null)
    {
        throw new ArgumentException("User not found.");
    }

    // Step 2: Get UserRoles for the given userId
    var userRoles = user.UserRoles;
    if (userRoles == null || !userRoles.Any())
    {
        throw new ArgumentException("User has no roles assigned.");
    }

    // Step 3: Fetch Applications based on Role's ApplicationId
    var applicationIds = userRoles.Select(ur => ur.Role.ApplicationId).Distinct().ToList();
    if (applicationIds == null || !applicationIds.Any())
    {
        throw new ArgumentException("No applications associated with the user's roles.");
    }

    // Step 4: Fetch Applications based on ApplicationIds
    var applications = await applicationRepository.GetApplicationsByIdsAsync(applicationIds);
    if (applications == null || !applications.Any())
    {
        throw new ArgumentException("No applications found for the provided ApplicationIds.");
    }

    // Return the list of applications
    return applications;
}


        public async Task<List<string>> GetUserAccessAsync(long userId, string clientId)
        {
            // Step 1: Validate User and Client
            if (userId <= 0 || string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("Invalid userId or clientId.");
            }

            // Fetch the user
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Step 2: Find the user's active role
            var userRole = user.UserRoles.FirstOrDefault(ur => ur.IsDefault);
            if (userRole == null || userRole.Role == null)
            {
                throw new ArgumentException("User has no active role.");
            }

            var role = userRole.Role;
            long applicationId = role.ApplicationId;

            // Step 3: Fetch the application and check ClientId
            var application = await applicationRepository.GetApplicationByIdAsync(applicationId);
            if (application == null || application.ClientId != clientId)
            {
                throw new ArgumentException("Application not found or ClientId mismatch.");
            }

            // Step 4: Get Application Package (needed for Actees)
            var applicationPackage = await applicationPackageRepository.GetPackageByApplicationIdAsync(applicationId);
            if (applicationPackage == null)
            {
                throw new ArgumentException("No Application Package found.");
            }

            // Step 5: Find Actees linked to this Application Package
            var actees = await acteeRepository.GetActeesByApplicationPackageIdAsync(applicationPackage.Id);
            if (actees == null || !actees.Any())
            {
                throw new ArgumentException("No Actees found for this Application Package.");
            }

            // Step 6: Fetch MenuKeys associated with the Actees
            var acteeIds = actees.Select(a => a.Id).ToList();
            var menuKeys = await menuRepository.GetMenuKeysByActeeIdsAsync(acteeIds);
            if (menuKeys == null || !menuKeys.Any())
            {
                throw new ArgumentException("No menus found for these Actees.");
            }

            // Step 7: Fetch Permissions associated with Actees
            var permissions = await permissionRepository.GetPermissionsByActeeIdsAsync(acteeIds);
            if (permissions == null || !permissions.Any())
            {
                throw new ArgumentException("No permissions found for these Actees.");
            }

            // Step 8: Fetch Masks associated with Permissions
            var permissionIds = permissions.Select(p => p.Id).ToList();
            var masks = await maskRepository.GetMasksByPermissionIdsAsync(permissionIds);
            if (masks == null || !masks.Any())
            {
                throw new ArgumentException("No masks found for these permissions.");
            }

            // Step 9: Return the relevant mask IDs
            return masks.Select(m => m.PermissionId.ToString()).ToList();
        }
    }
}
