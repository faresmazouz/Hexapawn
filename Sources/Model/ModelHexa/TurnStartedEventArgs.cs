using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHexa
{
    public class TurnStartedEventArgs : EventArgs
    {
        public Player Player { get; }
        public TeamColor Team { get; }

        public TurnStartedEventArgs(Player player, TeamColor team)
        {
            Player = player;
            Team = team;
        }
    }
}
