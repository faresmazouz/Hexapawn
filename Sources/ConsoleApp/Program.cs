// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;

Console.WriteLine("Hello, World!");
Rules r = new Rules();
Board b = new Board(5);
Player p1 = new Player("Thomas", TeamColor.Player1);
Player p2=new Player("Thom2",TeamColor.Player2);
Player[] ptab = [p1, p2];
int joueur=0;
Player p;
bool win = false;
int X, Y;
Move m;
while (win == false)
{
    p = ptab[joueur];
    b.affiche();
    Console.WriteLine($"{p.teamColor}, entrez le Y");
    X = int.Parse(Console.ReadLine());
    Console.WriteLine($"{p.teamColor}, entrez le X");
    Y = int.Parse(Console.ReadLine());
    Console.WriteLine($"{p.teamColor}, entrez votre mouvement");
    m = (Move)Enum.Parse(typeof(Move), Console.ReadLine());
    b.MovePawn(r, p, b.GetCell(X, Y), m, ref win);
    joueur=(joueur+1)%2;
}



//Dictionary<Cell, List<Move>> dict = rules.allMoves(b, TeamColor.Player1);

//foreach (Cell c in dict.Keys)
//{
//    Console.WriteLine(c);
//    foreach (Move move in dict[c]) { Console.WriteLine(move); }
//}