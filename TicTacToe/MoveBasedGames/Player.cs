using System.Linq;

namespace TicTacToe.MoveBasedGames
{
    public class Player
    {
        private readonly MovePicker _pickMove;

        public Player(MovePicker pickMove)
        {
            _pickMove = pickMove;
        }

        public void TryToMakeMove(IMoveBasedGame game)
        {
            var moves = game.GetAvailableMoves(this);
            if (moves.Any())
            {
                var selectedMove = _pickMove(moves);
                selectedMove.Apply();
            }
        }
    }
}