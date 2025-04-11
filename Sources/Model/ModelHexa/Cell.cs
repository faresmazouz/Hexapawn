using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Platform;

namespace ModelHexa
{
    class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        private Pawn? pawn;
        public Cell(int x, int y, Pawn p)
        {
            X = x;
            Y = y;
            pawn = p;
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            pawn = null;
        }

        public bool Empty()
        {
            return pawn == null;
        }
        public void setPawn(Pawn pawn)
        {
            pawn = p;
        }

    }

}







