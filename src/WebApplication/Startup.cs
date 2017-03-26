using WebApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IBM.EntityFrameworkCore;

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Blogs}/{action=Index}/{id?}");
        });
    }
    public void ConfigureServices(IServiceCollection services)
    {
        var connection = "DATABASE=dbname;SERVER=hostname:sslport;UID=username;PWD=password; Security = SSL; ";
         services.AddDbContext<BloggingContext>(options => options.UseDb2(connection));
        services.AddMvc();
    }
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
                         .AddCommandLine(args)
                         .Build();

        var host = new WebHostBuilder()
                       .UseKestrel()
                       .UseConfiguration(config)
                       //.UseStartup()
                       .Build();
        host.Run();
    }
}
