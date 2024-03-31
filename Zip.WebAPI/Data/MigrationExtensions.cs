using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Zip.WebAPI.Data;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var zipPayContext = scope.ServiceProvider.GetRequiredService<ZipPayContext>();
        
        zipPayContext.Database.Migrate();
        SeedData.SeedData.Seed(zipPayContext);
    }
}