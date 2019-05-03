using System.Collections.Generic;
using Triangle.Code;
using Triangle.Tests.Helpers;
using Xunit;

namespace Triangle.Tests.Code
{
    public class PathFinderWithRecursionTests
    {
        [Theory]
        [MemberData(nameof(System.Data), MemberType = typeof(TriangleData))]
        public void GetMaxPath(List<List<int>> value, List<int> expected)
        {
            var pathFinder = new PathFinderWithRecursion(value);

            var result = pathFinder.GetMaxPath();

            Assert.Equal(expected, result);
        }
    }
}