using System;
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

        private static Block[,] GenerateBoard(int xSize, int ySize)
        {
            var rnd = new Random();
            var board = new Block[xSize, ySize];

            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    board[x, y] = new Block((Transparency)rnd.Next(5), (ConsoleColor) rnd.Next(16));
                }
            }

            return board;
        }
    }
}
