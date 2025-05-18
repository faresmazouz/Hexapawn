
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ObjectiveC;

namespace ModelHexa
{
    public abstract class Player
    {
        readonly string name;
        public readonly TeamColor teamColor;
        public int victoires;
        public string Name => name;
        

        public Player(string name, TeamColor teamColor)
        {
            this.name = name;
            this.teamColor = teamColor;
        }


        public virtual void PlayTurn(Dictionary<Cell, List<Move>> mymoves, Board b, Rules r, Player nextp, ref TeamColor winner)
        {
            //b.affiche();
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
            OnBoardChanged(new BoardChangedEventArgs(b, this, move, cmove));//On invoque l'évènement auquel on passe l'objet de l'évènement
            nextdict=r.allMoves(b, nextp.teamColor);
            if (nextdict.Count == 0 || win == true)
            {
                winner=teamColor;
                return;
            }
            nextp.PlayTurn(nextdict,b,r,this,ref winner);


        }


        public virtual Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
        {
            int choix = 0;
            Move mfin = Move.cantMove;
            
            OnUserHaveToChoose(new UserHaveToChooseEventArgs($"Choisissez quel mouvement jouer avec le pion ({c.X},{c.Y}): "));
            OnUserHaveToChoose(new UserHaveToChooseEventArgs("0. Changer de pion à jouer"));
            foreach (Move m in l)
            {
                choix++;
                OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{choix}. {GetNomMove(m)}"));
            }
            choix = -2;
            OnUserHaveToChoose(new UserHaveToChooseEventArgs("Entrez le numéro de l'action: "));
            choix = int.Parse(Console.ReadLine()) - 1;
            while (choix >= l.Count || choix < -1) {
                OnUserChoose(new WrongInputEventArgs("Numéro incorrect, entrez le bon numéro de l'action: "));
                choix = int.Parse(Console.ReadLine()) - 1;
            }
            if (choix==-1) return Move.cantMove;
            choixFait = true;
            return l[choix];
        }


        public virtual Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            int i = 1;
            List<Cell> l= [];   //déclaration de liste temporaire contenant les clés du dictionnaire
            OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{teamColor}, choisissez votre pion à bouger parmis (X,Y): "));
            foreach (Cell c in dict.Keys)   //On remplir la liste temporaire et on en profite pour afficher les pions
            {
                OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{i}. Pion de coordonnées ({c.X},{c.Y})"));
                l.Add(c);
                i++;
            }
            OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{teamColor}, entrez maintenant le numéro du pion à jouer: "));
            i = int.Parse( Console.ReadLine() )-1;
            OnUserHaveToChoose(new UserHaveToChooseEventArgs("\n"));
            while (i >= l.Count||i<0)
            {
                OnUserChoose(new WrongInputEventArgs($"{teamColor}, le numéro entré est incorrect, s'il vous plaît entrez le bon numéro: "));
                i = int.Parse(Console.ReadLine()) - 1;
                OnUserHaveToChoose(new UserHaveToChooseEventArgs("\n"));
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
        public event EventHandler<BoardChangedEventArgs> BoardChanged;//On utilise le délégué EventHandler pour déclarer l'évènement
        protected void OnBoardChanged(BoardChangedEventArgs b) //On permet d'invoquer l'évènement
        {
            if (BoardChanged != null)
            {
                BoardChanged(this, b);
            }
        }


        public event EventHandler<UserHaveToChooseEventArgs> UserHaveToChoose;
        protected void OnUserHaveToChoose(UserHaveToChooseEventArgs u)
        {
            if (UserHaveToChoose != null)
            {
                UserHaveToChoose(this, u);
            }
        }


        public event EventHandler<WrongInputEventArgs> UserChoose;

        protected void OnUserChoose(WrongInputEventArgs u)
        {
            if (UserChoose != null)
            {
                UserChoose(this, u);
            }
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
