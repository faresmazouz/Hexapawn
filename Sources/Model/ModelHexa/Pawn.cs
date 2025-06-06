using System.Diagnostics.CodeAnalysis;

namespace ModelHexa
{
    public struct Pawn
    {
        public TeamColor Color { get; private init; }
        public Pawn()
        {
            Color = TeamColor.Unknown;
        }
        public Pawn(TeamColor T)
        {
            Color = T;
        }
        public override string ToString()
        {
            if (Color == TeamColor.Player1) return "♙";
            return "♟";
        }
    }
    public enum TeamColor
    {
        Unknown,
        Player1,
        Player2
    }
}
