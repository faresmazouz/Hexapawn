
using System.Collections.Generic;

namespace ModelHexa
{
    public class Player
    {
        readonly string name;
        public readonly TeamColor teamColor;

        public Player(string name, TeamColor teamColor)
        {
            this.name = name;
            this.teamColor = teamColor;
        }


        public void PlayTurn(Dictionary<Cell, List<Move>> mymoves, Board b, Rules r, Player nextp, ref TeamColor winner)
        {
            b.affiche();
            Dictionary<Cell, List<Move>> nextdict;
            bool choixfait = false, win = false;
            Cell cmove=new Cell(0,0);
            Move move=Move.cantMove;
            while (!choixfait)
            {
                cmove = ChoosePawn(mymoves);
                move = ChooseMove(mymoves[cmove], cmove, ref choixfait);
            }
            b.MovePawn(r, this, cmove, move, ref win);
            nextdict=r.allMoves(b, nextp.teamColor);
            if (nextdict.Count == 0 || win == true)
            {
                winner=teamColor;
                return;
            }
            nextp.PlayTurn(nextdict,b,r,this,ref winner);


        }


        public Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
        {
            int choix = 0;
            Move mfin = Move.cantMove;
            Console.WriteLine($"Choisissez quel mouvement jouer avec le pion ({c.X},{c.Y}): ");
            Console.WriteLine("0. Changer de pion à jouer");
            foreach (Move m in l)
            {
                choix++;
                Console.WriteLine($"{choix}. {GetNomMove(m)}");
            }
            choix = -2;
            Console.Write("Entrez le numéro de l'action: ");
            choix = int.Parse(Console.ReadLine()) - 1;
            while (choix >= l.Count || choix < -1) {
                Console.Write("Numéro incorrect, entrez le bon numéro de l'action: ");
                choix = int.Parse(Console.ReadLine()) - 1;
            }
            if (choix==-1) return Move.cantMove;
            choixFait = true;
            return l[choix];
        }


        public Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            int i = 1;
            List<Cell> l= [];
            Console.WriteLine($"{teamColor}, choisissez votre pion à bouger parmis (X,Y): ");
            foreach (Cell c in dict.Keys)
            {
                Console.WriteLine($"{i}. Pion de coordonnées ({c.X},{c.Y})");
                l.Add(c);
                i++;
            }
            Console.Write($"{teamColor}, entrez maintenant le numéro du pion à jouer: ");
            i = int.Parse( Console.ReadLine() )-1;
            Console.Write("\n");
            while (i >= l.Count||i<0)
            {
                Console.Write($"{teamColor}, le numéro entré est incorrect, s'il vous plaît entrez le bon numéro: ");
                i = int.Parse(Console.ReadLine()) - 1;
                Console.Write("\n");
            }
            return l[i];

        }




        public static string GetNomMove(Move m)
        {
            return m switch
            {
                Move.cantMove => "Ne peut pas bouger",
                Move.eatRight => "Manger le pion à droite",
                Move.eatLeft => "Manger le pion à gauche",
                Move.moveBy1 => "Avancer d'une case",
                Move.moveBy2 => "Avancer de deux cases"
            };
        }
    }
    public enum Move
    {
        cantMove,
        eatRight,
        eatLeft,
        moveBy2,
        moveBy1
    }
}
