using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {   
        Task<User> UpdateAsync(User entity);
        Task<User> SaveUserRoleAsync(User entity);

    }
}
