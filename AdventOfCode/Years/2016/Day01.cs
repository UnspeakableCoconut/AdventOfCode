using AdventOfCode;

namespace AoC2016
{
    public class Day01
    {
        public static List<string> locations = [];
        private static string[] instructions = [];
        private static int x = 0, y = 0, direction = 0, dist;

        public static void Run()
        {
            int yr = 2016,
                day = 1;
            string input = Setup.GetInput(yr, day)[0];
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string input)
        {
            instructions = input.Split(',').Select(x => x.Trim()).ToArray();
            foreach(string line in instructions)
            {
                direction = GetNextDirection(line, direction);
                dist = line.Extract(@"\d+").ToInt();
                UpdateLocation();
            }
            return GetBlocksMoved();
        }

        static int PartTwo(string input)
        {
            x = 0;
            y = 0;
            direction = 0;
            return FindDuplicateLocation(instructions);
        }

        static int GetBlocksMoved()
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        static int GetNextDirection(string line, int direction)
        {
            if (line[0] == 'R') direction++; else direction--;
            return direction == 4 ? 0 : direction == -1 ? 3 : direction;
        }

        static void UpdateLocation()
        {
            if (direction == 0) y += dist;
            else if (direction == 1) x += dist;
            else if (direction == 2) y -= dist;
            else if (direction == 3) x -= dist;
        }

        static int FindDuplicateLocation(string[] instructions)
        {
            int toMove;
            string loc;
            foreach (string line in instructions)
            {
                direction = GetNextDirection(line, direction);
                dist = line.Extract(@"\d+").ToInt();
                if (direction == 0)
                {
                    toMove = y + dist;
                    while (y < toMove)
                    {
                        loc = $"{x},{y}";
                        if (locations.Contains(loc)) return GetBlocksMoved();
                        locations.Add(loc);
                        y++;
                    }
                }
                else if (direction == 1)
                {
                    toMove = x + dist;
                    while (x < toMove)
                    {
                        loc = $"{x},{y}";
                        if (locations.Contains(loc)) return GetBlocksMoved();
                        locations.Add(loc);
                        x++;
                    }
                }
                else if (direction == 2)
                {
                    toMove = y - dist;
                    while (y > toMove)
                    {
                        loc = $"{x},{y}";
                        if (locations.Contains(loc)) return GetBlocksMoved();
                        locations.Add(loc);
                        y--;
                    }
                }
                else if (direction == 3)
                {
                    toMove = x - dist;
                    while (x > toMove)
                    {
                        loc = $"{x},{y}";
                        if (locations.Contains(loc)) return GetBlocksMoved();
                        locations.Add(loc);
                        x--;
                    }
                }
            }
            return 0;
        }
    }
}