using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Block
    {
        public Transparency Transparency { get; set; }
        public ConsoleColor Color { get; set; }
        public bool IsFilled => Transparency != Transparency.Full;

        public Block(Transparency transparency = Transparency.None, ConsoleColor color = ConsoleColor.White)
        {
            Transparency = transparency;
            Color = color;
        }

        public string Value
        {
            get
            {
                switch (Transparency)
                {
                    case Transparency.None:
                        return $"{(char)9608}{(char)9608}";
                    case Transparency.Quarter:
                        return $"{(char)9619}{(char)9619}";
                    case Transparency.Half:
                        return $"{(char)9618}{(char)9618}";
                    case Transparency.ThreeQuarter:
                        return $"{(char)9617}{(char)9617}";
                    case Transparency.Full:
                        return "  ";
                }

                throw new InvalidOperationException("Bad block properties");
            }
        }

        public override string ToString() => Value;

        public static implicit operator string(Block block) => block.Value;
    }
}