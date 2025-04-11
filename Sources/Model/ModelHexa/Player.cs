
namespace ModelHexa
{
    public class Player
    {
        readonly string name;
        readonly TeamColor teamColor;

        public Player(string name, TeamColor teamColor)
        {
            this.name = name;
            this.teamColor = teamColor;
        }

        public void PlayTurn() { }
        public virtual void MovePawn() { }
        public Move ChooseMove() {return new Move(); }
        public enum Move
        {
            cantMove,
            eat,
            moveBy2,
            moveBy1,
        }
    }
}
