using AdventOfCode;

namespace AoC2015
{
    public class Day12
    {
        public static void Run()
        {
            int yr = 2015,
                day = 12;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string input)
        {
            return input.ExtractAll(@"\-?\d+").ToList().ConvertAll(int.Parse).Sum();
        }

        static int PartTwo(string input)
        {
            input = input.ExtractAll(@"\{|\[|\-?\d+|red|\}|\]").Concat(" "); // remove unnecessary characters, keep {, [, -, 0-9, red, ], }
            while (input.Extract(@"\{|\[") != "")
            {
                string[] extractions = input.ExtractAll(@"\[[\-\dred ]*\]|\{[\-\dred ]*\}");
                input = EditInput(input, extractions);
            }
            return input.ExtractAll(@"\-?\d+").ToInt().Sum();
        }

        static string EditInput(string input, string[] extractions)
        {
            string rplcmnt;
            foreach (string ext in extractions)
            {
                char first = ext[0];
                if (first == '{' && ext.Contains("red")) input = input.Replace(ext, " "); // remove objects that have "red" in them
                else if (first == '[' && ext.Contains("red")) // arrays with "red" can stay (remove "red" so it doesn't accidentally remove an object later)
                {
                    rplcmnt = ext.Replace("red", " ");
                    input = input.Replace(ext, rplcmnt);
                }
                else
                {
                    rplcmnt = ext[1..^1]; // fancy way of saying remove {, [, ], & } from strings like { 1 } or [ 1 ]
                    input = input.Replace(ext, rplcmnt);
                }
            }
            return input.ReplaceRegex(" {2,}", " ");
        }
    }

    public static class Day12Helpers
    {
        
    }
}
