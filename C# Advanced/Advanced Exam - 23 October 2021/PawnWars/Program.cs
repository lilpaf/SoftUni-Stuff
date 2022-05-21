using System;

namespace Pawn_Wars
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] matrix = new Char[8, 8];

            int[] coordinates0 = new[] { 8, 7, 6, 5, 4, 3, 2, 1 };
            char[] coordinates1 = new[] {'a', 'b', 'c', 'd', 'e', 'f','g', 'h'};

            int[] positionWhite = null;
            int[] positionBlack = null;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string input = Console.ReadLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = input[j];
                    
                    if (input[j] == 'b')
                    {
                        positionBlack = new[] {i, j};
                    }
                    else if (input[j] == 'w')
                    {
                        positionWhite = new[] {i, j};
                    }
                }
            }

            while (true)
            {
                
                if (positionWhite[0] - 1 >= 0)
                {
                    if (positionWhite[0] - 1 == positionBlack[0])
                    {
                        if (positionWhite[1] - 1 == positionBlack[1] || positionWhite[1] + 1 == positionBlack[1])
                        {
                            Console.WriteLine($"Game over! White capture on {coordinates1[positionBlack[1]]}{coordinates0[positionBlack[0]]}.");
                            break;
                        }
                    }

                    matrix[positionWhite[0], positionWhite[1]] = '-';
                    matrix[positionWhite[0] - 1, positionWhite[1]] = 'w';
                    positionWhite = new[] { positionWhite[0] - 1, positionWhite[1] };
                }
                else
                {
                    Console.WriteLine($"Game over! White pawn is promoted to a queen at {coordinates1[positionWhite[1]]}{coordinates0[positionWhite[0]]}.");
                    break;
                }

                if (positionBlack[0] + 1 < matrix.GetLength(0))
                {
                    if (positionBlack[0] + 1 == positionWhite[0])
                    {
                        if (positionBlack[1] + 1 == positionWhite[1] || positionBlack[1] - 1 == positionWhite[1])
                        {
                            Console.WriteLine($"Game over! Black capture on {coordinates1[positionWhite[1]]}{coordinates0[positionWhite[0]]}.");
                            break;
                        }
                    }
                    
                    matrix[positionBlack[0], positionBlack[1]] = '-';
                    matrix[positionBlack[0] + 1, positionBlack[1]] = 'b';
                    positionBlack = new[] {positionBlack[0] + 1, positionBlack[1]};
                }
                else
                {
                    Console.WriteLine($"Game over! Black pawn is promoted to a queen at {coordinates1[positionBlack[1]]}{coordinates0[positionBlack[0]]}.");
                    break;
                }
                
            }

        }
    }
}
