using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests;
using EventmanagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers.V1
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);
            
            
            return Ok();
        }
    }
}