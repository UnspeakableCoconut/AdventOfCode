using AdventOfCode;

namespace AoC2015
{
    public class Day17
    {
        public static int[,] counts = new int[0,0]; // this looks like an owl

        public static void Run()
        {
            int yr = 2015,
                day = 17;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            int[] containers = input.ToInt();
            counts = LoadCounts(containers);
            return Enumerable.Range(0, 20).Sum(i => counts[150, i]);
        }

        static int PartTwo(string[] input)
        {
            int minCount = Enumerable.Range(0, 21).Where(n => counts[150, n] > 0).Min();
            return counts[150, minCount];
        }

        static int[,] LoadCounts(int[] containers)
        {
            int[,] counts = new int[151, 21];
            counts[0, 0] = 1;
            foreach (int sizeOf in containers)
            {
                for (int volume = 150 - sizeOf; volume >= 0; volume--)
                {
                    for (int count = 20; count > 0; count--)
                        counts[volume + sizeOf, count] += counts[volume, count - 1];
                }
            }
            return counts;
        }
    }
}