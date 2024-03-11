using Devlivery.Infraestrutura;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Devlivery.API.Configuration
{
    public static class IdentityConfig
    { /*
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AppDbContext>(options =>
                                                    //  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
           
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMessagesPtBr>()
                //.AddErrorDescriber<IdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            return services;

        }*/
    }
}
