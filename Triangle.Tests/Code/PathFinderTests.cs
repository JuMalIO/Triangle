using System.Collections.Generic;
using Triangle.Code;
using Xunit;

namespace Triangle.Tests.Code
{
    public class PathFinderTests
    {
        [Theory]
        [MemberData(nameof(System.Data), MemberType = typeof(TriangleTestData))]
        public void GetMaxPath(List<List<int>> value, List<int> expected)
        {
            var pathFinder = new PathFinder(value);

            var result = pathFinder.GetMaxPath();

            Assert.Equal(expected, result);
        }
    }
}
