# Fluent Validation with .net core, Autofac and xUnit

This project demonstrates the following using .net core:

### 1. Autofac

   Autofac is an inversion of control container for .net.

   For more information visit https://autofac.org/.
   ```
   Installation: dotnet add package Autofac
   ```

### 2. FluentValidation

   Validation library by Jeremy Skinner that uses a fluent interface and lambda expressions for building validation rules. This is helpful for Test Driven Development (TDD) and Behavior Driven Development (BDD).
   
   FluentValidation is well documented on github.
   
   For more information visit https://github.com/JeremySkinner/FluentValidation.
   ```
   Installation: dotnet add package FluentValidation
   ```

### 3. FluentAssertions
   
   This is a set of advanced extension methods that allow you to more naturally specify expected outcome of a TDD or BDD style test. It makes unit tests more readable.
   
   For more information visit http://www.fluentassertions.com/.
   ```
   Installation: dotnet add package FluentAssertions
   ```

### 4. Moq
   
   Mocking framework for TDD and BDD.
   
   For more information visit http://www.moqthis.com/ or https://github.com/Moq/moq4.
   ```
   Installation: dotnet add package Moq
   ```

### 5. AutoFixture

   AutoFixture is an open source library for .NET designed to minimize the 'Arrange' phase of your unit tests in order to maximize maintainability.
   
   For more information visit https://github.com/AutoFixture/AutoFixture.
   ```
   Installation: dotnet add package AutoFixture
   ```

### 6. xUnit
   
   xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.

   For more information visit https://xunit.github.io/.

### 7. dotnet core commands

   Follow these commands to create this project:

   ```
    dotnet new sln -n Validation

    dotnet new classlib -n Validation.Core
    cd Validation.Core
    dotnet add package FluentValidation
    dotnet add package AutoFac

    cd ..
    dotnet new console -n Validation.App
    cd Validation.App
    dotnet add package AutoFac
    dotnet add reference ../Validation.Core/Validation.Core.csproj

    cd ..
    dotnet new xunit -n Validation.Core.Tests
    cd Validation.Core.Tests
    dotnet add package AutoFac
    dotnet add package AutoFixture
    dotnet add package FluentAssertions
    dotnet add package Moq
    dotnet add reference ../Validation.Core/Validation.Core.csproj

    cd ..
    dotnet sln add ./Validation.Core/Validation.Core.csproj
    dotnet sln add ./Validation.App/Validation.App.csproj
    dotnet sln add ./Validation.Core.Tests/Validation.Core.Tests.csproj
   ```
