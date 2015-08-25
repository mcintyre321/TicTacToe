using System;

namespace TicTacToe.GameManagement
{
    public class GameInstanceManager
    {
        internal const string StartNewGameMessage = "Press enter to start a new game. Press escape to exit.";
        private readonly RenderOutput _render;
        private readonly KeyboardInput _keyboard;
        private readonly Func<IGameInstance> _gameFactory;
        private IGameInstance _game;

        public GameInstanceManager(RenderOutput render, KeyboardInput keyboard, Func<IGameInstance> gameFactory)
        {
            _render = render;
            _keyboard = keyboard;
            _gameFactory = gameFactory;
            render(StartNewGameMessage);
            _keyboard.KeySent += StartGameOnEnterPress;
        }

        public bool Exited { get; private set; }

        private void StartGameOnEnterPress(object sender, ConsoleKeyInfo e)
        {
            if (e.Key == ConsoleKey.Enter) //i.e. a newline was sent
            {
                _keyboard.KeySent -= StartGameOnEnterPress;
                _game = _gameFactory();
                _game.StateChanged += UpdateOnStateChange;
                _render(_game.GameStateAsRenderableString);
            }
            if (e.Key == ConsoleKey.Escape)
            {
                Exited = true;
            }

        }

        private void UpdateOnStateChange(object sender, EventArgs args)
        {
            var textToBeDisplayed = _game.GameStateAsRenderableString;

            if (_game.IsInProgress == false)
            {
                textToBeDisplayed += "\r\n" + StartNewGameMessage;
                _game.StateChanged -= UpdateOnStateChange;
                _keyboard.KeySent += StartGameOnEnterPress;
            }

            _render(textToBeDisplayed);
        }
    }
}