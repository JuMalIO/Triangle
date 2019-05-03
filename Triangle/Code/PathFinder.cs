using System;
using System.Collections.Generic;
using System.Linq;
using Triangle.Interfaces;

namespace Triangle.Code
{
    public class PathFinder : IPathFinder
    {
        private List<List<int>> _triangle;

        public PathFinder(List<List<int>> triangle)
        {
            _triangle = triangle;
        }

        public List<int> GetMaxPath()
        {
            if (_triangle == null || _triangle.Count == 0 || _triangle[0].Count == 0)
                return null;
            
            var topValue = _triangle[0][0];

            if (_triangle.Count == 1)
                return new List<int> { topValue };

            var isTopValueEven = topValue % 2 == 0;
            var isLastValueEven = _triangle.Count % 2 == 0 ? !isTopValueEven : isTopValueEven;

            var clonedTriangle = CloneTriangle(_triangle);
            for (int i = clonedTriangle.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    var max = Math.Max(
                        GetValueIfOddOrEven(clonedTriangle[i + 1][j], isLastValueEven),
                        GetValueIfOddOrEven(clonedTriangle[i + 1][j + 1], isLastValueEven));

                    clonedTriangle[i][j] += max;
                }

                if (i % 2 == 0)
                {
                    isLastValueEven = !isLastValueEven;
                }
            }

            var index = 0;
            var result = new List<int> { topValue };
            var value = clonedTriangle[0][0] - topValue;
            for (int i = 1; i < _triangle.Count; i++)
            {
                for (int j = index; j < index + 2; j++)
                {
                    if (clonedTriangle[i][j] == value)
                    {
                        index = j;
                        value = clonedTriangle[i][j] - _triangle[i][j];
                        result.Add(_triangle[i][j]);
                        break;
                    }
                }
            }

            return result;
        }

        #region Private

        private int GetValueIfOddOrEven(int value, bool isEven)
        {
            return (value % 2 == 0) == isEven
                ? value
                : int.MinValue;
        }

        private List<List<int>> CloneTriangle(List<List<int>> triangle)
        {
            return triangle.Select(x => x.Select(y => y).ToList()).ToList();
        }

        #endregion
    }
}
