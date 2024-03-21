using Relativity.API;
using System.Web.Mvc;

namespace SampleApplication.CustomPage.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAPILog _logger;
		private readonly ICPHelper _helper;
		private readonly IAuthenticationMgr _auth;

		public HomeController(IAPILog logger, ICPHelper helper, IAuthenticationMgr auth)
		{
			_logger = logger;
			_helper = helper;
			_auth = auth;
		}

		public ActionResult Index()
		{
			int activeCaseArtifactId = _helper.GetActiveCaseID();
			int activeUserArtifactId = _auth.UserInfo.ArtifactID;

			ViewBag.Message = $"Hello! ICPHelper's ActiveCaseID is {activeCaseArtifactId} and IAuthenticationMgr's UserInfo ArtifactID is {activeUserArtifactId}.";

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