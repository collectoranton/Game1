using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1
{
    public class GameBoard
    {
        private Block[,] _board;
        private bool _isRunning = false;
        private readonly Random _random = new Random();

        public int XSize => _board.GetLength(0);
        public int YSize => _board.GetLength(1);
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public GameBoard(int xSize, int ySize)
        {
            _board = new Block[xSize, ySize];
        }

        public void Run()
        {
            _isRunning = true;

            while (_isRunning)
            {
                Thread.Sleep(1000);
                GenerateRandomBoard();
                Draw();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Draw()
        {
            Console.CursorTop = YPosition;
            Console.Write(GetStringFromBoard());
        }

        public void DrawColor()
        {
            var startColor = Console.ForegroundColor;
            Console.CursorLeft = XPosition;
            Console.CursorTop = YPosition;

            for(var y = 0; y < YSize; y++)
            {
                Console.CursorLeft = XPosition;

                for (var x = 0; x < XSize; x++)
                {
                    Console.ForegroundColor = _board[x, y].Color;
                    Console.Write(_board[x, y]);
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = startColor;
        }

        public void SetBoard(Block[,] board)
        {
            if (board == null) throw new ArgumentNullException(nameof(board));
            if (!IsCorrectSize(board)) throw new ArgumentException($"Incorrect board size. ({nameof(board)}: {board.GetLength(0)}x{board.GetLength(1)} GameBoard: {XSize}x{YSize})", nameof(board));

            _board = board;
        }

        public void SetBlock(int x, int y, Block filled)
        {
            if (x >= XSize || x < 0) throw new ArgumentOutOfRangeException(nameof(x), $"Position is out of range. (x: {x} {nameof(XSize)}: {XSize})");
            if (y >= XSize || y < 0) throw new ArgumentOutOfRangeException(nameof(y), $"Position is out of range. (x: {y} {nameof(YSize)}: {YSize})");

            _board[x, y] = filled;
        }

        private void GenerateRandomBoard()
        {
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    _board[x, y] = new Block((Transparency)_random.Next(5), (ConsoleColor)_random.Next(16));
                }
            }
        }

        private string GetStringFromBoard()
        {
            var boardString = new StringBuilder();

            for (var y = 0; y < YSize; y++)
            {
                for (var count = 0; count < XPosition; count++)
                {
                    boardString.Append(' ');
                }

                for (var x = 0; x < XSize; x++)
                {
                    boardString.Append(_board[x, y]);
                }

                boardString.AppendLine();
            }

            return boardString.ToString();
        }

        private bool IsCorrectSize(Block[,] board)
        {
            return board.GetLength(0) == _board.GetLength(0) || board.GetLength(1) == _board.GetLength(1);
        }
    }
}