using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class GameEndedEventArgs : EventArgs
    {
		public TeamColor Winner { get; }
		public Board FinalBoard { get; }

		public GameEndedEventArgs(TeamColor winner, Board finalBoard)
		{
			Winner = winner;
			FinalBoard = finalBoard;
		}
	}
}
