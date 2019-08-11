using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Triangle.Code;
using Xunit;

namespace Triangle.Tests.Utilities
{
    public class FileReaderTests
    {
        private readonly Mock<ILogger<FileReader>> _mockLogger = new Mock<ILogger<FileReader>>();

        [Fact]
        public void ReadFile()
        {
            var file = "filename";

            var expected = new List<List<int>>
            {
                new List<int> { 1 },
                new List<int> { 8, 9 },
                new List<int> { 1, 5, 9 },
                new List<int> { 4, 5, 2, 3 }
            };

            var fileReader = new Mock<FileReader>(_mockLogger.Object) { CallBase = true };

            fileReader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(new[]
            {
                "1",
                "8 9",
                "1 5 9",
                "4 5 2 3"
            });

            var result = fileReader.Object.ReadTriangleFile(file);

            fileReader.Verify(x => x.ReadAllLines(It.IsAny<string>()), Times.Once());

            Assert.Equal(expected, result);
        }
    }
}
