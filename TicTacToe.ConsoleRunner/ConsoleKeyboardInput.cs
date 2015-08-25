using System;
using System.Threading.Tasks;
using TicTacToe.GameManagement;

namespace TicTacToe.ConsoleRunner
{
    internal class ConsoleKeyboardInput : KeyboardInput
    {
        private readonly object _lockObject;

        public ConsoleKeyboardInput(object lockObject)
        {
            _lockObject = lockObject;
            Task.Run(() => ListenToKeyboard());
        }

        private void ListenToKeyboard()
        {
            while (true)
            {
                var key = Console.ReadKey(false);

                lock (_lockObject)
                {
                    this.SendKey(key);
                }
            }
        }
    }
}