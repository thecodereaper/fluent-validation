using Autofac;
using FluentValidation;
using Validation.Core.Services;

namespace Validation.Core
{
    public sealed class ValidationCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ValidatorService>().As<IValidatorService>();
        }
    }
}
