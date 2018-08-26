using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Game1
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameBoard = new GameBoard(10, 20);
            var board = new[,] {{true, true, true}, {false, true, false}, {true, false, true}};

            gameBoard.XPosition = 4;
            gameBoard.YPosition = 2;

            //while (true)
            //{
            //    gameBoard.SetBoard(GenerateBoard(gameBoard.XSize, gameBoard.YSize));
            //    gameBoard.Draw();
            //    Task.Delay(1000).Wait();
            //}

            gameBoard.Run();
        }
    }
}
