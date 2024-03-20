using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;

namespace SampleApplication.CustomPage.LocalHarness
{
	public class LocalAuthenticationMgr : IAuthenticationMgr
	{
		public IUserInfo UserInfo => new LocalUserInfo();

		public string GetAuthenticationToken()
		{
			throw new NotImplementedException();
		}
	}
}