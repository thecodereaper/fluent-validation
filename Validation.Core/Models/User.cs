using FluentValidation;

namespace Validation.Core.Models
{
    public sealed class User
    {
        public User(string firstName, string lastName, string email, Country country)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public Country Country { get; }

        internal sealed class Validator : AbstractValidator<User>
        {
            public Validator(IValidator<Country> countryValidator)
            {
                RuleFor(user => user.FirstName).NotEmpty();
                RuleFor(user => user.LastName).NotEmpty();
                RuleFor(user => user.Email).NotNull().EmailAddress();
                RuleFor(user => user.Country).NotNull().SetValidator(countryValidator);
            }
        }
    }
}
