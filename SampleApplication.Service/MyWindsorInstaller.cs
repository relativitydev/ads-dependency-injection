using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SampleApplication.Service
{
    /// <summary>
    /// Kepler has first class support for Castle Windsor as a DI framework.
    /// If your Kepler Service assembly contains a PUBLIC implementation of IWindsorInstaller,
    /// the Kepler framework will automatically use it to register your dependencies.
    /// See also: https://platform.relativity.com/RelativityOne/Content/Kepler_framework/Dependency_injection.htm
    /// </summary>
    public class MyWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // When a constructor parameter is an `ICustomDependency`, CastleWindsor injects a `CustomDependency`.
            container.Register(Component.For<ICustomDependency>().ImplementedBy<CustomDependency>());
        }
    }
}
