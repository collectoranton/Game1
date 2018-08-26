using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Game1.Tetrominoes;

namespace Game1
{
    public class GameBoard
    {
        private Block[,] _board;
        private Tetromino _activeTetromino;
        private bool _isRunning = false;
        private readonly List<Block> _deadBlocks = new List<Block>();
        private readonly Stopwatch _timer = new Stopwatch();
        private readonly Random _random = new Random();
        private readonly Tetromino[] _tetrominoes = ShapeBuilder.GetAllTetrominoes();
        private int _score;

        public int XSize => _board.GetLength(0);
        public int YSize => _board.GetLength(1);
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public GameBoard(int xSize, int ySize)
        {
            _board = new Block[xSize, ySize];
            SetRandomActiveTetromino();
        }

        public void Run(int updateIntervalMilliseconds = 500)
        {
            _isRunning = true;
            _timer.Start();
            Console.CursorVisible = false;

            while (_isRunning)
            {
                if (_timer.ElapsedMilliseconds >= updateIntervalMilliseconds)
                {
                    PrintTopLeft($"{_timer.ElapsedMilliseconds}ms");
                    Update();
                }
                
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            MoveRight();
                            break;
                        case ConsoleKey.UpArrow:
                            Rotate();
                            break;
                        case ConsoleKey.DownArrow:
                            Drop();
                            break;
                        case ConsoleKey.P:
                            Pause();
                            break;
                    }

                    Update(afterMove: true);
                }
            }
        }

        private void PrintTopLeft(string print, int row = 0)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = row;
            Console.WriteLine(print);
        }

        private Block[,] GeneratePauseBoard()
        {
            var pauseBoard = new Block[XSize, YSize];

            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    pauseBoard[x, y] = _board[x, y];
                }
            }

            // Add pause text

            return pauseBoard;
        }

        private void Update(bool afterMove = false)
        {
            if (!afterMove)
            {
                _timer.Restart();                
            }            

            var newPosition = new Position(0, 1);

            if (WillCollide(_activeTetromino, newPosition))
            {
                Kill();
            }
            else if (!afterMove)
            {
                _activeTetromino.Position.Y++;
            }

            DrawBoard();

            PrintTopLeft(_score.ToString(), 1);
        }

        private void Kill()
        {
            _deadBlocks.AddRange(_activeTetromino.GetBlocks());

            Blow();

            SetRandomActiveTetromino();
            DrawBoard();
        }

        private void Blow()
        {
            var fullRows = GetFullRows();

            if (fullRows.Any())
            {
                BlowRows(fullRows);
            }
        }

        private void BlowRows(IEnumerable<int> fullRows)
        {
            var rows = fullRows.OrderBy(r => r).ToList();

            for (var i = rows.Count - 1; i >= 0; i--)
            {
                _deadBlocks.RemoveAll(b => b.Position.Y == rows[i]);

                _deadBlocks
                    .Where(b => b.Position.Y < rows[i])
                    .ToList()
                    .ForEach(b => b.Position.Y++);

                for (var j = 0; j < rows.Count; j++)
                {
                    rows[j]++;
                }

                rows.RemoveAt(i);
            }
        }

        private IEnumerable<int> GetFullRows()
        {
            for (var y = 0; y < YSize; y++)
            {
                if (_deadBlocks.Count(b => b.Position.Y == y) == XSize)
                {
                    yield return y;
                }
            }
        }

        public void Pause()
        {
            _isRunning = false;
            _timer.Stop();
            PrintTopLeft("PAUSED");

            Console.ReadKey(true);

            PrintTopLeft("      ");
            _timer.Start();
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void MoveLeft()
        {
            var newPosition = new Position(-1, 0);

            if (!WillCollide(_activeTetromino, newPosition))
            {
                _activeTetromino.Position.X--;
            }
        }

        private void MoveRight()
        {
            var newPosition = new Position(1, 0);

            if (!WillCollide(_activeTetromino, newPosition))
            {
                _activeTetromino.Position.X++;
            }
        }

        private void Rotate()
        {
            //if (_activeTetromino is IRotatable tetromino)
            //{
            //    tetromino.Rotate();
            //}
            _activeTetromino.Rotatee();
        }

        private void Drop()
        {
            while (!WillCollide(_activeTetromino, new Position(0, 1)))
            {
                _activeTetromino.Position.Y++;
            }
        }

        private void DrawBoard()
        {
            GenerateBoard();
            DrawColor();
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

        private void SetRandomActiveTetromino()
        {
            _tetrominoes.ToList().ForEach(t => t.Position.Reset(XSize / 2));
            _activeTetromino = _tetrominoes[_random.Next(_tetrominoes.Length)];
        }

        private void GenerateRandomBoard()
        {
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    _board[x, y] = new Block(x, y, (ConsoleColor)_random.Next(16), (Transparency)_random.Next(5));
                }
            }
        }

        private void GenerateBoard()
        {
            for (var y = 0; y < YSize; y++)
            {
                for (var x = 0; x < XSize; x++)
                {
                    var position = new Position(x, y);

                    if (_activeTetromino.GetBlocks().Any(b => b.Position == position))
                    {
                        _board[x, y] = new Block(position, _activeTetromino.Color);
                    }
                    else if (_deadBlocks.Any(b => b.Position == position))
                    {
                        _board[x, y] = new Block(position, _deadBlocks.Single(b => b.Position == position).Color);
                    }
                    else
                    {
                        _board[x, y] = new Block(position, ConsoleColor.White, Transparency.ThreeQuarter);
                    }
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

        private bool WillCollide(Tetromino tetromino, Position newPosition)
        {
            var blocks = tetromino.GetBlocks().ToList();
            var deadBlocks = false;

            if (_deadBlocks.Count > 0)
            {
                deadBlocks = _deadBlocks.Any(db => blocks.Any(b => db.Position == b.Position + newPosition));
            }

            var bounds = blocks.Any(b => IsOutOfBoardBounds(b.Position + newPosition));

            return bounds || deadBlocks;
        }

        private bool WillCollideOnRotation()
        {
            return false;
        }

        private bool IsOutOfBoardBounds(Position position)
        {
            return position.X < 0 || position.Y < 0 || position.X > XSize - 1 || position.Y > YSize - 1;
        }

        private bool IsCorrectSize(Block[,] board)
        {
            return board.GetLength(0) == _board.GetLength(0) || board.GetLength(1) == _board.GetLength(1);
        }
    }
}