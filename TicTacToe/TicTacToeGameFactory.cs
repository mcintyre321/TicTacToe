using System;
using System.Threading.Tasks;
using TicTacToe.MoveBasedGames;

namespace TicTacToe
{
    public static class TicTacToeGameFactory
    {
        public static TicTacToeGame CreateWithRandomPlayers(object lockObject)
        {
            var random = new Random();
            Func<Player> createPlayer = () => new Player(MovePickerFactory.CreateRandom(random.Next));

            var player1 = createPlayer();
            var player2 = createPlayer();
            var game = new TicTacToeGame("         ", player1, player2, () => DateTimeOffset.UtcNow);
            Task.Run(() =>
            {
                Func<bool> performUpdate = () =>
                {
                    lock (lockObject)
                    {
                        player1.TryToMakeMove(game);
                        player2.TryToMakeMove(game);
                        return game.IsInProgress;
                    }
                };

                while (performUpdate())
                {
                    Task.Delay(100).Wait();
                }
            });
            return game;
        }

        
    }
}
