namespace ModelHexa
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pawn? pawn { get; }


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
        public void SetPawn(Pawn p)
        {
            pawn = p;
        }

    }
}







