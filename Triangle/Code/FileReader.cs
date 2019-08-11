using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using Triangle.Interfaces;

namespace Triangle.Code
{
    public class FileReader : IFileReader
    {
        private readonly ILogger<FileReader> _logger;

        public FileReader(ILogger<FileReader> logger)
        {
            _logger = logger;
        }

        public List<List<int>> ReadTriangleFile(string file)
        {
            try
            {
                var result = new List<List<int>>();

                var lines = ReadAllLines(file);

                foreach (var line in lines)
                {
                    result.Add(new List<int>());

                    var characters = line.Split(' ');
                    foreach (var character in characters)
                    {
                        if (int.TryParse(character, out var number))
                        {
                            result[result.Count - 1].Add(number);
                        }
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while reading from triangle file.");

                return null;
            }
        }

        public virtual string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }
    }
}
