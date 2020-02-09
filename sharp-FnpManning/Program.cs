using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using static LaYumba.Functional.F;
using static System.Console;
using Unit = System.ValueTuple;

namespace sharp_FnpManning
{
    class Program
    {
        static void Main()
        {
            var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

            ListFormatter
                .Format(shoppingList)
                .ForEach(Console.WriteLine);

            WriteLine($"Age is {CalculateRiskProfile(new Age(119), Gender.Female)}");

            Option<string> _ = None;
            Option<string> john = Some("John");

            WriteLine(greet(None));
            WriteLine(greet(Some("Vitax")));

            WriteLine(GreetingFor(new Subscriber { Email = "some@dot.com", Name = "vITAX" }) + "\n");
            WriteLine($"Parse: {Parse("10")}");

            try
            {
                var empty = new NameValueCollection();
                var green = empty.Lookup("green");
                WriteLine($"green!: {green}");

                var alsoEmpty = new Dictionary<string, string>();
                var blue = alsoEmpty.Lookup("blue");
                WriteLine($"blue!: {blue}");

            }
            catch (Exception e)
            {

                WriteLine(e.GetType().Name);
            }
        }



        public static Option<int> Parse(string str)
        {
            return int.TryParse(str, out var result)
                ? Some(result)
                : None;
        }

        public static string GreetingFor(Subscriber subscriber) =>
            subscriber.Name.Match(
                None: () => $"Dear subscriber...",
                Some: name => $"Dear {name.TrimStart().ToUpper()[0]}{name.TrimStart().Substring(1).ToLower()}"
            );

        public static string greet(Option<string> greetee) =>
            greetee.Match(
                None: () => "Sorry who?",
                Some: name => $"Hello, {name}"
            );

        // dishonest fn transformed to a honest fn
        //public static Risk CalculateRiskProfile(int age)
        //{
        //    if (age < 0 || 120 <= age)
        //        throw new ArgumentException($"{age} is not a valid age");
        //    return (age < 60) ? Risk.Low : Risk.Medium;
        //}

        // honest fn
        public static Risk CalculateRiskProfile(Age age, Gender gender)
        {
            var threshold = (gender == Gender.Female) ? 62 : 60;
            return (age < threshold) ? Risk.Low : Risk.Medium;
        }

        //------------------------------------------------
        public static void Time(string op, Action act) =>
            Time<Unit>(op, act.ToFunc());

        public static T Time<T>(string op, Func<T> f)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------
    }

    public class Subscriber
    {
        public Option<string> Name { get; set; }
        public string Email { get; set; }
    }

    internal enum Gender
    {
        Female,
        Male
    }

    internal enum Risk
    {
        Low,
        Medium,
        High
    }

    public class Age
    {
        private int Value { get; }
        public static bool operator <(Age l, int r) => l.Value < new Age(r).Value;
        public static bool operator >(Age l, int r) => l.Value > new Age(r).Value;

        public Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age.");
            Value = value;
        }

        private static bool IsValid(int value) =>
            0 < value && value < 120;
    }


    public sealed class BicFormatValidator : IValidator<MakeTransfer>
    {
        private static readonly Regex Regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");

        public bool IsValid(MakeTransfer cmb) =>
            Regex.IsMatch(cmb.Bic);
    }

    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        public bool IsValid(MakeTransfer cmb) =>
            (DateTime.UtcNow.Date <= cmb.Date.Date);
    }


    public interface IValidator<in T>
    {
        bool IsValid(T t);
    }


    public abstract class Command
    {
    }
}