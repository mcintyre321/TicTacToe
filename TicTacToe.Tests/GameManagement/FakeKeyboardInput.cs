using System;
using TicTacToe.GameManagement;

namespace TicTacToe.Tests.GameManagement
{
    public class FakeKeyboardInput : KeyboardInput
    {
        public void SendConsoleKey(ConsoleKey enter)
        {
            
            base.SendKey(new ConsoleKeyInfo(default(char), enter, false, false, false));
        }
    }
}