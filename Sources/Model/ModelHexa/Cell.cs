<<<<<<< HEAD
﻿
namespace ModelHexa
=======
﻿namespace ModelHexa
>>>>>>> 0a54c4631e1d3e44aa10dce24f4c965296101798
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        private Pawn? pawn;
<<<<<<< HEAD
=======
        public Cell(int x, int y, Pawn p)
        {
            X = x;
            Y = y;
            pawn = p;
        }

>>>>>>> 0a54c4631e1d3e44aa10dce24f4c965296101798
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            pawn = null;
        }
<<<<<<< HEAD
    }
=======

        public bool Empty()
        {
            return pawn == null;
        }
        public void SetPawn(Pawn p)
        {
            pawn = p;
        }

    }

>>>>>>> 0a54c4631e1d3e44aa10dce24f4c965296101798
}







