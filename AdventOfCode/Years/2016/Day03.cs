using AdventOfCode;
using System.ComponentModel.DataAnnotations.Schema;

namespace AoC2016
{
    public class Day03
    {
        public static void Run()
        {
            int yr = 2016,
                day = 03;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            return input.Where(line => IsTriangle(line)).ToArray().Length;
        }

        static int PartTwo(string[] input)
        {
            input = input.Select(line => line.ReplaceRegex(" {2,}", " ").Trim())
                .ToArray().Extract(@"\d+ \d+ \d+");
            string[] firstCol = input.Extract(@"^\d+"),
                secondCol = input.Extract(@" \d+ "),
                thirdCol = input.Extract(@"\d+$");

            return CountColumnTriangles(firstCol) +
                CountColumnTriangles(secondCol) +
                CountColumnTriangles(thirdCol);
        }

        static bool IsTriangle(string line)
        {
            int[] sides = line.ExtractAll(@"\d+").ToInt();
            int first = sides[0],
                second = sides[1],
                third = sides[2];
            return
                (first + second > third) &&
                (first + third > second) &&
                (second + third > first);
        }

        static int CountColumnTriangles(string[] column)
        {
            int count = 0;
            string line;
            for (int i = 0; i < column.Length; i += 3)
            {
                line = $"{column[i]} {column[i + 1]} {column[i + 2]}";
                if (IsTriangle(line))
                    count++;
            }
            return count;
        }
    }
}