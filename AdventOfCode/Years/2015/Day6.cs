using AdventOfCode;

namespace AoC2015
{
    public class Day6
    {
        public static Dictionary<string, bool> boolLights = [];
        public static Dictionary<string, int> intLights = [];

        public static void Run()
        {
            int yr = 2015,
                day = 6;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        // part one
        #region
        private static int PartOne(string[] input)
        {
            boolLights = GetBoolLights();
            foreach (string line in input)
            {
                string[] ranges = line.ExtractAll("\\d+,\\d+");
                int[] xArr = ranges.Extract("^\\d+").ToInt().ToRange();
                int[] yArr = ranges.Extract("\\d+$").ToInt().ToRange();
                string[] keys = GetKeys(xArr, yArr);
                string op = line.Extract("on|off");
                foreach (string key in keys) UpdateBoolLight(key, op);
            }
            return boolLights.Where(d => d.Value).ToArray().Length;
        }

        private static Dictionary<string, bool> GetBoolLights()
        {
            Dictionary<string, bool> lights = [];
            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1000; j++)
                    lights.Add($"{i},{j}", false);
            return lights;
        }

        private static string[] GetKeys(int[] xArr, int[] yArr)
        {
            List<string> keys = [];
            foreach (int x in xArr)
                foreach (int y in yArr) keys.Add($"{x},{y}");
            return keys.ToArray();
        }

        private static void UpdateBoolLight(string key, string op)
        {
            boolLights[key] = op == "on" ? true : op == "off" ? false : !boolLights[key];
        }
        #endregion

        // part two
        #region
        private static int PartTwo(string[] input)
        {
            intLights = GetIntLights();
            foreach (string line in input)
            {
                string[] ranges = line.ExtractAll("\\d+,\\d+");
                int[] xArr = ranges.Extract("^\\d+").ToInt().ToRange();
                int[] yArr = ranges.Extract("\\d+$").ToInt().ToRange();
                string[] keys = GetKeys(xArr, yArr);
                string op = line.Extract("on|off");
                foreach (string key in keys) UpdateIntLight(key, op);
            }
            return intLights.Skip(1).Sum(x => x.Value);
        }

        private static Dictionary<string, int> GetIntLights()
        {
            Dictionary<string, int> lights = [];
            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1000; j++)
                    lights.Add($"{i},{j}", 0);
            return lights;
        }

        private static void UpdateIntLight(string key, string op)
        {
            int val = intLights[key];
            if (op == "on") val++;
            else if (op == "off") val = val > 0 ? val - 1 : 0;
            else val += 2;
            intLights[key] = val;
        }
        #endregion
    }

    public static class Day6Helpers
    {
        public static int[] ToRange(this int[] endpoints)
        {
            int start = endpoints[0],
                end = endpoints[1];
            List<int> range = [];
            for (int i = start; i <= end; i++) range.Add(i);
            return [.. range];
        }
    }
}
