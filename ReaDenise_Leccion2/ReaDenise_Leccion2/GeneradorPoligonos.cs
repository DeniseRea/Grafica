using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    public class GeneradorPoligonos
    {
        public static Point[] GenerarPoligonoRegular(Point centro, int radio, int vertices, double rotacion = 0)
        {
            Point[] puntos = new Point[vertices];
            double angulo = 2 * Math.PI / vertices;

            for (int i = 0; i < vertices; i++)
            {
                double theta = i * angulo + rotacion;
                int x = (int)(centro.X + radio * Math.Cos(theta));
                int y = (int)(centro.Y + radio * Math.Sin(theta));
                puntos[i] = new Point(x, y);
            }

            return puntos;
        }
    }
}
