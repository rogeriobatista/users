﻿using Users.Domain.Users.Entities;

namespace Users.Domain.Users.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public static UserDto CreateUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email
            };
        }
    }
}
