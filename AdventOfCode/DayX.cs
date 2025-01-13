using AdventOfCode;

namespace AoC2015
{
    public class DayX
    {
        public static void Run()
        {
            int yr = 20,
                day = 0;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            return 0;
        }

        static int PartTwo(string[] input)
        {
            return 0;
        }
    }
}
