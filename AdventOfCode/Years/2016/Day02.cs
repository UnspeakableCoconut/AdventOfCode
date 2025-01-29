using AdventOfCode;
using System.ComponentModel;
using System.Formats.Asn1;

namespace AoC2016
{
    public class Day02
    {
        private static readonly int[]
            iNoUp = [1, 2, 3],
            iNoRight = [3, 6, 9],
            iNoDown = [7, 8, 9],
            iNoLeft = [1, 4, 7];

        private static string keypad = "123|456|789".Replace("|", "");
        private static char[]
            noUp = "123".ToCharArray(),
            noRight = "369".ToCharArray(),
            noDown = "789".ToCharArray(),
            noLeft = "147".ToCharArray();

        public static void Run()
        {
            int yr = 2016,
                day = 02;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static string PartOne(string[] input)
        {
            return FindBathroomCode(input);
        }

        static string PartTwo(string[] input)
        {
            LoadNewKeypad();
            return FindBathroomCode(input);
        }

        static string FindBathroomCode(string[] input)
        {
            char btn = '5';
            string ans = "";
            int verticalJump = Math.Sqrt(keypad.Length).ToString().ToInt();
            foreach (string line in input)
            {
                btn = FindNextPress(btn, line, verticalJump);
                ans += btn;
            }
            return ans;
        }

        static char FindNextPress(char btn, string line, int verticalJump)
        {
            foreach (char direction in line.ToCharArray())
                btn = FindNextBtn(btn, direction, verticalJump);
            return btn;
        }

        static char FindNextBtn(char c, char direction, int verticalJump)
        {
            int index = keypad.IndexOf(c);
            if (direction == 'U' && !noUp.Contains(c))
                index -= verticalJump;
            else if (direction == 'R' && !noRight.Contains(c))
                index += 1;
            else if (direction == 'D' && !noDown.Contains(c))
                index += verticalJump;
            else if (direction == 'L' && !noLeft.Contains(c))
                index -= 1;
            return keypad[index];
        }

        static void LoadNewKeypad()
        {
            keypad = "  1  | 234 |56789| ABC |  D  ".Replace("|", "");
            noUp = "52149".ToCharArray();
            noRight = "149CD".ToCharArray();
            noDown = "5ADC9".ToCharArray();
            noLeft = "125AD".ToCharArray();
        }
    }
}