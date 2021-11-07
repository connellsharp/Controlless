using System.IO;
using Controlless.FunctionalTests.Sample;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Controlless.FunctionalTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                            .ConfigureWebHostDefaults(x =>
                            {
                                x.UseStartup<Startup>().UseTestServer();
                            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // This is needed to get round an issue running these tests
            // https://github.com/dotnet/aspnetcore/issues/17707#issuecomment-609061917
            
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            return base.CreateHost(builder);
        }
    }
}
