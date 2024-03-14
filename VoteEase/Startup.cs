using VoteEase.IoC.Dependencies;

namespace VoteEase.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            configuration = Configuration;
        }

        public IConfiguration Configuration { get; }
        public void IConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);
        }
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            //if ()
            //{

            //}
        }
    }
}
