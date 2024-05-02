using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {   
        Task<Book> UpdateAsync(Book entity);
        
    }
}
