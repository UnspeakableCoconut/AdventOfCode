using AdventOfCode;

namespace AoC2015
{
    public class Day9
    {
        public static Dictionary<string, int> distances = [];
        public static List<int> dists = [];

        public static void Run()
        {
            int yr = 2015,
                day = 9;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadDistances(input);
            string[] locs = GetLocs(distances);
            List<List<string>> perms = PermuteLocs(locs, 0, locs.Length - 1, []);
            foreach (List<string> perm in perms) dists.Add(CheckTrip(perm));
            return dists.Min();
        }

        static int PartTwo(string[] input)
        {
            return dists.Max();
        }

        static void LoadDistances(string[] input)
        {
            foreach (string line in input)
            {
                string[] locs = line.ExtractAll("[A-Za-z]{3,}");
                string prim = locs[0],
                    sec = locs[1];
                int dist = line.Extract("\\d+").ToInt();
                distances.Add($"{prim},{sec}", dist);
                distances.Add($"{sec},{prim}", dist);
            }
        }

        static string[] GetLocs(Dictionary<string, int> dict)
        {
            List<string> locs = [];
            foreach (string key in dict.Keys)
            {
                string[] arr = key.Split(",");
                foreach (string s in arr) locs.Add(s);
            }
            return locs.Distinct().ToArray();
        }

        // and suddenly, on day 13, this permutation method becomes useful again
        static List<List<string>> PermuteLocs(string[] locs, int start, int end, List<List<string>> list)
        {
            if (start == end) list.Add(new List<string>(locs));
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Swap(ref locs[start], ref locs[i]);
                    PermuteLocs(locs, start + 1, end, list);
                    Swap(ref locs[start], ref locs[i]);
                }
            }
            return list;
        }

        static void Swap(ref string a, ref string b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        static int CheckTrip(List<string> locs)
        {
            List<int> dists = [];
            string[] keys = GetKeys(locs);
            foreach (string key in keys) dists.Add(distances[key]);
            return dists.Sum();
        }

        static string[] GetKeys(List<string> locs)
        {
            List<string> keys = [];
            for (int i = 1; i < locs.Count; i++) keys.Add($"{locs[i - 1]},{locs[i]}");
            return keys.ToArray();
        }
    }
}
