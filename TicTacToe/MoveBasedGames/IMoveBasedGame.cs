using System.Collections.Generic;

namespace TicTacToe.MoveBasedGames
{
    public interface IMoveBasedGame
    {
        IReadOnlyCollection<IMove> GetAvailableMoves(Player player);
    }

}