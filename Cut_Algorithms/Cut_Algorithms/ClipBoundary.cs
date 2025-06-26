using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Cut_Algorithms
{
    /// <summary>
    /// Define la ventana de recorte rectangular.
    /// </summary>
    public class ClipBoundary : IShape
    {
        /// <summary>
        /// Límite izquierdo de la ventana de recorte.
        /// </summary>
        public float Left { get; }

        /// <summary>
        /// Límite derecho de la ventana de recorte.
        /// </summary>
        public float Right { get; }

        /// <summary>
        /// Límite superior de la ventana de recorte.
        /// </summary>
        public float Top { get; }

        /// <summary>
        /// Límite inferior de la ventana de recorte.
        /// </summary>
        public float Bottom { get; }

        /// <summary>
        /// Ancho de la ventana de recorte.
        /// </summary>
        public float Width => Right - Left;

        /// <summary>
        /// Alto de la ventana de recorte.
        /// </summary>
        public float Height => Bottom - Top;

        /// <summary>
        /// Inicializa una nueva instancia de la clase ClipBoundary.
        /// </summary>
        /// <param name="left">Límite izquierdo.</param>
        /// <param name="top">Límite superior.</param>
        /// <param name="right">Límite derecho.</param>
        /// <param name="bottom">Límite inferior.</param>
        public ClipBoundary(float left, float top, float right, float bottom)
        {
            if (right <= left) throw new ArgumentException("El límite derecho debe ser mayor que el izquierdo.");
            if (bottom <= top) throw new ArgumentException("El límite inferior debe ser mayor que el superior.");

            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ClipBoundary a partir de un RectangleF.
        /// </summary>
        /// <param name="rectangle">Rectángulo que define los límites.</param>
        public ClipBoundary(RectangleF rectangle)
            : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        /// <summary>
        /// Convierte ClipBoundary a RectangleF.
        /// </summary>
        public RectangleF ToRectangle()
        {
            return new RectangleF(Left, Top, Width, Height);
        }

        /// <summary>
        /// Dibuja el rectángulo de recorte en el contexto gráfico proporcionado.
        /// </summary>
        /// <param name="graphics">El contexto gráfico donde se dibujará la forma.</param>
        public void Draw(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));

            // Dibuja el contorno del rectángulo de recorte con línea discontinua
            using (Pen pen = new Pen(Color.Blue, 1))
            {
                pen.DashStyle = DashStyle.Dash;
                graphics.DrawRectangle(pen, Left, Top, Width, Height);
            }
        }

        /// <summary>
        /// Obtiene los puntos que definen la forma.
        /// </summary>
        /// <returns>Una colección de puntos que representan la forma.</returns>
        public IEnumerable<PointF> GetPoints()
        {
            return new[]
            {
                    new PointF(Left, Top),     // Esquina superior izquierda
                    new PointF(Right, Top),    // Esquina superior derecha
                    new PointF(Right, Bottom), // Esquina inferior derecha
                    new PointF(Left, Bottom)   // Esquina inferior izquierda
                };
        }

        /// <summary>
        /// Determina si un punto está dentro del rectángulo de recorte.
        /// </summary>
        /// <param name="point">El punto a comprobar.</param>
        /// <returns>True si el punto está dentro de la forma; de lo contrario, false.</returns>
        public bool Contains(PointF point)
        {
            return point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;
        }

        /// <summary>
        /// Convierte el ClipBoundary a un formato de cadena.
        /// </summary>
        /// <returns>Una representación en cadena de texto del ClipBoundary.</returns>
        public override string ToString()
        {
            return $"[{Left}, {Top}, {Right}, {Bottom}]";
        }
    }
}
