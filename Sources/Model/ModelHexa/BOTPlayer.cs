using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class BOTPlayer: Player
    {
        private static readonly Random rdm = new Random();
        public BOTPlayer(TeamColor teamColor):base("Robot",teamColor) { }
        public override Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
        {
                          
            Move m = l[rdm.Next(l.Count)];          //prend un mouvement dans la liste d'indice aléatoire de 0 à l'indice max de la liste
            choixFait = true;                       //On met le choix à true pour que ça ne se répète qu'une fois
            return m;
        }
        public override Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            List<Cell> listOfPawn = new List<Cell>(dict.Keys); 

            return listOfPawn[rdm.Next(listOfPawn.Count)];  //Renvoie un élément aléatoire de la liste des pions

            
        }
    }
}
