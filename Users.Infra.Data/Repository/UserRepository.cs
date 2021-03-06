﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Users.Domain.Users.Dtos;
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

        public async Task<bool> Exist(string email)
        {
            return await _context.Set<User>().Select(x => x.Email).AnyAsync(x => x == email);
        }

        public async Task<string> Login(UserDto userDto)
        {
            return await _context.Set<User>().Where(x => x.Email == userDto.Email && x.Password == userDto.Password).Select(x => x.Email).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
