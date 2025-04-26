namespace ModelHexa
{
    public class Cell
    {
        public int X { get;}
        public int Y { get;}
        public Pawn? pawn { get; set; }


        public Cell(int x, int y, Pawn p)
        {
            X = x;
            Y = y;
            pawn = p;
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            pawn = null;
        }
        public bool Empty()
        {
            return pawn == null;
        }

    }
}







