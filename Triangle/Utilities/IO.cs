using System;
using System.Collections.Generic;
using System.IO;
using Triangle.Interfaces;

namespace Triangle.Utilities
{
    public static class IO
    {
        public static List<List<int>> ReadFile(IFileReader fileReader, string file)
        {
            try
            {
                var result = new List<List<int>>();

                var lines = fileReader.ReadAllLines(file);

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
            catch
            {
                return null;
            }
        }
    }
}
