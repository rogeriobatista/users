using Users.Domain.Users.Entities;
using Xunit;

namespace Users.Tests.Domain.Users
{
    public class UserTest
    {
        private readonly string _name;
        private readonly string _nameInvalid;
        private readonly string _email;
        private readonly string _emailInvalid;
        private readonly string _password;
        private readonly string _passwordInvalid;

        public UserTest()
        {
            _name = Faker.Name.FullName();
            _nameInvalid = null;
            _email = Faker.Internet.Email();
            _emailInvalid = null;
            _password = Faker.Lorem.GetFirstWord();
            _passwordInvalid = null;
        }

        [Fact]
        public void ShouldCreteUser()
        {
            User user = new User(_email, _password, _name);

            Assert.True(user.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutEmail()
        {
            User user = new User(_emailInvalid, _password, _name);

            Assert.False(user.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutPassword()
        {
            User user = new User(_email, _passwordInvalid, _name);

            Assert.False(user.Validate());
        }

        [Fact]
        public void ShouldNotCreateUserWithoutName()
        {
            User user = new User(_email, _password, _nameInvalid);

            Assert.False(user.Validate());
        }

        [Fact]
        public void ShouldCreateUserWithSpecificEmail()
        {
            User user = new User(_email, _password, _name);

            Assert.Equal(user.Email, _email);
        }

        [Fact]
        public void ShouldCreateUserWithSpecificPassword()
        {
            User user = new User(_email, _password, _name);

            Assert.Equal(user.Password, _password);
        }

        [Fact]
        public void ShouldCreateUserWithSpecificName()
        {
            User user = new User(_email, _password, _name);

            Assert.Equal(user.Name, _name);
        }
    }
}
