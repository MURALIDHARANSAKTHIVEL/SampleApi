using System;
using System.IO;
using ProjectName.Contract.Classes;
using ProjectName.Gateway.Utils.Extensions;
using ProjectName.Shared.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

using ProjectName.Contract.Orchestrations;
using ProjectName.Contract.Repositories;

using ProjectName.Business.Orchestrations;
using ProjectName.Business.Repositories;

namespace ProjectName.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });
            services.Configure<DbConfig>(Configuration.GetSection(Constants.AppSettings).GetSection(Constants.DbConfiguration));
            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton<IStudentOrchestration, StudentOrchestration>();
            
            services.RegisterServices();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowAnyOrigin() // .WithOrigins("http://www.contoso.com")
                                          .AllowCredentials()

            );
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}



// DO NOT REFER THIS BLOCK

// services.AuthenticationServices(Configuration);
// services.AuthorizationServices();
//services.AddSingleton<ILoggerManager, LoggerManager>();


// app.Use(async (context, next) =>
// {
//     context.Response.Headers.Add("X-Frame-Options", String.Format("ALLOW-FROM {0}", clientConfig.LoginUrl));
//     context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
//     context.Response.Headers.Add("X-Xss-Protection", "1;mode=block");
//     await next();
// });

//app.UseAuthentication();
//app.UseMiddleware(typeof(AppExceptionMiddleware));
