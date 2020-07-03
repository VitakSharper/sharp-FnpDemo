using System.Linq;

namespace FpPatterns
{
    internal static class Program
    {
        private static void Main()
        {
            var someArr = Enumerable.Range(0, 5);

            var result = someArr.MapTest((i) => i + 2);
        }
    }
}