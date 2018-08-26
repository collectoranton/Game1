using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public class I : Tetromino, IRotatable
    {
        public I(int xPosition, int yPosition, ConsoleColor color = ConsoleColor.Cyan, ClockwiseRotation rotation = ClockwiseRotation.Zero) : base(xPosition, yPosition, color, rotation)
        {
            _shape = ShapeBuilder.GetShapeArray(this);
        }

        public void Rotate(bool clockwise = true)
        {
            throw new NotImplementedException();
        }
    }
}
