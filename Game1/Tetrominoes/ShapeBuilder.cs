using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Tetrominoes
{
    public class ShapeBuilder
    {
        public static Tetromino[] GetAllTetrominoes()
        {
            return new Tetromino[]
            {
                new I(0, 0),
                new J(0, 0),
                new L(0, 0),
                new O(0, 0),
                new S(0, 0),
                new T(0, 0),
                new Z(0, 0)
            };
        }

        public static bool[,] GetShapeArray(Tetromino tetromino)
        {
            switch (tetromino)
            {
                case I i:
                    return GetShapeOfI();
                case J j:
                    return GetShapeOfJ();
                case L l:
                    return GetShapeOfL();
                case O o:
                    return GetShapeOfO();
                case S s:
                    return GetShapeOfS();
                case T t:
                    return GetShapeOfT();
                case Z z:
                    return GetShapeOfZ();
                case null:
                    throw new ArgumentNullException(nameof(tetromino));
                default:
                    throw new ArgumentOutOfRangeException(nameof(tetromino));
            }
        }

        private static bool[,] GetShapeOfI()
        {
            return new[,]
            {
                {false, false, false, false},
                {true, true, true, true},
                {false, false, false, false},
                {false, false, false, false}
            };
        }

        private static bool[,] GetShapeOfJ()
        {
            return new[,]
            {
                {true, false, false },
                {true, true, true },
                {false, false, false }
            };
        }

        private static bool[,] GetShapeOfL()
        {
            return new[,]
            {
                {false, false, true },
                {true, true, true },
                {false, false, false }
            };
        }

        private static bool[,] GetShapeOfO()
        {
            return new[,]
            {
                {false, true, true, false},
                {false, true, true, false},
                {false, false, false, false}
            };
        }

        private static bool[,] GetShapeOfS()
        {
            return new[,]
            {
                {false, true, true },
                {true, true, false },
                {false, false, false }
            };
        }

        private static bool[,] GetShapeOfT()
        {
            return new[,]
            {
                {false, true, false },
                {true, true, true },
                {false, false, false }
            };
        }

        private static bool[,] GetShapeOfZ()
        {
            return new[,]
            {
                {true, true, false },
                {false, true, true },
                {false, false, false }
            };
        }


    }
}
