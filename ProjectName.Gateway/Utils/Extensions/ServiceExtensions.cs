using System.Text;
using ProjectName.Business.Orchestrations;
using ProjectName.Business.Repositories;
using ProjectName.Contract.Classes;
using ProjectName.Contract.Data;
using ProjectName.Contract.Orchestrations;
using ProjectName.Contract.Repositories;
using ProjectName.Data.Implementations;
using ProjectName.Logging.Contracts;
using ProjectName.Logging.Implementations;
using ProjectName.Shared.Utils.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace ProjectName.Gateway.Utils.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<AccessToken>();
            services.AddSingleton<Hashing>();

            //Business 

            //services.AddSingleton<IStudentRepository, StudentRepository>();
            //services.AddSingleton<IStudentOrchestration, StudentOrchestration>();

            //Data

            services.AddSingleton<IStudentDataAccess, StudentDataAccess>();

            // services.AddSingleton<IAuthorizationHandler, SessionHandler>();

            return services;
        }

        public static IServiceCollection AuthorizationServices(
            this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                // sample policies
                options.AddPolicy("CreateStudentPolicy",
                    policy => policy.RequireClaim("ViewUser")
                    //.Requirements.Add(new SessonRequirement())
                );
            });

            return services;
        }

        public static IServiceCollection AuthenticationServices(
            this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "ProjectName.com",
                        ValidAudience = "ProjectName.com",
                        IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetSection(Constants.SecurityConfig).GetSection(Constants.JwtKey).Value))

                    };

                });

            return services;
        }
    }
}