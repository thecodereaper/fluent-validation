using System;
using System.Linq;
using Autofac;
using FluentValidation;
using FluentValidation.Results;
using Validation.Core.Exceptions;

namespace Validation.Core.Services
{
    internal sealed class ValidatorService : IValidatorService
    {
        private readonly IComponentContext _container;

        public ValidatorService(IComponentContext container)
        {
            _container = container;
        }

        public void Validate<T>(T obj)
        {
            IValidator validator = GetValidator<T>();

            if (validator == null)
                throw new NotSupportedException();

            ValidationResult validationResult = validator.Validate(obj);

            if (validationResult.IsValid)
                return;

            string errorMessage = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            throw new FailedValidationException(errorMessage);
        }

        private IValidator GetValidator<T>()
        {
            Type validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));

            if (_container.TryResolve(validatorType, out object validator))
                return (IValidator)validator;

            return null;
        }
    }
}
