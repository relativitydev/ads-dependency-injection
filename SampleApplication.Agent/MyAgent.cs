using Castle.MicroKernel.Registration;
using Castle.Windsor;
using kCura.Agent;
using Relativity.API;
using System;

namespace SampleApplication.Agent
{
    /// <summary>
    /// MyAgent uses the Lazy<T> model to shim in the Castle Windsor DI container.
    /// Business logic _does not go here_ - it goes in `MyTestableWorker`, where it's testable.
    /// </summary>
    [System.Runtime.InteropServices.Guid("A04271F3-9BC8-4627-A87A-E44DF9749525")]
    public class MyAgent : AgentBase
    {
        private const string _name = "My Testable Agent";

        private readonly IWindsorContainer _container;
        private readonly Lazy<ITestableWorker> _testableWorker;

        [kCura.Agent.CustomAttributes.Name(_name)]
        public MyAgent()
        {
            _container = new WindsorContainer();

            /*
             * There are two ways to handle this dependency injection, and the route you take will depend on your specific needs.
             * 
             * 1) Register the "top level" dependencies only.
             * In this example, we are *only* registering `IAgentHelper` and `ITestableWorker`. Because only `Helper` will be injected
             * into `MyTestableWorker`, all testing needs to mock out everything starting from `IAgentHelper`.
             * See SampleApplication.Tests.Agent.MyTestableAgentTests for what that looks like.
             * 
             * 2) Register all dependencies.
             * This allows all dependencies to be injected and may make testing easier. See SampleApplication.CustomPage.Controllers.HomeController for an example of this.
             */
            _container.Register(
                Component.For<IAgentHelper>().UsingFactoryMethod(() => Helper).LifestyleSingleton(),
                Component.For<ITestableWorker>().ImplementedBy<MyTestableWorker>().LifestyleSingleton()
            ); ;

            // The Func<ITestableWorker> delegate runs when `_testableWorker.Value` is called.
            _testableWorker = new Lazy<ITestableWorker>(() => _container.Resolve<ITestableWorker>());
        }

        public override string Name => _name;

        public override void Execute()
        {
            try
            {
                // The Func<ITestableWorker> delegate runs here when `_testableWorker.Value` is called - after all the DI registration is complete.
                DidWork = _testableWorker.Value.ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                RaiseMessage($"Agent ran successfully ({DateTime.UtcNow}).", (int)AgentMessage.AgentMessageType.Informational);

            }
            catch (Exception ex)
            {
                RaiseError(ex.Message, ex.ToString());
            }
        }
    }
}
