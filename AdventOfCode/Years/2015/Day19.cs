using AdventOfCode;

namespace AoC2015
{
    public class Day19
    {
        public static List<string[]> converters = [];

        public static void Run()
        {
            int yr = 2015,
                day = 19;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            LoadConverter(input);
            string molecule = input[input.Length - 1];
            return GetPossibleMolecules(molecule);
        }

        static int PartTwo(string[] input)
        {
            return 0;
        }

        static void LoadConverter(string[] input)
        {
            input = input.Where(line => line.Contains("=>")).ToArray();
            foreach (string line in input)
            {
                string key = line.Extract(@"^\w+");
                string value = line.Extract(@"\w+$");
                converters.Add([key, value]);
            }
        }

        static int GetPossibleMolecules(string molecule)
        {
            List<string> molecules = []; // don't include original molecule in count
            foreach (string[] converter in converters)
            {
                string[] editedMolecules = FindNewMolecules(molecule, converter);
                foreach (string editedMolecule in editedMolecules)
                    molecules.Add(editedMolecule);
            }
            Console.WriteLine(string.Join(Environment.NewLine + Environment.NewLine + Environment.NewLine, molecules));
            return molecules.Count();
        }

        static string[] FindNewMolecules(string molecule, string[] converter)
        {
            string toReplace = converter[0],
                rplcmnt = converter[1],
                newMolecule;
            string[] parts = molecule.Split(toReplace);
            List<string> editedMolecules = [];
            for (int i = 0; i < parts.Length; i++)
            {
                newMolecule = "";
                for (int j = 0; j < parts.Length; j++)
                {
                    newMolecule += parts[i];
                    if (parts[j] != parts[parts.Length - 1]) newMolecule += (i == j ? rplcmnt : toReplace);
                }
                editedMolecules.Add(newMolecule);
            }
            return editedMolecules.Distinct().ToArray();
        }
    }
}