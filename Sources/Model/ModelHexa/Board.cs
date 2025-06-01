using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelHexa
{
    public class Board
    {
        public ObservableCollection<ObservableCollection<Cell>> BoardOfCell { get; }
        public ObservableCollection<Cell> FlatBoard {  get; }
        public int Length { get; }
        /// <summary>
        /// Plateau de jeu contenant un tableau à deux dimensions de Cell. C'est sur lui que va se dérouler la partie
        /// </summary>
        /// <param name="length">Le plateau va toujours être carré et cette propriété va donner la longueur des cotés. Ce paramètre va aider aux calculs</param>
        public Board(int length)
        {
            FlatBoard=new ObservableCollection<Cell>();
            this.Length = length;
            this.BoardOfCell = new ObservableCollection<ObservableCollection<Cell>>();
            this.BoardOfCell = new ObservableCollection<ObservableCollection<Cell>>();

            for (int i = 0; i < length; i++)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < length; j++)
                {
                    if (i == 0)
                    {
                        row.Add(new Cell(i, j, new Pawn(TeamColor.Player1)));
                    }
                    else if (i == length - 1)
                    {
                        row.Add(new Cell(i, j, new Pawn(TeamColor.Player2)));
                    }
                    else
                    {
                        row.Add(new Cell(i, j));
                    }
                }
                this.BoardOfCell.Add(row);
            }
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++) FlatBoard.Add(BoardOfCell[i][j]);
            }
        }
        public Cell? this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < Length && y >= 0 && y < Length)
                    return BoardOfCell[x][y];
                return null;
            }
        }


        public bool allPawns(ref Cell[] tab1, ref Cell[] tab2)
        {
            Cell? tmpCell;
            List<Cell> t1 = new List<Cell>();
            List<Cell> t2 = new List<Cell>();
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
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
            tab1 = t1.ToArray();
            tab2 = t2.ToArray();
            return true;
        }


        public bool MovePawn(Rules r, Player p, Cell c, Move m, ref bool win)
        {
            if (m == Move.cantMove || !r.isMoveValid(this, m, p.teamColor, c)) return false;
            int X, Y;
            Pawn newp;
            if (p.teamColor == TeamColor.Player1)
            {
                if (m == Move.eatLeft)
                {
                    X = c.X + 1;
                    Y = c.Y - 1;
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
                if (X == Length - 1) win = true;
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
                    X = c.X - 2;
                    Y = c.Y;
                }
                else
                {
                    X = c.X - 1;
                    Y = c.Y;
                }
                if (X == 0) win = true;
            }
            newp = new Pawn(p.teamColor);
            BoardOfCell[X][Y].Pawn = newp;
            BoardOfCell[c.X][c.Y].Pawn = null;
            return true;
        }
        public void Affiche()
        {
            Console.WriteLine(" X ");
            for (int i = Length - 1; i >= 0; i--)
            {
                Console.Write($" {i} ");
                for (int j = 0; j < Length; j++)
                {
                    if (BoardOfCell[i][j].Pawn.HasValue)
                    {
                        if (BoardOfCell[i][j].Pawn.Value.Color == TeamColor.Player1) Console.Write(" W ");
                        else Console.Write(" B ");
                    }
                    else Console.Write(" _ ");
                }
                Console.Write("\n");
            }
            Console.Write("   ");
            for (int i = 0; i < Length; i++)
            {
                Console.Write($" {i} ");
            }
            Console.Write(" Y \n");
        }



    }
}
