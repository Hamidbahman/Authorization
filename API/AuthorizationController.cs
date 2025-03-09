using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetApplications([FromHeader] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized("Token is required.");
            }

            try
            {
                var applications = await _authorizationService.GetApplicationsByUserRolesAsync(token);

                if (applications == null || applications.Count == 0)
                {
                    return NotFound("No applications found for the user.");
                }

                return Ok(applications);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
