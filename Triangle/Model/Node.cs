using System.Collections.Generic;

namespace Triangle.Model
{
    public class Node
    {
        public int Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
    }
}
