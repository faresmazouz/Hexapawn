// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using ModelHexa;

Console.WriteLine("Hello, World!");
TeamColor winner=TeamColor.Unknown;
Rules r = new Rules();
Board b = new Board(3);
Player p1 = new Player("Thomas", TeamColor.Player1);
Player p2=new Player("Thom2",TeamColor.Player2);
p1.PlayTurn(r.allMoves(b,p1.teamColor),b,r,p2,ref winner);
Console.WriteLine($"{winner} a gagné!");

