namespace Validation.Core.Services
{
    public interface IValidatorService
    {
        void Validate<T>(T obj);
    }
}
