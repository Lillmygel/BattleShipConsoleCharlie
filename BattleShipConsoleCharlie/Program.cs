using BattleShipConsoleCharlie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipConsoleCharlie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(95, 45);
            Console.SetWindowPosition(5, 0);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~Welcome to Charlie & Max's Battleship game~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Game.printBoard(Game.CreatePlayerGrid(), Game.CreatePlayerGrid());
            Console.Write("Var god skriv in namn: ");
            var Name = Console.ReadLine();
            Console.WriteLine($"Hej {Name}, vill du söka efter spel?(Ja/Nej).");
            Console.Write("Om du svarar nej, så kommer du att vara värd för en spelomgång: ");
            var search = Console.ReadLine();
            Console.ReadKey();
        }
    }
}
