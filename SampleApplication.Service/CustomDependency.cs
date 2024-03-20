using Relativity.Kepler.Logging;

namespace SampleApplication.Service
{
	internal class CustomDependency : ICustomDependency
	{
		private readonly ILog _logger;

		public CustomDependency(ILog log)
		{
			_logger = log.ForContext<CustomDependency>();
		}

		public void DoCustomWork()
		{
			_logger.LogInformation("Doing custom work!");
		}
	}
}
