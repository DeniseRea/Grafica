using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    public class FiguraMalla
    {
        public static List<Point[]> GenerarMalla4x4(Point esquinaSuperiorIzquierda, int tamañoCelda)
        {
            var lineas = new List<Point[]>();

            // Líneas horizontales
            for (int i = 0; i <= 4; i++)
            {
                int y = esquinaSuperiorIzquierda.Y + i * tamañoCelda;
                lineas.Add(new Point[] {
                    new Point(esquinaSuperiorIzquierda.X, y),
                    new Point(esquinaSuperiorIzquierda.X + 4 * tamañoCelda, y)
                });
            }

            // Líneas verticales
            for (int i = 0; i <= 4; i++)
            {
                int x = esquinaSuperiorIzquierda.X + i * tamañoCelda;
                lineas.Add(new Point[] {
                    new Point(x, esquinaSuperiorIzquierda.Y),
                    new Point(x, esquinaSuperiorIzquierda.Y + 4 * tamañoCelda)
                });
            }

            // Diagonales alternadas
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        int x1 = esquinaSuperiorIzquierda.X + i * tamañoCelda;
                        int y1 = esquinaSuperiorIzquierda.Y + j * tamañoCelda;
                        int x2 = x1 + tamañoCelda;
                        int y2 = y1 + tamañoCelda;
                        lineas.Add(new Point[] { new Point(x1, y1), new Point(x2, y2) });
                    }
                    else
                    {
                        int x1 = esquinaSuperiorIzquierda.X + (i + 1) * tamañoCelda;
                        int y1 = esquinaSuperiorIzquierda.Y + j * tamañoCelda;
                        int x2 = esquinaSuperiorIzquierda.X + i * tamañoCelda;
                        int y2 = y1 + tamañoCelda;
                        lineas.Add(new Point[] { new Point(x1, y1), new Point(x2, y2) });
                    }
                }
            }

            return lineas;
        }
    }
}
