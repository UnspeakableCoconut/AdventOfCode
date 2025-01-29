using AdventOfCode;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day08
    {
        public static void Run()
        {
            int yr = 2015,
                day = 8;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);

        }

        static int PartOne(string[] input)
        {
            int verbCount = 0,
                litCount = 0;
            foreach (string line in input)
            {
                verbCount += line.Length;
                string lineLit = Regex.Unescape(line).Replace("^\"", "").Replace("\"$", "");
                litCount += lineLit[1..^1].Length;
            }
            return verbCount - litCount;
        }

        static int PartTwo(string[] input)
        {
            int result = 0;
            foreach (string line in input) result += ParScape(line);
            return result;
        }

        // why did I name it this way? it should be called "ParseEscapes". real "legendHandles" => "legHands" => "feet" situation.
        // "sometimes I tryly hate my past self"
        static int ParScape(string line) 
        {
            int escaped = 2;
            foreach (char c in line) escaped += ((int)c == 92 || (int)c == 34) ? 1 : 0;
            return escaped;
        }
    }
}
