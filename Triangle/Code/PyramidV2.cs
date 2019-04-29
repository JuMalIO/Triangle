using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Triangle.Code
{
    public class PyramidV2
    {
        private List<List<int>> _tree;

        public PyramidV2(string file)
        {
            _tree = ReadFile(file);
        }

        public List<int> GetMaxPath()
        {
            if (_tree == null || _tree.Count == 0 || _tree[0].Count == 0)
                return null;
            
            var topValue = _tree[0][0];

            if (_tree == null || _tree.Count == 1)
                return new List<int> { topValue };

            var isTopValueEven = topValue % 2 == 0;
            var isLastValueEven = _tree.Count % 2 == 0 ? !isTopValueEven : isTopValueEven;

            var clonedTree = CloneTree(_tree);
            for (int i = clonedTree.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    var max = Math.Max(
                        GetValueIfOddOrEven(clonedTree[i + 1][j], isLastValueEven),
                        GetValueIfOddOrEven(clonedTree[i + 1][j + 1], isLastValueEven));

                    clonedTree[i][j] += max;
                }

                if (i % 2 == 0)
                {
                    isLastValueEven = !isLastValueEven;
                }
            }

            var index = 0;
            var result = new List<int> { topValue };
            var value = clonedTree[0][0] - topValue;
            for (int i = 1; i < _tree.Count; i++)
            {
                for (int j = index; j < index + 2; j++)
                {
                    if (clonedTree[i][j] == value)
                    {
                        index = j;
                        value = clonedTree[i][j] - _tree[i][j];
                        result.Add(_tree[i][j]);
                        break;
                    }
                }
            }

            return result;
        }

        #region Private

        private static int GetValueIfOddOrEven(int value, bool isEven)
        {
            return (value % 2 == 0) == isEven
                ? value
                : int.MinValue;
        }

        private static List<List<int>> CloneTree(List<List<int>> tree)
        {
            return tree.Select(x => x.Select(y => y).ToList()).ToList();
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
