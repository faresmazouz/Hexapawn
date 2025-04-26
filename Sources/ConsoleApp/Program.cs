// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;

Console.WriteLine("Hello, World!");
Rules rules = new Rules();
Board b = new Board(3);
Player p = new Player("Thomas", TeamColor.Player1);
bool win = false;
b.MovePawn(rules, p, b.GetCell(0,0),Move.moveBy1,ref win);
Console.WriteLine(b.MovePawn(rules, p, b.GetCell(1, 0), Move.eatLeft, ref win));
Console.WriteLine(b.MovePawn(rules, p, b.GetCell(1, 0), Move.eatRight, ref win));
Console.WriteLine(win);



Dictionary<Cell, List<Move>> dict = rules.allMoves(b, TeamColor.Player1);

foreach (Cell c in dict.Keys)
{
    Console.WriteLine(c);
    foreach (Move move in dict[c]) { Console.WriteLine(move); }
}