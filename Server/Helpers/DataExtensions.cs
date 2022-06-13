using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Helpers;

public static class DataExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MyContext>();
        db.Database.Migrate();
        return app;
    }
}