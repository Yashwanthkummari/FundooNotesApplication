using BusinessLayer.Interface;
using BusinessLayer.Services;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            services.AddTransient<INotesBusiness, NotesBusiness>();
            services.AddTransient<INotesRepo, NotesRepo>();
            services.AddTransient<FileService, FileService>();

            IConfigurationSection configurationSection = Configuration.GetSection("CloudinarySettings");
            Account account = new Account(
                configurationSection["my_cloud_name"],
                configurationSection["my_api_key"],
                configurationSection["my_api_secret"]
                );
            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Register",
                    Version = "v1",
                    Description = "User Registration",
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                { securitySchema, new[] { "Bearer" } }
                });
            });
            // Configure JWT authentication
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                Account cloudinaryAccount = new Account(
                                           "dfjtq6pm0",//CloudinaryName
                                           "891343748812318",//Cloudinary API key
                                           "tzwsJF6gJL8gQJt5qvw7LP4640E");//Cloudinary API Secret
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();    

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
