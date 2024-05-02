using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {   
        Task<UserRole> UpdateAsync(UserRole entity);
        
    }
}
