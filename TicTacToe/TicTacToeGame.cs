using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.GameManagement;
using TicTacToe.MoveBasedGames;

namespace TicTacToe
{
    public class TicTacToeGame : IGameInstance, IMoveBasedGame, IRenderableGridGame
    {
        public const string DrawMessage = "The game has been drawn.";
        public const string WinMessage = "{0} has won the game.";
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Func<DateTimeOffset> _getTime;
        public char[] Squares { get; private set; }
        public event EventHandler StateChanged = (sender, args) => { };

        private string completionMessage = null; //we set this when the game is won or drawn. At some point a status enum might be a useful thing to use instead

        public bool IsInProgress => completionMessage == null;

        string IRenderableGridGame.StatusMessage => completionMessage;
        int IRenderableGridGame.NumberOfCols => 3;
        int IRenderableGridGame.NumberOfRows => 3;

        public TicTacToeGame(string squares, Player player1, Player player2, Func<DateTimeOffset> getTime)
        {
            Squares = squares.ToCharArray();
            _player1 = player1;
            _player2 = player2;
            _getTime = getTime;
            CheckForEndConditions('x');
            CheckForEndConditions('o');

        }

        public string GameStateAsRenderableString => GridGameRenderer.Render(this);


        public IReadOnlyCollection<IMove> GetAvailableMoves(Player player)
        {
            //No moves are available within a second of the previous move occurring 
            if (_getTime() - lastMoveTaken < TimeSpan.FromSeconds(1))
            {
                return new List<IMove>();
            }


            var numberOfMovesSoFar = Squares.Count(sq => sq != ' ');
            var nextPlayer = numberOfMovesSoFar % 2 == 0 ? _player1 : _player2;

            //No moves are available to players whos turn it aint
            if (nextPlayer != player) return new List<IMove>();

            var nextPlayerSymbol = numberOfMovesSoFar % 2 == 0 ? 'x' : 'o';

            return Squares
                .Select((c, i) => c == ' ' ? new TicTacToeMove(this, i, nextPlayerSymbol) : null)
                .Where(move => move != null)
                .ToList()
                .AsReadOnly();
        }

        private DateTimeOffset? lastMoveTaken;
        internal void Apply(TicTacToeMove move)
        {
            Squares[move.Index] = move.PlayerSymbol;
            CheckForEndConditions(move.PlayerSymbol);
            lastMoveTaken = _getTime();
            StateChanged(this, EventArgs.Empty);
        }

        private void CheckForEndConditions(char symbol)
        {
            if (this.CheckForVictory(symbol))
            {
                this.completionMessage = string.Format(WinMessage, symbol);
            }
            else if (this.CheckForStalemate())
            {
                completionMessage = DrawMessage;
            }
        }

        private bool CheckForStalemate() => Squares.All(sq => sq != ' ');

        private bool CheckForVictory(char c) =>
            AllSame(c, 0, 1, 2) ||
            AllSame(c, 3, 4, 5) ||
            AllSame(c, 6, 7, 8) ||
            AllSame(c, 0, 3, 6) ||
            AllSame(c, 1, 4, 7) ||
            AllSame(c, 2, 5, 8) ||
            AllSame(c, 0, 4, 8) ||
            AllSame(c, 2, 4, 5);

        private bool AllSame(char c, int i, int i1, int i2)
        {
            return c == Squares[i] && c == Squares[i1] && c == Squares[i2];
        }


    }

    public class GridGameRenderer
    {
        public static string Render(IRenderableGridGame g)
        {
            var output = new StringBuilder();

            for (var row = 0; row < g.NumberOfRows; row++)
            {
                for (var col = 0; col < g.NumberOfCols; col++)
                {
                    output.Append(g.Squares[col + g.NumberOfCols * row].ToString());
                }
                output.Append(Environment.NewLine);
            }
            if (g.StatusMessage != null) output.AppendLine("\r\n" + g.StatusMessage);
            return output.ToString();
        }
    }

    public interface IRenderableGridGame
    {
        char[] Squares { get; }
        string StatusMessage { get; }
        int NumberOfRows { get; }
        int NumberOfCols { get; }
    }
}