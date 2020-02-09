using LaYumba.Functional;
using LaYumba.Functional.Option;
using System.Collections.Generic;
using System.Collections.Specialized;

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
    }
}