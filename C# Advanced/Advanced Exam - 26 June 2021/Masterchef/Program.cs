using System;
using System.Collections.Generic;
using System.Linq;

namespace Masterchef
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ingredients = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] freshnessLevel = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            SortedDictionary<string, int> cookedFood = new SortedDictionary<string, int>();
            Dictionary<string, int> dishes = new Dictionary<string, int>();

            dishes.Add("Lobster", 400);
            dishes.Add("Chocolate cake", 300);
            dishes.Add("Green salad", 250);
            dishes.Add("Dipping sauce", 150);

            Queue<int> ingredientValue = new Queue<int>(ingredients);
            Stack<int> freshnessValue = new Stack<int>(freshnessLevel);

            while (freshnessValue.Count != 0 && ingredientValue.Count != 0)
            {
                int totalFreshness = freshnessValue.Peek() * ingredientValue.Peek();
                int ingredientToAdd = ingredientValue.Peek() + 5;
                bool madeChanges = false;

                if (ingredientValue.Peek() == 0)
                {
                    ingredientValue.Dequeue();
                    continue;
                }
                
                foreach (var pair in dishes)
                {
                    if (pair.Value == totalFreshness)
                    {
                        if (!cookedFood.ContainsKey(pair.Key))
                        {
                            cookedFood.Add(pair.Key, 0);
                        }

                        madeChanges = true;
                        cookedFood[pair.Key]++;
                        break;
                    }
                }
                
                ingredientValue.Dequeue();

                if (madeChanges == false)
                {
                    ingredientValue.Enqueue(ingredientToAdd);
                }

                freshnessValue.Pop();
            }

            if (cookedFood.Keys.Count >= 4)
            {
                Console.WriteLine("Applause! The judges are fascinated by your dishes!");
                
                if (ingredientValue.Any())
                {
                    Console.WriteLine($"Ingredients left: {ingredientValue.Sum()}");
                }

                foreach (var pair in cookedFood)
                {
                    Console.WriteLine($" # {pair.Key} --> {pair.Value}");
                }
            }
            else
            {
                Console.WriteLine("You were voted off. Better luck next year.");

                if (ingredientValue.Any())
                {
                    Console.WriteLine($"Ingredients left: {ingredientValue.Sum()}");
                }

                if (cookedFood.Any())
                {
                    foreach (var pair in cookedFood)
                    {
                        Console.WriteLine($" # {pair.Key} --> {pair.Value}");
                    }
                }
            }
        }
    }
}
