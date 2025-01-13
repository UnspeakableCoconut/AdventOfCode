using AdventOfCode;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day5
    {
        public static void Run()
        {
            int yr = 2015,
                day = 5;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        private static int PartOne(string[] input)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            List<string> dbls = [];
            foreach (char c in alphabet) dbls.Add($"{c}{c}");

            string vwlRegex = "a|e|i|o|u",
                dblRegex = string.Join("|", dbls),
                badRegex = "ab|cd|pq|xy";

            int niceCount = 0;
            foreach (string line in input)
            {
                if (line.ExtractAll(vwlRegex).Length >= 3 && Regex.IsMatch(line, dblRegex) && line.Extract(badRegex) == "")
                    niceCount++;
            }
            return niceCount;
        }

        private static int PartTwo(string[] input)
        {
            int niceCount = 0;
            foreach (string line in input)
            {
                string[] pairs = line.GetPairs();
                pairs = pairs.GroupBy(s => s).Where(s => s.Count() > 1).Select(s => s.Key).ToArray();
                if (pairs.Length == 0) continue;
                pairs = pairs.Where(s => s.ToCharArray().Distinct().Count() == 1).ToArray();

                string[] trios = pairs.GetTrios();
                string badRegex = string.Join("|", trios);
                if (line.Extract(badRegex) != "") continue;

                string goodRegex = line.GetLtrRegex();
                if (line.Extract(goodRegex) == "") continue;
                niceCount++;
            }
            return niceCount;
        }
    }

    public static class Day5Helpers
    {
        public static string[] GetPairs(this string s)
        {
            char[] ltrs = s.ToCharArray();
            List<string> pairs = [];
            for (int i = 1; i < ltrs.Length; i++)
                pairs.Add($"{ltrs[i - 1]}{ltrs[i]}");
            return pairs.ToArray();
        }

        public static string[] GetTrios(this string[] pairs)
        {
            List<string> trios = [];
            foreach (string pair in pairs)
            {
                string full = pair + pair;
                trios.Add(full.Extract("^\\w{3}"));
                trios.Add(full.Extract("\\w{3}$"));
            }
            return trios.ToArray();
        }

        public static string GetLtrRegex(this string line)
        {
            char[] ltrs = line.ToCharArray();
            List<string> exprs = [];
            foreach (char ltr in ltrs) exprs.Add($"{ltr}[^{ltr}]{ltr}");
            return string.Join("|", exprs);
        }
    }
}
