using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cut_Algorithms
{
    /// <summary>
    /// Representa un polígono mediante una colección de vértices.
    /// </summary>
    public class Polygon : IShape
    {
        /// <summary>
        /// Lista de vértices que forman el polígono.
        /// </summary>
        public List<Point2D> Vertices { get; } = new List<Point2D>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase Polygon.
        /// </summary>
        public Polygon()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Polygon con los vértices especificados.
        /// </summary>
        /// <param name="vertices">Lista de vértices que definen el polígono.</param>
        public Polygon(IEnumerable<Point2D> vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            Vertices.AddRange(vertices);
        }

        /// <summary>
        /// Añade un vértice al polígono.
        /// </summary>
        /// <param name="vertex">El vértice a añadir.</param>
        public void AddVertex(Point2D vertex)
        {
            if (vertex == null) throw new ArgumentNullException(nameof(vertex));
            Vertices.Add(vertex);
        }

        /// <summary>
        /// Añade un vértice al polígono con las coordenadas especificadas.
        /// </summary>
        /// <param name="x">Coordenada X del vértice.</param>
        /// <param name="y">Coordenada Y del vértice.</param>
        public void AddVertex(float x, float y)
        {
            AddVertex(new Point2D(x, y));
        }

        /// <summary>
        /// Dibuja el polígono en el contexto gráfico proporcionado.
        /// </summary>
        public void Draw(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (Vertices.Count < 3) return; // No se puede dibujar un polígono con menos de 3 vértices

            // Convertir los vértices a un array de puntos
            PointF[] points = Vertices.Select(v => new PointF(v.X, v.Y)).ToArray();

            // Dibujar el perímetro del polígono
            using (Pen pen = new Pen(Color.Black, 1))
            {
                graphics.DrawPolygon(pen, points);
            }
        }

        /// <summary>
        /// Obtiene los puntos que definen la forma.
        /// </summary>
        public IEnumerable<PointF> GetPoints()
        {
            return Vertices.Select(v => new PointF(v.X, v.Y));
        }

        /// <summary>
        /// Determina si un punto está dentro de esta forma utilizando el algoritmo de Ray Casting.
        /// </summary>
        public bool Contains(PointF point)
        {
            if (Vertices.Count < 3)
                return false;

            bool inside = false;
            int count = Vertices.Count;

            for (int i = 0, j = count - 1; i < count; j = i++)
            {
                // Comprueba si el punto está dentro del polígono usando el algoritmo de Ray Casting
                if (((Vertices[i].Y > point.Y) != (Vertices[j].Y > point.Y)) &&
                    (point.X < (Vertices[j].X - Vertices[i].X) * (point.Y - Vertices[i].Y) /
                               (Vertices[j].Y - Vertices[i].Y) + Vertices[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
