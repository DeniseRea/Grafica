using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cut_Algorithms
{
    /// <summary>
    /// Implementación del algoritmo Sutherland-Hodgman para recorte de polígonos.
    /// </summary>
    public class SutherlandHodgmanClipper : IClipper
    {
        // Enumeración para los bordes de recorte
        private enum ClipEdge
        {
            Left,
            Top,
            Right,
            Bottom
        }

        /// <summary>
        /// Determina si una forma necesita ser recortada.
        /// </summary>
        /// <param name="shape">La forma a comprobar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>True si la forma necesita ser recortada; de lo contrario, false.</returns>
        public bool NeedsClipping(IShape shape, RectangleF clipBounds)
        {
            if (shape == null)
                throw new ArgumentNullException(nameof(shape));

            // Si la forma no es un polígono, no se puede aplicar este algoritmo
            if (!(shape is Polygon))
                throw new ArgumentException("El algoritmo Sutherland-Hodgman solo puede recortar polígonos.", nameof(shape));

            Polygon polygon = (Polygon)shape;

            // Si el polígono tiene menos de 3 vértices, no es un polígono válido
            if (polygon.Vertices.Count < 3)
                return false;

            // Si todos los vértices están dentro del límite, el polígono no necesita recorte
            bool allInside = polygon.Vertices.All(v =>
                v.X >= clipBounds.Left && v.X <= clipBounds.Right &&
                v.Y >= clipBounds.Top && v.Y <= clipBounds.Bottom);

            if (allInside)
                return false;

            // Si todos los vértices están fuera del mismo borde, el polígono está completamente fuera
            bool allLeft = polygon.Vertices.All(v => v.X < clipBounds.Left);
            bool allRight = polygon.Vertices.All(v => v.X > clipBounds.Right);
            bool allTop = polygon.Vertices.All(v => v.Y < clipBounds.Top);
            bool allBottom = polygon.Vertices.All(v => v.Y > clipBounds.Bottom);

            if (allLeft || allRight || allTop || allBottom)
                return false;

            // En cualquier otro caso, el polígono necesita recorte
            return true;
        }

        /// <summary>
        /// Realiza el recorte de una forma según los límites especificados.
        /// </summary>
        /// <param name="shape">La forma a recortar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>La forma resultante después del recorte.</returns>
        public IShape ClipShape(IShape shape, RectangleF clipBounds)
        {
            if (shape == null)
                throw new ArgumentNullException(nameof(shape));

            // Si la forma no es un polígono, no se puede aplicar este algoritmo
            if (!(shape is Polygon))
                throw new ArgumentException("El algoritmo Sutherland-Hodgman solo puede recortar polígonos.", nameof(shape));

            Polygon polygon = (Polygon)shape;

            // Si el polígono tiene menos de 3 vértices, no es un polígono válido
            if (polygon.Vertices.Count < 3)
                return null;

            // Convertir los vértices del polígono a una lista de puntos
            List<Point2D> subjectPolygon = new List<Point2D>(polygon.Vertices);
            List<Point2D> outputList = new List<Point2D>(subjectPolygon);

            // Recortar contra cada borde del rectángulo en secuencia
            outputList = ClipAgainstEdge(outputList, ClipEdge.Left, clipBounds);
            if (outputList.Count == 0) return null;

            outputList = ClipAgainstEdge(outputList, ClipEdge.Top, clipBounds);
            if (outputList.Count == 0) return null;

            outputList = ClipAgainstEdge(outputList, ClipEdge.Right, clipBounds);
            if (outputList.Count == 0) return null;

            outputList = ClipAgainstEdge(outputList, ClipEdge.Bottom, clipBounds);
            if (outputList.Count == 0) return null;

            // Crear un nuevo polígono con los vértices resultantes
            return new Polygon(outputList);
        }

        /// <summary>
        /// Recorta un polígono contra uno de los bordes del rectángulo.
        /// </summary>
        /// <param name="subjectPolygon">Los vértices del polígono a recortar.</param>
        /// <param name="clipEdge">El borde contra el que recortar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>Los vértices del polígono después del recorte.</returns>
        private List<Point2D> ClipAgainstEdge(List<Point2D> subjectPolygon, ClipEdge clipEdge, RectangleF clipBounds)
        {
            List<Point2D> outputList = new List<Point2D>();

            // Si no hay vértices, devolver lista vacía
            if (subjectPolygon.Count == 0)
                return outputList;

            // El último punto del polígono
            Point2D S = subjectPolygon[subjectPolygon.Count - 1];

            for (int i = 0; i < subjectPolygon.Count; i++)
            {
                Point2D E = subjectPolygon[i]; // Punto actual

                if (IsInside(E, clipEdge, clipBounds))
                {
                    // Si el punto final está dentro pero el punto inicial no lo está,
                    // añadir el punto de intersección y el punto final
                    if (!IsInside(S, clipEdge, clipBounds))
                    {
                        outputList.Add(ComputeIntersection(S, E, clipEdge, clipBounds));
                    }

                    outputList.Add(E);
                }
                else if (IsInside(S, clipEdge, clipBounds))
                {
                    // Si el punto inicial está dentro pero el punto final no lo está,
                    // añadir solo el punto de intersección
                    outputList.Add(ComputeIntersection(S, E, clipEdge, clipBounds));
                }

                // El punto actual se convierte en el punto inicial para la siguiente iteración
                S = E;
            }

            return outputList;
        }

        /// <summary>
        /// Determina si un punto está dentro del rectángulo con respecto a un borde.
        /// </summary>
        /// <param name="point">El punto a comprobar.</param>
        /// <param name="clipEdge">El borde contra el que comprobar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>True si el punto está dentro con respecto al borde; de lo contrario, false.</returns>
        private bool IsInside(Point2D point, ClipEdge clipEdge, RectangleF clipBounds)
        {
            switch (clipEdge)
            {
                case ClipEdge.Left:
                    return point.X >= clipBounds.Left;

                case ClipEdge.Top:
                    return point.Y >= clipBounds.Top;

                case ClipEdge.Right:
                    return point.X <= clipBounds.Right;

                case ClipEdge.Bottom:
                    return point.Y <= clipBounds.Bottom;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Calcula el punto de intersección de una línea con un borde del rectángulo.
        /// </summary>
        /// <param name="p1">El primer punto de la línea.</param>
        /// <param name="p2">El segundo punto de la línea.</param>
        /// <param name="clipEdge">El borde con el que calcular la intersección.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>El punto de intersección.</returns>
        private Point2D ComputeIntersection(Point2D p1, Point2D p2, ClipEdge clipEdge, RectangleF clipBounds)
        {
            float x = 0, y = 0;
            float t;

            switch (clipEdge)
            {
                case ClipEdge.Left:
                    x = clipBounds.Left;
                    t = (x - p1.X) / (p2.X - p1.X);
                    y = p1.Y + t * (p2.Y - p1.Y);
                    break;

                case ClipEdge.Top:
                    y = clipBounds.Top;
                    t = (y - p1.Y) / (p2.Y - p1.Y);
                    x = p1.X + t * (p2.X - p1.X);
                    break;

                case ClipEdge.Right:
                    x = clipBounds.Right;
                    t = (x - p1.X) / (p2.X - p1.X);
                    y = p1.Y + t * (p2.Y - p1.Y);
                    break;

                case ClipEdge.Bottom:
                    y = clipBounds.Bottom;
                    t = (y - p1.Y) / (p2.Y - p1.Y);
                    x = p1.X + t * (p2.X - p1.X);
                    break;
            }

            return new Point2D(x, y);
        }
    }
}
