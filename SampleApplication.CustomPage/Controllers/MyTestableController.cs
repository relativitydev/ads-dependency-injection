using Relativity.API;

namespace SampleApplication.CustomPage.Controllers
{
    public class MyTestableController : ITestableController
    {
        private readonly IAPILog _logger;
        private readonly ICPHelper _helper;
        private readonly IAuthenticationMgr _auth;

        public MyTestableController(IAPILog logger, ICPHelper helper, IAuthenticationMgr auth)
        {
            _logger = logger.ForContext<MyTestableController>();
            _helper = helper;
            _auth = auth;
        }

        public int GetActiveCaseArtifactID()
        {
            int artifactID = _helper.GetActiveCaseID();
            _logger.LogInformation("The active case's artifact ID is " + artifactID);
            return artifactID;
        }

        public int GetActiveUserArtifactID()
        {
            int artifactID = _auth.UserInfo.ArtifactID;
            _logger.LogInformation("The active user's artifact ID is " + artifactID);
            return artifactID;
        }
    }
}