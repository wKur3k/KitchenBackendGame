using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using SimpleBackendGame.Models.Validators;
using SimpleBackendGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackendGame
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
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddControllers().AddFluentValidation();
            services.AddDbContext<GameDbContext>();
            services.AddScoped<GameDbSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameDbSeeder seeder)
        {
            app.UseCors("FrontendClient");

            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
