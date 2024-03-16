using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using rs_backends_api_2019.Business.Extensions;
using rs_backends_api_2019.Business.Helpers.LoggerHelper.Filters.Routes;
using rs_backends_api_2019.Business.Helpers.LoggerHelper.Middleware.Routes;
using Microsoft.EntityFrameworkCore;
using rs_backends_api_2019.DAL.UnitOfWork;
using rs_backends_api_2019.DAL.Interfaces;
using rs_backends_api_2019.Presentation.ServicesExtension;

namespace rs_backends_api_2019.Presentation
{
    public class Startup
    {
        public IHostingEnvironment Env { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Env = env;

            var builderConfig = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            Configuration = builderConfig.Build();

            Console.WriteLine("Evnironment Name : {0}", env.EnvironmentName);
            Console.WriteLine("FixedURL : {0}", Configuration["FixedURL"]);

            rs_backends_api_2019.Business.Extensions.DateTimeExtension.SetDateEnv();

            //Set NLog.config
            env.ConfigureNLog("NLog.config");
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //*** Inject Logger to Middleware
            services.AddScoped<AuthenticationFilter>();
            services.AddMvc(filter =>
            {
                filter.Filters.Add(typeof(AuthenticationFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(    // แก้ปัญหาการ Convert Json (Newtonsoft.Json.JsonConvert.SerializeObject) เกี่ยวกับ Relationship
               options => options.SerializerSettings.ReferenceLoopHandling =
               Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddDbContext<dot_exam_guide_v2Context>(options =>
            //  options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = "RS2019_Frontend";
                cfg.DefaultChallengeScheme = "RS2019_Frontend";
            })
            .AddJwtBearer("RS2019_Frontend", cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Base64UrlTextEncoder.Decode(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.FromMinutes(10800)
                };
            });

            //*** Injection UnitOfWork.
            //services.AddScoped<IUnitOfWork, UnitOfWork<dot_exam_guide_v2Context>>();

            //call this in case you need aspnet-user-authtype/aspnet-user-identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //=>Inject services
            services.AddServices_v1_0();
            services.AddServices_v1_1();

            //=>Inject helpers
            services.AddHelpers(Env);

            // Enable Cross-Origin Requests.
            services.AddCors();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //Add versioning
            services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            //=> Start : NLog
            //Add Logger Proveider using NLog
            loggerFactory.AddNLog();

            //Add NLog Extension
            app.AddNLogWeb();

            //Set Variable NLog
            LogManager.Configuration.Variables["userName"] = "-";
            LogManager.Configuration.Variables["strQueryString"] = "QueryString: ";
            LogManager.Configuration.Variables["strFormData"] = "FormData: ";
            //=> Stop : NLog

            //*** Injection to Extension
            rs_backends_api_2019.Business.Global.GlobalInjections.configure(
                app
            );

            rs_backends_api_2019.DAL.EFExtensions.PaginationOrderedExtension.configure(
                app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(),
                app.ApplicationServices.GetRequiredService<IConfiguration>()
              );

            rs_backends_api_2019.DAL.EFExtensions.PaginationExtension.configure(
               app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(),
               app.ApplicationServices.GetRequiredService<IConfiguration>()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Add middleware authen
            app.UseAuthentication();

            app.UseStaticFiles();

            //=>Check folder and create
            var storage = Path.Combine((!string.IsNullOrEmpty(Configuration["Storage"]) ? Configuration["Storage"] : env.ContentRootPath), "Storage/app");

            if (!System.IO.Directory.Exists(storage))
            {
                System.IO.Directory.CreateDirectory(storage);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine((!string.IsNullOrEmpty(Configuration["Storage"]) ? Configuration["Storage"] : env.ContentRootPath), "Storage/app")),
                RequestPath = Configuration["FixedUploadsPath"]
            });

            app.UseMvc(routes =>
            {
                // For routes missing
                routes.Routes.Add(new NotFoundMiddleware(routes.DefaultHandler, loggerFactory));
            });


            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader());
        }
    }
}
