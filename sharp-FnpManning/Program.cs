using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

            var age = Age.Of(100).Match(
                () => $"-1",
                 value => $"{value}"
                );

            Console.WriteLine($"Option age: {age}\n");
            //WriteLine($"Age is {CalculateRiskProfile(age, Gender.Female)}");

            Option<string> _ = None;
            Option<string> john = Some("John");

            WriteLine(Greet(None));
            WriteLine(Greet(Some("Vitax")));

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

            new List<int>().Lookup2(IsOdd);

            var email = Email.Create("my@dot.com");
            Console.WriteLine(email.Match(() => $"Mail is not valid format.", email1 => $"Mails is {email1}"));

            System.Func<int, int> f = x => x * 3;
            Enumerable.Range(1, 9).Map2(f);

        }

        public static bool IsOdd(int i) => i % 2 == 1;


        

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

        public static string Greet(Option<string> greetee) =>
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