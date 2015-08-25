using System.Collections.Generic;

namespace TicTacToe.MoveBasedGames
{
    //Strategies work very well as stateless functions
    public delegate IMove MovePicker(IReadOnlyCollection<IMove> moves);
}