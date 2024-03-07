using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VG.Serilog.Sinks.EntityFrameworkCore;
using VoteEase.Application.Error;
using VoteEase.Application.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Data.Context;
using VoteEase.Infrastructure.Error;
using VoteEase.Infrastructure.Votings;

namespace VoteEase.IoC.Dependencies
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable(LookupKey.DotNetEnvironmentKey);

            #region DB Connection
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

            //Log Context
            try
            {
                if (env == "Production")
                {
                    services.AddDbContext<LogDbContext>(options =>
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
                    services.AddDbContext<LogDbContext>(options =>
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
                    services.AddDbContext<LogDbContext>(options =>
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
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<INominationService, NominationService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IErrorService, ErrorService>();
            #endregion

            //Add Hangfire Services
            services.AddHangfire(configuration => configuration
                                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                .UseSimpleAssemblyNameTypeSerializer()
                                .UseRecommendedSerializerSettings()
                                .UseSerilogLogProvider());

            services.AddHangfireServer();


            return services;
        }
    }
}
