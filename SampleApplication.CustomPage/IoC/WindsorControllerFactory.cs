using Castle.MicroKernel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SampleApplication.CustomPage.IoC
{
	/// <summary>
	/// This allows us to use Castle Windsor as the IoC container for our MVC controllers.
	/// </summary>
	public class WindsorControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel _kernel;

		public WindsorControllerFactory(IKernel kernel)
		{
			_kernel = kernel;
		}

		public override void ReleaseController(IController controller)
		{
			base.ReleaseController(controller);
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, $"The controller for path '{requestContext.HttpContext.Request.Path}' could not be found.");
			}
			return (IController)_kernel.Resolve(controllerType);
		}
	}
}