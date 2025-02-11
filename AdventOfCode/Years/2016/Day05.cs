using AdventOfCode;
using AoC2015;
using System.Security.Cryptography;

namespace AoC2016
{
    public class Day05
    {
        public static void Run()
        {
            int yr = 2016,
                day = 5;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static string PartOne(string input)
        {
            string result = "";
            int i = 1;
            while (result.Length < 8)
            {
                result += GetHash5(input + i);
                i++;
            }
            return result;
        }

        static string PartTwo(string input)
        {
            char[] result = "        ".ToCharArray();
            int i = 1;
            while (result.Conjoin().Matches(@"\s"))
            {
                result = GetHashVariable(input + i, result);
                i++;
            }
            return result.Conjoin();
        }

        static string GetHash(string input)
        {
            return MD5.HashData(System.Text.Encoding.UTF8.GetBytes(input)).Select(s => s.ToString("X2")).Conjoin();
        }

        static string GetHash5(string input)
        {
            string hash = GetHash(input);
            return hash.Extract("^0{5}") == "" ? "" : hash[5].ToString();
        }

        static char[] GetHashVariable(string input, char[] result)
        {
            string hash = GetHash(input);
            if (hash.Extract("^0{5}[0-7]") == "") return result;
            int posToUpdate = hash[5].ToString().ToInt();
            if (result[posToUpdate] != ' ') return result;
            result[posToUpdate] = hash[6];
            return result;
        }
    }
}