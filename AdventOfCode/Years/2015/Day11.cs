using AdventOfCode;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day11
    {
        public static string nextPass = "";

        public static void Run()
        {
            int yr = 2015,
                day = 11;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(nextPass).Message(2);
        }

        static string PartOne(string input)
        {
            bool meetsReqs = false,
                hasStraight,
                hasIOL,
                hasPairs;
            int[] nums = input.AsInts();
            while (!meetsReqs)
            {
                nums = IncrementPassword(nums, input.Length - 1);
                input = nums.AsString();
                hasStraight = HasStraight(nums);
                hasIOL = input.Extract("i|o|l") == "";
                hasPairs = HasPairs(input);
                meetsReqs = hasStraight && hasIOL && hasPairs;
            }
            nextPass = input;
            return nums.AsString();
        }

        static int[] IncrementPassword(int[] nums, int pos)
        {
            List<int> newNums = [];
            bool checkAgain = false;
            for (int i = 0; i < nums.Length; i++)
            {
                int toAdd = nums[i];
                if (i != pos) newNums.Add(toAdd);
                else if (toAdd < 122) newNums.Add(toAdd + 1);
                else
                {
                    newNums.Add(97);
                    checkAgain = true;
                }
            }
            nums = newNums.ToArray();
            if (checkAgain) nums = IncrementPassword(nums, pos - 1);
            return nums;
        }

        static bool HasStraight(int[] nums)
        {
            for (int i = 0; i < nums.Length - 3; i++)
            {
                if (nums[i + 1] - nums[i] != 1) continue;
                if (nums[i + 2] - nums[i + 1] != 1) continue;
                return true;
            }
            return false;
        }

        static bool HasPairs(string input)
        {
            string[] pairs = GetPairs(input);
            if (pairs.Length < 2) return false;
            foreach (string pair in pairs)
            {
                string regex = (pair + pair).Substring(0, 3);
                if (input.Extract(regex) != "") return false;
            }
            return true;
        }

        static string[] GetPairs(string input)
        {
            List<string> pairs = [];
            char c1, c2;
            for (int i = 1; i < input.Length; i++)
            {
                c1 = input[i - 1];
                c2 = input[i];
                string pair = $"{c1}{c2}";
                if (c1 == c2) pairs.Add($"{c1}{c2}");
            }
            return pairs.ToArray();
        }

        static string PartTwo(string input)
        {
            return PartOne(input);
        }
    }

    public static class Day11Helpers
    {
        public static int[] AsInts(this string s)
        {
            return s.ToCharArray().ToList().ConvertAll(x => (int)x).ToArray();
        }

        public static string AsString(this int[] nums)
        {
            return string.Join("", nums.ToList().ConvertAll<char>(i => (char)i));
        }
    }
}