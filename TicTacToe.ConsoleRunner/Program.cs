using System;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.GameManagement;
using TicTacToe.MoveBasedGames;

namespace TicTacToe.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var lockObject = new object();
            

            RenderOutput writeToConsole = s =>
            {
                lock (lockObject)
                {
                    Console.Clear();
                    Console.WriteLine(s);
                }
            };

            var manager = new GameInstanceManager(
                writeToConsole, 
                new ConsoleKeyboardInput(lockObject), 
                () => TicTacToeGameFactory.CreateWithRandomPlayers(lockObject)
            );
            while (!manager.Exited)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
