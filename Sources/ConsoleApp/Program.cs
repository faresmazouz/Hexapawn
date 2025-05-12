// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;




bool launchGame() 
{
    string stop;
    int i = 1, choix;
    Console.WriteLine("Liste des différentes actions:");
    foreach (ActionDebut act in Enum.GetValues(typeof(ActionDebut)))
    {
        Console.WriteLine($"{i}. {chois(act)}");
        i++;
    }
    Console.Write("Entrez le numéro de l'action à réaliser: ");
    choix =int.Parse(Console.ReadLine());
    while (choix <1|| choix >= i)
    {
        Console.WriteLine("Erreur, entrez le bon numéro");
        choix = int.Parse(Console.ReadLine());
    }
    if (choix == 1)
    {
        while (createPart()) ;
    }
    Console.WriteLine("Quitter la partie oui(y) ou non (n'importe quoi d'autre) ?");
    stop=Console.ReadLine();
    if (stop=="y") return false;
    return true;

}
string chois(ActionDebut act)
{
    return act switch
    {
        ActionDebut.LancerPartie => "Lancer une partie "

    };
}


bool createPart()
{
    IPlayer theWinner;
    bool bot;
    Rules r=new Rules();
    int nbCases;
    string rep;
    TeamColor win=TeamColor.Unknown;
    Console.WriteLine("De combien de cases de longueur voulez-vous que le plateau soit? Entrez un chiffre: ");
    nbCases=int.Parse(Console.ReadLine());
    while (nbCases <3)
    {
        Console.WriteLine("Erreur, le nombre de cases ne peut pas être inférieur à 3. Entrez un chiffre: ");
        nbCases = int.Parse(Console.ReadLine());
    }
    Board b= new Board(nbCases);
    Console.WriteLine("Voulez vous que le 1er joueur soit un BOT?(y/n'importe quoi d'autre)");
    bot = Console.ReadLine()=="y";
    Console.WriteLine("Entrez le nom du 1er joueur");
    IPlayer p1= bot?new BOTPlayer(TeamColor.Player1, Console.ReadLine()):new HumanPlayer(Console.ReadLine(), TeamColor.Player1);
    Console.WriteLine("Voulez vous que le 2ème joueur soit un BOT?(y/n'importe quoi d'autre)");
    bot = Console.ReadLine() == "y";
    Console.WriteLine("Entrez le nom du 2ème joueur");
    IPlayer p2 = bot ? new BOTPlayer(TeamColor.Player2, Console.ReadLine()) : new HumanPlayer(Console.ReadLine(), TeamColor.Player2);
    p1.BoardChanged += OnBoardChanged;//On branche l'objet à l'évènement
    p2.BoardChanged += OnBoardChanged;//pareil
    Console.WriteLine("Êtes-vous sûr des informations?(y/n\'importe quoi d\'autre)");
    rep=Console.ReadLine();
    if (rep != "y") return true;
    b.affiche();
    p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2, ref win);
    if (win == TeamColor.Player1) theWinner= p1;
    else theWinner=p2;
    theWinner.victoires += 1;
    Console.WriteLine($"Félicitation, {win} ({theWinner.victoires} victoires) a gagné!");
    return false;
}

void OnBoardChanged(object? sender, BoardChangedEventArgs e)//On définit l'évènement
{
    e.BoardChanged.affiche();
}

while (launchGame()) ;




Console.WriteLine("Hello, World!");
TeamColor winner=TeamColor.Unknown;
Rules r = new Rules();
Board b = new Board(3);
HumanPlayer p1 = new HumanPlayer("Thomas", TeamColor.Player1);
HumanPlayer p2=new HumanPlayer("Thom2",TeamColor.Player2);
p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2,ref winner);
Console.WriteLine($"{winner} a gagné!");

public enum ActionDebut
{
    LancerPartie

}
