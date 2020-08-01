using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

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

            services
                .AddAuthentication(config =>
                {
                    //Check the cookie to confirm that we are authenticated
                    config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //When we sign in, we will deal out a cookie
                    config.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //use this to check if we are allowed to do something
                    config.DefaultChallengeScheme = "UserManagementOAuth";
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    //options.ExpireTimeSpan = TimeSpan.FromTicks(DateTime.Now.AddMinutes(15).Ticks);
                })
                .AddOAuth("UserManagementOAuth", options =>
                {
                    options.CallbackPath = "/oauth/callback";
                    options.AuthorizationEndpoint = "https://localhost:44398/oauth/authenticate";
                    options.TokenEndpoint = "https://localhost:44398/oauth/token";
                    options.ClientId = "UserManagement";
                    options.ClientSecret = "19F6CEAB-4A5C-4555-8E39-A355EFDB357C";
                    options.SaveTokens = true;
                    options.Events.OnTicketReceived = (context) =>
                    {
                        context.Properties.ExpiresUtc = DateTimeOffset.Parse(context.Properties.Items[".Token.expires_at"]);
                        return Task.CompletedTask;
                    };
                });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "BasicAuth";
            //    options.RequireAuthenticatedSignIn = true;
            //})
            //.AddCookie("BasicAuth", options =>
            //{
            //    options.Cookie.Name = "BasicAuth.Cookie";
            //    options.LoginPath = "/Home/SignIn";
            //});

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
