using System.Collections.Generic;
using System.IO;
using System.Linq;
using Triangle.Model;

namespace Triangle.Code
{
    public class PyramidV1
    {
        private Node _tree;

        public PyramidV1(string file)
        {
            _tree = ListOfListToTree(ReadFile(file));
        }

        public List<int> GetMaxPath(bool filterRepeatingOddEvenValues = true)
        {
            if (_tree == null)
                return null;

            var tree = _tree;
            if (filterRepeatingOddEvenValues)
                tree = FilterRepeatingOddEvenValues(_tree);

            List<int> result = null;

            var max = int.MinValue;
            var paths = GetPaths(new List<int> { tree.Value }, tree.Children);
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

        #region Private

        private List<List<int>> GetPaths(List<int> parents, List<Node> children)
        {
            List<List<int>> result = new List<List<int>>();

            foreach (var child in children)
            {
                var parentClone = parents.Select(item => item).ToList();
                parentClone.Add(child.Value);

                if (child.Children.Count == 0)
                {
                    result.Add(parentClone);
                }
                else
                {
                    result.AddRange(GetPaths(parentClone, child.Children));
                }
            }

            return result;
        }

        public static Node CloneTree(Node parent)
        {
            var result = new Node
            {
                Value = parent.Value
            };

            result.Children = parent.Children.Select(x => CloneTree(x)).ToList();

            return result;
        }

        private static Node FilterRepeatingOddEvenValues(Node parent)
        {
            var tree = CloneTree(parent);

            RemoveRepeatingOddEvenValues(tree);

            return tree;
        }

        private static void RemoveRepeatingOddEvenValues(Node parent)
        {
            for (var i = parent.Children.Count - 1; i >= 0; i--)
            {
                var child = parent.Children[i];

                if (parent.Value % 2 == child.Value % 2)
                    parent.Children.RemoveAt(i);
                else
                    RemoveRepeatingOddEvenValues(child);
            }
        }

        private static Node ListOfListToTree(List<List<int>> listOfList)
        {
            if (listOfList == null || listOfList.Count == 0 || listOfList[0].Count == 0)
                return null;

            var result = new Node()
            {
                Value = listOfList[0][0]
            };

            var parent = new List<Node>
            {
                result
            };

            for (var i = 1; i < listOfList.Count; i++)
            {
                var children = new List<Node>();

                for (var j = 0; j < listOfList[i].Count; j++)
                {
                    var node = new Node
                    {
                        Value = listOfList[i][j]
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

        private static List<List<int>> ReadFile(string file)
        {
            try
            {
                var result = new List<List<int>>();

                using (var reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Add(new List<int>());

                        var numbers = line.Split(' ');
                        foreach (var number in numbers)
                        {
                            int n;
                            if (int.TryParse(number, out n))
                            {
                                result[result.Count - 1].Add(n);
                            }
                        }
                    }
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
