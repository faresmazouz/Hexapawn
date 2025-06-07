using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class HumanPlayer: Player
    {
        public HumanPlayer(string name, TeamColor teamColor) : base(name, teamColor){}
        public override Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
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
            while (choix >= l.Count || choix < -1)
            {
                OnUserChoose(new WrongInputEventArgs("Numéro incorrect, entrez le bon numéro de l'action: "));
                choix = int.Parse(Console.ReadLine()) - 1;
            }
            if (choix == -1) return Move.cantMove;
            choixFait = true;
            return l[choix];
        }
        public override Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            int i = 1;
            List<Cell> l = [];   //déclaration de liste temporaire contenant les clés du dictionnaire
            OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{teamColor}, choisissez votre pion à bouger parmis (X,Y): "));
            foreach (Cell c in dict.Keys)   //On remplir la liste temporaire et on en profite pour afficher les pions
            {
                OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{i}. Pion de coordonnées ({c.X},{c.Y})"));
                l.Add(c);
                i++;
            }
            OnUserHaveToChoose(new UserHaveToChooseEventArgs($"{teamColor}, entrez maintenant le numéro du pion à jouer: "));
            i = int.Parse(Console.ReadLine()) - 1;
            OnUserHaveToChoose(new UserHaveToChooseEventArgs("\n"));
            while (i >= l.Count || i < 0)
            {
                OnUserChoose(new WrongInputEventArgs($"{teamColor}, le numéro entré est incorrect, s'il vous plaît entrez le bon numéro: "));
                i = int.Parse(Console.ReadLine()) - 1;
                OnUserHaveToChoose(new UserHaveToChooseEventArgs("\n"));
            }
            return l[i];

        }
        public override void PlayTurn(Dictionary<Cell, List<Move>> mymoves, Board b, Rules r, Player nextp, ref TeamColor winner)
        {
            //b.affiche();
            Dictionary<Cell, List<Move>> nextdict;
            bool choixfait = false, win = false;
            Cell cmove = new Cell(0, 0);
            Move move = Move.cantMove;
            while (!choixfait)
            {
                cmove = ChoosePawn(mymoves);
                move = ChooseMove(mymoves[cmove], cmove, ref choixfait);
            }
            b.MovePawn(r, this, cmove, move, ref win);
            OnBoardChanged(new BoardChangedEventArgs(b, this, move, cmove));//On invoque l'évènement auquel on passe l'objet de l'évènement
            nextdict = r.allMoves(b, nextp.teamColor);
            if (nextdict.Count == 0 || win == true)
            {
                winner = teamColor;
                return;
            }
            nextp.PlayTurn(nextdict, b, r, this, ref winner);


        }
    }
}
