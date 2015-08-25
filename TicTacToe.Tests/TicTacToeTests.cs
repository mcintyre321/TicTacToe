using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TicTacToe.MoveBasedGames;
using TicTacToe.Tests.MoveBasedGames;

namespace TicTacToe.Tests
{
    public class TicTacToeTests
    {
        [Test]
        public void PlayersCanMakeMoves()
        {
            //Given two players who play squares in index order
            var player1 = new Player(moves => moves.First());
            var player2 = new Player(moves => moves.First());
            var selfAdvancingClock = new SelfAdvancingClock();
            var game = new TicTacToeGame("         ", player1, player2, () => selfAdvancingClock.GetTime());

            //When the players make moves
            for (var i = 0; i < 4; i++)
            {
                var nextPlayer = i % 2 == 0 ? player1 : player2;
                var waitingPlayer = i % 2 == 0 ? player2 : player1;
                //Then the next player has available moves
                Assert.IsNotEmpty(game.GetAvailableMoves(nextPlayer));
                //And the other player has no available moves
                Assert.IsEmpty(game.GetAvailableMoves(waitingPlayer));
                nextPlayer.TryToMakeMove(game);
            }
        }

        [Test]
        public void GameDetectsWinStatesCorrectly()
        {
            //When a game is created with a winning game layout
            var selfAdvancingClock = new SelfAdvancingClock();
            var game = new TicTacToeGame("xxxoo    ", new Player(m => m.First()), new Player(m => m.First()), () => selfAdvancingClock.GetTime());
            
            //Then the game state should reflect this
            var lastLine = game.GameStateAsRenderableString
                .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            Assert.AreEqual("x has won the game.", lastLine);
            //And the game should be marked as in progress correctly
            Assert.False(game.IsInProgress);
        }


        [Test]
        public void PlayerCannotMoveLessThanASecondAfterThePrevious()
        {
            //Given a two player game
            DateTimeOffset now = DateTimeOffset.Now;
            var player1 = new Player(m => m.First());
            var player2 = new Player(m => m.First());
            var game = new TicTacToeGame("         ", player1, player2, () => now);
            //And the first player has made a move 
            player1.TryToMakeMove(game);
            
            //When the clock has advanced less than a second
            now = now.AddMilliseconds(999);
            //And the next player looks for moves
            var moves = game.GetAvailableMoves(player2);

            //Then none are available
            Assert.IsEmpty(moves);
        }

        [Test]
        public void PlayersCanMoveAfterASecondsDelay()
        {
            //Given a two player game
            DateTimeOffset now = DateTimeOffset.Now;
            var player1 = new Player(m => m.First());
            var player2 = new Player(m => m.First());
            var game = new TicTacToeGame("         ", player1, player2, () => now);
            //And the first players has made a move 
            player1.TryToMakeMove(game);
            
            //When the clock has advanced by a second
            now = now.AddSeconds(1);

            //Then the next player has moves available
            var moves = game.GetAvailableMoves(player2);
            Assert.IsNotEmpty(moves);

        }
    }
}
