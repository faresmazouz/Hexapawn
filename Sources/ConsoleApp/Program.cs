using System;
using System.Collections.Generic;
using System.Linq;
using ModelHexa;

Console.WriteLine("Sa marche");
Dictionary<string, int> dictScores = new();
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
	int i = 1;
	Console.WriteLine("Liste des différentes actions:");
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
	Console.WriteLine("Quitter l'application ? oui (y) / non (autre)");
	return Console.ReadLine() == "y";
}

string GetActionLabel(ActionDebut act) => act switch
{
	ActionDebut.LancerPartie => "Lancer une partie",
	ActionDebut.CheckScores => "Voir les scores des joueurs",
	_ => "Action inconnue"
};

void AfficherScores(Dictionary<string, int> dictScores)
{
	var classement = CalculerClassement(dictScores);
	Console.WriteLine("Appuyez sur 'Entrée' pour voir tous les joueurs, ou entrez un nom précis :");
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
	foreach (var kvp in dictScores.OrderByDescending(kvp => kvp.Value))
		classement[kvp.Key] = i++;
	return classement;
}

void AfficherClassement(Dictionary<string, int> dictScores, Dictionary<string, int> dictPos)
{
	foreach (var kvp in dictScores.OrderByDescending(kvp => kvp.Value))
		Console.WriteLine($"{dictPos[kvp.Key]}. {kvp.Key} : {kvp.Value} victoires");
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
	p1.GameEnded += OnGameEnded;
	p2.GameEnded += OnGameEnded;
	p1.PawnCaptured += OnPawnCaptured;
    p2.PawnCaptured += OnPawnCaptured;
    p1.TurnStarted += OnTurnStarted;
    p2.TurnStarted += OnTurnStarted;
    if (!DemanderConfirmation()) return true;

	b.Affiche();
	Rules r = new Rules();
	TeamColor win = TeamColor.Unknown;

	p1.PlayTurn(r.allMoves(b, p1.teamColor), b, r, p2, ref win);

	Player winner = win == TeamColor.Player1 ? p1 : p2;
	winner.victoires++;
	dictScores[winner.Name]++;
	Console.WriteLine($"Félicitations, {winner.Name} a gagné ! ({dictScores[winner.Name]} victoires)");
	return false;
}

int DemanderNbCases()
{
	int nb;
	do
	{
		Console.Write("Longueur du plateau (>= 3) : ");
	} while (!int.TryParse(Console.ReadLine(), out nb) || nb < 3);
	return nb;
}

Player CreerJoueur(int numero, Dictionary<string, int> dictScores, string? nomJoueurExclu)
{
	bool bot = DemanderSiBot(numero);
	string nom = bot ? "Robot" : DemanderNomJoueur(numero, nomJoueurExclu);
	Player joueur = bot
		? new BOTPlayer(numero == 1 ? TeamColor.Player1 : TeamColor.Player2)
		: new HumanPlayer(nom, numero == 1 ? TeamColor.Player1 : TeamColor.Player2);
	dictScores.TryAdd(joueur.Name, 0);
	return joueur;
}

bool DemanderSiBot(int numero)
{
	Console.WriteLine($"Joueur {numero} : BOT ? (y/n)");
	return Console.ReadLine()?.ToLower() == "y";
}

string DemanderNomJoueur(int numero, string? exclu)
{
	string nom;
	do
	{
		Console.WriteLine($"Nom du joueur {numero} : ");
		nom = Console.ReadLine();
	} while (string.IsNullOrWhiteSpace(nom) || nom == "Robot" || nom == exclu);
	return nom;
}

bool DemanderConfirmation()
{
	Console.WriteLine("Confirmez-vous ces paramètres ? (y pour oui)");
	return Console.ReadLine()?.ToLower() == "y";
}

void BrancherEvenements(Player p)
{
	p.BoardChanged += OnBoardChanged;
	p.UserChoose += OnUserChoose;
	p.UserHaveToChoose += OnUserHaveToChoose;
}

void OnBoardChanged(object? sender, BoardChangedEventArgs e)
{
	e.BoardChanged.Affiche();
}

void OnUserHaveToChoose(object? sender, UserHaveToChooseEventArgs e)
{
	Console.WriteLine(e.Question);
}

void OnUserChoose(object? sender, WrongInputEventArgs e)
{
	Console.WriteLine(e.ErrorMessage);
}
void OnGameEnded(object? sender, GameEndedEventArgs e)
{
	Console.WriteLine($"Fin de la partie ! Gagnant : {e.Winner}");
	Console.WriteLine("Plateau final :");
	e.FinalBoard.Affiche();
}

void OnPawnCaptured(object? sender, PawnCapturedEventArgs e)
{
    Console.WriteLine($"{e.Attacker.Name} a capturé un pion en ({e.To.X}, {e.To.Y})");
}

void OnTurnStarted(object? sender, TurnStartedEventArgs e)
{
    Console.WriteLine($"--- Début du tour de {e.Player.Name} ({e.Team}) ---");
}

enum ActionDebut
{
	LancerPartie,
	CheckScores
}