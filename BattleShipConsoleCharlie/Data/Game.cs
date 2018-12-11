using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipConsoleCharlie.Data
{
    public class Game
    {
        public static List<PrintMessage> SavedMessages = new List<PrintMessage>();

        Dictionary<int, string> responses = new Dictionary<int, string>()
        {
            {210,"210 BATTLESHIP/1.0" },
            {220, "220 <remote player name>" },
            {221, "221 Client Starts" },
            {222, "222 Host Starts" },
            {230, "230 Miss!" },
            {241, "241 You hit my Carrier" },
            {242, "242 You hit my Battleship" },
            {243, "243 You hit my Destroyer" },
            {244, "244 You hit my Submarine" },
            {245, "245 You hit my Patrol Boat" },
            {251, "251 You sunk my Carrier" },
            {252, "252 You sunk my Battleship" },
            {253, "253 You sunk my Destroyer" },
            {254, "254 You sunk my Submarine" },
            {255, "255 You sunk my Patrol Boat" },
            {260, "260 You win!" },
            {270, "270 Connection closed" },
            {500, "500 Syntax error" },
            {501, "501 Sequence error" }
        };
        TcpListener _listener;

        public static string[,] CreatePlayerGrid(List<Ship> MyShips)
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

        public static void printBoard(string[,] grid, string[,] enemyGrid, Player player, Player opponent)
        {
            Console.Clear();
            Console.WriteLine($"Me: Name:{player.Name} Address:{player.HostAddress} Port:{player.Port} | Enemy: Name:{opponent.Name} Address:{opponent.HostAddress}  Port:{opponent.Port}");
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
            Console.WriteLine();

            foreach (var message in Game.SavedMessages)
            {
                if (message.isBlue)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(message.Message);
                Console.ResetColor();
            }
        }

        public void StartListen(int port)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();
                Console.WriteLine($"Börjar lyssna efter ett spel på port: {port}");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Börjar lyssna efter ett spel på port: {port}\r\n", isBlue = false });
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Kunde inte lyssna på port {port}. Kanske redan används?.");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Kunde inte lyssna på port {port}. Kanske redan används?.\r\n", isBlue = false });
                Environment.Exit(1);
            }
        }

        public void Host(Player player)
        {
            while (true)
            {
                Console.WriteLine("Väntar på motståndare...");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Väntar på motståndare...\r\n", isBlue = false });

                using (var client = _listener.AcceptTcpClient())
                using (var networkStream = client.GetStream())
                using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
                using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
                {
                    Console.WriteLine($"Du är nu uppkopplad mot:  {client.Client.RemoteEndPoint}!");
                    Game.SavedMessages.Add(new PrintMessage() { Message = $"Du är nu uppkopplad mot:  {client.Client.RemoteEndPoint}!\r\n", isBlue = false });
                    writer.WriteLine(responses[210]);
                    TcpMessage(true, responses[210]);

                    while (client.Connected)
                    {
                        var command = reader.ReadLine();
                        TcpMessage(false, command);
                        writer.WriteLine("220 " + player.Name);
                        TcpMessage(true, "220 " + player.Name);


                        if (string.Equals(command, "EXIT", StringComparison.InvariantCultureIgnoreCase))
                        {
                            writer.WriteLine("Hej då");
                            break;
                        }

                        if (string.Equals(command, "DATE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            writer.WriteLine(DateTime.UtcNow.ToString("o"));
                            break;
                        }

                        writer.WriteLine($"Okänt kommando: {command}");
                    }
                }

            }
        }

        public void ConnectToServer(string hostAdress, int hostPort)
        {
            using (var client = new TcpClient(hostAdress, hostPort))
            using (var networkStream = client.GetStream())
            using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
            using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
            {
                Console.WriteLine($"Uppkopplad till: {client.Client.RemoteEndPoint}");
                Game.SavedMessages.Add(new PrintMessage() { Message = $"Uppkopplad till:  {client.Client.RemoteEndPoint}\r\n", isBlue = false });

                while (client.Connected)
                {
                    Console.WriteLine("Skriv in text för att skicka: (Skriv QUIT för att avsluta)");
                    Game.SavedMessages.Add(new PrintMessage() { Message = $"Skriv in text för att skicka: (Skriv QUIT för att avsluta)\r\n", isBlue = false });

                    var text = Console.ReadLine();
                    Game.SavedMessages.Add(new PrintMessage() { Message = $"{text}\r\n", isBlue = false });
                    if (text == "QUIT") break;

                    // Skicka text
                    writer.WriteLine(text);

                    if (!client.Connected) break;

                    // Läs minst en rad
                    do
                    {
                        var line = reader.ReadLine();
                        Console.WriteLine($"Svar: {line}");
                        Game.SavedMessages.Add(new PrintMessage() { Message = $"Svar: {line}\r\n", isBlue = false });
                    } while (networkStream.DataAvailable);
                };
            }
        }

        public void Play(string hostAdress, int hostPort, Player player)
        {
            if (hostAdress == "")
            {
                if (hostPort != 0)
                {
                    StartListen(hostPort);
                    Host(player);
                }
                else
                {
                    Console.WriteLine("Något gick snett, du verkar inte ha skrivit in någon port.");
                    Game.SavedMessages.Add(new PrintMessage() { Message = $"Något gick snett, du verkar inte ha skrivit in någon port.\r\n", isBlue = false });
                }
            }
            else if (hostAdress != "" && hostPort != 0)
            {
                ConnectToServer(hostAdress, hostPort);
            }
        }

        public void TcpMessage(bool blue, string message)
        {
            if (blue)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(message);
            Game.SavedMessages.Add(new PrintMessage() { Message = $"{message}\r\n", isBlue = blue });
            Console.ResetColor();
        }
    }
}
