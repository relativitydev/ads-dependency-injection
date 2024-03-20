# ADS Dependency Injection

This sample ADS application demonstrates how to do dependency injection with the different ADS platform extensibility points.

Note: Dependency Injection for Event Handlers is the same as Agents, so the examples are omitted here.

## Dependency Injection
Dependency Injection is a specific implementation of Dependency Inversion, one of the five S.O.L.I.D. object-oriented principles.

The internet is full of excellent documentation on the topic, but here are a few specific links:
- [Dependency inversion (Microsoft)](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)
- [Dependency injection (Wikipedia)](https://en.wikipedia.org/wiki/Dependency_injection)
- [Dependency Injection Demystified (James Shore)](https://www.jamesshore.com/v2/blog/2006/dependency-injection-demystified)

### Dependency Injection in Kepler services

Relativity's Kepler framework supports Dependency Injection with Castle Windsor. Support is fully documented in the [Relativity Developer Documentation](https://platform.relativity.com/RelativityOne/Content/Kepler_framework/Dependency_injection.htm).

See `SampleApplication.Service.MyService` for a sample implementation.

### Dependency Injection in Custom Pages

Because the Custom Page sample is an ASP.NET MVC project, Castle Windsor Dependency Injection can be wired into the MVC application itself. This can be used at the unit testing level, to inject mocked classes. This can also be used at the test harness level.

See `SampleApplication.CustomPage.Controllers.HomeController.cs` and the `SampleApplication.CustomPage.IoC` folder for more information.

### Dependency Injection in Agent and EventHandler extensibility points

Agent and EventHAndler extensibility points don't have built-in support for Dependency Injection, but it is still possible. Two variations of a technique for implementing Dependency Injection are shown in `SampleApplication.Agent.MyAgent`. The technique leverages the [Lazy\<T\> class](https://learn.microsoft.com/en-us/dotnet/api/system.lazy-1?view=net-8.0) to delay DI Container resolution.

## Building and Running

The code here compiles and can be tested in Visual Studio 2022. It references packages available on [nuget.org](https://www.nuget.org/).

The Agent and Service assemblies need to be uploaded to Relativity as Resource Files, and the CustomPage needs to be published, zipped, and uplaoded to Relativity as a Custom Page.