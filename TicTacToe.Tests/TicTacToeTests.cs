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

        /// In the interests of saving time I have only a few states tests
        /// 
        /// 

        [TestCase("xxxoo    ", true, "x", Description = "top line can win game for x")] 
        [TestCase("oooxx x  ", true, "o", Description = "top line can win game for o")]
        [TestCase(" ox xox  ", false, null, Description = "reverse diagonal can win for x")] 
        [TestCase("         ", false, null, Description = "empty board is still in progress")] 
        [TestCase(" x o   x ", false, null, Description = "partial board is still in progress")] 
        [TestCase("oxoxxoxox", true, null, Description = "draw")] 
        public void GameDetectsWinStatesCorrectly(string board, bool isComplete, string winner)
        {
            //When a game is created with a game layout
            var selfAdvancingClock = new SelfAdvancingClock();
            var game = new TicTacToeGame(board, new Player(m => m.First()), new Player(m => m.First()), () => selfAdvancingClock.GetTime());

            //Then the game should be marked as in progress correctly
            Assert.AreEqual(!isComplete, game.IsInProgress);

            //And the game state should reflect the result of the game if the game is complete
            if (isComplete)
            {
                var expectedMessage = string.IsNullOrWhiteSpace(winner)
                    ? TicTacToeGame.DrawMessage
                    : string.Format(TicTacToeGame.WinMessage, winner);
                Assert.AreEqual(expectedMessage, ((IRenderableGridGame) game).StatusMessage);
            }
        }

        [Test]
        public void GameDetectsDrawStatesCorrectly()
        {
            //When a game is created with a winning game layout
            var selfAdvancingClock = new SelfAdvancingClock();
            var game = new TicTacToeGame("xxxoo    ", new Player(m => m.First()), new Player(m => m.First()), () => selfAdvancingClock.GetTime());

            //Then the game state should reflect this
            var lastLine = game.GameStateAsRenderableString
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
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
