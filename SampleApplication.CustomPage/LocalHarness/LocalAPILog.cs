using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.CustomPage.LocalHarness
{
	public class LocalAPILog : IAPILog
	{
		public IAPILog ForContext<T>()
		{
			return this;
			//throw new NotImplementedException();
		}

		public IAPILog ForContext(Type source)
		{
			throw new NotImplementedException();
		}

		public IAPILog ForContext(string propertyName, object value, bool destructureObjects)
		{
			throw new NotImplementedException();
		}

		public IDisposable LogContextPushProperty(string propertyName, object obj)
		{
			throw new NotImplementedException();
		}

		public void LogDebug(string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogDebug(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogError(string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogError(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogFatal(string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogFatal(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogInformation(string messageTemplate, params object[] propertyValues)
		{
			Console.WriteLine(string.Format("INFO: " + messageTemplate, propertyValues));
		}

		public void LogInformation(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			Console.WriteLine(string.Format("INFO: " + messageTemplate, propertyValues));
			Console.WriteLine("INFO(ex): " + exception.ToString());
		}

		public void LogVerbose(string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogVerbose(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			throw new NotImplementedException();
		}
	}
}