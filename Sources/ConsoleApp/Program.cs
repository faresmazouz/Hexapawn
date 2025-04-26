// See https://aka.ms/new-console-template for more information
using ModelHexa;

Console.WriteLine("Hello, World!");
Rules rules = new Rules();
Board b = new Board(4);
Dictionary<Cell, List<Move>> dict = rules.allMoves(b, TeamColor.Player1);

foreach (Cell c in dict.Keys)
{
    Console.WriteLine(c);
    foreach (Move move in dict[c]) {  Console.WriteLine(move); }
}