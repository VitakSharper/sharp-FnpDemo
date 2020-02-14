using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using static LaYumba.Functional.F;
using Enum = System.Enum;
using Unit = System.ValueTuple;

namespace sharp_FnpManning
{
    public static class StringExt
    {
        public static string ToSentenceCase(this string s) =>
            $"{s.ToUpper()[0]}{s.ToLower().Substring(1)}";

        public static Option<string> Lookup(this NameValueCollection @this, string key) =>
            @this[key];

        public static Option<T> Lookup<K, T>
            (this IDictionary<K, T> dict, K key)
            => dict.TryGetValue(key, out var value)
                ? Some(value)
                : None;

        public static Option<T> Parse2<T>(this string s) where T : struct =>
            Enum.TryParse(s, out T t) ? Some(t) : None;

        // Lookup :IEnumerable<T> -> (T->bool) -> Option<T>
        public static Option<T> Lookup2<T>(this IEnumerable<T> ts, Func<T, bool> predicate)
        {
            foreach (T t in ts)
            {
                if (predicate(t)) return Some(t);
            }

            return None;
        }

        public static IEnumerable<R> Map2<T, R>(this IEnumerable<T> ts, Func<T, R> f)
        {
            foreach (T t in ts) yield return f(t);
        }

        public static IEnumerable<Unit> ForEach2<T>(this IEnumerable<T> ts, Action<T> action) =>
            ts.Map2(action.ToFunc()).ToImmutableList();

        public static Option<Unit> ForEach<T>(this Option<T> opt, Action<T> action) =>
            Map(opt, action.ToFunc());
    }
}