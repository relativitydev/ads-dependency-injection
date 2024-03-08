using Moq;
using NUnit.Framework;
using Relativity.API;
using SampleApplication.Agent;
using System;
using System.Threading.Tasks;

namespace SampleApplication.Tests
{
    /// <summary>
    /// The more complicated testing setup here is the downside of the "simple" dependency injection of only the root objects (e.g. `IAgentHelper`).
    /// Technically this gives us the most freedom to inject mocks in the tests, but that may not be required. We have to mock out *everything* in the dependency
    /// tree, including the intermediate ILogFactory, and do some self-referential mocking on the IAPILog, just for the test to run.
    /// </summary>
    [TestFixture]
    public class MyTestableAgentTests
    {

        Mock<IAgentHelper> helperMock;
        Mock<ILogFactory> logFactoryMock;
        Mock<IAPILog> loggerMock;
        Mock<IInstanceSettingsBundle> bundleMock;

        [Test]
        public async Task ExecuteAsync_InstanceSettingExists_ReturnsTrue()
        {
            // Arrange
            helperMock = new Mock<IAgentHelper>();
            logFactoryMock = new Mock<ILogFactory>();
            loggerMock = new Mock<IAPILog>();
            bundleMock = new Mock<IInstanceSettingsBundle>();

            helperMock.Setup(h => h.GetLoggerFactory()).Returns(logFactoryMock.Object);
            logFactoryMock.Setup(l => l.GetLogger()).Returns(loggerMock.Object);
            loggerMock.Setup(l => l.ForContext<MyTestableWorker>()).Returns(loggerMock.Object);
            helperMock.Setup(h => h.GetInstanceSettingBundle()).Returns(bundleMock.Object);
            bundleMock.Setup(b => b.GetBoolAsync("MySection", "MySetting")).ReturnsAsync(true);

            ITestableWorker agent = new MyTestableWorker(helperMock.Object);

            // Act
            bool result = await agent.ExecuteAsync();

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase(false)]
        [TestCase(null)]
        public void ExecuteAsync_InstanceSettingIsMissing_Throws(bool? instanceSettingValue)
        {
            // Arrange
            helperMock = new Mock<IAgentHelper>();
            logFactoryMock = new Mock<ILogFactory>();
            loggerMock = new Mock<IAPILog>();
            bundleMock = new Mock<IInstanceSettingsBundle>();

            helperMock.Setup(h => h.GetLoggerFactory()).Returns(logFactoryMock.Object);
            logFactoryMock.Setup(l => l.GetLogger()).Returns(loggerMock.Object);
            loggerMock.Setup(l => l.ForContext<MyTestableWorker>()).Returns(loggerMock.Object);
            helperMock.Setup(h => h.GetInstanceSettingBundle()).Returns(bundleMock.Object);
            bundleMock.Setup(b => b.GetBoolAsync("MySection", "MySetting")).ReturnsAsync(instanceSettingValue);

            ITestableWorker agent = new MyTestableWorker(helperMock.Object);

            // Act/Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => agent.ExecuteAsync());
        }
    }
}
