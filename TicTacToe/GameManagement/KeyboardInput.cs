using System;

namespace TicTacToe.GameManagement
{
    public class KeyboardInput
    {
        protected void SendKey(ConsoleKeyInfo keyPress)
        {
            KeySent(this, keyPress);
        }
        public event EventHandler<ConsoleKeyInfo> KeySent = (sender, o) => { };
    }
}