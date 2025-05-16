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
        /// <summary>
        /// Plateau de jeu contenant un tableau à deux dimensions de Cell. C'est sur lui que va se dérouler la partie
        /// </summary>
        /// <param name="length">Le plateau va toujours être carré et cette propriété va donner la longueur des cotés. Ce paramètre va aider aux calculs</param>
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
        public Cell? this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < length && y >= 0 && y < length)
                    return boardOfCell[x, y];
                return null;
            }
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
                    tmpCell = this[i, j];
                    if (tmpCell != null && tmpCell.Pawn.HasValue)
                    {
                        if (tmpCell.Pawn.Value.Color == TeamColor.Player1) t1.Add(tmpCell);
                        else if (tmpCell.Pawn.Value.Color == TeamColor.Player2) t2.Add(tmpCell);
                        else return false;
                    }
                }
            }
            tab1=t1.ToArray();
            tab2=t2.ToArray();
            return true;
        }


        public bool MovePawn(Rules r, IPlayer p, Cell c, Move m, ref bool win)
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
            boardOfCell[X, Y].Pawn = newp;
            boardOfCell[c.X, c.Y].Pawn = null;
            return true;
        }

        public void affiche()
        {
            Console.WriteLine(" X ");
            for (int i = length-1; i >= 0; i--)
            {
                Console.Write($" {i} ");
                for (int j = 0; j <length; j++)
                {
                    if (boardOfCell[i, j].Pawn.HasValue)
                    {
                        if (boardOfCell[i, j].Pawn.Value.Color == TeamColor.Player1) Console.Write(" W ");
                        else Console.Write(" B ");
                    }
                    else Console.Write(" _ ");
                }
                Console.Write("\n");
            }
            Console.Write("   ");
            for (int i=0; i < length; i++)
            {
                Console.Write($" {i} ");
            }
            Console.Write(" Y \n");
        }


        public int Length => length;


    }
    public enum ActionDebut
    {
        LancerPartie

    }

}
