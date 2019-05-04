using System.IO;
using Triangle.Interfaces;

namespace Triangle.Code
{
    public class FileReader : IFileReader
    {
        public string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }
    }
}
