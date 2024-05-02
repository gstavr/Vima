using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface IRoleRepository : IRepository<Role>
    {   
        Task<Role> UpdateAsync(Role entity);
        
    }
}
