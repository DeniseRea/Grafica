/**
 * Clase Circulo
 * 
 * Implementa el algoritmo del Punto Medio (una variante de Bresenham)
 * para dibujar círculos en una superficie digital.
 * Calcula un octante del círculo y usa simetría para completarlo.
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
    public class Circulo : Algoritmo
    {
        /**
         * Implementa el algoritmo del Punto Medio para círculos
         * Calcula los puntos basándose en el centro y radio proporcionados
         */
        public override void CalcularPuntos()
        {
            // Centro del círculo
            int xc = PuntoInicial.X;
            int yc = PuntoInicial.Y;
            int r = Radio;

            List<Point> points = new List<Point>();

            // Variables para el algoritmo del Punto Medio
            int x = 0;
            int y = r;
            int p = 1 - r; // Valor inicial del parámetro de decisión p

            // Agregar el punto inicial
            AgregarPuntosSimetricos(x, y, xc, yc, points);

            // Se cicla hasta trazar todo un octante
            while (x < y)
            {
                // Incrementar x
                x = x + 1;

                // Actualizar el valor de decisión
                if (p < 0)
                {
                    // Caso este del punto medio
                    p = p + 2 * x + 3;
                }
                else
                {
                    // Caso sureste del punto medio
                    y = y - 1;
                    p = p + 2 * (x - y) + 5;
                }

                // Agregar los puntos simétricos para cada paso
                AgregarPuntosSimetricos(x, y, xc, yc, points);
            }

            // Asignar los puntos calculados a la propiedad de la clase base
            Puntos = points.ToArray();
        }

        /**
         * Método auxiliar que aprovecha la simetría del círculo
         * para generar 8 puntos a partir de las coordenadas calculadas
         */
        private static void AgregarPuntosSimetricos(int x, int y, int xc, int yc, List<Point> points)
        {
            // Dibuja los 8 puntos simétricos
            points.Add(new Point(xc + x, yc + y));
            points.Add(new Point(xc - x, yc + y));
            points.Add(new Point(xc + x, yc - y));
            points.Add(new Point(xc - x, yc - y));
            points.Add(new Point(xc + y, yc + x));
            points.Add(new Point(xc - y, yc + x));
            points.Add(new Point(xc + y, yc - x));
            points.Add(new Point(xc - y, yc - x));
        }
    }
}
