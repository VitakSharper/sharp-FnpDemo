using System;
using System.Collections.Generic;

namespace FpPatterns
{
    public static class FpPatternsExtensions
    {
        // Map pattern implementation;
        public static IEnumerable<R> MapTest<T, R>(this IEnumerable<T> ts, Func<T, R> fn)
        {
            foreach (var t in ts)
                yield return fn(t);
        }
    }
}