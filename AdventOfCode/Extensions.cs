using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Setup
    {
        public static string[] GetInput(int yr, int day)
        {
            string path = $"C:/Users/{Environment.UserName}/source/repos/AdventOfCode/AdventOfCode/Years/{yr}/Inputs/day{day}.txt";
            return File.ReadAllLines(path);
        }
    }

    public static class AOCExtensions
    {
        public static void Message(this object result, int part = 1)
        {
            string num = part == 1 ? "One" : "Two";
            Console.WriteLine($"Part {num}: {result}");
            if (part == 2) Console.WriteLine();
        }

        public static string Extract(this string s, string regex)
        {
            return Regex.Match(s, regex).Value;
        }

        public static string[] ExtractAll(this string s, string regex)
        {
            List<string> subs = [];
            foreach (Match m in Regex.Matches(s, regex)) subs.Add(m.Value);
            return [.. subs];
        }

        public static string[] Extract(this string[] arr, string regex)
        {
            List<string> subs = [];
            foreach (string s in arr) subs.Add(s.Extract(regex));
            return [.. subs];
        }

        public static int[] ToInt(this string[] arr) 
        {
            return Array.ConvertAll(arr, int.Parse);
        }

        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }

        public static string Concat(this string[] arr, string separator = "")
        {
            return string.Join(separator, arr);
        }

        public static string ReplaceRegex(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }
    }
}

