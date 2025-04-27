
namespace ModelHexa
{
    public class Player
    {
        readonly string name;
        public readonly TeamColor teamColor;

        public Player(string name, TeamColor teamColor)
        {
            this.name = name;
            this.teamColor = teamColor;
        }

        public Cell ChoosePawn(Dictionary<Cell, List<Move>> dict)
        {
            int i = 1;
            List<Cell> l= [];
            Console.WriteLine($"{teamColor}, choisissez votre pion à bouger parmis (X,Y): ");
            foreach (Cell c in dict.Keys)
            {
                Console.WriteLine($"{i}. Pion de coordonnées ({c.X},{c.Y})");
                l.Add(c);
                i++;
            }
            Console.Write($"{teamColor}, entrez maintenant le numéro du pion à jouer: ");
            i = int.Parse( Console.ReadLine() )-1;
            Console.Write("\n");
            while (i >= l.Count||i<0)
            {
                Console.Write($"{teamColor}, le numéro entré est incorrect, s'il vous plaît entrez le bon numéro: ");
                i = int.Parse(Console.ReadLine()) - 1;
                Console.Write("\n");
            }
            return l[i];

        }

        public void PlayTurn() { }
        public virtual void MovePawn() { }
        public Move ChooseMove() {return new Move(); }
    }
    public enum Move
    {
        cantMove,
        eatRight,
        eatLeft,
        moveBy2,
        moveBy1,
    }
}
