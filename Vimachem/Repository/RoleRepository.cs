

using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;


namespace Vimachem.Repository
{
    public class RoleRepository(ApplicationDbContext db) : Repository<Role>(db), IRoleRepository
    {
        public async Task<Role> UpdateAsync(Role entity)
        {   
            db.Role.Update(entity);
            await db.SaveChangesAsync();

            return entity;

        }
    }
}
