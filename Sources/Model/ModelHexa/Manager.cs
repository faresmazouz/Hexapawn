using System;
using ModelHexa;
namespace Model
{
    public class Manager
    {
        public Score Victoires { get; private set; }
            = new Score(987654321);

        public Score Defaites { get; private set; }
            = new Score(9656565);

        public Score VictoiresVsBot { get; private set; }
            = new Score(566546);

        public Score DefaitesVsBot { get; private set; }
            = new Score(54646496);
    }
}
