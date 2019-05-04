using System.Collections.Generic;
using System.Linq;
using Triangle.Extensions;
using Triangle.Interfaces;
using Triangle.Models;

namespace Triangle.Code
{
    public class PathFinderWithRecursion : IPathFinder
    {
        private readonly Node _tree;

        public PathFinderWithRecursion(List<List<int>> triangle)
        {
            _tree = TriangleToTree(triangle);
        }

        public List<int> GetMaxPath()
        {
            if (_tree == null)
                return null;

            if (_tree.Children.Count == 0)
                return new List<int> { _tree.Value };

            var tree = FilterRepeatingOddEvenValues(_tree);

            var paths = GetPaths(new List<int> { tree.Value }, tree.Children);

            var result = GetMaxPath(paths);

            return result;
        }

        #region Private

        private List<int> GetMaxPath(List<List<int>> paths)
        {
            List<int> result = null;

            var max = int.MinValue;
            foreach (var path in paths)
            {
                var sum = path.Sum();
                if (sum > max)
                {
                    max = sum;
                    result = path;
                }
            }

            return result;
        }

        private List<List<int>> GetPaths(List<int> parents, List<Node> children)
        {
            List<List<int>> result = new List<List<int>>();

            foreach (var child in children)
            {
                var parentClone = parents.Clone();

                parentClone.Add(child.Value);

                if (child.Children.Count == 0)
                    result.Add(parentClone);
                else
                    result.AddRange(GetPaths(parentClone, child.Children));
            }

            return result;
        }

        private Node FilterRepeatingOddEvenValues(Node parent)
        {
            var tree = parent.Clone();

            RemoveRepeatingOddEvenValues(tree);

            return tree;
        }

        private void RemoveRepeatingOddEvenValues(Node parent)
        {
            for (var i = parent.Children.Count - 1; i >= 0; i--)
            {
                var child = parent.Children[i];

                if (parent.Value.IsEven() == child.Value.IsEven())
                    parent.Children.RemoveAt(i);
                else
                    RemoveRepeatingOddEvenValues(child);
            }
        }

        private Node TriangleToTree(List<List<int>> triangle)
        {
            if (triangle == null || triangle.Count == 0 || triangle[0].Count == 0)
                return null;

            var result = new Node()
            {
                Value = triangle[0][0]
            };

            var parent = new List<Node>
            {
                result
            };

            for (var i = 1; i < triangle.Count; i++)
            {
                var children = new List<Node>();

                for (var j = 0; j < triangle[i].Count; j++)
                {
                    var node = new Node
                    {
                        Value = triangle[i][j]
                    };

                    children.Add(node);
                }

                for (var j = 0; j < parent.Count; j++)
                {
                    if (children.Count > j)
                    {
                        parent[j].Children.Add(children[j]);
                    }

                    if (children.Count > j + 1)
                    {
                        parent[j].Children.Add(children[j + 1]);
                    }
                }

                parent = children;
            }

            return result;
        }

        #endregion
    }
}
