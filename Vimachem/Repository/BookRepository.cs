
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;


namespace Vimachem.Repository
{
    public class BookRepository(ApplicationDbContext db) : Repository<Book>(db), IBookRepository
    {
        public async Task<Book> UpdateAsync(Book entity)
        {
            entity.UpdatedDate = DateTime.Now;
            db.Book.Update(entity);
            await db.SaveChangesAsync();

            return entity;

        }
    }
}
