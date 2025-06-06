using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class PawnCapturedEventArgs : EventArgs
    {
        public Player Attacker { get; }
        public Cell From { get; }
        public Cell To { get; }

        public PawnCapturedEventArgs(Player attacker, Cell from, Cell to)
        {
            Attacker = attacker;
            From = from;
            To = to;
        }
    }
}
