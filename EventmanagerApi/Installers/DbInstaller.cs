using Microsoft.AspNetCore.Identity;
using EventmanagerApi.Data;
using EventmanagerApi.Domain;
using EventmanagerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventmanagerApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IOrganizedEventService, OrganizedEventService>();
        }
    }
}