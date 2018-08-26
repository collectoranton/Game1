using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public class Z : Tetromino, IRotatable
    {
        public Z(int xPosition, int yPosition, ConsoleColor color = ConsoleColor.Red, ClockwiseRotation rotation = ClockwiseRotation.Zero) : base(xPosition, yPosition, color, rotation)
        {
            _shape = ShapeBuilder.GetShapeArray(this);
        }

        public void Rotate(bool clockwise = true)
        {
            throw new NotImplementedException();
        }
    }
}
