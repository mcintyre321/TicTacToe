using System;
using System.Linq;

namespace TicTacToe.MoveBasedGames
{
    public static class MovePickerFactory
    {
        public static MovePicker CreateRandom(Func<int, int> getRandomInteger)
        {
            return moves =>
            {
                var indexOfChosenMove = getRandomInteger(moves.Count);
                var chosenMove = moves.ElementAt(indexOfChosenMove);
                return chosenMove;
            };
        }
    }
}