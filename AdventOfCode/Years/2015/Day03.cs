using AdventOfCode;

namespace AoC2015
{
    public class Day03
    {
        public static void Run()
        {
            int yr = 2015,
                day = 3;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        private static int PartOne(string input)
        {
            List<string> houses = [];
            char[] dirs = input.ToCharArray();
            int x = 0, y = 0;
            houses.Add($"{x},{y}");
            foreach (char dir in dirs)
            {
                if (dir == '^') y++;
                else if (dir == 'v') y--;
                else if (dir == '>') x++;
                else if (dir == '<') x--;
                houses.Add($"{x},{y}");
            }
            return houses.Distinct().ToArray().Length;
        }

        private static int PartTwo(string input)
        {
            List<string> houses = [];
            char[] dirs = input.ToCharArray();
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            houses.Add($"{x1},{y1}");
            for (int i = 0; i < dirs.Length; i++)
            {
                char dir = dirs[i];
                if (i % 2 == 0)
                {
                    if (dir == '^') y1++;
                    else if (dir == 'v') y1--;
                    else if (dir == '>') x1++;
                    else if (dir == '<') x1--;
                    houses.Add($"{x1},{y1}");
                }
                else
                {
                    if (dir == '^') y2++;
                    else if (dir == 'v') y2--;
                    else if (dir == '>') x2++;
                    else if (dir == '<') x2--;
                    houses.Add($"{x2},{y2}");
                }
            }
            return houses.Distinct().ToArray().Length;
        }
    }
}


