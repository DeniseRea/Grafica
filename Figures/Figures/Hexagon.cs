using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Hexagon : RegularPolygon
    {
        public Hexagon(int numberOfSides, double base_, double height) : base(numberOfSides, base_, height)
        {
        }

        public Hexagon() : base()
        {
        }

   setArea((p * height) / 2);
        }

        public override void calculateApothema()
        {
            setHeight((getBase() * Math.Sqrt(3)) / 2);
        }
    }
}
