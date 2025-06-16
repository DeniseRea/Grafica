/**
 * Clase DDA 
 * 
 * Implementa el algoritmo DDA para dibujar líneas digitales.
 * Utiliza cálculos de punto flotante para determinar los puntos
 * que conforman una línea entre dos coordenadas.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AlgoritmosGraficar
{

    public class DDA : Algoritmo
    {
        /**
         * Implementa el algoritmo DDA para calcular los puntos de una línea
         * El algoritmo usa una aproximación de punto flotante para determinar
         * las posiciones de píxeles en la pantalla
         */
        public override void CalcularPuntos()
        {
            // Obtener las coordenadas de los puntos inicial y final
            int xi = PuntoInicial.X;
            int yi = PuntoInicial.Y;
            int xf = PuntoFinal.X;
            int yf = PuntoFinal.Y;

            List<Point> points = new List<Point>();

            // 1. Cálculo de la diferencia entre puntos
            float dx = xf - xi;
            float dy = yf - yi;

            // 2. Cálculo de los K pasos (siempre en valor absoluto)
            float steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            // Asegurar que steps no sea cero
            if (steps == 0)
            {
                Puntos = new Point[] { new Point(xi, yi) };
                return;
            }

            // 3. Cálculo del incremento en cada dirección
            float xInc = dx / steps;
            float yInc = dy / steps;

            // 4. Cálculo de cada punto
            float x = xi;
            float y = yi;

            // Añadir el punto inicial
            points.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));

            // Calcular los puntos intermedios
            for (int k = 0; k < steps; k++)
            {
                x += xInc;
                y += yInc;
                points.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
            }

            // Asignar los puntos calculados a la propiedad de la clase base
            Puntos = points.ToArray();
        }
    }
}
