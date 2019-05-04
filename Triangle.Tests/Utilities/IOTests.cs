using Moq;
using System.Collections.Generic;
using Triangle.Interfaces;
using Triangle.Utilities;
using Xunit;

namespace Triangle.Tests.Utilities
{
    public class IOTests
    {
        private readonly Mock<IFileReader> fileReader = new Mock<IFileReader>();

        [Fact]
        public void ReadFile()
        {
            var file = "input.txt";

            var expected = new List<List<int>>
            {
                new List<int> { 1 },
                new List<int> { 8, 9 },
                new List<int> { 1, 5, 9 },
                new List<int> { 4, 5, 2, 3 }
            };

            fileReader.Setup(x => x.ReadAllLines(It.IsAny<string>())).Returns(new[]
            {
                "1",
                "8 9",
                "1 5 9",
                "4 5 2 3"
            });

            var result = IO.ReadFile(fileReader.Object, file);

            fileReader.Verify(x => x.ReadAllLines(It.IsAny<string>()), Times.Once());

            Assert.Equal(expected, result);
        }
    }
}
