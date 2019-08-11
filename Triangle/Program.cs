using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using Triangle.Code;
using Triangle.Extensions;
using Triangle.Interfaces;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSerilog(configuration);
            serviceCollection.AddTransient<IFileReader, FileReader>();
            serviceCollection.AddTransient<IPathFinder, PathFinder>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var pathFinder = serviceProvider.GetService<IPathFinder>();

            var file = configuration.GetValue<string>("file");

            var maxPath = pathFinder.GetMaxPath(file);
            if (maxPath != null)
            {
                Console.WriteLine($"Max sum: {maxPath.Sum()}");
                Console.WriteLine($"Path: {string.Join(", ", maxPath)}");
            }

            Console.ReadKey();
        }
    }
}
