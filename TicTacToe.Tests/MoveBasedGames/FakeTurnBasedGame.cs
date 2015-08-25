using System.Collections.Generic;
using TicTacToe.MoveBasedGames;

namespace TicTacToe.Tests.MoveBasedGames
{
    public class FakeTurnBasedGame : IMoveBasedGame
    {
        public List<IMove> AvailableMoves { get; } = new List<IMove>();

        public IReadOnlyCollection<IMove> GetAvailableMoves(Player player)
        {
            return AvailableMoves;
        }

    }
}