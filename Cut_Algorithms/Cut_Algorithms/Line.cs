using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cut_Algorithms
{
    /// <summary>
    /// Representa una línea entre dos puntos.
    /// </summary>
    public class Line : IShape
    {
        /// <summary>
        /// Punto inicial de la línea.
        /// </summary>
        public Point2D Start { get; set; }

        /// <summary>
        /// Punto final de la línea.
        /// </summary>
        public Point2D End { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Line.
        /// </summary>
        /// <param name="start">Punto inicial de la línea.</param>
        /// <param name="end">Punto final de la línea.</param>
        public Line(Point2D start, Point2D end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Line.
        /// </summary>
        /// <param name="x1">Coordenada X del punto inicial.</param>
        /// <param name="y1">Coordenada Y del punto inicial.</param>
        /// <param name="x2">Coordenada X del punto final.</param>
        /// <param name="y2">Coordenada Y del punto final.</param>
        public Line(float x1, float y1, float x2, float y2)
            : this(new Point2D(x1, y1), new Point2D(x2, y2))
        {
        }

        /// <summary>
        /// Dibuja la línea en el contexto gráfico proporcionado.
        /// </summary>
        public void Draw(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));

            // Dibuja una línea entre los puntos de inicio y fin
            using (Pen pen = new Pen(Color.Black, 1))
            {
                graphics.DrawLine(pen, Start.X, Start.Y, End.X, End.Y);
            }
        }

        /// <summary>
        /// Obtiene los puntos que definen la forma.
        /// </summary>
        public IEnumerable<PointF> GetPoints()
        {
            return new[] { new PointF(Start.X, Start.Y), new PointF(End.X, End.Y) };
        }

        /// <summary>
        /// Determina si un punto está dentro de esta forma.
        /// </summary>
        public bool Contains(PointF point)
        {
            // Para líneas, consideramos que un punto está en la línea si
            // está lo suficientemente cerca de la línea
            const float epsilon = 3.0f; // Tolerancia en píxeles

            // Calcular la distancia del punto a la línea usando la fórmula
            // distancia = ||(p - a) - ((p - a)·(b - a)/(b - a)·(b - a))(b - a)||
            float ax = Start.X;
            float ay = Start.Y;
            float bx = End.X;
            float by = End.Y;
            float px = point.X;
            float py = point.Y;

            // Vector AB (línea)
            float abx = bx - ax;
            float aby = by - ay;

            // Vector AP (punto a la línea)
            float apx = px - ax;
            float apy = py - ay;

            // Producto escalar AB·AB
            float abDotAb = abx * abx + aby * aby;

            // Evitar división por cero
            if (abDotAb < float.Epsilon)
                return false;

            // Producto escalar AP·AB
            float apDotAb = apx * abx + apy * aby;

            // El factor t para el punto más cercano
            float t = apDotAb / abDotAb;

            // Si t está fuera del rango [0,1], el punto está más cerca de un extremo
            if (t < 0 || t > 1)
                return false;

            // El punto más cercano en la línea
            float qx = ax + t * abx;
            float qy = ay + t * aby;

            // Distancia entre el punto y el punto más cercano en la línea
            float dx = px - qx;
            float dy = py - qy;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            return distance <= epsilon;
        }
    }
}
