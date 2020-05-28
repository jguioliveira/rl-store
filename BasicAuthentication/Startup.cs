using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicAuthentication
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
            services.AddMvc();

            services.AddAuthentication(options => {
                options.DefaultScheme = "BasicAuth";
                options.RequireAuthenticatedSignIn = true;
            })
            .AddCookie("BasicAuth", options => {
                options.Cookie.Name = "BasicAuth.Cookie";
                options.LoginPath = "/Home/SignIn";
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.ConfigureMongoDb(options =>
            {
                options.ConnectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                options.DatabaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
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
