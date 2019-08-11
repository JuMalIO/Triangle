using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Triangle.Code;
using Triangle.Interfaces;
using Xunit;

namespace Triangle.Tests.Code
{
    public class PathFinderTests
    {
        private readonly Mock<ILogger<PathFinder>> _mockLogger = new Mock<ILogger<PathFinder>>();
        private readonly Mock<IFileReader> _mockFileReader = new Mock<IFileReader>();

        [Theory]
        [MemberData(nameof(System.Data), MemberType = typeof(TriangleTestData))]
        public void GetMaxPath(List<List<int>> value, List<int> expected)
        {
            _mockFileReader.Setup(x => x.ReadTriangleFile(It.IsAny<string>())).Returns(value);

            var pathFinder = new PathFinder(_mockLogger.Object, _mockFileReader.Object);

            var file = "filename";

            var result = pathFinder.GetMaxPath(file);

            Assert.Equal(expected, result);
        }
    }
}
