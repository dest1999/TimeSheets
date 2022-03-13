using AutoMapper;
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
        private Mapper mapper;
        public UserDBRepository(SQLiteDBContext context)
        {
            db = context;
            MapperConfiguration mapperConfiguration = new(cfg => cfg.CreateMap<UserDTO, User>());
            mapper = new Mapper(mapperConfiguration);
        }
        public async Task Create(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            user.Id = db.Users.Count() + 1;
            db.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            return await db.FindAsync <User>(id);
        }

        public User GetByLogin(string login)
        {
            return db.Users.FirstOrDefault(x => x.Login == login);
        }
        public User GetByToken(string token)
        {
            return db.Users.FirstOrDefault(x => x.Token == token);
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
