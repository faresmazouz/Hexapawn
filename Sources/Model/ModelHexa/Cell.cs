
namespace ModelHexa
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        private Pawn? pawn;
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            pawn = null;
        }
    }
}







