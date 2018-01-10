using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinArbiter
{
    public class Startup
    {
        //Set-up configuration for dependancy injection
        public IConfiguration Configuration { get; }

        //Inject Configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Add middleware
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        //Configure environment with middleware and default behaviors
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
