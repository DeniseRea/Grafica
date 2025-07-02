/**
 * Clase Bresenham
 * 
 * Implementa el algoritmo de Bresenham para dibujar líneas digitales.
 * Utiliza únicamente operaciones con enteros, lo que lo hace más
 * eficiente que otros algoritmos como el DDA.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmosGraficar
{
    public class Bresenham : Algoritmo
    {
        /**
         * Implementa el algoritmo de Bresenham para calcular los puntos de una línea
         * El algoritmo usa sólo operaciones enteras y maneja casos para diferentes pendientes
         */
        public override void CalcularPuntos()
        {
            int x1 = PuntoInicial.X;
            int y1 = PuntoInicial.Y;
            int x2 = PuntoFinal.X;
            int y2 = PuntoFinal.Y;

            List<Point> points = new List<Point>();

            // Paso 1: Calcular las constantes
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            // Paso 2: Evaluar la pendiente y definir caso
            bool isSteep = dy > dx;
            int p0;
            if (isSteep)
            {
                // Caso 2: Pendiente > 1
                p0 = 2 * dx - dy;
            }
            else
            {
                // Caso 1: 0 < Pendiente < 1
                p0 = 2 * dy - dx;
            }

            // Paso 3: Determinar valores iniciales de decisión
            int p = p0;

            // Paso 4: Calcular puntos intermedios
            while (true)
            {
                points.Add(new Point(x1, y1));

                if (x1 == x2 && y1 == y2)
                    break;

                // Calcular los puntos basados en la decisión
                if (p < 0)
                {
                    if (isSteep)
                    {
                        y1 += sy;
                    }
                    else
                    {
                        x1 += sx;
                    }
                    p += 2 * (isSteep ? dx : dy);
                }
                else
                {
                    // Incremento en ambos ejes
                    x1 += sx;
                    y1 += sy;
                    p += 2 * (isSteep ? dx : dy) - 2 * (isSteep ? dy : dx);
                }
            }

            Puntos = points.ToArray();
        }
    }
}
