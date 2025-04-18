namespace ModelHexa
{
    public class Rules
    {
        public bool isMoveValid(Board b, Move move, TeamColor t, Cell c)
        {
            if (t==TeamColor.Unknown||move==Move.cantMove|| move==Move.Unknown||c==null||b==null) return false;
            else if (! c.pawn.HasValue || c.pawn.Value.Color!=t) return false;
            else if (move == Move.cantMove) return false;
            else if (move == Move.eatLeft)
            {
                if (t == TeamColor.Player1)
                {
                    Cell tempc = b.GetCell((c.X) + 1, (c.Y) + 1);
                    if (!tempc.pawn.HasValue || t == tempc.pawn.Value.Color) return false;
                }
                else
                {
                    Cell tempc = b.GetCell(c.X - 1, c.Y + 1);
                    if (!tempc.pawn.HasValue || t == tempc.pawn.Value.Color) return false;
                }
            }
            else if (move == Move.eatRight)
            {
                if (t == TeamColor.Player1)
                {
                    Cell tempc = b.GetCell((c.X) + 1, (c.Y) - 1);
                    if (!tempc.pawn.HasValue || t == tempc.pawn.Value.Color) return false;
                }
                else
                {
                    Cell tempc = b.GetCell(c.X - 1, c.Y - 1);
                    if (!tempc.pawn.HasValue || t == tempc.pawn.Value.Color) return false;
                }
            }
            else if (move == Move.moveBy1)
            {
                if (t == TeamColor.Player1)
                {
                    Cell tempc = b.GetCell((c.X) + 1, (c.Y));
                    if (tempc.pawn.HasValue) return false;
                }
                else
                {
                    Cell tempc = b.GetCell(c.X - 1, c.Y);
                    if (tempc.pawn.HasValue) return false;
                }
            }
            else if (move == Move.moveBy2)
            {
                if (t == TeamColor.Player1)
                {
                    if (c.X != 0) return false;
                    Cell tempc1 = b.GetCell((c.X) + 1, (c.Y));
                    Cell tempc2 = b.GetCell((c.X) + 2, (c.Y));
                    if (tempc1.pawn.HasValue|| tempc2.pawn.HasValue) return false;
                }
                else
                {
                    if (c.X != 2) return false;
                    Cell tempc1 = b.GetCell((c.X) - 1, (c.Y));
                    Cell tempc2 = b.GetCell((c.X) - 2, (c.Y));
                    if (tempc1.pawn.HasValue || tempc2.pawn.HasValue) return false;
                }
            }
                return true;
        }
    }
}
