
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
    }
    public enum TeamColor
    {
        Unknown,
        Player1,
        Player2
    }
}
