using System.Collections.Generic;
using Triangle.Extensions;
using Xunit;

namespace Triangle.Tests.Extensions
{
    public class ListExtensionsTests
    {
        [Fact]
        public void CloneListOfList()
        {
            var expected = new List<List<int>>();

            var result = expected.Clone();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CloneList()
        {
            var expected = new List<int>();

            var result = expected.Clone();

            Assert.Equal(expected, result);
        }
    }
}
