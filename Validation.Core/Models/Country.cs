using FluentValidation;

namespace Validation.Core.Models
{
    public sealed class Country
    {
        public Country(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }
        public string Name { get; }

        internal sealed class Validator : AbstractValidator<Country>
        {
            public Validator()
            {
                RuleFor(country => country.Code).NotNull().Length(3);
                RuleFor(country => country.Name).NotEmpty();
            }
        }
    }
}
