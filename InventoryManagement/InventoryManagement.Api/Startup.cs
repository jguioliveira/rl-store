using Asp.Versioning;
using InventoryManagement.Api.Requirements;
using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Handlers;
using InventoryManagement.Domain.Repositories;
using InventoryManagement.Infrastructure.Data.Configuration;
using InventoryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InventoryManagement.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddAuthentication("ApiAuth")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("ApiAuth", null);

            services.AddAuthorization(options =>
            {
                var policeBuilder = new AuthorizationPolicyBuilder();
                policeBuilder.Requirements.Add(new JwtRequirement());
                options.DefaultPolicy = policeBuilder.Build();
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version")
                );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Inventory API", Version = "v2" });

                s.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Specify the authorization token. i.e. Bearer {token}",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey, 
                    BearerFormat = "JWT"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "BearerAuth"
                       }
                      },
                      new string[] { }
                    }
                });
            });

            services.ConfigureInventoryDb(_configuration.GetConnectionString("InventoryDb"));

            services.AddScoped<IAuthorizationHandler, JwtRequirementHandler>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IHandler<CreateManufacturerCommand>, ManufacturerHandler>();
            services.AddTransient<IHandler<UpdateManufacturerCommand>, ManufacturerHandler>();
            services.AddTransient<IHandler<CreateCategoryCommand>, CategoryHandler>();
            services.AddTransient<IHandler<UpdateCategoryCommand>, CategoryHandler>();
            services.AddTransient<IHandler<CreateProductCommand>, ProductHandler>();
            services.AddTransient<IHandler<UpdateProductCommand>, ProductHandler>();
            services.AddTransient<IHandler<UpdateInventoryCommand>, ProductHandler>();
            services.AddTransient<IHandler<UpdateBookMarksCommand>, ProductHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
