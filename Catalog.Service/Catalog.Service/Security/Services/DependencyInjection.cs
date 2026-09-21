
using Catalog.Service.Security.Application.Contracts;
using Catalog.Service.Security.Application.Users;
using Catalog.Service.Security.Application.Users.Contracts;
using Catalog.Service.Security.Services.Domain.Roles;
using Catalog.Service.Security.Services.Domain.Users;
using Catalog.Service.Security.Services.Infrustrctre.Persistance;
using Catalog.Service.Security.Services.Infrustrctre.Services.Authentication;
using Catalog.Service.Security.Services.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Service.Security;

public static class DependencyInjection
{
     public static void AddSecurityServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {

        services.AddDbContext<SecuirityContext>(setup =>
        {
            if (env.IsDevelopment())
            {
                var cnnstr = configuration.GetConnectionString("Security");
                setup.UseSqlServer(cnnstr);
            }

        })
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<SecuirityContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IIdentityService, IdentityService>();

    
        


    }
}