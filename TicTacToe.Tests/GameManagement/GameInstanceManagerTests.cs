using System;
using NUnit.Framework;
using TicTacToe.GameManagement;

namespace TicTacToe.Tests.GameManagement
{

    public class GameInstanceManagerTests
    {
        private string renderedOutput;
        private FakeKeyboardInput _keyboard;
        private FakeGameInstance _fakeGameInstance;
        private GameInstanceManager _manager;

        private void CreateGameManager()
        {
            RenderOutput writeOutputToField = output => this.renderedOutput = output;
            _keyboard = new FakeKeyboardInput();
            Func<FakeGameInstance> gameInstanceFactory = () => _fakeGameInstance = new FakeGameInstance();

            _manager = new GameInstanceManager(writeOutputToField, _keyboard, gameInstanceFactory);
        }


        [Test]
        public void StartGameOptionIsDisplayed()
        {
            //When a new game controller is created with a game instance
            CreateGameManager();

            //Then the start game option should be displayed to the user
            Assert.AreEqual("Press enter to start a new game. Press escape to exit.", renderedOutput);
        }

        

        [Test]
        public void GameCanBeStartedByPressingEnter()
        {
            //Given a game which is waiting for the user to start it
            CreateGameManager();

            //When the user presses the enter key
            _keyboard.SendConsoleKey(ConsoleKey.Enter);
            
            //Then the game manager should have updated the renderer with the game state
            Assert.AreEqual("game started", renderedOutput);
        }

        [Test]
        public void GameIsDisplayedWhenGameUpdates()
        {
            //Given a game 
            CreateGameManager();
            //And the game has been started
            _keyboard.SendConsoleKey(ConsoleKey.Enter);

            //When the game updates
            _fakeGameInstance.SetState(true, "game updated");

            //Then the game manager should have updated the renderer
            Assert.AreEqual("game updated", renderedOutput);
        }

        [Test]
        public void FinishedGameIsDisplayedWhenGameEnds()
        {
            //Given a game 
            CreateGameManager();
            //And the game has been started
            _keyboard.SendConsoleKey(ConsoleKey.Enter);
            
            //When the game completes
            _fakeGameInstance.SetState(false, "game finished");
            
            var renderedState = renderedOutput;
            //Then the completed game state should be displayed
            Assert.True(renderedState.StartsWith("game finished"));
            //And the prompt to start another game should be displayed
            Assert.True(renderedState.EndsWith("\r\n" + GameInstanceManager.StartNewGameMessage));
        }


        [Test]
        public void ANewGameCanBeStartedAfterThePreviousOne()
        {
            //Given a game 
            CreateGameManager();
            //And the game has been started
            _keyboard.SendConsoleKey(ConsoleKey.Enter);

            //And the game has then completed
            _fakeGameInstance.SetState(false, "game finished");

            var renderedState = renderedOutput;
            //Then the completed game state should be displayed
            Assert.True(renderedState.StartsWith("game finished"));
            //And the prompt to start another game should be displayed
            Assert.True(renderedState.EndsWith("\r\n" + GameInstanceManager.StartNewGameMessage));
        }


    }
}
