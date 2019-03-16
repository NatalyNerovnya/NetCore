using Microsoft.Extensions.Logging;
using Moq;
using NetCoreMentoring.Controllers;
using NUnit.Framework;

namespace Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<HomeController>>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}