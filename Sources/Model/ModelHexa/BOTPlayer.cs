using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace ModelHexa
{
    public class BOTPlayer : Player
    {
        private static readonly RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();

        private static int GetSecureRandomInt(int maxValue)
        {
            if (maxValue <= 0) throw new ArgumentOutOfRangeException(nameof(maxValue));
            byte[] intBytes = new byte[4];
            int value;
            do
            {
                randomGenerator.GetBytes(intBytes);
                value = BitConverter.ToInt32(intBytes, 0) & int.MaxValue;
            } while (value >= int.MaxValue - (int.MaxValue % maxValue));
            return value % maxValue;
        }

        public BOTPlayer(TeamColor teamColor) : base("Robot", teamColor) { }

        public override Move ChooseMove(List<Move> l, Cell c, ref bool choixFait)
        {
            Move m = l[GetSecureRandomInt(l.Count)];
            return m;
        }

        public override Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            List<Cell> listOfPawn = new List<Cell>(dict.Keys);
            return listOfPawn[GetSecureRandomInt(listOfPawn.Count)];
        }
        public override void PlayTurn(Dictionary<Cell, List<Move>> mymoves, Board b, Rules r, Player nextp, ref TeamColor winner)
        {
            bool c= false;
            Cell abouger = ChoosePawn(mymoves);
            Move move = ChooseMove(mymoves[abouger], abouger, ref c );
        }
    }
}
