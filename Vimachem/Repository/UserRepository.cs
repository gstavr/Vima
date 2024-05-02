

using Vimachem.Data;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;


namespace Vimachem.Repository
{
    public class UserRepository(ApplicationDbContext db) : Repository<User>(db), IUserRepository
    {
        public async Task<User> SaveUserRoleAsync(User entity)
        {

            await db.UserRoles.AddAsync(new UserRole() {UserId = entity.Id, RoleId = 2});
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {   
            db.User.Update(entity);
            await db.SaveChangesAsync();

            return entity;

        }
    }
}
