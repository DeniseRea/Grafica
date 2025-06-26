using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cut_Algorithms
{
    /// <summary>
    /// Representa un punto en un espacio bidimensional.
    /// </summary>
    public class Point2D : IShape
    {
        /// <summary>
        /// Coordenada X del punto.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Coordenada Y del punto.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Radio para dibujar el punto (representado como un círculo pequeño).
        /// </summary>
        public float Radius { get; set; } = 2.0f;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Point2D.
        /// </summary>
        /// <param name="x">Coordenada X del punto.</param>
        /// <param name="y">Coordenada Y del punto.</param>
        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Convierte un Point2D a PointF.
        /// </summary>
        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        /// <summary>
        /// Crea un Point2D a partir de un PointF.
        /// </summary>
        public static Point2D FromPointF(PointF point)
        {
            return new Point2D(point.X, point.Y);
        }

        /// <summary>
        /// Dibuja el punto en el contexto gráfico proporcionado.
        /// </summary>
        public void Draw(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));

            // Dibuja un punto como un círculo pequeño
            graphics.FillEllipse(Brushes.Black, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        }

        /// <summary>
        /// Obtiene los puntos que definen la forma.
        /// </summary>
        public IEnumerable<PointF> GetPoints()
        {
            return new[] { new PointF(X, Y) };
        }

        /// <summary>
        /// Determina si un punto está dentro de esta forma.
        /// </summary>
        public bool Contains(PointF point)
        {
            // Calcula la distancia entre este punto y el punto dado
            float dx = point.X - X;
            float dy = point.Y - Y;
            float distanceSquared = dx * dx + dy * dy;

            // Considera que el punto está contenido si está dentro del radio
            return distanceSquared <= Radius * Radius;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
