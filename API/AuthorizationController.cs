using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Services;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Get applications based on the user roles from the JWT token.
        /// </summary>
        /// <param name="token">The JWT token sent in the authorization header</param>
        /// <returns>List of applications accessible to the user based on their roles</returns>
        [HttpGet("applications")]
        public async Task<IActionResult> GetApplications([FromHeader] long userId)
        {
                var applications = await _authorizationService.GetApplicationsByUserRolesAsync(userId);

                if (applications == null)
                {
                    return NotFound("No applications found for the user.");
                }

                return Ok(applications);
        }

        /// <summary>
        /// Get masks based on the user ID, client ID, and menu key.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="clientId">Client ID</param>
        /// <param name="menuKey">Menu Key</param>
        /// <returns>List of masks the user has access to</returns>
        [HttpGet("masks")]
public async Task<IActionResult> GetMasks([FromQuery] long userId, [FromQuery] string clientId)
{
    if (userId <= 0 || string.IsNullOrWhiteSpace(clientId))
    {
        return BadRequest("Invalid parameters.");
    }

    try
    {
        var masks = await _authorizationService.GetMasksAsync(userId, clientId);

        if (masks == null || !masks.Any())
        {
            return NotFound("No masks found for the provided parameters.");
        }

        return Ok(masks);
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(ex.Message);
    }
    catch (UnauthorizedAccessException ex)
    {
        return Forbid();
    }
    catch (Exception ex)
    {
        return StatusCode(500, "An error occurred while processing your request.");
    }
}

    }
}
