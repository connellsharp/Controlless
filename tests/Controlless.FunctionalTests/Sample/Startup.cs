using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Controlless.FunctionalTests.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRequestModelBinders(typeof(Startup));
            services.AddRequestObjects();

            services.AddSingleton(typeof(IRequestHandler<TestRequest>), typeof(TestRequestHandler));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapBinders();
                endpoints.MapControllers();
            });
        }
    }
}
