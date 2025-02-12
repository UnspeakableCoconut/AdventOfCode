using AdventOfCode;

namespace AoC2016
{
    public class Day06
    {
        private static readonly List<string> columns = [];

        public static void Run()
        {
            int yr = 2016,
                day = 6;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo().Message(2);
        }

        static string PartOne(string[] input)
        {
            LoadColumns(input);
            return columns.Select(
                col => col.ToCharArray().Distinct()
                .Select(c => new { Key = c, Value = col.ReplaceRegex($"[^{c}]*", "").Length })
                .OrderByDescending(c => c.Value).Take(1).Select(c => c.Key).Conjoin()
                ).Conjoin();
        }

        static string PartTwo()
        {
            return columns.Select(
                col => col.ToCharArray().Distinct()
                .Select(c => new { Key = c, Value = col.ReplaceRegex($"[^{c}]*", "").Length })
                .OrderBy(c => c.Value).Take(1).Select(col => col.Key).Conjoin()
                ).Conjoin();
        }

        static void LoadColumns(string[] input)
        {
            for (int i = 0; i < input[0].Length; i++)
                columns.Add(input.Select(line => line[i]).Conjoin());
        }
    }
}