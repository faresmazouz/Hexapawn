using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelHexa
{
    public class Board
    {
        private Cell[,] boardOfCell { get; }
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

        public bool allPawns(ref Cell[] tab1, ref Cell[] tab2)
        {
            Cell? tmpCell;
            List<Cell> t1=new List<Cell>();
            List<Cell> t2 = new List<Cell>();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    tmpCell = GetCell(i, j);
                    if (tmpCell != null && tmpCell.pawn.HasValue)
                    {
                        if (tmpCell.pawn.Value.Color == TeamColor.Player1) t1.Add(tmpCell);
                        else if (tmpCell.pawn.Value.Color == TeamColor.Player2) t2.Add(tmpCell);
                        else return false;
                    }
                }
            }
            tab1=t1.ToArray();
            tab2=t2.ToArray();
            return true;
        }


        public bool MovePawn(Rules r, Player p, Cell c, Move m, ref bool win)
        {
            if (m==Move.cantMove||!r.isMoveValid(this, m, p.teamColor, c)) return false;
            int X, Y;
            Pawn newp;
            if (p.teamColor == TeamColor.Player1)
            {
                if (m == Move.eatLeft)
                {
                    X = c.X + 1;
                    Y= c.Y - 1;
                }
                else if (m == Move.eatRight)
                {
                    X = c.X + 1;
                    Y = c.Y + 1;
                }
                else if (m == Move.moveBy2)
                {
                    X = c.X + 2;
                    Y = c.Y;
                }
                else
                {
                    X = c.X + 1;
                    Y = c.Y;
                }
                if (X == length-1) win=true;
            }
            else
            {
                if (m == Move.eatLeft)
                {
                    X = c.X - 1;
                    Y = c.Y - 1;
                }
                else if (m == Move.eatRight)
                {
                    X = c.X - 1;
                    Y = c.Y + 1;
                }
                else if (m == Move.moveBy2)
                {
                    X = c.X -2;
                    Y = c.Y;
                }
                else
                {
                    X = c.X - 1;
                    Y = c.Y;
                }
                if (X==0) win = true;
            }
            newp = new Pawn(p.teamColor);
            boardOfCell[X, Y].pawn = newp;
            boardOfCell[c.X, c.Y].pawn = null;
            return true;
        }

        public void affiche()
        {
            for (int i = length-1; i >= 0; i--)
            {
                for (int j = 0; j <length; j++)
                {
                    if (boardOfCell[i, j].pawn.HasValue)
                    {
                        if (boardOfCell[i, j].pawn.Value.Color == TeamColor.Player1) Console.Write(" W ");
                        else Console.Write(" B ");
                    }
                    else Console.Write(" _ ");
                }
                Console.Write("\n");
            }
        }


        public int Length => length;


    }
}