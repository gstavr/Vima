using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkDockerSqlServer.Data;

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
                //_context.Seed(app);
            }


            // TODO : For Better Testing and Seeding Data next time :D
            //using var scope1 = app.Services.CreateScope();
            //var testContext = scope1.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //testContext.Database.EnsureCreated();
            //testContext.Seed();
        }

       
    }
}
