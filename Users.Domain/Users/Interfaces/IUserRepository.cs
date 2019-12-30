using System.Threading.Tasks;
using Users.Domain.Users.Entities;
using Users.Generics.Interfaces;

namespace Users.Domain.Users.Interfaces
{
    public interface IUserRepository : IBaseRepository<long, User>
    {
        Task<User> GetByIdAsync(long id);
    }
}
