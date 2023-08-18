using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:FundooDB"]));
            services.AddControllers();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Register",
                    Version = "v1",
                    Description = "User Registration",
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)

            app.UseSwaggerUI(y =>
            {
                y.SwaggerEndpoint("/swagger/v1/swagger.json", "Register V1");
            });
        }
    }
}
