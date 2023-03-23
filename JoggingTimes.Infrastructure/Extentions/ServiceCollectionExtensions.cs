//using FluentValidation.AspNetCore;
//using Jogging_Times.Core.Helpers;
//using Jogging_Times.Core.Models;
//using Jogging_Times.Core.Services;
//using Jogging_Times.Core.Validation;
//using JoggingTimes.Infrastructure.DataContext;
//using JoggingTimes.Infrastructure.Extentions;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Jogging_Times.Core.Extentions
//{
//    public static class ServiceCollectionExtensions
//    {
//        public static void ConfigureCors(this IServiceCollection services)
//        {
//            services.AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader());
//            });
//        }

//        public static void ConfigureSqlServer(this IServiceCollection services)
//        {
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(AppSettings.Current.ConnectionStrings.DBConection));
//        }

//        public static void ConfigureIdentity(this IServiceCollection services)
//        {
//            services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();
//        }
//        public static void ConfigureFluentValidation(this IServiceCollection services)
//        {
//            services.AddFluentValidation(option =>
//                option.RegisterValidatorsFromAssemblyContaining<RegisterUserValidation>());

//        }

//        public static void AddServices(this IServiceCollection services)
//        {
//            #region Services
//            //services.AddScoped<IAuthenticationService, AuthenticationService>();
//            #endregion Services
//        }
//    }
//}
