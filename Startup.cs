using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AdminService.Middleware;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.Extension;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EzilineTask
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
            services.AddDbContext<DataContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            IdentityBuilder builder=services.AddIdentityCore<User>();

            builder =new IdentityBuilder(builder.UserType,typeof(Role),builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddTransient<SeedData>();
            services.AddAutoMapper();
            services.AddSignalR();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>{
                    options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience=false,
                        ClockSkew=TimeSpan.Zero
                    };});
            services.AddAuthorization(options=>
            {
                     options.AddPolicy("RequireAdmin",policy=>policy.RequireRole("Admin"));  
                     options.AddPolicy("RequireMember",policy=>policy.RequireRole("Admin","Member","Moderator","Vip"));  
            });

            services.AddCors();        
            services.AddMvc(options=>
            {
                    var policy =new AuthorizationPolicyBuilder()
                               .RequireAuthenticatedUser()
                               .Build();
                               options.Filters.Add(new AuthorizeFilter(policy));
                         
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
              .AddJsonOptions(options=>
              {
                           options.SerializerSettings
                                  .ReferenceLoopHandling=Newtonsoft.Json
                                  .ReferenceLoopHandling
                                  .Ignore;
              });

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,SeedData seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(Builder=>
                {
                   
                       Builder.Run(async context=>
                       {
                              
                              context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                              var error =context.Features.Get<IExceptionHandlerFeature>();
                              if(error!=null)
                              {
                                    context.Response.AddApplicationError(error.Error.Message);
  
                                    var ExceptionDetails=context.Features.Get<IExceptionHandlerPathFeature>();

                                    await context.Response.WriteAsync(ExceptionDetails.Error.Message); 
                              }
                       });
                });
                // app.UseHsts();
            }
            // app.UseHttpsRedirection();
            seeder.SeedingData();
            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());
            app.UseAuthentication();
            app.UseSignalR(routes=>
            {
                
                routes.MapHub<BroadCast>("/notify");                
            });
            app.UseMvc();
        }
    }
}
