namespace ModelHexa
{
    public class Rules
    {
        public bool isMoveValid(Board b, Move move, TeamColor t, Cell c)
        {
            if (t==TeamColor.Unknown||move==Move.cantMove||c==null||b==null) return false;
            else if (! c.Pawn.HasValue || c.Pawn.Value.Color!=t) return false;
            else if (move == Move.cantMove) return false;
            else if (move == Move.eatLeft)
            {
                if (t == TeamColor.Player1)
                {
                    Cell? tempc = b[(c.X) + 1, (c.Y) - 1];
                    if (tempc==null||!tempc.Pawn.HasValue || t == tempc.Pawn.Value.Color) return false;
                }
                else
                {
                    Cell? tempc = b[c.X - 1, c.Y - 1];
                    if (tempc == null || !tempc.Pawn.HasValue || t == tempc.Pawn.Value.Color) return false;
                }
            }
            else if (move == Move.eatRight)
            {
                if (t == TeamColor.Player1)
                {
                    Cell? tempc = b[(c.X) + 1, (c.Y) + 1];
                    if (tempc == null || !tempc.Pawn.HasValue || t == tempc.Pawn.Value.Color) return false;
                }
                else
                {
                    Cell? tempc = b[c.X - 1, c.Y + 1];
                    if (tempc == null || !tempc.Pawn.HasValue || t == tempc.Pawn.Value.Color) return false;
                }
            }
            else if (move == Move.moveBy1)
            {
                if (t == TeamColor.Player1)
                {
                    Cell? tempc = b[(c.X) + 1, (c.Y)];
                    if (tempc == null || tempc.Pawn.HasValue) return false;
                }
                else
                {
                    Cell? tempc = b[c.X - 1, c.Y];
                    if (tempc == null || tempc.Pawn.HasValue) return false;
                }
            }
            else if (move == Move.moveBy2)
            {
                if (b.Length<=3) return false;
                if (t == TeamColor.Player1)
                {
                    if (c.X != 0) return false;
                    Cell? tempc1 = b[(c.X) + 1, (c.Y)];
                    Cell? tempc2 = b[(c.X) + 2, (c.Y)];
                    if (tempc2 == null || tempc1 == null || tempc1.Pawn.HasValue|| tempc2.Pawn.HasValue) return false;
                }
                else
                {
                    if (c.X != b.Length-1) return false;
                    Cell? tempc1 = b[(c.X) - 1, (c.Y)];
                    Cell? tempc2 = b[(c.X) - 2, (c.Y)];
                    if (tempc2 == null || tempc1 == null || tempc1.Pawn.HasValue || tempc2.Pawn.HasValue) return false;
                }
            }
                return true;
        }
        public Dictionary<Cell, List<Move>> allMoves(Board b, TeamColor t)
        {
            Dictionary<Cell, List<Move>> dict = new Dictionary<Cell, List<Move>>();
            Cell[] grbg = [], pawns = [];
            if (t == TeamColor.Player1) b.allPawns(ref pawns, ref grbg);
            else if (t == TeamColor.Player2) b.allPawns(ref grbg, ref pawns);
            else return dict;//lancer une exeption à la place
            foreach (Cell c in pawns)
            {
                foreach (Move m in Enum.GetValues(typeof(Move)))
                {
                    if (isMoveValid(b, m, t, c))
                    {
                        if (!dict.ContainsKey(c)) dict.Add(c, new List<Move>());
                        dict[c].Add(m);
                    }
                }
            }
            return dict;
        }
    }
        
}
