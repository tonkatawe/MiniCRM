using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MiniCRM.Web.Areas.Identity.IdentityHostingStartup))]

namespace MiniCRM.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
