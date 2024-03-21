using Castle.Windsor;
using System.Web.Mvc;

namespace SampleApplication.CustomPage.IoC
{
	public static class Container
	{
		private static IWindsorContainer _container;

		public static void Setup()
		{
			_container = new WindsorContainer().Install(new ControllersInstaller());

			WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}
	}
}