using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.MappingProfiles;
using WebNetLibrary.BLL.Services;
using WebNetLibrary.DAL.Context;

namespace WebNetLibrary.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
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
    }
}
