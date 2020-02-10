using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using static LaYumba.Functional.F;

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
                ? Some(value) : None;

        public static Option<T> Parse2<T>(this string s) where T : struct =>
            System.Enum.TryParse(s, out T t) ? Some(t) : None;

        // Lookup :IEnumerable<T> -> (T->bool) -> Option<T>
        public static Option<T> Lookup2<T>(this IEnumerable<T> ts, Func<T, bool> predicate)
        {
            foreach (T t in ts)
            {
                if (predicate(t)) return Some(t);
            }

            return None;
        }

    }
}