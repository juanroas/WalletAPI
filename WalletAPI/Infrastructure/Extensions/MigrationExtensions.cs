using WalletAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WalletAPI.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            
            using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            dbContext.Database.Migrate();

        }
    }
}
