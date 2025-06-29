using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    public class AlgoritmoBresenham : Algoritmo
    {
        public override void CalcularPuntos()
        {
            List<Point> puntos = DibujarLineaBresenham(PuntoInicial, PuntoFinal);
            Puntos = puntos.ToArray();
        }

        public static List<Point> DibujarLineaBresenham(Point p1, Point p2)
        {
            List<Point> puntos = new List<Point>();

            int x1 = p1.X, y1 = p1.Y;
            int x2 = p2.X, y2 = p2.Y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            int x = x1, y = y1;

            while (true)
            {
                puntos.Add(new Point(x, y));

                if (x == x2 && y == y2) break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }

            return puntos;

        }
    }

}
