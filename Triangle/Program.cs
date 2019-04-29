using System;
using System.Linq;
using Triangle.Code;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            var pyramid = new PyramidV2("Resources/input1.txt");

            var maxPath = pyramid.GetMaxPath();
            Console.WriteLine($"Max sum: {maxPath?.Sum()}");
            Console.WriteLine($"Path: {(maxPath != null ? string.Join(", ", maxPath) : null)}");

            Console.ReadKey();
        }
    }
}
