

using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;


namespace Vimachem.Repository
{
    public class UserRoleRepository(ApplicationDbContext db) : Repository<UserRole>(db), IUserRoleRepository
    {
        public async Task<UserRole> UpdateAsync(UserRole entity)
        {
            entity.UpdatedDate = DateTime.Now;
            db.UserRoles.Update(entity);
            await db.SaveChangesAsync();

            return entity;

        }
    }
}
