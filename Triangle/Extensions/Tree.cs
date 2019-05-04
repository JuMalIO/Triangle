using System.Linq;
using Triangle.Models;

namespace Triangle.Extensions
{
    public static class Tree
    {
        public static Node Clone(this Node parent)
        {
            var result = new Node
            {
                Value = parent.Value
            };

            result.Children = parent.Children.Select(x => x.Clone()).ToList();

            return result;
        }
    }
}
