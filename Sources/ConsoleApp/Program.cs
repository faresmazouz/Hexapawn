// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;




bool launchGame() 
{
    string stop;
    int i = 1, choix;
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
    Console.WriteLine("Entrez le nom du 1er joueur");
    Player p1=new Player(Console.ReadLine(),TeamColor.Player1);
    Console.WriteLine("Entrez le nom du 2ème joueur");
    Player p2 = new Player(Console.ReadLine(), TeamColor.Player2);
    Console.WriteLine("Êtes-vous sûr des informations?(y/n\'importe quoi d\'autre)");
    rep=Console.ReadLine();
    if (rep != "y") return true;
    p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2, ref win);
    else if (win == TeamColor.Player2) Console.WriteLine($"Félicitation, {win} a gagné!");
    else Console.WriteLine($"Félicitation, {win} a gagné!");
    return false;
}





while (launchGame()) ;




Console.WriteLine("Hello, World!");
TeamColor winner=TeamColor.Unknown;
Rules r = new Rules();
Board b = new Board(3);
Player p1 = new Player("Thomas", TeamColor.Player1);
Player p2=new Player("Thom2",TeamColor.Player2);
p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2,ref winner);
Console.WriteLine($"{winner} a gagné!");

public enum ActionDebut
{
    LancerPartie

}
