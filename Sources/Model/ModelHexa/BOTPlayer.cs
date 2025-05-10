using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class BOTPlayer: IPlayer
    {
        public BOTPlayer(TeamColor teamColor, string name="Robot"):base(name,teamColor) { }
        public override Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
        {
            //l.Count;
            choixFait = true;
            return Move.eatLeft;
        }
    }
}
