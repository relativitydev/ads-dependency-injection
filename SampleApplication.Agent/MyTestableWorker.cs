using Relativity.API;
using System.Threading.Tasks;

namespace SampleApplication.Agent
{
    /// <summary>
    /// MyLazyAgent is fully decoupled and only depends on interfaces - it can now be tested in isolation.
    /// </summary>
    public class MyTestableWorker : ITestableWorker
    {
        private readonly IAPILog _logger;
        private readonly IAgentHelper _helper;

        public MyTestableWorker(IAgentHelper helper)
        {
            // We didn't set up DI for the `IAPILog`, but we could have.
            // In this simple example, it's fine to just get it here.
            _logger = helper.GetLoggerFactory().GetLogger().ForContext<MyTestableWorker>();
            _helper = helper;
        }

        // Specific tests for this "business logic" is in SampleApplication.Tests.Agent.MyTestableWorkerTests
        public async Task<bool> ExecuteAsync()
        {
            _logger.LogInformation("Agent is running!");

            // `IInstanceSettingsBundle` could have also been set up with DI. Because it's not, and we're getting it this way, 
            // our test mocks need to be set up in a slightly more complicated manner.
            IInstanceSettingsBundle bundle = _helper.GetInstanceSettingBundle();

            bool? isOk = await bundle.GetBoolAsync("MySection", "MySetting");

            if (isOk.HasValue && isOk.Value)
            {
                _logger.LogInformation("The setting is true.");
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("[MySection].[MySetting] is not true!");
            }
        }
    }
}
