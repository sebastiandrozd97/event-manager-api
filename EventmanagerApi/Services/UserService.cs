using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1.Requests.UserRequests;
using EventmanagerApi.Data;
using EventmanagerApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventmanagerApi.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(DataContext dataContext, UserManager<ApplicationUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        
        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _dataContext.ApplicationUsers
                .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser userToUpdate)
        {
            _dataContext.ApplicationUsers.Update(userToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser userToUpdate, ChangePasswordRequest request)
        {
            var changedPassword = await _userManager.ChangePasswordAsync(userToUpdate, request.CurrentPassword, request.NewPassword);

            return changedPassword;
        }
    }
}