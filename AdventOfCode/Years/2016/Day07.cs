using AdventOfCode;
using AoC2015;

namespace AoC2016
{
    public class Day07
    {
        private static string abbaRegex = "";
        private static Dictionary<string, string> sslRegex = [];

        public static void Run()
        {
            int yr = 2016,
                day = 7;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadRegex();
            return input.Where(line => !line.ExtractAll(@"\[[^\]]*\]").Conjoin().Matches(abbaRegex))
                .Where(line => line.Matches(abbaRegex))
                .Count();
        }

        static int PartTwo(string[] input)
        {
            string firstRegex = sslRegex.Keys.Conjoin("|"),
                secondRegex = sslRegex.Values.Conjoin("|"),
                squareRegex = @"\[[^\]]*\]";

            KeyValuePair<string, string>[] lines = input.Select(line => new { 
                Key = line.ReplaceRegex(squareRegex, " "), 
                Value = line.ExtractAll(squareRegex).Conjoin(" ") 
            }).Select(o => new KeyValuePair<string, string>(o.Key, o.Value))
            .ToArray();

            List<string> matchedKeys = [];
            foreach (string regex in sslRegex.Keys)
            {
                foreach (KeyValuePair<string, string> line in lines)
                {
                    if (matchedKeys.Contains(line.Key)) continue;
                    if (line.Key.Extract(regex) == "") continue;
                    if (line.Value.Extract(sslRegex[regex]) == "") continue;
                    matchedKeys.Add(line.Key);
                }
            }
            return matchedKeys.Count;
        }

        static void LoadRegex()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            List<string> pairs = [];
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (i == j) continue;
                    pairs.Add($"{alphabet[i]}{alphabet[j]}");
                }
            }
            abbaRegex = pairs.Select(pair => pair + pair.ToCharArray().Reverse().Conjoin()).Conjoin("|");
            sslRegex = pairs.Select(pair => pair + pair).ToDictionary(s => s.Substring(0, 3), s => s.Substring(1, 3));
        }
    }
}