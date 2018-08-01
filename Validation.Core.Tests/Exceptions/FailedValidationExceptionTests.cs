using System;
using AutoFixture;
using FluentAssertions;
using Validation.Core.Exceptions;
using Xunit;

namespace Validation.Core.Tests.Exceptions
{
    public sealed class FailedValidationExceptionTests
    {
        private readonly IFixture _fixture;

        public FailedValidationExceptionTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void FailedValidationException_WhenNoParametersAreSpecified_ShouldCreateAndThrowException()
        {
            Action action = () => throw new FailedValidationException();

            action.Should().Throw<FailedValidationException>();
        }

        [Fact]
        public void FailedValidationException_WhenMessageIsSpecified_ShouldThrowExceptionWithSpecifiedMessage()
        {
            string message = _fixture.Create<string>();

            Action action = () => throw new FailedValidationException(message);

            action.Should().Throw<FailedValidationException>().WithMessage(message);
        }

        [Fact]
        public void FailedValidationException_WhenMessageAndInnerExceptionAreSpecified_ShouldThrowExceptionWithSpecifiedMessageAndInnerException()
        {
            string message = _fixture.Create<string>();
            NotSupportedException innerException = _fixture.Create<NotSupportedException>();

            Action action = () => throw new FailedValidationException(message, innerException);

            action.Should().Throw<FailedValidationException>().WithMessage(message).WithInnerException<NotSupportedException>();
        }
    }
}
