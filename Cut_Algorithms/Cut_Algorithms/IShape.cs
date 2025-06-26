using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cut_Algorithms
{
    public interface IShape
    {
        /// <summary>
        /// Dibuja la forma en el contexto gráfico proporcionado.
        /// </summary>
        /// <param name="graphics">El contexto gráfico donde se dibujará la forma.</param>
        void Draw(Graphics graphics);

        /// <summary>
        /// Obtiene los puntos que definen la forma.
        /// </summary>
        /// <returns>Una colección de puntos que representan la forma.</returns>
        IEnumerable<PointF> GetPoints();

        /// <summary>
        /// Determina si un punto está dentro de la forma.
        /// </summary>
        /// <param name="point">El punto a comprobar.</param>
        /// <returns>True si el punto está dentro de la forma; de lo contrario, false.</returns>
        bool Contains(PointF point);
    }
}
