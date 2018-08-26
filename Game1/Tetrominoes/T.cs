using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public class T : Tetromino, IRotatable
    {
        public T(int xPosition, int yPosition, ConsoleColor color = ConsoleColor.DarkMagenta, ClockwiseRotation rotation = ClockwiseRotation.Zero) : base(xPosition, yPosition, color, rotation)
        {
            _shape = ShapeBuilder.GetShapeArray(this);
        }

        public void Rotate(bool clockwise = true)
        {
            throw new NotImplementedException();
        }
    }
}
