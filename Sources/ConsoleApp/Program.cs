// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using ConsoleApp;
using ModelHexa;



bool launchGame(ref Dictionary<string, int> dictScores) 
{
    string stop;
    int i = 1, choix;
    bool saisieValide = false;
    Console.WriteLine("Liste des différentes actions:");
    foreach (ActionDebut act in Enum.GetValues(typeof(ActionDebut)))
    {
        Console.WriteLine($"{i}. {chois(act)}");
        i++;
    }
    do
    {
    Console.Write("Entrez le numéro de l'action à réaliser: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out choix) && choix >= 1 && choix < i)
    {
        saisieValide = true;
    }
    else
    {
        Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entier valide entre 1 et " + (i - 1));
    }
    } while (!saisieValide);
    if (choix == 1)
    {
        while (createPart(ref dictScores)) ;
    }
    else if (choix == 2)
    {
        while (checkScores(dictScores)) ;
    }
    Console.WriteLine("Quitter l'application oui(y) ou non (n'importe quoi d'autre) ?");
    stop=Console.ReadLine();
    if (stop=="y") return false;
    return true;

}
string chois(ActionDebut act)
{
    return act switch
    {
        ActionDebut.LancerPartie => "Lancer une partie ",
        ActionDebut.CheckScores=> "Voir les scores des joueurs"

    };
}
bool checkScores(Dictionary<string, int> dictScores)
{
    string choix;
    Dictionary<string,int> dictPos=new Dictionary<string, int>();
    var a=dictScores.OrderByDescending(Kvp => Kvp.Value);
    int i = 1;
    foreach (var elt in a)
    {
        dictPos.Add(elt.Key, i);
        i++;
    }
    Console.WriteLine("Pour voir tous les joueurs, appuyez sur \"entrer\", sinon entrez le nom du joueur");
    choix=Console.ReadLine();
    if (choix == "") affichClassement(dictScores, dictPos);
    else if (dictScores.ContainsKey(choix)) Console.WriteLine($"{dictPos[choix]}. {choix} : {dictScores[choix]} victoires");
    else Console.WriteLine("Aucun joueur n'a ce nom là");
    return false;
}





void affichClassement(Dictionary<string, int> dictScores, Dictionary<string,int> dictPos)
{
    var a = dictScores.OrderByDescending(Kvp => Kvp.Value);
    foreach (var elt in a)
    {
        Console.WriteLine($"{dictPos[elt.Key]}. {elt.Key} : {elt.Value} victoires");
    }
}



bool createPart(ref Dictionary<string, int> dictScores)
{
    Player theWinner;
    bool bot;
    Rules r=new Rules();
    int nbCases;
    bool saisieValide = false;
    string rep;
    string nomJoueur="";
    TeamColor win=TeamColor.Unknown;
    do
    { 
        Console.Write("De combien de cases de longueur voulez-vous que le plateau soit? Entrez un chiffre: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out nbCases) && nbCases >= 3)
        {
            saisieValide = true;
        }
        else
        {
            Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entier valide supérieur à 3");
        }
    } while (!saisieValide);
    Board b= new Board(nbCases);
    Console.WriteLine("Voulez vous que le 1er joueur soit un BOT?(y/n'importe quoi d'autre)");
    bot = Console.ReadLine()=="y";
    if (!bot)
    {
        Console.WriteLine("Entrez le nom du 1er joueur");
        nomJoueur=Console.ReadLine();
        while (nomJoueur == "" || nomJoueur == "Robot")
        {
            Console.WriteLine("Erreur, votre nom ne doit pas être vide et vous ne pouvez pas vous appeler Robot, entrez un nom correct");
            nomJoueur = Console.ReadLine();
        }
    }
    Player p1 = bot ? new BOTPlayer(TeamColor.Player1) : new HumanPlayer(nomJoueur, TeamColor.Player1);
    dictScores.TryAdd(p1.Name, 0);
    Console.WriteLine("Voulez vous que le 2ème joueur soit un BOT?(y/n'importe quoi d'autre)");
    bot = Console.ReadLine() == "y";
    if (!bot)
    {
        Console.WriteLine("Entrez le nom du 2ème joueur");
        nomJoueur = Console.ReadLine();
        while (nomJoueur == "" || nomJoueur == "Robot"||nomJoueur==p1.Name)
        {
            Console.WriteLine("Erreur, votre nom ne doit pas être vide et vous ne pouvez pas vous appeler Robot et vous ne pouvez pas avoir le même nom que le 1er joueur, entrez un nom correct");
            nomJoueur = Console.ReadLine();
        }
    }
    Player p2 = bot ? new BOTPlayer(TeamColor.Player2) : new HumanPlayer(nomJoueur, TeamColor.Player2);
    dictScores.TryAdd(p2.Name, 0);
    p1.BoardChanged += OnBoardChanged;//On branche l'objet à l'évènement
    p2.BoardChanged += OnBoardChanged;//pareil
    p1.UserChoose += OnUserChoose;
    p2.UserChoose += OnUserChoose;
    p1.UserHaveToChoose += OnUserHaveToChoose;
    p2.UserHaveToChoose += OnUserHaveToChoose;
    Console.WriteLine("Êtes-vous sûr des informations?(y/n\'importe quoi d\'autre)");
    rep = Console.ReadLine();
    if (rep != "y") return true;
    b.Affiche();
    p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2, ref win);
    if (win == TeamColor.Player1) theWinner= p1;
    else theWinner=p2;
    theWinner.victoires += 1;
    dictScores[theWinner.Name] += 1;
    Console.WriteLine($"Félicitation, {theWinner.Name} ({dictScores[theWinner.Name]} victoires) a gagné!");
    return false;
}

void OnBoardChanged(object? sender, BoardChangedEventArgs e)//On définit l'évènement
{
    e.BoardChanged.Affiche();
}


Dictionary<string, int> dictScores = new Dictionary<string, int>();
while (launchGame(ref dictScores)) ;


void OnUserHaveToChoose(object? sender, UserHaveToChooseEventArgs e)
{
    Console.WriteLine(e.Question);
}

void OnUserChoose(object? sender, WrongInputEventArgs e)
{
    Console.WriteLine(e.ErrorMessage);
}


//Console.WriteLine("Hello, World!");
//TeamColor winner=TeamColor.Unknown;
//Rules r = new Rules();
//Board b = new Board(3);
//HumanPlayer p1 = new HumanPlayer("Thomas", TeamColor.Player1);
//HumanPlayer p2=new HumanPlayer("Thom2",TeamColor.Player2);
//p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2,ref winner);
//Console.WriteLine($"{winner} a gagné!");


