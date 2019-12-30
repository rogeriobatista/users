using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Users.Domain.Users.Entities;
using Users.Domain.Users.Interfaces;
using Users.Generics.Repository;
using Users.Infra.Data.Context;

namespace Users.Infra.Data.Repository
{
    public class UserRepository : BaseRepository<long, User>, IUserRepository
    {
        private readonly UsersDbContext _context;
        public UserRepository(UsersDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
