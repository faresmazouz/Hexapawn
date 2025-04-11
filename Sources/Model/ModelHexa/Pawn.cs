<<<<<<< HEAD
﻿using Microsoft.Maui.Graphics.Platform;

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
=======
﻿using Microsoft.Maui.Graphics.Platform;

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
>>>>>>> 0a54c4631e1d3e44aa10dce24f4c965296101798
}