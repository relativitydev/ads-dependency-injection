using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Relativity.API;
using Relativity.Kepler.Logging;
using SampleApplication.Service;
using System.Threading.Tasks;

namespace SampleApplication.Tests
{
    /// <summary>
    /// MyService can be instantiated with mocks that let you set up any precondition you need for testing.
    /// </summary>
    public class MyServiceTests
    {
        [Test]
        public async Task DoWork_WhenCalled_CallsDoCustomWork()
        {
            // Arrange
            var logger = new Mock<ILog>();
            var helper = new Mock<IServiceHelper>();
            var dependency = new Mock<ICustomDependency>();

            logger.Setup(l => l.ForContext<MyService>()).Returns(logger.Object);

            var service = new MyService(logger.Object, helper.Object, dependency.Object);

            // Act
            string result = await service.DoWorkAsync();

            //  Assert
            ClassicAssert.AreEqual("Work is done!", result);
        }

        [Test]
        public async Task DoWork_CustomDependencyThrows()
        {
            var logger = new Mock<ILog>();
            var helper = new Mock<IServiceHelper>();
            var dependency = new Mock<ICustomDependency>();

            logger.Setup(l => l.ForContext<MyService>()).Returns(logger.Object);
            dependency.Setup(d => d.DoCustomWork()).Throws(new System.Exception("Custom dependency failed!"));

            var service = new MyService(logger.Object, helper.Object, dependency.Object);

            // Act
            string result = await service.DoWorkAsync();

            //  Assert
            ClassicAssert.AreEqual("Work was not done!", result);

        }
    }
}
