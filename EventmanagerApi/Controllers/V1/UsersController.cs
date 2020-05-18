using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests.UserRequests;
using EventmanagerApi.Contracts.V1.Responses.UserResponses;
using EventmanagerApi.Extensions;
using EventmanagerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers.V1
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.ApplicationUser.Get)]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetUserAsync(HttpContext.GetUserId());

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<UserResponse>(user));
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.ApplicationUser.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var user = await _userService.GetUserAsync(HttpContext.GetUserId());
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;

            var updated = await _userService.UpdateUserAsync(user);

            if (updated)
            {
                return Ok(_mapper.Map<UserResponse>(user));
            }

            return NotFound();
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.ApplicationUser.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest(new {error = "New passwords do not match"});
            }
            
            var user = await _userService.GetUserAsync(HttpContext.GetUserId());
            
            var changedPassword = await _userService.ChangePasswordAsync(user, request);

            if (changedPassword.Succeeded)
            {
                return Ok(_mapper.Map<UserResponse>(user));
            }
            
            return BadRequest(new AuthFailedResponse
            {
                Errors = changedPassword.Errors.Select(x => x.Description)
            });
        }
    }
}