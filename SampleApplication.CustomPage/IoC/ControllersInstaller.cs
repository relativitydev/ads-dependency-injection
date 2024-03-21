using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Relativity.API;
using Relativity.CustomPages;
using System.Configuration;

namespace SampleApplication.CustomPage.IoC
{
	public class ControllersInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly()
				.Pick().If(t => t.Name.EndsWith("Controller"))
				.Configure(configurer => configurer.Named(configurer.Implementation.Name))
				.LifestyleTransient());

			if (ConfigurationManager.AppSettings["IsLocal"] == "true")
			{
				container.Register(
					Component.For<IAPILog>().ImplementedBy<LocalHarness.LocalAPILog>().LifestyleSingleton(),
					Component.For<ICPHelper>().ImplementedBy<LocalHarness.LocalCPHelper>().LifestyleSingleton(),
					Component.For<IAuthenticationMgr>().ImplementedBy<LocalHarness.LocalAuthenticationMgr>().LifestyleSingleton()
				);
			}
			else
			{
				container.Register(
					Component.For<IAPILog>().UsingFactoryMethod(() => ConnectionHelper.Helper().GetLoggerFactory().GetLogger()).LifestyleSingleton(),
					Component.For<ICPHelper>().UsingFactoryMethod(() => ConnectionHelper.Helper()).LifestyleSingleton(),
					Component.For<IAuthenticationMgr>().UsingFactoryMethod(() => ConnectionHelper.Helper().GetAuthenticationManager()).LifestyleSingleton()
				);
			}
		}
	}
}