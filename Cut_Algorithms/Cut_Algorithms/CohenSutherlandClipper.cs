using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cut_Algorithms
{
    /// <summary>
    /// Implementación del algoritmo Cohen-Sutherland para recorte de líneas.
    /// </summary>
    public class CohenSutherlandClipper : IClipper
    {
        // Definición de códigos de región para el algoritmo Cohen-Sutherland
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

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

            // Si la forma no es una línea, no se puede aplicar este algoritmo
            if (!(shape is Line))
                throw new ArgumentException("El algoritmo Cohen-Sutherland solo puede recortar líneas.", nameof(shape));

            Line line = (Line)shape;

            // Obtener los códigos de región para ambos extremos de la línea
            int code1 = ComputeOutCode(line.Start.X, line.Start.Y, clipBounds);
            int code2 = ComputeOutCode(line.End.X, line.End.Y, clipBounds);

            // Si ambos extremos están dentro del límite, la línea no necesita recorte
            if (code1 == INSIDE && code2 == INSIDE)
                return false;

            // Si los códigos de región comparten al menos un bit 1, la línea está completamente fuera
            if ((code1 & code2) != 0)
                return false;

            // En cualquier otro caso, la línea necesita recorte
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

            // Si la forma no es una línea, no se puede aplicar este algoritmo
            if (!(shape is Line))
                throw new ArgumentException("El algoritmo Cohen-Sutherland solo puede recortar líneas.", nameof(shape));

            Line line = (Line)shape;

            // Copiar las coordenadas para modificarlas durante el algoritmo
            float x1 = line.Start.X;
            float y1 = line.Start.Y;
            float x2 = line.End.X;
            float y2 = line.End.Y;

            float xmin = clipBounds.Left;
            float ymin = clipBounds.Top;
            float xmax = clipBounds.Right;
            float ymax = clipBounds.Bottom;

            // Obtener los códigos de región iniciales
            int code1 = ComputeOutCode(x1, y1, clipBounds);
            int code2 = ComputeOutCode(x2, y2, clipBounds);

            bool accept = false;

            while (true)
            {
                // Si ambos extremos están dentro del límite, aceptar la línea
                if ((code1 | code2) == 0)
                {
                    accept = true;
                    break;
                }
                // Si los códigos de región comparten al menos un bit 1, la línea está completamente fuera
                else if ((code1 & code2) != 0)
                {
                    break;
                }
                // En caso contrario, la línea necesita ser recortada
                else
                {
                    // Seleccionar un punto fuera del rectángulo
                    int codeOut = code1 != 0 ? code1 : code2;

                    float x = 0, y = 0;

                    // Encontrar el punto de intersección con el borde correspondiente
                    if ((codeOut & TOP) != 0) // Punto está por encima del límite
                    {
                        x = x1 + (x2 - x1) * (ymin - y1) / (y2 - y1);
                        y = ymin;
                    }
                    else if ((codeOut & BOTTOM) != 0) // Punto está por debajo del límite
                    {
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                        y = ymax;
                    }
                    else if ((codeOut & RIGHT) != 0) // Punto está a la derecha del límite
                    {
                        y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                        x = xmax;
                    }
                    else if ((codeOut & LEFT) != 0) // Punto está a la izquierda del límite
                    {
                        y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                        x = xmin;
                    }

                    // Reemplazar el punto y actualizar el código
                    if (codeOut == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = ComputeOutCode(x1, y1, clipBounds);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = ComputeOutCode(x2, y2, clipBounds);
                    }
                }
            }

            // Si la línea fue aceptada, devolver la línea recortada
            if (accept)
            {
                return new Line(new Point2D(x1, y1), new Point2D(x2, y2));
            }

            // En caso contrario, la línea está completamente fuera
            return null;
        }

        /// <summary>
        /// Calcula el código de región para un punto según el algoritmo Cohen-Sutherland.
        /// </summary>
        /// <param name="x">Coordenada X del punto.</param>
        /// <param name="y">Coordenada Y del punto.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>El código de región para el punto.</returns>
        private int ComputeOutCode(float x, float y, RectangleF clipBounds)
        {
            int code = INSIDE;

            if (x < clipBounds.Left)
                code |= LEFT;
            else if (x > clipBounds.Right)
                code |= RIGHT;

            if (y < clipBounds.Top)
                code |= TOP;
            else if (y > clipBounds.Bottom)
                code |= BOTTOM;

            return code;
        }
    }
}
