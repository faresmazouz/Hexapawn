using Microsoft.Maui.Graphics.Platform;

namespace ModelHexa
{
    public struct Pawn
    {
        public TeamColor Color { get; set; }
        public Pawn()
        {
            Color = TeamColor.Unknown;
        }
        public Pawn(TeamColor T)
        {
            Color = T;
        }
    }
    public enum TeamColor
    {
        Unknown,
        Player1,
        Player2,
    }
}