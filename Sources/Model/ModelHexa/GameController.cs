using ModelHexa;

namespace GameController
{
    public class GameController
    {
        private Board board;
        private Player currentPlayer;
        private Player opponent;
        private Rules rules;

        public GameController(Player player1, Player player2, int boardSize)
        {
            board = new Board(null, boardSize);
            currentPlayer = player1;
            opponent = player2;
            rules = new Rules();
        }

        public bool TryMove(Cell fromCell, Move move)
        {
            if (!rules.isMoveValid(board, move, currentPlayer.TeamColor, fromCell))
                return false;

            ExecuteMove(fromCell, move);
            SwapPlayers();
            return true;
        }

        private void ExecuteMove(Cell fromCell, Move move)
        {
            int dir = (fromCell.pawn.Color == TeamColor.Player1) ? 1 : -1;
            int newX = fromCell.X;
            int newY = fromCell.Y;

            switch (move)
            {
                case Move.moveBy1:
                    newX += dir;
                    break;
                case Move.moveBy2:
                    newX += 2 * dir;
                    break;
                case Move.eatLeft:
                    newX += dir;
                    newY -= 1;
                    break;
                case Move.eatRight:
                    newX += dir;
                    newY += 1;
                    break;
            }

            Cell toCell = board.GetCell(newX, newY);
            toCell.SetPawn(fromCell.pawn);
            fromCell.RemovePawn();
        }

        private void SwapPlayers()
        {
            var temp = currentPlayer;
            currentPlayer = opponent;
            opponent = temp;
        }

        public Board GetBoard() => board;
        public Player GetCurrentPlayer() => currentPlayer;
    }
}
