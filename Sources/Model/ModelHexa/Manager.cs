using System;
namespace ModelHexa;

public class Manager
{
    public Score Victoires { get; private set; }
        = new Score(89);

    public Score Defaites { get; private set; }
        = new Score(42);

    public Score VictoiresVsBot { get; private set; }
        = new Score(42);

    public Score DefaitesVsBot { get; private set; }
        = new Score(42);

    public Score PartiesTotales { get; private set; }
        = new Score(42);

    public Score PartiesVsBot { get; private set; }
        = new Score(42);
}
