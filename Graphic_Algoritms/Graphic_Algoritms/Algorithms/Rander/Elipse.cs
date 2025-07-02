using System;
using System.Collections.Generic;
using System.Drawing;

namespace AlgoritmosGraficar
{
    public class Elipse : Algoritmo
    {
        // Semiejes de la elipse
        public int Rx { get; set; }
        public int Ry { get; set; }

        // Si quieres usar PuntoInicial como centro y PuntoFinal para calcular los semiejes:
        public override void CalcularPuntos()
        {
            int xc = PuntoInicial.X;
            int yc = PuntoInicial.Y;

            // Si PuntoFinal está definido, calcula los semiejes
            if (PuntoFinal != null)
            {
                Rx = Math.Abs(PuntoFinal.X - xc);
                Ry = Math.Abs(PuntoFinal.Y - yc);
            }

            List<Point> points = new List<Point>();

            int x = 0;
            int y = Ry;

            // Región 1
            double Rx2 = Rx * Rx;
            double Ry2 = Ry * Ry;
            double p1 = Ry2 - (Rx2 * Ry) + (0.25 * Rx2);
            double dx = 2 * Ry2 * x;
            double dy = 2 * Rx2 * y;

            while (dx < dy)
            {
                AgregarPuntosSimetricos(xc, yc, x, y, points);
                if (p1 < 0)
                {
                    x++;
                    dx = dx + (2 * Ry2);
                    p1 = p1 + dx + Ry2;
                }
                else
                {
                    x++;
                    y--;
                    dx = dx + (2 * Ry2);
                    dy = dy - (2 * Rx2);
                    p1 = p1 + dx - dy + Ry2;
                }
            }

            // Región 2
            double p2 = (Ry2) * ((x + 0.5) * (x + 0.5)) + (Rx2) * ((y - 1) * (y - 1)) - (Rx2 * Ry2);
            while (y >= 0)
            {
                AgregarPuntosSimetricos(xc, yc, x, y, points);
                if (p2 > 0)
                {
                    y--;
                    dy = dy - (2 * Rx2);
                    p2 = p2 + Rx2 - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * Ry2);
                    dy = dy - (2 * Rx2);
                    p2 = p2 + dx - dy + Rx2;
                }
            }

            Puntos = points.ToArray();
        }

        private void AgregarPuntosSimetricos(int xc, int yc, int x, int y, List<Point> points)
        {
            points.Add(new Point(xc + x, yc + y));
            points.Add(new Point(xc - x, yc + y));
            points.Add(new Point(xc + x, yc - y));
            points.Add(new Point(xc - x, yc - y));
        }
    }
}
