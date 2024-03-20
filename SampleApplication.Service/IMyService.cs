using Relativity.Kepler.Services;
using System;
using System.Threading.Tasks;

namespace SampleApplication.Service
{
	/// <summary>
	/// This interface is used to identify the service itself to the Kepler framework.
	/// </summary>
	[WebService("MyServiceManager")]
	[RoutePrefix("AdamService")]
	[ServiceAudience(Audience.Public)]
	public interface IMyService : IDisposable
	{
		// GET https://<hostname>/Relativity.REST/api/AdamModule/AdamService/DoWork
		[HttpGet]
		[Route("DoWork")]
		Task<string> DoWorkAsync();
	}
}
