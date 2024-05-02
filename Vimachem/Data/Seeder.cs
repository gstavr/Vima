using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Vimachem.Data;
using Vimachem.Models.Domain;

namespace WebApiEntityFrameworkDockerSqlServer.Data
{
    public static class Seeder
    {
        
        // This is purely for a demo, don't anything like this in a real application!
        public static void Seed(this ApplicationDbContext dbContext, WebApplication app)
        {

            //using (var scope = app.Services.CreateScope())
            //{
            //    //var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    //db.Database.EnsureCreated();
            //    //db.Database.Migrate();
            //}

           // if (!dbContext.Category.Any())
           // {
           //     Fixture fixture = new Fixture();
           //     fixture.Customize<Category>(category => category.Without(p => p.UpdatedDate));
           //     //--- The next two lines add 100 rows to your database
           //     List<Category> category = fixture.CreateMany<Category>(100).ToList();
           //     dbContext.AddRange(category);
           //     dbContext.SaveChanges();
           //}
        }
    }
}