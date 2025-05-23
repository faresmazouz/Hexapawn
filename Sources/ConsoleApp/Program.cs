// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using ConsoleApp;
using ModelHexa;


Dictionary<string, int> dictScores = new Dictionary<string, int>();
while (LaunchGame(ref dictScores)) ;

bool LaunchGame(ref Dictionary<string, int> dictScores)
{
    AfficherActions();
    int choix = DemanderChoixAction();
    switch (choix)
    {
        case 1:
            LancerPartie(ref dictScores);
            break;
        case 2:
            AfficherScores(dictScores);
            break;
    }
    return !DemanderQuitter();
}

void AfficherActions()
{
    Console.WriteLine("Liste des différentes actions:");
    int i = 1;
    foreach (ActionDebut act in Enum.GetValues(typeof(ActionDebut)))
    {
        Console.WriteLine($"{i}. {GetActionLabel(act)}");
        i++;
    }
}

int DemanderChoixAction()
{
    int nbActions = Enum.GetValues(typeof(ActionDebut)).Length;
    int choix;
    do
    {
        Console.Write("Entrez le numéro de l'action à réaliser: ");
    } while (!int.TryParse(Console.ReadLine(), out choix) || choix < 1 || choix > nbActions);
    return choix;
}

bool DemanderQuitter()
{
    Console.WriteLine("Quitter l'application oui(y) ou non (n'importe quoi d'autre) ?");
    return Console.ReadLine() == "y";
}

string GetActionLabel(ActionDebut act) => act switch
{
    ActionDebut.LancerPartie => "Lancer une partie ",
    ActionDebut.CheckScores => "Voir les scores des joueurs",
    _ => "Action inconnue"
};

void AfficherScores(Dictionary<string, int> dictScores)
{
    var classement = CalculerClassement(dictScores);
    Console.WriteLine("Pour voir tous les joueurs, appuyez sur \"entrer\", sinon entrez le nom du joueur");
    string choix = Console.ReadLine();
    if (string.IsNullOrEmpty(choix))
        AfficherClassement(dictScores, classement);
    else
        AfficherScoreJoueur(dictScores, classement, choix);
}

void AfficherScoreJoueur(Dictionary<string, int> dictScores, Dictionary<string, int> classement, string nom)
{
    if (dictScores.ContainsKey(nom))
        Console.WriteLine($"{classement[nom]}. {nom} : {dictScores[nom]} victoires");
    else
        Console.WriteLine("Aucun joueur n'a ce nom là");
}

Dictionary<string, int> CalculerClassement(Dictionary<string, int> dictScores)
{
    var classement = new Dictionary<string, int>();
    int i = 1;
    foreach (var elt in dictScores.OrderByDescending(kvp => kvp.Value))
        classement[elt.Key] = i++;
    return classement;
}

void AfficherClassement(Dictionary<string, int> dictScores, Dictionary<string, int> dictPos)
{
    foreach (var elt in dictScores.OrderByDescending(kvp => kvp.Value))
        Console.WriteLine($"{dictPos[elt.Key]}. {elt.Key} : {elt.Value} victoires");
}

void LancerPartie(ref Dictionary<string, int> dictScores)
{
    while (CreerEtJouerPartie(ref dictScores)) ;
}

bool CreerEtJouerPartie(ref Dictionary<string, int> dictScores)
{
    int nbCases = DemanderNbCases();
    Board b = new Board(nbCases);
    Player p1 = CreerJoueur(1, dictScores, null);
    Player p2 = CreerJoueur(2, dictScores, p1.Name);
    BrancherEvenements(p1);
    BrancherEvenements(p2);
    if (!DemanderConfirmation())
        return true;
    b.affiche();
    TeamColor win = TeamColor.Unknown;
    Rules r = new Rules();
    p1.PlayTurn(r.allMoves(b, p1.teamColor), b, r, p2, ref win);
    Player theWinner = (win == TeamColor.Player1) ? p1 : p2;
    theWinner.victoires += 1;
    dictScores[theWinner.Name] += 1;
    Console.WriteLine($"Félicitation, {theWinner.Name} ({dictScores[theWinner.Name]} victoires) a gagné!");
    return false;
}

int DemanderNbCases()
{
    int nbCases;
    do
    {
        Console.WriteLine("De combien de cases de longueur voulez-vous que le plateau soit? Entrez un chiffre: ");
    } while (!int.TryParse(Console.ReadLine(), out nbCases) || nbCases < 3);
    return nbCases;
}

Player CreerJoueur(int numero, Dictionary<string, int> dictScores, string? nomJoueurExclu)
{
    bool bot = DemanderSiBot(numero);
    string nomJoueur = bot ? "Robot" : DemanderNomJoueur(numero, nomJoueurExclu);
    Player joueur = bot
        ? new BOTPlayer(numero == 1 ? TeamColor.Player1 : TeamColor.Player2)
        : new HumanPlayer(nomJoueur, numero == 1 ? TeamColor.Player1 : TeamColor.Player2);
    dictScores.TryAdd(joueur.Name, 0);
    return joueur;
}

bool DemanderSiBot(int numero)
{
    Console.WriteLine($"Voulez vous que le {numero}er joueur soit un BOT?(y/n'importe quoi d'autre)");
    return Console.ReadLine() == "y";
}

string DemanderNomJoueur(int numero, string? nomJoueurExclu)
{
    string nomJoueur;
    do
    {
        Console.WriteLine($"Entrez le nom du {numero}ème joueur");
        nomJoueur = Console.ReadLine();
    } while (string.IsNullOrEmpty(nomJoueur) || nomJoueur == "Robot" || (nomJoueurExclu != null && nomJoueur == nomJoueurExclu));
    return nomJoueur;
}

bool DemanderConfirmation()
{
    Console.WriteLine("Êtes-vous sûr des informations?(y/n'importe quoi d'autre)");
    return Console.ReadLine() == "y";
}

void BrancherEvenements(Player p)
{
    p.BoardChanged += OnBoardChanged;
    p.UserChoose += OnUserChoose;
    p.UserHaveToChoose += OnUserHaveToChoose;
}

void OnBoardChanged(object? sender, BoardChangedEventArgs e)
{
    e.BoardChanged.affiche();
}

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


