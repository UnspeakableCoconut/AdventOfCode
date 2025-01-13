using AdventOfCode;

namespace AoC2015
{
    public class Day1
    {
        public static void Run()
        {
            int yr = 2015,
                day = 1;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        private static int PartOne(string input)
        {
            int up = input.ExtractAll("\\(").Length;
            int down = input.ExtractAll("\\)").Length;
            return up - down;
        }

        private static int PartTwo(string input)
        {
            char[] steps = input.ToCharArray();
            int floor = 0,
                count = 0;
            for (int i = 0; i < steps.Length; i++)
            {
                if (floor < 0) continue;
                if (steps[i] == '(') floor++;
                else floor--;
                count = i;
            }
            return count + 1;
        }
    }
}
