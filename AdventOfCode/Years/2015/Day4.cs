using AdventOfCode;
using System.Security.Cryptography;
using System.Text;

namespace AoC2015
{
    public class Day4
    {
        public static void Run()
        {
            int yr = 2015,
                day = 4;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        private static int PartOne(string input)
        {
            MD5 md5 = MD5.Create();
            bool found = false;
            int i = 1;
            while (!found)
            {
                byte[] buffer = Encoding.ASCII.GetBytes($"{input}{i}");
                buffer = md5.ComputeHash(buffer);
                found = buffer[0] == 0 && buffer[1] == 0 && buffer[2] < 10;
                i++;
            }
            return i - 1;
        }

        private static int PartTwo(string input)
        {
            MD5 md5 = MD5.Create();
            bool found = false;
            int i = 1;
            while (!found)
            {
                byte[] buffer = Encoding.ASCII.GetBytes($"{input}{i}");
                buffer = md5.ComputeHash(buffer);
                found = buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0;
                i++;
            }
            return i - 1;
        }
    }
}