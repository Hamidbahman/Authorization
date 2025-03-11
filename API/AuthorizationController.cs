using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/access")]
    public class UserAccessController : ControllerBase
    {
        private readonly UserAccessService _userAccessService;

        public UserAccessController(UserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }

        // POST api/access (Get User Access with Mask IDs)
        [HttpPost("user-access")]
        public async Task<IActionResult> GetUserAccess([FromBody] UserAccessRequest request)
        {
            try
            {
                // Call the service to get the user access
                var result = await _userAccessService.GetUserAccessAsync(request.UserId, request.ClientId);
                return Ok(result);  // Return the list of mask IDs as a response
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });  // Return the error message in case of an exception
            }
        }

        // POST api/access/applications (Get Applications for the User)
        [HttpPost("user-applications")]
        public async Task<IActionResult> GetUserApplications([FromBody] UserApplicationsRequest request)
        {
            try
            {
                // Call the service to get the applications based on userId
                var result = await _userAccessService.GetUserApplicationsAsync(request.UserId);
                return Ok(result);  // Return the list of applications associated with the user
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });  // Return the error message in case of an exception
            }
        }
    }

    // Model for the user access request body
    public class UserAccessRequest
    {
        public long UserId { get; set; }
        public string ClientId { get; set; }
    }

    // Model for the user applications request body
    public class UserApplicationsRequest
    {
        public long UserId { get; set; }
    }
}
