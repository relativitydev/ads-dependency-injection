using Relativity.API;
using Relativity.Kepler.Logging;
using System;
using System.Threading.Tasks;

namespace SampleApplication.Service
{
    /// <summary>
    /// MyService only depends on interfaces - it can be tested fully in isolation.
    /// </summary>
    public class MyService : IMyService
    {
        private readonly ILog _logger;
        private readonly IHelper _helper;
        private readonly ICustomDependency _dependency;

        // The Kepler framework automatically sets up dependency injection for ILog and IHelper
        // See `MyWindsorInstaller` for ICustomDependency's registration.
        public MyService(ILog logger, IHelper helper, ICustomDependency dependency)
        {
            _logger = logger.ForContext<MyService>();
            _helper = helper;
            _dependency = dependency;
        }

        public async Task<string> DoWorkAsync()
        {
            await Task.Yield();

            try
            {
                if (_helper is IServiceHelper helper)
                {
                    _dependency.DoCustomWork();
                    return "Work is done!";
                }
                else
                {
                    _logger.LogError("This is not a service helper.");
                    return "Work was not done!";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Oh no!");
                return "Work was not done!";
            }
        }

        public void Dispose()
        {
            // Clean up any resources here.
        }
    }
}
