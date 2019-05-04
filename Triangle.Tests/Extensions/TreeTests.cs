using Newtonsoft.Json;
using Triangle.Extensions;
using Triangle.Models;
using Xunit;

namespace Triangle.Tests.Extensions
{
    public class TreeTests
    {
        [Fact]
        public void CloneNode()
        {
            var expected = new Node();

            var result = expected.Clone();

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }
    }
}
