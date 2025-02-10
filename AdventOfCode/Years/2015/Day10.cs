using AdventOfCode;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day10
    {
        public static Dictionary<string, string> converter = [];

        public static void Run()
        {
            int yr = 2015,
                day = 10;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string input)
        {
            for (int i = 0; i < 40; i++) input = LookAndSay(input);
            return input.Length;
        }

        static string LookAndSay(string input)
        {
            return Regex.Match(input, @"((.)\2*)+").Groups[1].Captures
                .Cast<Capture>()
                .Select(v => v.Value)
                .Select(c => c.Length + c.Substring(0, 1))
                .ToArray().Conjoin();
        }

        static int PartTwo(string input)
        {
            for (int i = 0; i < 50; i++) input = LookAndSay(input);
            return input.Length;
        }
    }
}
