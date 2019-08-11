using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Triangle.Interfaces;
using Triangle.Extensions;

namespace Triangle.Code
{
    public class PathFinder : IPathFinder
    {
        private readonly ILogger<PathFinder> _logger;
        private readonly IFileReader _fileReader;

        public PathFinder(ILogger<PathFinder> logger, IFileReader fileReader)
        {
            _logger = logger;
            _fileReader = fileReader;
        }

        public List<int> GetMaxPath(string file)
        {
            try
            {
                var triangle = _fileReader.ReadTriangleFile(file);

                if (triangle == null)
                    return null;

                if (triangle.Count == 1)
                    return new List<int> { triangle[0][0] };

                var summedTriangle = GetSummedTriangle(triangle);

                var result = GetPathFromOrigianlAndSummedTriangle(triangle, summedTriangle);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while counting max triangle path.");

                return null;
            }
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
