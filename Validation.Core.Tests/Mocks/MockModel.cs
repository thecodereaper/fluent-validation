using FluentValidation;

namespace Validation.Core.Tests.Mocks
{
    public sealed class MockModel
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }

        internal sealed class Validator : AbstractValidator<MockModel>
        {
            public Validator()
            {
                RuleFor(mockModel => mockModel.Property1).NotNull();
                RuleFor(mockModel => mockModel.Property2).NotNull();
            }
        }
    }
}
