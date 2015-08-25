using TicTacToe.MoveBasedGames;

namespace TicTacToe
{
    internal class TicTacToeMove : IMove
    {
        private readonly TicTacToeGame _ticTacToe;
        internal readonly char PlayerSymbol;
        public int Index { get; set; }

        public TicTacToeMove(TicTacToeGame ticTacToe, int i, char playerSymbol)
        {
            _ticTacToe = ticTacToe;
            PlayerSymbol = playerSymbol;
            Index = i;
        }

        public void Apply()
        {
            _ticTacToe.Apply(this);

        }
    }
}