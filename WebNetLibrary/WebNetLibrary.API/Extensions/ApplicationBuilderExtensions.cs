using Microsoft.EntityFrameworkCore;
using WebNetLibrary.DAL.Context;

namespace WebNetLibrary.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseLibraryContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<LibraryContext>();
            context?.Database.Migrate();
        }
    }
}
