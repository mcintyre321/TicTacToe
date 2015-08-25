using System;

namespace TicTacToe.GameManagement
{
    public interface IGameInstance
    {
        event EventHandler StateChanged;
        bool IsInProgress { get; }
        string GameStateAsRenderableString { get; }
    }
}