using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebNetLibrary.DAL.Context;

namespace WebNetLibrary.API.Tests.Integration.Setup;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<LibraryContext>));

            services.Remove(descriptor);

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseInMemoryDatabase("TestDB");
            });
        });
    }
}