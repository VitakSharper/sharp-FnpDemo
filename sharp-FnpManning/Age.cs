using LaYumba.Functional;
using System;
using System.Text.RegularExpressions;
using static LaYumba.Functional.F;


namespace sharp_FnpManning
{
    public struct Age
    {
        private int Value { get; }
        public static bool operator <(Age l, int r) => l.Value < new Age(r).Value;
        public static bool operator >(Age l, int r) => l.Value > new Age(r).Value;

        public static Option<Age> Of(int age) =>
            IsValid(age)
                ? Some(new Age(age))
                : None;

        private Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age.");
            Value = value;
        }

        private static bool IsValid(int value) =>
            0 < value && value < 120;
    }

    public class Email
    {
        private static readonly Regex Regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private string Value { get; }
        private Email(string value) => Value = value;

        public static Option<Email> Create(string s) =>
            Regex.IsMatch(s)
                ? Some(new Email(s))
                : None;

        public static implicit operator string(Email e) =>
            e.Value;

    }
}