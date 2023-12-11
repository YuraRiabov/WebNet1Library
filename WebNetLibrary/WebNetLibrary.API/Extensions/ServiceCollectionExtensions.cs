using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.MappingProfiles;
using WebNetLibrary.BLL.Services;
using WebNetLibrary.BLL.Services.Options;
using WebNetLibrary.DAL.Context;

namespace WebNetLibrary.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddLibraryContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("LibraryDBConnection");
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(LibraryContext).Assembly.GetName().Name)));
        }
        
        
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(BookProfile)));
        }

        public static void AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            var auth0Section = configuration.GetRequiredSection("Auth0");
            services.Configure<Auth0Options>(options =>
            {
                auth0Section.Bind(options);
                options.ClientSecret = configuration["LibraryAuthSecret"]!;
            });
            services.AddHttpClient<IAuthorizationService, AuthorizationService>();
        }
    }
}
