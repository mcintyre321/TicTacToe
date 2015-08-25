using System.Linq;
using NUnit.Framework;
using TicTacToe.MoveBasedGames;

namespace TicTacToe.Tests.MoveBasedGames
{
    public class PlayerTests
    {
        [Test]
        public void PlayerMakesMovesWhenItCan()
        {
            //Given a player who is playing in a game
            var game = new FakeTurnBasedGame();
            var player = new Player(moves => moves.First());
            //And the player has an available move
            var move = new FakeMove();
            game.AvailableMoves.Add(move);

            //When the player tries to make a move
            player.TryToMakeMove(game);

            //Then the move should have been applied by the player
            Assert.True(move.IsApplied); 
        }

    }
}