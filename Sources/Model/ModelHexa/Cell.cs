namespace ModelHexa
{
    public class Cell
    {
        public int X { get; private init; }
        public int Y { get; private init; }
        public Pawn? Pawn { get; set; }


        public Cell(int x, int y, Pawn? p=null)
        {
            X = x;
            Y = y;
            Pawn = p;
        }
        public bool Empty()
        {
            return Pawn == null;
        }

    }
}






