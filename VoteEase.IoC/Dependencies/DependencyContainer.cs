using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoteEase.Application.Data;
using VoteEase.Application.Error;
using VoteEase.Application.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Data.Context;
using VoteEase.Data_Access.Implementation;
using VoteEase.Data_Access.Interface;
using VoteEase.Infrastructure.Error;
using VoteEase.Infrastructure.Votings;

namespace VoteEase.IoC.Dependencies
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable(LookupKey.DotNetEnvironmentKey);

            #region DB CONNECTION
            //VoteEase context
            try
            {
                if (env == "Production")
                {
                    services.AddDbContext<VoteEaseDbContext>(options =>
                    {
                        options.UseMySql(connectionString: Environment.GetEnvironmentVariable("WebApiDatabase_Production"), serverVersion: ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("WebApiDatabase_Production")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
                else if (env == "Staging")
                {
                    services.AddDbContext<VoteEaseDbContext>(options =>
                    {
                        options.UseMySql(connectionString: Environment.GetEnvironmentVariable("WebApiDatabase_Staging"), serverVersion: ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("WebApiDatabase_Staging")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
                else if (env == "Development")
                {
                    services.AddDbContext<VoteEaseDbContext>(options =>
                    {
                        options.UseMySql(connectionString: configuration.GetConnectionString("WebApiDatabase_Development"), serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("WebApiDatabase_Development")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
            }
            catch (Exception)
            {

            }

            //Application context
            try
            {
                if (env == "Production")
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseMySql(connectionString: Environment.GetEnvironmentVariable("WebApiDatabase_Production"), serverVersion: ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("WebApiDatabase_Production")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
                else if (env == "Staging")
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseMySql(connectionString: Environment.GetEnvironmentVariable("WebApiDatabase_Staging"), serverVersion: ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("WebApiDatabase_Staging")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
                else if (env == "Development")
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseMySql(connectionString: configuration.GetConnectionString("WebApiDatabase_Development"), serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("WebApiDatabase_Development")), mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                    });
                }
            }
            catch (Exception)
            {

            }
            #endregion

            #region REGISTER SERVICES   
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<INominationService, NominationService>();
            services.AddScoped<IAccreditedMemberService, AccreditedMemberService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddTransient<IErrorService, ErrorService>();
            #endregion

            //#region REGISTER HANGFIRE
            ////Add Hangfire Services
            //services.AddHangfire(configuration => configuration
            //                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //                    .UseSimpleAssemblyNameTypeSerializer()
            //                    .UseRecommendedSerializerSettings()
            //                    .UseSerilogLogProvider());



            //services.AddHangfireServer();
            //#endregion

            #region IDENTITY
            services.AddIdentity<VoteEaseUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            return services;
        }
    }
}
