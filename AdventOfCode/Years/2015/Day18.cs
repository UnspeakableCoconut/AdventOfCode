using AdventOfCode;

namespace AoC2015
{
    public class Day18
    {
        public static int[,] originalLights = new int[100, 100];
        public static int[,] lights = new int[100, 100];
        public static int[,] nextLights = new int[100, 100];

        public static void Run()
        {
            int yr = 2015,
                day = 18;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo().Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadMatrix(input);
            originalLights = lights;
            for (int i = 0; i < 100; i++)
                ConfigureLights();
            return CountLightsOn();
        }

        static int PartTwo()
        {
            lights = originalLights;
            KeepCornersOn();
            for (int i = 0; i < 100; i++)
                ConfigureLights(true);
            return CountLightsOn();
        }

        static void LoadMatrix(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                        lights[i, j] = 1;
                    else lights[i, j] = 0;
                }
            }
        }

        static void ConfigureLights(bool keepCorners = false)
        {
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    nextLights[x, y] = GetNextState(x, y);
                }
            }
            lights = nextLights;
            if (keepCorners) KeepCornersOn();
            nextLights = new int[100, 100]; // be sure to reset the nextLights object
        }

        static int GetNextState(int x, int y)
        {
            int curr = lights[x, y];
            int neighborsOn = SumNeighbors(x, y);
            if (curr == 0 && neighborsOn == 3)
                return 1;
            else if (curr == 1 && (neighborsOn == 2 || neighborsOn == 3))
                return 1;
            return 0;
        }

        static int SumNeighbors(int x, int y)
        {
            List<int[]> toCheck =
            [
                [ x - 1, y - 1 ],
                [ x, y - 1 ],
                [ x + 1, y - 1 ],
                [ x + 1, y ],
                [ x + 1, y + 1 ],
                [ x, y + 1 ],
                [ x - 1, y + 1 ],
                [ x - 1, y ]
            ];
            int sum = 0;
            foreach (int[] arr in toCheck)
            {
                if (arr[0] < 0 || arr[0] > 99 || arr[1] < 0 || arr[1] > 99) continue;
                sum += lights[arr[0], arr[1]];
            }
            return sum;
        }

        static int CountLightsOn()
        {
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                    sum += lights[i, j];
            }
            return sum;
        }

        static void KeepCornersOn()
        {
            List<int[]> leaveOn =
            [
                [0, 0],
                [0, 99],
                [99, 0],
                [99, 99],
            ];
            foreach (int[] arr in leaveOn)
                lights[arr[0], arr[1]] = 1;
        }
    }
}