using ModelHexa;
namespace TestProject2
{
    public class UnitTest1
    {
        // --- Board ---
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Board_Constructeur_LengthCorrect(int length)
        {
            var b = new Board(length);
            Assert.Equal(length, b.Length);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Board_GetCell_ReturnsCorrectCell(int x, int y)
        {
            var b = new Board(3);
            var cell = b[x, y];
            Assert.NotNull(cell);
            Assert.Equal(x, cell.X);
            Assert.Equal(y, cell.Y);
        }
        /*
        [Theory]
        [InlineData(Move.cantMove, false)]
        [InlineData(Move.moveBy1, false)] // Supposons que moveBy1 n'est pas valide sur la case (0,0) initialement
        public void Board_MovePawn_InvalidMove_ReturnsFalse(Move move, bool expected)
        {
            var b = new Board(3);
            var r = new Rules();
            var p = new HumanPlayer("Test", TeamColor.Player1);
            var cell = b[0, 0];
            bool win = false;
            var result = b.MovePawn(r, p, cell, move, ref win);
            Assert.Equal(expected, result);
        }
        */
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Board_allPawns_ReturnsAllPawns(int length)
        {
            var b = new Board(length);
            Cell[] tab1 = null, tab2 = null;
            var result = b.allPawns(ref tab1, ref tab2);
            Assert.True(result);
            Assert.NotNull(tab1);
            Assert.NotNull(tab2);
        }
        /*
        [Fact]
        public void Board_Affiche_DoesNotThrow()
        {
            var b = new Board(3);
            Exception ex = Record.Exception(() => b.Affiche());
            Assert.Null(ex);
        }
        */
        // --- Cell ---
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        public void Cell_Empty_ReturnsTrueIfNoPawn(int x, int y)
        {
            var cell = new Cell(x, y);
            Assert.True(cell.Empty());
        }

        [Theory]
        [InlineData(0, 0, TeamColor.Player1)]
        [InlineData(1, 1, TeamColor.Player2)]
        public void Cell_Empty_ReturnsFalseIfPawn(int x, int y, TeamColor color)
        {
            var pawn = new Pawn(color);
            var cell = new Cell(x, y, pawn);
            Assert.False(cell.Empty());
        }

        [Theory]
        [InlineData(0, 0, null)]
        [InlineData(1, 1, TeamColor.Player1)]
        public void Cell_Constructor_SetsProperties(int x, int y, TeamColor? color)
        {
            Pawn? pawn = color.HasValue ? new Pawn(color.Value) : null;
            var cell = new Cell(x, y, pawn);
            Assert.Equal(x, cell.X);
            Assert.Equal(y, cell.Y);
            Assert.Equal(pawn, cell.Pawn);
        }

        // --- Rules ---
        [Theory]
        [InlineData(TeamColor.Player1)]
        [InlineData(TeamColor.Player2)]
        public void Rules_allMoves_ReturnsDictionary(TeamColor color)
        {
            var b = new Board(3);
            var r = new Rules();
            var moves = r.allMoves(b, color);
            Assert.NotNull(moves);
        }

        [Theory]
        [InlineData(Move.cantMove, TeamColor.Player1, 0, 0, false)]
        [InlineData(Move.moveBy1, TeamColor.Player2, 1, 1, false)] // Supposons que ce n'est pas valide
        public void Rules_isMoveValid_InvalidMove_ReturnsFalse(Move move, TeamColor color, int x, int y, bool expected)
        {
            var b = new Board(3);
            var r = new Rules();
            var cell = b[x, y];
            var result = r.isMoveValid(b, move, color, cell);
            Assert.Equal(expected, result);
        }

        /* --- Pawn ---
        [Theory]
        [InlineData(TeamColor.Player1)]
        [InlineData(TeamColor.Player2)]
        public void Pawn_Constructor_SetsTeamColor(TeamColor color)
        {
            var pawn = new Pawn(color);
            Assert.Equal(color, pawn.Team);
        }*/

        // --- Player ---
        [Theory]
        [InlineData("Farès", TeamColor.Player1)]
        [InlineData("Alice", TeamColor.Player2)]
        public void Player_Victoires_Increment(string name, TeamColor color)
        {
            var player = new HumanPlayer(name, color);
            int initial = player.victoires;
            player.victoires++;
            Assert.Equal(initial + 1, player.victoires);
        }

        [Theory]
        [InlineData("Farès", TeamColor.Player1)]
        public void HumanPlayer_BoardChanged_Event_IsRaised(string name, TeamColor color)
        {
            var player = new HumanPlayer(name, color);
            bool eventRaised = false;
            player.BoardChanged += (sender, args) => eventRaised = true;

            var board = new Board(3);
            var move = Move.cantMove;
            var cell = board[0, 0];
            var eventArgs = new BoardChangedEventArgs(board, player, move, cell);

            var method = typeof(Player).GetMethod("OnBoardChanged", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(player, new object[] { eventArgs });

            Assert.True(eventRaised);
        }

        [Theory]
        [InlineData(TeamColor.Player2)]
        public void BOTPlayer_BoardChanged_Event_IsRaised(TeamColor color)
        {
            var bot = new BOTPlayer(color);
            bool eventRaised = false;
            bot.BoardChanged += (sender, args) => eventRaised = true;

            var board = new Board(3);
            var move = Move.cantMove;
            var cell = board[0, 0];
            var eventArgs = new BoardChangedEventArgs(board, bot, move, cell);

            var method = typeof(Player).GetMethod("OnBoardChanged", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(bot, new object[] { eventArgs });

            Assert.True(eventRaised);
        }

        // --- BOTPlayer ---
        [Theory]
        [InlineData(TeamColor.Player1, Move.moveBy1)]
        [InlineData(TeamColor.Player2, Move.moveBy2)]
        public void BOTPlayer_ChooseMove_ReturnsMove(TeamColor color, Move expectedMove)
        {
            var bot = new BOTPlayer(color);
            var moves = new List<Move> { Move.moveBy1, Move.moveBy2 };
            var cell = new Cell(0, 0);
            bool choixFait = false;
            var move = bot.ChooseMove(moves, cell, ref choixFait);
            Assert.Contains(move, moves);
            Assert.True(choixFait);
        }

        [Theory]
        [InlineData(0, 0, 1, 1)]
        [InlineData(2, 2, 0, 1)]
        public void BOTPlayer_ChoosePawn_ReturnsCell(int x1, int y1, int x2, int y2)
        {
            var bot = new BOTPlayer(TeamColor.Player1);
            var cell1 = new Cell(x1, y1);
            var cell2 = new Cell(x2, y2);
            var dict = new Dictionary<Cell, List<Move>>
                {
                    { cell1, new List<Move> { Move.moveBy1 } },
                    { cell2, new List<Move> { Move.moveBy2 } }
                };
            var chosen = bot.ChoosePawn(dict);
            Assert.Contains(chosen, dict.Keys);
        }

        // --- BoardChangedEventArgs ---
        [Theory]
        [InlineData(0, 0, TeamColor.Player1, Move.moveBy1)]
        [InlineData(1, 1, TeamColor.Player2, Move.moveBy2)]
        public void BoardChangedEventArgs_Constructor_SetsProperties(int x, int y, TeamColor color, Move move)
        {
            var board = new Board(3);
            var player = new HumanPlayer("Test", color);
            var cell = new Cell(x, y);
            var args = new BoardChangedEventArgs(board, player, move, cell);
            Assert.Equal(board, args.BoardChanged);
            Assert.Equal(player, args.Player);
            Assert.Equal(move, args.MoveUsed);
            Assert.Equal(cell, args.CellChanged);
        }

        // --- UserHaveToChooseEventArgs ---
        [Theory]
        [InlineData("Quel pion voulez-vous bouger ?")]
        [InlineData("Quel mouvement ?")]
        public void UserHaveToChooseEventArgs_Constructor_SetsQuestion(string question)
        {
            var args = new UserHaveToChooseEventArgs(question);
            Assert.Equal(question, args.Question);
        }
        /*
        // --- WrongInputEventArgs ---
        [Theory]
        [InlineData("Erreur de saisie")]
        [InlineData("Mot interdit")]
        public void WrongInputEventArgs_Constructor_SetsErrorMessage(string error)
        {
            var args = new WrongInputEventArgs(error);
            Assert.Equal(error, args.ErrorMessage);
            Assert.NotNull(args.ForbiddenWords);
        }
        */
        // --- Enum Move ---
        [Theory]
        [InlineData(Move.cantMove)]
        [InlineData(Move.eatRight)]
        [InlineData(Move.eatLeft)]
        [InlineData(Move.moveBy2)]
        [InlineData(Move.moveBy1)]
        public void Move_Enum_Values_AreDefined(Move move)
        {
            Assert.True(Enum.IsDefined(typeof(Move), move));
        }

        // --- Enum TeamColor ---
        [Theory]
        [InlineData(TeamColor.Unknown)]
        [InlineData(TeamColor.Player1)]
        [InlineData(TeamColor.Player2)]
        public void TeamColor_Enum_Values_AreDefined(TeamColor color)
        {
            Assert.True(Enum.IsDefined(typeof(TeamColor), color));
        }
    }

}