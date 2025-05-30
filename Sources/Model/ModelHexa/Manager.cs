using System;
namespace ModelHexa;

public class Manager
{
    public Score Victoires { get; private set; }
        = new Score();

    public Score Defaites { get; private set; }
        = new Score();

    public Score VictoiresVsBot { get; private set; }
        = new Score();

    public Score DefaitesVsBot { get; private set; }
        = new Score();

    public Score PartiesTotales { get; private set; }
        = new Score();

    public Score PartiesVsBot { get; private set; }
        = new Score();
}
