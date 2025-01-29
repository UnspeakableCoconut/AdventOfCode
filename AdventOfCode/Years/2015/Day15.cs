using AdventOfCode;
using System.Diagnostics.Metrics;

namespace AoC2015
{
    public class Day15
    {
        private static List<CookieIngredient> ingredients = [];

        public static void Run()
        {
            int yr = 2015,
                day = 15;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input) // part one sounds like a linear algebra problem to me. fingers crossed.
        {
            LoadCookieIngredients(input);
            return FindBestCookie();
        }

        static int PartTwo(string[] input)
        {
            bool considerCalories = true;
            return FindBestCookie(considerCalories);
        }

        static void LoadCookieIngredients(string[] input)
        {
            string numRegex = @"\-?\d+";
            foreach (string line in input)
                ingredients.Add(new CookieIngredient(line, numRegex));
        }

        static int FindBestCookie(bool considerCalories = false)
        {
            CookieIngredient ing1 = ingredients[0],
                ing2 = ingredients[1],
                ing3 = ingredients[2],
                ing4 = ingredients[3];
            int max = 0,
                capacity,
                durability,
                flavor,
                texture,
                total;
            for (int a = 0; a < 100; a++)
            {
                for (int b = 0; b < 100 - a; b++)
                {
                    for (int c = 0; c < 100 - a - b; c++)
                    {
                        int d = 100 - a - b - c;
                        if (considerCalories)
                        {
                            if (!Has500Calories(a, b, c, d)) continue;
                        }
                        capacity = (a * ing1.Capacity) + (b * ing2.Capacity) + (c * ing3.Capacity) + (d * ing4.Capacity);
                        if (capacity < 0) 
                            capacity = 0;
                        durability = (a * ing1.Durability) + (b * ing2.Durability) + (c * ing3.Durability) + (d * ing4.Durability);
                        if (durability < 0)
                            durability = 0;
                        flavor = (a * ing1.Flavor) + (b * ing2.Flavor) + (c* ing3.Flavor) + (d * ing4.Flavor);
                        if (flavor < 0) 
                            flavor = 0;
                        texture = (a * ing1.Texture) + (b * ing2.Texture) + (c * ing3.Texture) + (d * ing4.Texture);
                        if (texture < 0) 
                            texture = 0;
                        total = capacity * durability * flavor * texture;
                        if (max < total)
                            max = total;
                    }
                }
            }
            return max;
        }

        static bool Has500Calories(int a, int b, int c, int d)
        {
            int calories = (a * ingredients[0].Calories) +
                (b * ingredients[1].Calories) +
                (c * ingredients[2].Calories) +
                (d * ingredients[3].Calories);
            return calories == 500;
        }
    }    

    public readonly struct CookieIngredient(string line, string numRegex)   // intellisense suggested I make this a primary constructor.
    {                                                                       // this is almost illegible to me.
        public int Capacity { get; } = line.Extract("capacity[^,]*,").Extract(numRegex).ToInt();
        
        public int Durability { get; } = line.Extract("durability[^,]*,").Extract(numRegex).ToInt();
        
        public int Flavor { get; } = line.Extract("flavor[^,]*,").Extract(numRegex).ToInt();
        
        public int Texture { get; } = line.Extract("texture[^,]*,").Extract(numRegex).ToInt();

        public int Calories { get; } = line.Extract("calories.*$").Extract(numRegex).ToInt();
    }
}
