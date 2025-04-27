// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;

Console.WriteLine("Hello, World!");
Rules r = new Rules();
Board b = new Board(3);
Player p1 = new Player("Thomas", TeamColor.Player1);
Player p2=new Player("Thom2",TeamColor.Player2);
Player[] ptab = [p1, p2];
int joueur=0;
Player p;
bool win = false;
int X, Y;
Move m=Move.cantMove;
Cell cmove=new Cell(0,0);
Dictionary<Cell, List<Move>> d;
while (win == false)
{
    bool choixFait=false;
    p = ptab[joueur];
    d = r.allMoves(b, p.teamColor);
    if (d.Count == 0) break;
    b.affiche();
    while (!choixFait)
    { 
        cmove = p.ChoosePawn(d);
        m = p.ChooseMove(d[cmove], cmove, ref choixFait);
    }
    b.MovePawn(r, p, cmove, m, ref win);
    joueur=(joueur+1)%2;
}



//Dictionary<Cell, List<Move>> dict = rules.allMoves(b, TeamColor.Player1);

//foreach (Cell c in dict.Keys)
//{
//    Console.WriteLine(c);
//    foreach (Move move in dict[c]) { Console.WriteLine(move); }
//}