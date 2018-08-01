using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Validation.Core.Exceptions;
using Validation.Core.Services;
using Validation.Core.Tests.Mocks;
using Xunit;

namespace Validation.Core.Tests.Services
{
    public sealed class ValidatorServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IComponentContext> _mockContainer;
        private readonly Mock<IValidator> _mockValidator;
        private readonly IValidatorService _validatorService;

        public ValidatorServiceTests()
        {
            _fixture = new Fixture();
            _mockValidator = new Mock<IValidator>();
            _mockContainer = new Mock<IComponentContext>();

            Mock<IComponentRegistry> mockComponentRegistry = new Mock<IComponentRegistry>();
            _mockContainer.SetupGet(call => call.ComponentRegistry).Returns(mockComponentRegistry.Object);

            _validatorService = new ValidatorService(_mockContainer.Object);
        }

        [Fact]
        public void Validator_WhenValidatorDoesNotExist_ShouldThrowNotSupportedException()
        {
            object obj = _fixture.Create<object>();
            _mockContainer.Setup(call => call.ResolveComponent(It.IsAny<IComponentRegistration>(), It.IsAny<IEnumerable<Parameter>>())).Returns(null);

            Action action = () => _validatorService.Validate(obj);

            action.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void Validator_WhenValidatorExistsAndThereAreNoErrors_ShouldNotThrowFailedValidationError()
        {
            object obj = _fixture.Create<object>();
            _mockValidator.Setup(call => call.Validate(obj)).Returns(new ValidationResult());
            _mockContainer.Setup(call => call.ResolveComponent(It.IsAny<IComponentRegistration>(), It.IsAny<IEnumerable<Parameter>>())).Returns(_mockValidator.Object);

            Action action = () => _validatorService.Validate(obj);

            action.Should().NotThrow<FailedValidationException>();
        }

        [Fact]
        public void Validator_WhenValidatorExistsAndThereAreErrors_ShouldThrowFailedValidationErrorWithDetailedErrorMessage()
        {
            MockModel obj = new MockModel();
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<MockModel.Validator>().As<IValidator<MockModel>>();
            IContainer container = builder.Build();
            IValidatorService validatorService = new ValidatorService(container);
            const string expectedErrorMessage = "'Property1' must not be empty.;'Property2' must not be empty.";

            Action action = () => validatorService.Validate(obj);

            action.Should().Throw<FailedValidationException>().WithMessage(expectedErrorMessage);
        }
    }
}
