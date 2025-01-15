using AdventOfCode;

namespace AoC2015
{
    public class Day13
    {
        public static Dictionary<string, int> happyScores = [];
        public static string[] guests;

        public static void Run()
        {
            int yr = 2015,
                day = 13;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadScores(input);
            guests = input.Extract(@"^[^ ]*").Distinct().ToArray();
            return guests.Permute([]).ConvertAll(CheckPermutation).Max();
        }

        static void LoadScores(string[] input)
        {
            string sign, num, people;
            foreach (string line in input)
            {
                sign = line.Extract("gain") == "" ? "-" : "";
                num = line.Extract(@"\d+");
                int score = (sign + num).ToInt();
                people = line.ExtractAll(@"^[^ ]*|[^ ]*\.").Concat(",").Replace(".", "");
                happyScores.Add(people, score);
            }
        }

        static int CheckPermutation(List<string> permutation)
        {
            int score = 0;
            string[] pairs = GetPairs(permutation);
            foreach (string pair in pairs) score += happyScores[pair];
            return score;
        }

        static string[] GetPairs(List<string> permutation)
        {
            List<string> pairs = [];
            for (int i = 0; i < permutation.Count - 1; i++)
            {
                string[] guests = [permutation[i], permutation[i + 1]];
                pairs.Add(guests.Concat(","));
                pairs.Add($"{guests[1]},{guests[0]}");
            }
            int lastIndex = permutation.Count - 1;
            string first = permutation[0],
                last = permutation[lastIndex];
            pairs.Add($"{first},{last}");
            pairs.Add($"{last},{first}");
            return [.. pairs]; // this is a new way of converting a list to an array. to me, at least
        }

        static int PartTwo(string[] input)
        {
            foreach (string guest in guests)
            {
                happyScores.Add($"{guest},Me", 0);
                happyScores.Add($"Me,{guest}", 0);
            }
            List<string> includeMe = guests.ToList();
            includeMe.Add("Me");
            guests = includeMe.ToArray();
            return guests.Permute([]).ConvertAll(CheckPermutation).Max();
        }
    }
}
