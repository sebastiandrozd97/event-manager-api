using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1.Requests.UserRequests;
using EventmanagerApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace EventmanagerApi.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string userId);

        Task<bool> UpdateUserAsync(ApplicationUser userToUpdate);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser userToUpdate, ChangePasswordRequest request);
    }
}