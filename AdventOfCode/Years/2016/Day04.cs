using AdventOfCode;

namespace AoC2016
{
    public class Day04
    {
        static char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static void Run()
        {
            int yr = 2016,
                day = 4;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            return input.Where(line => !IsDecoyRoom(line)).ToArray().Extract(@"\d+").ToInt().Sum();
        }

        static int PartTwo(string[] input)
        {
            return input.Where(line => !IsDecoyRoom(line))
                .Select(line => line = ShiftForward(line))
                .Where(s => s.Extract("northpole") != "")
                .Conjoin().Extract(@"\d+").ToInt();
        }

        static bool IsDecoyRoom(string line)
        {
            string checksum = line.Extract(@"\[[^\]]*\]$").ReplaceRegex(@"\[|\]", ""),
                newline = line.Replace($"   [{checksum}]", "");
            char[] chars = newline.ExtractAll($"[a-z]+").Conjoin().ToCharArray().Distinct().ToArray();
            string trueChecksum = chars
                .Select(c => new { Key = c, Value = newline.ReplaceRegex($"[^{c}]*", "").Length })
                .OrderBy(o => o.Key).OrderByDescending(o => o.Value)
                .Take(5).Select(d => d.Key).Conjoin();
            return trueChecksum != checksum;
        }

        static string ShiftForward(string line)
        {
            int forwardShift = line.Extract(@"\d+").ToInt();
            return line.Extract(@"[^\d]*").Split('-')
                .Select(s => s.ToCharArray()
                .Select(s => Array.IndexOf(alphabet, s))
                .Select(s => (s + forwardShift) % 26)
                .Select(s => alphabet[s]).Conjoin()).Conjoin(" ") + forwardShift;
        }
    }
}