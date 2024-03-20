using System.Threading.Tasks;

namespace SampleApplication.Agent
{
	public interface ITestableWorker
	{
		Task<bool> ExecuteAsync();
	}
}
