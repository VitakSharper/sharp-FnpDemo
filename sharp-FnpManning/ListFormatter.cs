using System.Collections.Generic;
using System.Linq;

namespace sharp_FnpManning
{
    public static class ListFormatter
    {
        //private int _counter;

        //private string PrependCounter(string s) => $"{++_counter}. {s}";

        //public List<string> Format(List<string> list) =>
        //    list
        //        .Select(StringExt.ToSentenceCase)
        //        .Select(PrependCounter)
        //        .ToList();

        public static List<string> Format(List<string> list)
            =>
                list.AsParallel()
                    .Select(StringExt.ToSentenceCase)
                    .Zip(second: ParallelEnumerable.Range(1, list.Count), (s, i) => $"{i}. {s}")
                    .ToList();
    }
}