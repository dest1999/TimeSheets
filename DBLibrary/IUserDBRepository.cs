using System.Threading.Tasks;

namespace DBLibrary
{
    public interface IUserDBRepository
    {
        Task Create(User user);
        Task<User> Get(int id);
        Task Update(User user);
        Task Delete(int id);

    }
}
