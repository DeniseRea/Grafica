using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cut_Algorithms
{
    internal interface IClipper
    {
        /// <summary>
        /// Determina si una forma necesita ser recortada según ciertos límites.
        /// </summary>
        /// <param name="shape">La forma a comprobar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>True si la forma necesita ser recortada; de lo contrario, false.</returns>
        bool NeedsClipping(IShape shape, RectangleF clipBounds);

        /// <summary>
        /// Realiza el recorte de una forma según los límites especificados.
        /// </summary>
        /// <param name="shape">La forma a recortar.</param>
        /// <param name="clipBounds">Los límites para el recorte.</param>
        /// <returns>La forma resultante después del recorte.</returns>
        IShape ClipShape(IShape shape, RectangleF clipBounds);
    }
}
