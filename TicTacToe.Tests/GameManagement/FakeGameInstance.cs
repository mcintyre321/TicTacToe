using System;
using TicTacToe.GameManagement;

namespace TicTacToe.Tests.GameManagement
{
    public class FakeGameInstance : IGameInstance
    {
        public FakeGameInstance()
        {
            IsInProgress = true;
            GameStateAsRenderableString = "game started";
        }
        public event EventHandler StateChanged = (sender, args) => { }; 
        public bool IsInProgress { get; set; }
        public string GameStateAsRenderableString { get; set; }

        public void SetState(bool inProgress, string gameState)
        {
            IsInProgress = inProgress;
            GameStateAsRenderableString = gameState;
            StateChanged(this, EventArgs.Empty);
        }
    }
}