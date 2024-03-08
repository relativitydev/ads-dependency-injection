using Relativity.Kepler.Services;

namespace SampleApplication.Service
{
    /// <summary>
    /// This interface is used to identify the service module to the Kepler framework.
    /// </summary>
    [ServiceModule("MyService")]
    [RoutePrefix("AdamModule")]
    public interface IMyServiceModule
    {
    }
}
