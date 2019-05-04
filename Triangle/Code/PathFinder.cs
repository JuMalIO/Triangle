using System;
using System.Collections.Generic;
using Triangle.Interfaces;
using Triangle.Extensions;

namespace Triangle.Code
{
    public class PathFinder : IPathFinder
    {
        private readonly List<List<int>> _triangle;

        public PathFinder(List<List<int>> triangle)
        {
            _triangle = triangle;
        }

        public List<int> GetMaxPath()
        {
            if (_triangle == null || _triangle.Count == 0 || _triangle[0].Count == 0)
                return null;

            if (_triangle.Count == 1)
                return new List<int> { _triangle[0][0] };

            var summedTriangle = GetSummedTriangle(_triangle);

            var result = GetPathFromOrigianlAndSummedTriangle(_triangle, summedTriangle);

            return result;
        }

        #region Private

        private List<List<int>> GetSummedTriangle(List<List<int>> triangle)
        {
            var isTopValueEven = triangle[0][0].IsEven();
            var isLastValueEven = triangle.Count.IsEven()
                ? !isTopValueEven
                : isTopValueEven;

            var result = triangle.Clone();

            for (int i = result.Count - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    var max = Math.Max(
                        GetValueOrMinIntIfOddOrEven(result[i + 1][j], isLastValueEven),
                        GetValueOrMinIntIfOddOrEven(result[i + 1][j + 1], isLastValueEven));

                    result[i][j] += max;
                }

                if (i.IsEven())
                {
                    isLastValueEven = !isLastValueEven;
                }
            }

            return result;
        }

        private List<int> GetPathFromOrigianlAndSummedTriangle(List<List<int>> originalTriangle, List<List<int>> summedTriangle)
        {
            var result = new List<int>();

            var index = 0;

            var value = summedTriangle[0][0];

            for (int i = 0; i < originalTriangle.Count; i++)
            {
                for (int j = index; j < index + 2; j++)
                {
                    if (summedTriangle[i][j] == value)
                    {
                        index = j;

                        value = summedTriangle[i][j] - originalTriangle[i][j];

                        result.Add(originalTriangle[i][j]);

                        break;
                    }
                }
            }

            return result;
        }

        private int GetValueOrMinIntIfOddOrEven(int value, bool isEven)
        {
            return value.IsEven() == isEven
                ? value
                : int.MinValue;
        }

        #endregion
    }
}
