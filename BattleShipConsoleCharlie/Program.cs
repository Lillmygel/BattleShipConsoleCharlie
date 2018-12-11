using BattleShipConsoleCharlie.Data;
using System;
using System.Collections.Generic;

namespace BattleShipConsoleCharlie
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize all objects
            var player = new Player();
            var opponent = new Player()
            {
                HostAddress = ""
            };
            var search = "";
            var game = new Game();
            var intTryParser = 0;

            List<Ship> myShips = new List<Ship>()
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
            var enemyShips = new List<Ship>();


            //Call functions
            Console.SetWindowSize(95, 45);
            Console.SetWindowPosition(5, 0);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~Welcome to Charlie & Max's Battleship game~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Game.SavedMessages.Add(new PrintMessage() { Message = "~~~~~~~~~~~~~~~~~~~~~~~~~~Welcome to Charlie & Max's Battleship game~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n", isBlue = false });
            Console.Write("Var god skriv in namn: ");
            Game.SavedMessages.Add(new PrintMessage() { Message = "Var god skriv in namn: ", isBlue = false });
            player.Name = Console.ReadLine();
            Game.SavedMessages.Add(new PrintMessage() { Message = $"{player.Name}\r\n", isBlue = false });
            do
            {
                Console.WriteLine($"Hej {player.Name}, vill du söka efter spel?(Ja/Nej).");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Hej {player.Name}, vill du söka efter spel?(Ja/Nej).\r\n", isBlue = false });
                Console.Write("Om du svarar nej, så kommer du att vara värd för en spelomgång: ");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Om du svarar nej, så kommer du att vara värd för en spelomgång: ", isBlue = false });
                search = Console.ReadLine().ToLower();
                Game.SavedMessages.Add(new PrintMessage() { Message = $"{search}\r\n", isBlue = false });
            } while (search != "ja" && search != "nej");
            Game.printBoard(Game.CreatePlayerGrid(myShips), Game.CreatePlayerGrid(enemyShips), player, opponent);
            if (search == "ja")
            {
                Console.Write("Var god och skriv in värdens adress: ");
                Game.SavedMessages.Add(new PrintMessage() { Message = "Var god och skriv in värdens adress: ", isBlue = false });
                opponent.HostAddress = Console.ReadLine();
                Game.SavedMessages.Add(new PrintMessage() { Message = $"{opponent.HostAddress}\r\n", isBlue = false });
            }

            bool success;
            do
            {
                Console.Write("Var god och skriv in port: ");
                Game.SavedMessages.Add(new PrintMessage() { Message = "Var god och skriv in port: ", isBlue = false });
                var portInput = Console.ReadLine();
                Game.SavedMessages.Add(new PrintMessage() { Message = $"{portInput}\r\n", isBlue = false });
                success = Int32.TryParse(portInput, out intTryParser);
                if (!success)
                {
                    Console.WriteLine("Port måste vara ett nummer");
                    Game.SavedMessages.Add(new PrintMessage() { Message = "Port måste vara ett nummer\r\n", isBlue = false });
                }
            } while (!success);
            opponent.Port = intTryParser;
            game.Play(opponent.HostAddress, opponent.Port, player);
            Game.printBoard(Game.CreatePlayerGrid(myShips), Game.CreatePlayerGrid(enemyShips), player, opponent);
            Console.ReadKey();
        }
    }
}
