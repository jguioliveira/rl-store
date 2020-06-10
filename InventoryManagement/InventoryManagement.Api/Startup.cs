using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Handlers;
using InventoryManagement.Domain.Repositories;
using InventoryManagement.Infrastructure.Data.Configuration;
using InventoryManagement.Infrastructure.Repositories;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Inventory API", Version = "v2" });
            });

            services.ConfigureInventoryDb(_configuration.GetConnectionString("InventoryDb"));

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseApiVersioning();
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
