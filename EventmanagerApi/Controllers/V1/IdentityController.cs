using System.Linq;
using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests.UserRequests;
using EventmanagerApi.Contracts.V1.Responses.UserResponses;
using EventmanagerApi.Extensions;
using EventmanagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers.V1
{
    [ApiController]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
        
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            var response = new AuthSuccessResponse
            {
                Token = authResponse.Token
            };

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Identity.LoggedIn)]
        public async Task<bool> LoggedIn()
        {
            try
            {
                return await _identityService.UserLoggedInAsync(HttpContext.GetUserId());
            }
            catch
            {
                return false;
            }
            
        }
    }
}