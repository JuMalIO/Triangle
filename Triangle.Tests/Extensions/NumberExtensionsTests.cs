using System.Collections.Generic;
using Triangle.Extensions;
using Xunit;

namespace Triangle.Tests.Extensions
{
    public class NumberExtensionsTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(4, true)]
        public void IsEven(int value, bool expected)
        {
            var result = value.IsEven();

            Assert.Equal(expected, result);
        }
    }
}
