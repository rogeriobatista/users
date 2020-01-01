using FluentValidation;
using Users.Generics.Domain;

namespace Users.Domain.Users.Entities
{
    public class User : Entity<long, User>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }

        protected User() { }

        public User(string email,
                    string password,
                    string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public override bool Validate()
        {
            RuleFor(_ => _.Email)
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

        public void UpdateEmail(string email)
        {
            Email = email;
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
