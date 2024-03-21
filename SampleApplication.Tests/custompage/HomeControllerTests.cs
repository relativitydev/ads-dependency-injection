using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Relativity.API;
using SampleApplication.CustomPage.Controllers;
using System.Web.Mvc;

namespace SampleApplication.Tests
{
	[TestFixture]
	public class HomeControllerTests
	{

		Mock<ICPHelper> helperMock;
		Mock<IAPILog> loggerMock;
		Mock<IAuthenticationMgr> authMock;
		Mock<IUserInfo> userMock;

		[Test]
		public void Index_ViewBagMessage()
		{
			// Arrange
			helperMock = new Mock<ICPHelper>();
			loggerMock = new Mock<IAPILog>();
			authMock = new Mock<IAuthenticationMgr>();
			userMock = new Mock<IUserInfo>();

			loggerMock.Setup(l => l.ForContext<HomeController>()).Returns(loggerMock.Object);
			helperMock.Setup(h => h.GetActiveCaseID()).Returns(12345);
			authMock.Setup(a => a.UserInfo).Returns(userMock.Object);
			userMock.Setup(u => u.ArtifactID).Returns(67890);

			HomeController controller = new HomeController(loggerMock.Object, helperMock.Object, authMock.Object);

			// Act
			ActionResult result = controller.Index();

			// Assert
			ClassicAssert.AreEqual("Hello! ICPHelper's ActiveCaseID is 12345 and IAuthenticationMgr's UserInfo ArtifactID is 67890.", controller.ViewBag.Message);
		}
	}
}
