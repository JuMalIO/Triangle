using System.Collections.Generic;

namespace Triangle.Interfaces
{
    public interface IFileReader
    {
        List<List<int>> ReadTriangleFile(string file);
        string[] ReadAllLines(string file);
    }
}
