using AdventOfCode;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day7
    {
        private static Dictionary<string, int?> wires = [];
        private static Dictionary<string, string> ops = [];

        public static void Run()
        {
            int yr = 2015,
                day = 7;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo().Message(2);
        }

        // parts
        static int PartOne(string[] input)
        {
            wires = FindWires(input);
            ops = GetOps(input);
            while (wires["a"] == null) ParseLines(ops);
            return int.Parse("0" + wires["a"]);
        }

        static int PartTwo()
        {
            int? start = wires["a"];
            foreach (string key in wires.Keys) wires[key] = null;
            wires["b"] = start;
            while (wires["a"] == null) ParseLines(ops);
            return int.Parse("0" + wires["a"]);
        }

        static Dictionary<string, int?> FindWires(string[] inputs)
        {
            Dictionary<string, int?> keys = [];
            string[] toRemove = ["->", "AND", "RSHIFT", "OR", "NOT", "LSHIFT"];

            string[] wires = string.Join(" ", inputs).Split(" ")
                .Where(s => !(toRemove.Contains(s) || Regex.IsMatch(s, "\\d+")))
                .Distinct().ToArray();

            foreach (string wire in wires)
            {
                keys.Add(wire, null);
            }
            return keys;
        }

        static Dictionary<string, string> GetOps(string[] inputs)
        {
            Dictionary<string, string> keys = [];
            foreach (string line in inputs)
            {
                string[] sides = line.Split(" -> ");
                keys.Add(sides[1], sides[0]);
            }
            return keys;
        }

        // actions
        static void ParseLines(Dictionary<string, string> ops)
        {
            foreach (string key in ops.Keys)
            {
                if (wires[key] != null) continue;
                wires[key] = ParseOp(ops[key]);
            }
        }

        static int? ParseOp(string op)
        {
            string[] parts = op.Split(' ');
            int? num = parts.Length == 3 ? ParseTwo(parts) :
                parts.Length == 2 ? ~wires[parts[1]] :
                parts.Length == 1 ? (parts[0].Matches("\\d+") ? parts[0].ToPossInt() : wires[parts[0]]) : null;
            return num;
        }

        static int? ParseTwo(string[] parts)
        {
            string key1 = parts[0];
            string key2 = parts[2];
            int? num1 = key1.Matches("\\d+") ? key1.ToPossInt() : wires[key1];
            int? num2 = key2.Matches("\\d+") ? key2.ToPossInt() : wires[key2];
            if (num1 == null || num2 == null) return null;

            string op = parts[1];
            if (op == "AND") return (int)num1 & num2;
            else if (op == "OR") return (int)num1 ^ num2;
            else if (op == "RSHIFT") return (int)num1 >> num2;
            else if (op == "LSHIFT") return (int)num1 << num2;
            else return null;
        }
    }

    public static class Day7Helpers
    {
        public static int? ToPossInt(this object obj)
        {
            string sObj = obj + "";
            if (!sObj.Matches("^\\-?\\d+$")) return null;
            string toAdd = sObj.Matches("^\\-") ? "" : "0";
            return int.Parse(toAdd + sObj);
        }

        public static bool Matches(this string s, string regex)
        {
            return Regex.IsMatch(s, regex);
        }
    }
}
