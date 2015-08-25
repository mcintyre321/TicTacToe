using TicTacToe.MoveBasedGames;

namespace TicTacToe.Tests.MoveBasedGames
{
    public class FakeMove : IMove
    {
        public bool IsApplied { get; set; }
        public void Apply()
        {
            IsApplied = true;
        }
    }
}