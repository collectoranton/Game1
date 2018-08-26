using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public class O : Tetromino
    {
        public O(int xPosition, int yPosition, ConsoleColor color = ConsoleColor.Yellow, ClockwiseRotation rotation = ClockwiseRotation.Zero) : base(xPosition, yPosition, color, rotation)
        {
            _shape = ShapeBuilder.GetShapeArray(this);
        }
    }
}
