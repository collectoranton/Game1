using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public interface IRotatable
    {
        void Rotate(bool clockwise = true);
    }
}
