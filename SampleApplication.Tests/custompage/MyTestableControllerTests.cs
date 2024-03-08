using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Relativity.API;
using SampleApplication.CustomPage.Controllers;

namespace SampleApplication.Tests
{
    /// <summary>
    /// There are fewer dependencies to mock out in these tests because of how DI was set up in SampleApplication.CustomPage.Controllers.HomeController.
    /// </summary>
    [TestFixture]
    public class MyTestableControllerTests
    {

        Mock<ICPHelper> helperMock;
        Mock<IAPILog> loggerMock;
        Mock<IAuthenticationMgr> authMock;
        Mock<IUserInfo> userMock;

        [Test]
        public void GetActiveCaseArtifactID()
        {
            // Arrange
            helperMock = new Mock<ICPHelper>();
            loggerMock = new Mock<IAPILog>();
            authMock = new Mock<IAuthenticationMgr>();

            loggerMock.Setup(l => l.ForContext<MyTestableController>()).Returns(loggerMock.Object);
            helperMock.Setup(h => h.GetActiveCaseID()).Returns(12345);

            ITestableController controller = new MyTestableController(loggerMock.Object, helperMock.Object, authMock.Object);

            // Act
            int activeCaseArtifactId = controller.GetActiveCaseArtifactID();

            // Assert
            ClassicAssert.AreEqual(12345, activeCaseArtifactId);
        }

        [Test]
        public void GetActiveUserArtifactId()
        {
            // Arrange
            helperMock = new Mock<ICPHelper>();
            loggerMock = new Mock<IAPILog>();
            authMock = new Mock<IAuthenticationMgr>();
            userMock = new Mock<IUserInfo>();

            loggerMock.Setup(l => l.ForContext<MyTestableController>()).Returns(loggerMock.Object);
            authMock.Setup(a => a.UserInfo).Returns(userMock.Object);
            userMock.Setup(u => u.ArtifactID).Returns(67890);

            ITestableController controller = new MyTestableController(loggerMock.Object, helperMock.Object, authMock.Object);

            // Act
            int activeUserArtifactId = controller.GetActiveUserArtifactID();

            // Assert
            ClassicAssert.AreEqual(67890, activeUserArtifactId);
        }
    }
}
