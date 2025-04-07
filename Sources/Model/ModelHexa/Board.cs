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
        public Board(List<List<Cell>> boardOfCell, int length)
        {
            this.length = length;
            this.boardOfCell = new Cell[length, length];
            for (int j = 0; j<length; j++) {
                for (int i = 0; i < length; i++) {
                    if (i == 0)
                    {
                        Pawn p = new Pawn(TeamColor.Player1);
                    }
                    else if (i == length - 1)
                    {
                        Pawn p = new Pawn(TeamColor.Player2);
                    }
                    else
                    {
                        Pawn p = null;
                    }
                    this.boardOfCell[i, j] = new Cell(i, j, p);
        }
    }
}
