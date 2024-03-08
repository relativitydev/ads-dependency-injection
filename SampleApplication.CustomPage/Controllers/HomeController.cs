using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Relativity.API;
using Relativity.CustomPages;
using System;
using System.Web.Mvc;

namespace SampleApplication.CustomPage.Controllers
{
    /// <summary>
    /// HomeController uses the Lazy<T> model to shim in the Castle Windsor DI container.
    /// Business logic _does not go here_ - it goes in `MyTestableController`, where it's testable.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IWindsorContainer _container;
        private readonly Lazy<ITestableController> _testableController;

        public HomeController()
        {
            _container = new WindsorContainer();
            /*
             * There are two ways to handle this dependency injection, and the route you take will depend on your specific needs.
             * 
             * 1) Register all dependencies explicitly.
             * Registering all dependencies adds some coupling to the DI logic, but it allows completely custom injection rules.
             * If you have a lot of classes, it also means that individual classes can be dependent on specific interfaces instead of the entire dependency tree.
             * Testing setup is typically less complex with this method.
             * 
             * 2) Register the "top level" dependencies only.
             * This simplifies the DI registration but may make testing more complicated. See SampleApplication.Agent.MyAgent for an example of this.
             */
            _container.Register(
                Component.For<IAPILog>().UsingFactoryMethod(() => ConnectionHelper.Helper().GetLoggerFactory().GetLogger()).LifestyleSingleton(),
                Component.For<ICPHelper>().UsingFactoryMethod(() => ConnectionHelper.Helper()).LifestyleSingleton(),
                Component.For<IAuthenticationMgr>().UsingFactoryMethod(() => ConnectionHelper.Helper().GetAuthenticationManager()).LifestyleSingleton(),
                Component.For<ITestableController>().ImplementedBy<MyTestableController>().LifestyleSingleton()
            );

            // The Func<ITestableController> delegate runs when `_testableController.Value` is called.
            _testableController = new Lazy<ITestableController>(() => _container.Resolve<ITestableController>());
        }

        public ActionResult Index()
        {
            // The Func<ITestableController> delegate runs here when `_testableController.Value` is called - after all the DI registration is complete.
            int activeCaseArtifactId = _testableController.Value.GetActiveCaseArtifactID();
            int activeUserArtifactId = _testableController.Value.GetActiveUserArtifactID();

            ViewBag.Message = $"Hello! ActiveCaseArtifactID is {activeCaseArtifactId} and ActiveUserArtifactID is {activeUserArtifactId}.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}