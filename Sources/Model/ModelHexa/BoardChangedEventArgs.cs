using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class BoardChangedEventArgs : EventArgs
    {
        public Board BoardChanged { get; set; }
        public Player Player { get; set; }
        public Move MoveUsed { get; set; }
        public Cell CellChanged { get; set; }
        public BoardChangedEventArgs(Board b, Player p, Move m, Cell c)
        {
            BoardChanged = b;
            Player = p;
            MoveUsed = m;
            CellChanged = c;
        }

    }
}
