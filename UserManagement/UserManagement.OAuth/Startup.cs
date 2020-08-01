using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;
using UserManagement.OAuth.Configuration;

namespace UserManagement.OAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection("Settings").Get<Settings>();

            var secret = Encoding.UTF8.GetBytes(settings.OAuthSettings.Secret);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = settings.OAuthSettings.Issuer,
                        ValidAudience = settings.OAuthSettings.Audience,
                        //TODO: Testar
                        //ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret)
                    };
                });

            services.AddControllersWithViews();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserTokenRepository, UserTokenRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.ConfigureMongoDb(options =>
            {
                options.ConnectionString = settings.DatabaseSettings.ConnectionString;
                options.DatabaseName = settings.DatabaseSettings.DatabaseName;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
