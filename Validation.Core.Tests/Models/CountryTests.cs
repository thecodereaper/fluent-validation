using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Validation.Core.Models;
using Xunit;

namespace Validation.Core.Tests.Models
{
    public sealed class CountryTests
    {
        private readonly AbstractValidator<Country> _countryValidator;
        private readonly IFixture _fixture;

        public CountryTests()
        {
            _fixture = new Fixture();
            _countryValidator = new Country.Validator();
        }

        [Fact]
        public void Country_WhenConstructingANewCountry_ShouldCreateModelInstance()
        {
            string code = _fixture.Create<string>();
            string name = _fixture.Create<string>();

            Country country = new Country(code, name);

            country.Code.Should().Be(code);
            country.Name.Should().Be(name);
        }

        [Fact]
        public void Validator_WhenCountryCodeIsNotSpecified_ShouldHaveValidationFailure()
        {
            string code = null;
            string name = _fixture.Create<string>();
            Country country = new Country(code, name);

            _countryValidator.ShouldHaveValidationErrorFor(c => c.Code, country);
        }

        [Fact]
        public void Validator_WhenCountryCodeIsNotThreeCharacters_ShouldHaveValidationFailure()
        {
            const string code = "CA";
            string name = _fixture.Create<string>();
            Country country = new Country(code, name);

            _countryValidator.ShouldHaveValidationErrorFor(c => c.Code, country);
        }

        [Fact]
        public void Validator_WhenCountryNameIsNull_ShouldHaveValidationFailure()
        {
            const string code = "CAN";
            string name = null;
            Country country = new Country(code, name);

            _countryValidator.ShouldHaveValidationErrorFor(c => c.Name, country);
        }

        [Fact]
        public void Validator_WhenCountryNameIsNotSpecified_ShouldHaveValidationFailure()
        {
            const string code = "CAN";
            string name = string.Empty;
            Country country = new Country(code, name);

            _countryValidator.ShouldHaveValidationErrorFor(c => c.Name, country);
        }

        [Fact]
        public void Validator_WhenValidCodeAndNameAreSpecified_ShouldNotHaveValidationFailure()
        {
            const string code = "CAN";
            string name = _fixture.Create<string>();
            Country country = new Country(code, name);

            _countryValidator.ShouldNotHaveValidationErrorFor(c => c.Code, country);
            _countryValidator.ShouldNotHaveValidationErrorFor(c => c.Name, country);
        }
    }
}
