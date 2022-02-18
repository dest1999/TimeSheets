using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class UserDBRepository : IUserDBRepository
    {
        private SQLiteDBContext db;
        public UserDBRepository(SQLiteDBContext context)
        {
            db = context;
        }
        public async Task Create(User entity)
        {
            db.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            return await db.FindAsync <User>(id);
        }

        public async Task Update(User entity)
        {
            var user = Get(entity.Id).Result;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.Company = entity.Company;
            user.Age = entity.Age;
            user.IsDeleted = entity.IsDeleted;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = Get(id).Result;
            user.IsDeleted = true;
            await db.SaveChangesAsync();
        }
    }
}
