using FluentValidation;
using Users.Generics.Domain;

namespace Users.Domain.Users.Entities
{
    public class User : Entity<long, User>
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }

        protected User() { }

        public User(string username,
                    string password,
                    string name)
        {
            Username = username;
            Password = password;
            Name = name;
        }

        public override bool Validate()
        {
            RuleFor(_ => _.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(_ => _.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void UpdateUsername(string username)
        {
            Username = username;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
