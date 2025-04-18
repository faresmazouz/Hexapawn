using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelHexa
{
    public class Board
    {
        private readonly Cell[,] boardOfCell;
        readonly int length;
        public Board(int length)
        {
            this.length = length;
            this.boardOfCell = new Cell[length, length];
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == 0)
                    {
                        this.boardOfCell[i, j] = new Cell(i, j, new Pawn(TeamColor.Player1));
                    }
                    else if (i == length - 1)
                    {
                        this.boardOfCell[i, j] = new Cell(i, j, new Pawn(TeamColor.Player2));
                    }
                    else
                    {
                        this.boardOfCell[i, j] = new Cell(i, j);
                    }
                }
            }
        }
        public Cell? GetCell(int x, int y)
        {
            if (x >= 0 && x < length && y >= 0 && y < length)
                return boardOfCell[x, y];
            return null;
        }

        public int Length => length;


    }
}