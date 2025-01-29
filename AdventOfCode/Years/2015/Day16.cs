using AdventOfCode;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace AoC2015
{
    public class Day16
    {
        private static readonly List<AuntSue> aunts = [];

        public static void Run()
        {
            int yr = 2015,
                day = 16;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadAunts(input);
            for (int i = 0; i < aunts.Count; i++)
            {
                if (CheckAunt(aunts[i])) return i + 1;
            }
            return 0;
        }

        static int PartTwo(string[] input)
        {
            for (int i = 0; i < aunts.Count; i++)
            {
                if (CheckAuntRange(aunts[i])) return i + 1;
            }
            return 0;
        }

        static void LoadAunts(string[] input)
        {
            foreach (string line in input)
                aunts.Add(new AuntSue(line));
        }

        static bool CheckAunt(AuntSue sue)
        {
            bool not = false;
            if (!CheckStaticAttributes(sue)) return not;
            if (sue.Cats != 7 && sue.Cats != -1) return not;
            if (sue.Pomeranians != 3 && sue.Pomeranians != -1) return not;
            if (sue.Goldfish != 5 && sue.Goldfish != -1) return not;
            if (sue.Trees != 3 && sue.Trees != -1) return not;
            return true;
        }

        static bool CheckStaticAttributes(AuntSue sue)
        {
            bool not = false;
            if (sue.Children != 3 && sue.Children != -1) return not;
            if (sue.Samoyeds != 2 && sue.Samoyeds != -1) return not;
            if (sue.Akitas != -1) return not;
            if (sue.Vizslas != -1) return not;
            if (sue.Cars != 2 && sue.Cars != -1) return not;
            if (sue.Perfumes != 1 && sue.Perfumes != -1) return not;
            return true;
        }

        static bool CheckAuntRange(AuntSue sue)
        {
            bool not = false;
            if (!CheckStaticAttributes(sue)) return not;
            if (sue.Cats <= 7 && sue.Cats != -1) return not;
            if (sue.Pomeranians >= 3 && sue.Pomeranians != -1) return not;
            if (sue.Goldfish >= 5 && sue.Goldfish != -1) return not;
            if (sue.Trees <= 3 && sue.Trees != -1) return not;
            return true;
        }
    }

    public readonly struct AuntSue
    {
        public string Input { get; }

        public int Children { get; }

        public int Cats { get; }

        public int Samoyeds { get; }

        public int Pomeranians { get; }

        public int Akitas { get; }

        public int Vizslas { get; }

        public int Goldfish { get; }
        
        public int Trees { get; }

        public int Cars { get; }

        public int Perfumes { get; }

        public AuntSue(string input)
        {
            Input = input;
            Children = CheckAuntFor("children");
            Cats = CheckAuntFor("cats");
            Samoyeds = CheckAuntFor("samoyeds");
            Pomeranians = CheckAuntFor("pomeranians");
            Akitas = CheckAuntFor("akitas");
            Vizslas = CheckAuntFor("vizslas");
            Goldfish = CheckAuntFor("goldfish");
            Trees = CheckAuntFor("trees");
            Cars = CheckAuntFor("cars");
            Perfumes = CheckAuntFor("perfumes");
        }

        private int CheckAuntFor(string key)
        {
            string extracted = Input.Extract(key + @": \d+");
            return extracted == "" ? -1 : extracted.Extract(@"\d+").ToInt(); // default to -1 because some of them have a known 0
        }
    }
}