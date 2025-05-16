using ModelHexa;
namespace TestProject2
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Board b = new Board(3);
            Assert.True(true);
            //Assert.True(b.allPawns().Length==6);
        }
        [Fact]
        public void Board_Constructeur_LengthCorrect()
        {
            var b = new Board(4);
            Assert.Equal(4, b.Length);
        }

        [Fact]
        public void Board_GetCell_ReturnsCorrectCell()
        {
            var b = new Board(3);
            var cell = b[0, 0];
            Assert.NotNull(cell);
            Assert.Equal(0, cell.X);
            Assert.Equal(0, cell.Y);
        }

        [Fact]
        public void Board_MovePawn_InvalidMove_ReturnsFalse()
        {
            var b = new Board(3);
            var r = new Rules();
            var p = new HumanPlayer("Test", TeamColor.Player1);
            var cell = b[0, 0];
            bool win = false;
            var result = b.MovePawn(r, p, cell, Move.cantMove, ref win);
            Assert.False(result);
        }

        [Fact]
        public void Board_allPawns_ReturnsAllPawns()
        {
            var b = new Board(3);
            Cell[] tab1 = null, tab2 = null;
            var result = b.allPawns(ref tab1, ref tab2);
            Assert.True(result);
            Assert.NotNull(tab1);
            Assert.NotNull(tab2);
        }

        [Fact]
        public void Cell_Empty_ReturnsTrueIfNoPawn()
        {
            var cell = new Cell(0, 0);
            Assert.True(cell.Empty());
        }

        [Fact]
        public void Rules_allMoves_ReturnsDictionary()
        {
            var b = new Board(3);
            var r = new Rules();
            var moves = r.allMoves(b, TeamColor.Player1);
            Assert.NotNull(moves);
        }

        [Fact]
        public void Rules_isMoveValid_InvalidMove_ReturnsFalse()
        {
            var b = new Board(3);
            var r = new Rules();
            var cell = b[0, 0];
            var result = r.isMoveValid(b, Move.cantMove, TeamColor.Player1, cell);
            Assert.False(result);
        }

        [Fact]
        public void BOTPlayer_ChooseMove_ReturnsMove()
        {
            var bot = new BOTPlayer(TeamColor.Player1);
            var moves = new List<Move> { Move.moveBy1, Move.moveBy2 };
            var cell = new Cell(0, 0);
            bool choixFait = false;
            var move = bot.ChooseMove(moves, cell, ref choixFait);
            Assert.Contains(move, moves);
            Assert.True(choixFait);
        }

        [Fact]
        public void BOTPlayer_ChoosePawn_ReturnsCell()
        {
            var bot = new BOTPlayer(TeamColor.Player1);
            var cell1 = new Cell(0, 0);
            var cell2 = new Cell(1, 1);
            var dict = new Dictionary<Cell, List<Move>>
            {
                { cell1, new List<Move> { Move.moveBy1 } },
                { cell2, new List<Move> { Move.moveBy2 } }
            };
            var chosen = bot.ChoosePawn(dict);
            Assert.Contains(chosen, dict.Keys);
        }

        [Fact]
        public void Player_Victoires_Increment()
        {
            var player = new HumanPlayer("Farès", TeamColor.Player1);
            int initial = player.victoires;
            player.victoires++;
            Assert.Equal(initial + 1, player.victoires);
        }

        [Fact]
        public void HumanPlayer_BoardChanged_Event_IsRaised()
        {
            var player = new HumanPlayer("Farès", TeamColor.Player1);
            bool eventRaised = false;
            player.BoardChanged += (sender, args) => eventRaised = true;

            // Création des arguments requis pour BoardChangedEventArgs
            var board = new Board(3);
            var move = Move.cantMove;
            var cell = board[0, 0];
            var eventArgs = new BoardChangedEventArgs(board, player, move, cell);

            // Utilisation de la réflexion pour appeler la méthode protégée OnBoardChanged
            var method = typeof(IPlayer).GetMethod("OnBoardChanged", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(player, new object[] { eventArgs });

            Assert.True(eventRaised);
        }

        [Fact]
        public void BOTPlayer_BoardChanged_Event_IsRaised()
        {
            var bot = new BOTPlayer(TeamColor.Player2, "BotX");
            bool eventRaised = false;
            bot.BoardChanged += (sender, args) => eventRaised = true;

            // Création des arguments requis pour BoardChangedEventArgs
            var board = new Board(3);
            var move = Move.cantMove;
            var cell = board[0, 0];
            var eventArgs = new BoardChangedEventArgs(board, bot, move, cell);

            // Utilisation de la réflexion pour appeler la méthode protégée OnBoardChanged
            var method = typeof(IPlayer).GetMethod("OnBoardChanged", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(bot, new object[] { eventArgs });

            Assert.True(eventRaised);
        }

    }

}
