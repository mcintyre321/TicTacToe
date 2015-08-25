using System.Collections.Generic;
using NUnit.Framework;
using TicTacToe.MoveBasedGames;

namespace TicTacToe.Tests.MoveBasedGames
{
    public class RandomMoveSelectionStrategyTests
    {
        [Test]
        public void RandomPlayerMakesMovesAtRandom()
        {
            //Given several available moves for the next MovePicker
            var moves = new List<IMove> { new FakeMove(), new FakeMove(), new FakeMove() };

            //And an automated game MovePicker with a fixed random 
            var fixedRandomValue = 2;
            var pickAMove = MovePickerFactory.CreateRandom(limit => 2);

            //When asked to make a move in a game
            var selectedMove = pickAMove(moves.AsReadOnly());

            //Then a move should be have selected using the random function
            Assert.AreEqual(selectedMove, moves[fixedRandomValue]);
        }

     


    }
}