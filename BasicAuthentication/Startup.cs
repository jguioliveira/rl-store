using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.Infrastructure.Context;
using BasicAuthentication.Infrastructure.Repositories;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));

            services.AddAuthentication(options => {
                options.DefaultScheme = "BasicAuth";
                options.RequireAuthenticatedSignIn = true;
            })
            .AddCookie("BasicAuth", options => {
                options.Cookie.Name = "BasicAuth.Cookie";
                options.LoginPath = "/Home/SignIn";
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserDataContext, UserDataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserDataContext userDataContext)
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

            UserDataContextSeed.SeedAsync(userDataContext)
                .Wait();
        }
    }
}
