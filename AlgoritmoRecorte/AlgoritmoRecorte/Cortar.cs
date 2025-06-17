using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AlgoritmoRecorte
{
    /// Implementa el algoritmo de recorte de líneas Cohen-Sutherland.
    /// Se determina qué segmentos de una línea son visibles dentro de una región de recorte rectangular.
    internal class Cortar
    {
        private Rectangle clipRegion;


        public Cortar(Rectangle clipRegion)
        {
            this.clipRegion = clipRegion;
        }

        
        public List<(Point start, Point end, bool isInside)> ClipLines(List<(Point start, Point end, bool isInside)> lineSegments)
        {
            var newSegments = new List<(Point start, Point end, bool isInside)>();

            foreach (var line in lineSegments)
            {
                // CASO TRIVIAL DE RECHAZO:
                // Si la línea está completamente fuera de la región de recorte, se marca como externa y se continúa
                // con la siguiente línea. Este caso se detecta usando códigos de región: si el resultado de AND
                // bit a bit de los códigos de ambos extremos no es cero, entonces la línea está completamente fuera.
                if (IsCompletelyOutside(line.start, line.end))
                {
                    newSegments.Add((line.start, line.end, false));
                    continue;
                }

                // CASO TRIVIAL DE ACEPTACIÓN:
                // Si la línea está completamente dentro de la región de recorte, se marca como interna y se continúa
                // con la siguiente línea. Esto ocurre cuando ambos puntos de la línea están contenidos en el rectángulo.
                if (IsCompletelyInside(line.start, line.end))
                {
                    newSegments.Add((line.start, line.end, true));
                    continue;
                }

                // CÁLCULO DE INTERSECCIONES (LÍNEA PARCIALMENTE VISIBLE):
                // Si la línea no está ni completamente fuera ni completamente dentro, entonces cruza el borde
                // de la región de recorte. En este caso, calculamos las intersecciones y dividimos la línea
                // en segmentos, clasificando cada uno como dentro o fuera.
                var intersections = FindIntersections(line.start, line.end);
                if (intersections.Count == 0)
                {
                    // Si no hay intersecciones pero llegamos aquí, la línea está completamente fuera
                    newSegments.Add((line.start, line.end, false));
                }
                else
                {
                    // Ordena las intersecciones por distancia desde el punto inicial
                    intersections.Sort((a, b) => Distance(line.start, a).CompareTo(Distance(line.start, b)));

                    // Se añade los puntos inicial y final de la línea a la lista de intersecciones
                    intersections.Insert(0, line.start);
                    intersections.Add(line.end);

                    // Para cada par de puntos en la lista (incluyendo los originales),
                    // creamos un segmento y determinamos si está dentro o fuera
                    for (int i = 0; i < intersections.Count - 1; i++)
                    {
                        // Calcula el punto medio para determinar si el segmento está dentro o fuera
                        Point midPoint = new Point(
                            (intersections[i].X + intersections[i + 1].X) / 2,
                            (intersections[i].Y + intersections[i + 1].Y) / 2
                        );
                        bool isInside = IsPointInside(midPoint);
                        newSegments.Add((intersections[i], intersections[i + 1], isInside));
                    }
                }
            }

            return newSegments;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        /// caso trivial de aceptación del algoritmo.
        /// Determina si una línea está completamente dentro de la región de recorte.


        private bool IsCompletelyInside(Point p1, Point p2)
        {
            return clipRegion.Contains(p1) && clipRegion.Contains(p2);
        }

        /// Encuentra todas las intersecciones de una línea con los bordes de la región de recorte.
        /// para el caso donde la línea es parcialmente visible.
        
        private List<Point> FindIntersections(Point start, Point end)
        {
            var intersections = new List<Point>();

            // Probar intersecciones con los bordes horizontales (superior e inferior)
            TryIntersectHorizontal(start, end, clipRegion.Top, intersections);
            TryIntersectHorizontal(start, end, clipRegion.Bottom, intersections);

            // Probar intersecciones con los bordes verticales (izquierdo y derecho)
            TryIntersectVertical(start, end, clipRegion.Left, intersections);
            TryIntersectVertical(start, end, clipRegion.Right, intersections);

            // Eliminar duplicados y verificar que los puntos estén realmente en la línea
            return intersections.Distinct().Where(p => IsPointOnLineSegment(start, end, p)).ToList();
        }

        /// Intenta encontrar una intersección entre la línea y una línea horizontal en la coordenada y.
        
        private void TryIntersectHorizontal(Point start, Point end, int y, List<Point> intersections)
        {
            // Si la línea es horizontal, no habrá intersección única
            if (start.Y == end.Y) return;

            // Calcular la coordenada x de la intersección usando semejanza de triángulos
            float x = start.X + (float)(end.X - start.X) * (y - start.Y) / (end.Y - start.Y);

            // Si la intersección está dentro del rango horizontal de la región, añadirla
            if (x >= clipRegion.Left && x <= clipRegion.Right)
            {
                intersections.Add(new Point((int)x, y));
            }
        }

        
        /// Intenta encontrar una intersección entre la línea y una línea vertical en la coordenada x.
        private void TryIntersectVertical(Point start, Point end, int x, List<Point> intersections)
        {
            // Si la línea es vertical, no habrá intersección única
            if (start.X == end.X) return;

            // Calcular la coordenada y de la intersección usando semejanza de triángulos
            float y = start.Y + (float)(end.Y - start.Y) * (x - start.X) / (end.X - start.X);

            // Si la intersección está dentro del rango vertical de la región, añadirla
            if (y >= clipRegion.Top && y <= clipRegion.Bottom)
            {
                intersections.Add(new Point(x, (int)y));
            }
        }


        /// Verifica si un punto está en un segmento de línea, con una tolerancia pequeña.
        private bool IsPointOnLineSegment(Point start, Point end, Point test)
        {
            double d1 = Distance(start, test);
            double d2 = Distance(test, end);
            double lineLen = Distance(start, end);
            const double EPSILON = 0.1; // Tolerancia para errores de redondeo
            return Math.Abs(d1 + d2 - lineLen) < EPSILON;
        }


        /// Determina si un punto está dentro de la región de recorte.

        private bool IsPointInside(Point p)
        {
            return clipRegion.Contains(p);
        }

        
        /// Determina si una línea está completamente fuera de la región de recorte.
        /// Este es el caso trivial de rechazo del algoritmo.
        /// El algoritmo utiliza códigos de región (outcode) para cada extremo de la línea.
        /// Si ambos extremos están en el mismo lado externo (mismo bit en los códigos),
        /// entonces la línea está completamente fuera y puede ser rechazada inmediatamente.
        private bool IsCompletelyOutside(Point p1, Point p2)
        {
            // Calcular los códigos de región para ambos puntos
            int code1 = ComputeRegionCode(p1, clipRegion.Left, clipRegion.Right, clipRegion.Top, clipRegion.Bottom);
            int code2 = ComputeRegionCode(p2, clipRegion.Left, clipRegion.Right, clipRegion.Top, clipRegion.Bottom);

            // Si el AND bit a bit de los códigos no es cero, la línea está completamente fuera
            return (code1 & code2) != 0;
        }


        /// Calcula el código de región para un punto según el algoritmo Cohen-Sutherland.
        /// El código de 4 bits indica la posición del punto respecto a la región de recorte:
        /// - Bit 0 (1): A la izquierda del rectángulo
        /// - Bit 1 (2): A la derecha del rectángulo
        /// - Bit 2 (4): Por encima del rectángulo
        /// - Bit 3 (8): Por debajo del rectángulo
        
        private int ComputeRegionCode(Point p, int xmin, int xmax, int ymin, int ymax)
        {
            int code = 0;
            if (p.X < xmin) code |= 1;      // A la izquierda
            if (p.X > xmax) code |= 2;      // A la derecha
            if (p.Y < ymin) code |= 4;      // Por encima
            if (p.Y > ymax) code |= 8;      // Por debajo
            return code;
        }
    }
}
