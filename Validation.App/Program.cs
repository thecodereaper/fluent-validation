using System;
using Autofac;
using Validation.Core;
using Validation.Core.Models;
using Validation.Core.Services;

namespace Validation.App
{
    internal sealed class Program
    {
        private static void Main()
        {
            IContainer container = CreateContainer();
            IValidatorService validatorService = container.Resolve<IValidatorService>();

            Country country = new Country("CAN", "Canada");
            validatorService.Validate(country);

            User user = new User("Joe", "Bloggs", "joe@bloggs.com", country);
            validatorService.Validate(user);

            Console.WriteLine("Success!");
            Console.ReadLine();
        }

        private static IContainer CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new ValidationCoreModule());

            return builder.Build();
        }
    }
}
