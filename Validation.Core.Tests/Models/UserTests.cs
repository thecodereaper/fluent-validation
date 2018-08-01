using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;
using Validation.Core.Models;
using Xunit;

namespace Validation.Core.Tests.Models
{
    public sealed class UserTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IValidator<Country>> _mockCountryValidator;
        private readonly AbstractValidator<User> _userValidator;

        public UserTests()
        {
            _fixture = new Fixture();

            _mockCountryValidator = new Mock<IValidator<Country>>();
            _mockCountryValidator.Setup(call => call.Validate(It.IsAny<ValidationContext>())).Returns(new ValidationResult());

            _userValidator = new User.Validator(_mockCountryValidator.Object);
        }

        [Fact]
        public void User_WhenConstructingANewUser_ShouldCreateModelInstance()
        {
            string firstName = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            string email = _fixture.Create<string>();
            Country country = new Country(_fixture.Create<string>(), _fixture.Create<string>());

            User user = new User(firstName, lastName, email, country);

            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
            user.Email.Should().Be(email);
            user.Country.Should().Be(country);
        }

        [Fact]
        public void Validator_WhenPropertiesAreNull_ShouldHaveValidationFailure()
        {
            User user = new User(null, null, null, null);

            _userValidator.ShouldHaveValidationErrorFor(u => u.FirstName, user);
            _userValidator.ShouldHaveValidationErrorFor(u => u.LastName, user);
            _userValidator.ShouldHaveValidationErrorFor(u => u.Email, user);
            _userValidator.ShouldHaveValidationErrorFor(u => u.Country, user);
            _mockCountryValidator.Verify(call => call.Validate(It.IsAny<ValidationContext>()), Times.Never());
        }

        [Fact]
        public void Validator_WhenEmailAddressIsIncorrect_ShouldHaveValidationFailure()
        {
            string firstName = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            string email = _fixture.Create<string>();
            Country country = null;

            User user = new User(firstName, lastName, email, country);

            _userValidator.ShouldHaveValidationErrorFor(u => u.Email, user);
        }

        [Fact]
        public void Validator_WhenUserModelIsValid_ShouldNotHaveValidationFailure()
        {
            string firstName = _fixture.Create<string>();
            string lastName = _fixture.Create<string>();
            const string email = "test@tester.com";
            Country country = new Country("CAN", _fixture.Create<string>());

            User user = new User(firstName, lastName, email, country);

            _userValidator.ShouldNotHaveValidationErrorFor(u => u.FirstName, user);
            _userValidator.ShouldNotHaveValidationErrorFor(u => u.LastName, user);
            _userValidator.ShouldNotHaveValidationErrorFor(u => u.Email, user);
            _userValidator.ShouldNotHaveValidationErrorFor(u => u.Country, user);
            _mockCountryValidator.Verify(call => call.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce());
        }
    }
}
