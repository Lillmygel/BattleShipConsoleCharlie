using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipConsoleCharlie.Data
{
    public class Game
    {
        private static List<Ship> MyShips = new List<Ship>()
        {
            new Ship()
            {
                Name="Carrier",
                Health=5,
                YCoordinates= new int[5]{0,1,2,3,4},
                XCoordinates= new int[1]{1}
            },
            new Ship()
            {
                Name="BattleShip",
                Health=4,
                YCoordinates = new int[1] {3},
                XCoordinates = new int[4] {3,4,5,6}
            },
            new Ship()
            {
                Name="Destroyer",
                Health=3,
                YCoordinates = new int[3] {7,8,9},
                XCoordinates = new int[1] {8}
            },
            new Ship()
            {
                Name="Submarine",
                Health=3,
                YCoordinates = new int[1] {5},
                XCoordinates = new int[3] {3,4,5}
            },
            new Ship()
            {
                Name="Patrol Boat",
                Health=2,
                YCoordinates = new int[2] {8,9},
                XCoordinates = new int[1] {7}
            }
        };

        public static string[,] CreatePlayerGrid()
        {
            var grid = new string[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var isShip = MyShips.Where(x => x.YCoordinates.Contains(i) && x.XCoordinates.Contains(j)).FirstOrDefault();
                    if(isShip != null)
                    {
                        var letter = isShip.Name.ToCharArray()[0];
                        grid[i, j] = letter.ToString();
                    }
                    else
                    {
                    grid[i, j] = " ";
                    }
                }
            }

            return grid;
        }

        public static void printBoard(string[,] grid, string[,] enemyGrid)
        {
            Console.WriteLine();
            Console.WriteLine("                MY FIELD                                            ENEMY FIELD");
            Console.WriteLine("   1   2   3   4   5   6   7   8   9   10             1   2   3   4   5   6   7   8   9   10");
            Console.WriteLine("  ---------------------------------------            ---------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write("A");
                        break;
                    case 1:
                        Console.Write("B");
                        break;
                    case 2:
                        Console.Write("C");
                        break;
                    case 3:
                        Console.Write("D");
                        break;
                    case 4:
                        Console.Write("E");
                        break;
                    case 5:
                        Console.Write("F");
                        break;
                    case 6:
                        Console.Write("G");
                        break;
                    case 7: Console.Write("H");
                        break;
                    case 8: Console.Write("I");
                        break;
                    case 9: Console.Write("J");
                        break;
                    default:
                        Console.Write("");
                        break;
                }
                Console.Write("|");
                for (int j = 0; j < 10; j++)
                {
                    
                    Console.Write(" "+grid[i,j]+" ");
                    Console.Write("|");

                }
                Console.Write("         ");
                switch (i)
                {
                    case 0:
                        Console.Write("A");
                        break;
                    case 1:
                        Console.Write("B");
                        break;
                    case 2:
                        Console.Write("C");
                        break;
                    case 3:
                        Console.Write("D");
                        break;
                    case 4:
                        Console.Write("E");
                        break;
                    case 5:
                        Console.Write("F");
                        break;
                    case 6:
                        Console.Write("G");
                        break;
                    case 7:
                        Console.Write("H");
                        break;
                    case 8:
                        Console.Write("I");
                        break;
                    case 9:
                        Console.Write("J");
                        break;
                    default:
                        Console.Write("");
                        break;
                }
                Console.Write("|");
                for (int j = 0; j < 10; j++)
                {

                    Console.Write(" " + enemyGrid[i, j] + " ");
                    Console.Write("|");

                }
                Console.WriteLine();
                Console.Write(" |---------------------------------------|          |---------------------------------------|");
                
                Console.WriteLine();
            }
            Console.WriteLine("_______________________________________________________________________________________________");

        }
    }
}
