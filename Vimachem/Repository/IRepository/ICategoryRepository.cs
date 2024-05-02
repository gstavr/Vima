using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {   
        Task<Category> UpdateAsync(Category category);
        
    }
}
