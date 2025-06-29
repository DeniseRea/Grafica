using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    public class FiguraPentagramaPentagono
    {
        public static List<Point[]> GenerarPentagramaPentagono(Point centro, int radio)
        {
            double rotacion = -Math.PI / 2;

            var verticesPentagono = GeneradorPoligonos.GenerarPoligonoRegular(centro, radio, 5, rotacion);
            var verticesEstrella = GeneradorPoligonos.GenerarPoligonoRegular(centro, radio, 5, rotacion);

            var lineas = new List<Point[]>();

            // Pentágono - exterior
            for (int i = 0; i < 5; i++)
            {
                lineas.Add(new Point[] { verticesPentagono[i], verticesPentagono[(i + 1) % 5] });
            }

            lineas.Add(new Point[] { verticesPentagono[0], verticesEstrella[0] });

            // Pentagrama (estrella) - rotada respecto al pentágono
            for (int i = 0; i < 5; i++)
            {
                int siguiente = (i + 2) % 5;
                lineas.Add(new Point[] { verticesEstrella[i], verticesEstrella[siguiente] });
            }

            return lineas;
        }
    }
}
