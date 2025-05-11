using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Eneagono : RegularPolygon
    {
        public Eneagono(int numberOfSides, double base_, double height) : base(numberOfSides, base_, height)
        {

        }

        public Eneagono() : base()
        {
        }

        public override void CalculateArea()
        {
            // Fórmula del área de un eneágono regular
            CalculatePerimeter();
            calculateApothema();
            double p = getPerimeter();
            setArea((p * height) / 2);
        }

        public override void calculateApothema()
        {
            setHeight(getRadius() * (Math.PI / 180) * (180.0 / 9));
        }
    }
}
