using System;
using System.Linq;

namespace Survivor
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());

            char[][] jaggedArr = new Char[rows][];

            for (int i = 0; i < rows; i++)
            {
                char[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                
                jaggedArr[i] = input;
                
            }

            string[] commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int yourTokens = 0;
            int opponentTokens = 0;

            while (commands[0] != "Gong")
            {
                int row = int.Parse(commands[1]);
                int col = int.Parse(commands[2]);
                
                if (commands[0] == "Find")
                {
                    if (row >= 0 && row < jaggedArr.GetLength(0) && col >= 0 && col < jaggedArr[row].Length)
                    {
                        if (jaggedArr[row][col] == 'T')
                        {
                            yourTokens++;
                            jaggedArr[row][col] = '-';
                        }
                    }
                }
                
                else if (commands[0] == "Opponent")
                {
                    string direction = commands[3];

                    if (row >= 0 && row < jaggedArr.GetLength(0) && col >= 0 && col < jaggedArr[row].Length)
                    {
                        if (jaggedArr[row][col] == 'T')
                        {
                            opponentTokens++;
                            jaggedArr[row][col] = '-';
                        }

                        switch (direction)
                        {
                            case "up":
                                for (int i = 3 ; i > 0; i--)
                                {
                                    if (row - i >= 0)
                                    {
                                        if (jaggedArr[row - i][col] == 'T')
                                        {
                                            opponentTokens++;
                                            jaggedArr[row - i][col] = '-';
                                        }
                                    }
                                }
                                break;
                            
                            case "down":
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (row + i < jaggedArr.GetLength(0))
                                    {
                                        if (jaggedArr[row + i][col] == 'T')
                                        {
                                            opponentTokens++;
                                            jaggedArr[row + i][col] = '-';
                                        }
                                    }
                                }
                                break;
                            
                            case "left":
                                for (int i = 3; i > 0; i--)
                                {
                                    if (col - i >= 0)
                                    {
                                        if (jaggedArr[row][col - i] == 'T')
                                        {
                                            opponentTokens++;
                                            jaggedArr[row][col - i] = '-';
                                        }
                                    }
                                }
                                break;
                            
                            case "right":
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (col + i < jaggedArr[row].Length)
                                    {
                                        if (jaggedArr[row][col + i] == 'T')
                                        {
                                            opponentTokens++;
                                            jaggedArr[row][col + i] = '-';
                                        }
                                    }
                                }
                                break;
                        }
                        
                    }
                }

                commands = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            for (int i = 0; i < jaggedArr.GetLength(0); i++)
            {
                Console.WriteLine(string.Join(' ', jaggedArr[i]));
            }

            Console.WriteLine($"Collected tokens: {yourTokens}");
            Console.WriteLine($"Opponent's tokens: {opponentTokens}");
        }
    }
}
