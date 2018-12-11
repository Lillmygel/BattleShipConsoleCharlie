using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipConsoleCharlie.Data
{
    public class Ship
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public int[] YCoordinates { get; set; }
        public int[] XCoordinates { get; set; }
    }
}
