using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EventmanagerApi.Data;
using EventmanagerApi.Domain;
using EventmanagerApi.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EventmanagerApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly DataContext _context;
        
        public IdentityService(UserManager<ApplicationUser> userManager, JwtOptions jwtOptions, DataContext context)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _context = context;
        }
        
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            }

            var newUser = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FirstName = "User"
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            
            return GenerateAuthenticationResultForUserAsync(newUser);
        }
        
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User does not exist"}
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User/password combination is wrong"}
                };
            }
            
            return GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<bool> UserLoggedInAsync(string userId)
        {
            var user = await _context.ApplicationUsers
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                return true;
            }

            return false;
        }

//        private ClaimsPrincipal GetPrincipalFromToken(string token)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//
//            try
//            {
//                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
//                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
//                {
//                    return null;
//                }
//
//                return principal;
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
//        {
//            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
//                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
//                       StringComparison.InvariantCultureIgnoreCase);
//        }

        private AuthenticationResult GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtOptions.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}