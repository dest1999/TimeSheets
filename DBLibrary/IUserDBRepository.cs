using System.Threading.Tasks;

namespace DBLibrary
{
    public interface IUserDBRepository
    {
        Task Create(UserDTO user);
        Task<User> Get(int id);
        Task Update(User user);
        Task Delete(int id);
        User GetByLogin(string login);
        User GetByToken(string token);
    }
}
