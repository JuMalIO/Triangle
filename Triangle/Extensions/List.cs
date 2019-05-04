using System.Collections.Generic;
using System.Linq;

namespace Triangle.Extensions
{
    public static class List
    {
        public static List<List<int>> Clone(this List<List<int>> list)
        {
            return list.Select(x => x.Select(y => y).ToList()).ToList();
        }

        public static List<int> Clone(this List<int> list)
        {
            return list.Select(x => x).ToList();
        }
    }
}
