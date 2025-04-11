
using GameController;

namespace ModelHexa
{
    public class Rules
    {
        public bool isMoveValid(Board b, Move move, TeamColor t, Cell c)
        {
            if (c.pawn==null|| c.pawn.Color!=t) return false;
            if (move == Move.cantMove) return false;
            if ( move==Move.eatLeft)
            {
                
            }
        }
    }
}
