using System.Threading.Tasks;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}