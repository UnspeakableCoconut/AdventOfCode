using AdventOfCode;

namespace AoC2015
{
    public class Day2
    {
        public static void Run()
        {
            int yr = 2015,
                day = 2;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        private static int PartOne(string[] input)
        {
            List<int> areas = [];
            foreach (string line in input) 
                areas.Add(FindArea(line));
            return areas.Sum();
        }

        private static int FindArea(string line)
        {
            int[] dims = Array.ConvertAll(line.Split("x"), int.Parse);
            int l = dims[0],
                w = dims[1],
                h = dims[2],
                min = dims.Min(),
                mid = dims.Where((src, index) => index != Array.IndexOf(dims, min)).ToArray().Min(),
                area = (2 * l * w) + (2 * w * h) + (2 * h * l) + (min * mid);
            return area;
        }

        private static int PartTwo(string[] input)
        {
            List<int> lengths = [];
            foreach (string line in input)
                lengths.Add(FindRibbon(line));
            return lengths.Sum();
        }

        private static int FindRibbon(string line)
        {
            int[] dims = Array.ConvertAll(line.Split("x"), int.Parse);
            int min = dims.Min(),
                mid = dims.Where((src, index) => index != Array.IndexOf(dims, min)).ToArray().Min(),
                length = (2 * min) + (2 * mid) + dims.Aggregate(1, (a, b) => a * b);
            return length;
        }
    }
}
