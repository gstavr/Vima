

namespace Vimachem.Data
{
    public static class Migrations
    {
        public static void RunMigrations(WebApplication app)
        {   
            using (var scope = app.Services.CreateScope())
            {   
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                _context.Database.EnsureCreated();
            }
        }
       
    }
}
