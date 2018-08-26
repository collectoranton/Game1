using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public abstract class Tetromino
    {
        protected bool[,] _shape;

        public Position Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ClockwiseRotation ClockwiseRotation { get; set; }

        protected Tetromino(int xPosition, int yPosition, ConsoleColor color, ClockwiseRotation rotation = ClockwiseRotation.Zero)
        {
            Position = new Position(xPosition, yPosition);
            Color = color;
            ClockwiseRotation = rotation;
        }

        public IEnumerable<Block> GetBlocks()
        {
            var blocks = new List<Block>();

            for (var y = 0; y < _shape.GetLength(1); y++)
            {
                for (var x = 0; x < _shape.GetLength(0); x++)
                {
                    if (_shape[x, y])
                    {
                        yield return new Block(Position.X + x, Position.Y + y, Color);
                    }
                }
            }
        }

        public void Rotatee(bool clockwise = true)
        {
            var xSize = _shape.GetLength(0);
            var ySize = _shape.GetLength(1);

            var rotated = new bool[xSize, ySize];

            for (var x = 0; x < ySize; x++)
            {
                for (var y = 0; y < xSize; y++)
                {
                    rotated[x, y] = _shape[y, xSize - x - 1];
                }
            }

            _shape = rotated;
        }
    }
}
