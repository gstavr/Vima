
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;


namespace Vimachem.Repository
{
    public class CategoryRepository(ApplicationDbContext db) : Repository<Category>(db), ICategoryRepository
    {
        public async Task<Category> UpdateAsync(Category category)
        {
            category.UpdatedDate = DateTime.Now;
            db.Category.Update(category);
            await db.SaveChangesAsync();

            return category;

        }
    }
}
