using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Octagon : RegularPolygon
    {
        public Octagon(int numberOfSides, double base_, double height) : base(numberOfSides, base_, height)
        {

        }

        public Octagon() : base()
        {
        }

        public override void CalculateArea()
        {
            // Fórmula del área de un octágono regular
            CalculatePerimeter();
            calculateApothema();
            double p = getPerimeter();
            setArea((p * height) / 2);
        }

        public override void calculateApothema()
        {
            setHeight(2 * getRadius() * Math.Sin(Math.PI / 8));
        }
    }
}
