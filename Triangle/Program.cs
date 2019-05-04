using System;
using System.Linq;
using Triangle.Code;

namespace Triangle
{
    class Program
    {
        const string File = "Resources/input.txt";

        static void Main(string[] args)
        {
            var triangle = Utilities.IO.ReadFile(new FileReader(), File);

            if (triangle != null)
            {
                var pathFinder = new PathFinder(triangle);
                var maxPath = pathFinder.GetMaxPath();
                if (maxPath != null)
                {
                    Console.WriteLine($"Max sum: {maxPath.Sum()}");
                    Console.WriteLine($"Path: {string.Join(", ", maxPath)}");
                }
                else
                {
                    Console.WriteLine($"Error occured while getting path from triangle.\n{string.Join("\n", triangle.Select(x => string.Join(" ", x)))}");
                }
            }
            else
            {
                Console.WriteLine($"Error occured while reading file \"{File}\".");
            }

            Console.ReadKey();
        }
    }
}
