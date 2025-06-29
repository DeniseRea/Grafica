using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    internal class algoritmo
    {
        public static List<Point> DibujarLineaBresenham(Point p1, Point p2)
        {
            List<Point> puntos = new List<Point>();

            int x1 = p1.X, y1 = p1.Y;
            int x2 = p2.X, y2 = p2.Y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            int x = x1, y = y1;

            while (true)
            {
                puntos.Add(new Point(x, y));

                if (x == x2 && y == y2) break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }

            return puntos;
        }

        public static List<Point> DibujarLineaDDA(Point p1, Point p2)
        {
            List<Point> puntos = new List<Point>();

            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;

            int pasos = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xIncremento = (float)dx / pasos;
            float yIncremento = (float)dy / pasos;

            float x = p1.X;
            float y = p1.Y;

            for (int i = 0; i <= pasos; i++)
            {
                puntos.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
                x += xIncremento;
                y += yIncremento;
            }

            return puntos;
        }

        // Genera puntos para un polígono regular
        public static Point[] GenerarPoligonoRegular(Point centro, int radio, int vertices, double rotacion = 0)
        {
            Point[] puntos = new Point[vertices];
            double angulo = 2 * Math.PI / vertices;

            for (int i = 0; i < vertices; i++)
            {
                double theta = i * angulo + rotacion;
                int x = (int)(centro.X + radio * Math.Cos(theta));
                int y = (int)(centro.Y + radio * Math.Sin(theta));
                puntos[i] = new Point(x, y);
            }

            return puntos;
        }

        // Figuras específicas
        public static class Figuras
        {
            public static List<Point[]> PentagramaPentagono(Point centro, int radio)
            {
                double rotacion = -Math.PI / 2;

                var verticesPentagono = GenerarPoligonoRegular(centro, radio, 5, rotacion);
                var verticesEstrella = GenerarPoligonoRegular(centro, radio, 5, rotacion);

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

            public static List<Point[]> Malla4x4(Point esquinaSuperiorIzquierda, int tamañoCelda)
            {
                var lineas = new List<Point[]>();

                //horizontal
                for (int i = 0; i <= 4; i++)
                {
                    int y = esquinaSuperiorIzquierda.Y + i * tamañoCelda;
                    lineas.Add(new Point[] {
                        new Point(esquinaSuperiorIzquierda.X, y),
                        new Point(esquinaSuperiorIzquierda.X + 4 * tamañoCelda, y)
                    });
                }

                // vertical
                for (int i = 0; i <= 4; i++)
                {
                    int x = esquinaSuperiorIzquierda.X + i * tamañoCelda;
                    lineas.Add(new Point[] {
                        new Point(x, esquinaSuperiorIzquierda.Y),
                        new Point(x, esquinaSuperiorIzquierda.Y + 4 * tamañoCelda)
                    });
                }


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

        public static class AnalisisEuleriano
        {
            public static bool TieneCicloEuleriano(List<Point[]> lineas)
            {

                var gradoVertices = new Dictionary<Point, int>();

                foreach (var linea in lineas)
                {
                    if (!gradoVertices.ContainsKey(linea[0])) gradoVertices[linea[0]] = 0;
                    if (!gradoVertices.ContainsKey(linea[1])) gradoVertices[linea[1]] = 0;

                    gradoVertices[linea[0]]++;
                    gradoVertices[linea[1]]++;
                }


                return gradoVertices.Values.All(grado => grado % 2 == 0);
            }

            public static bool TieneSenderoEuleriano(List<Point[]> lineas)
            {
                var gradoVertices = new Dictionary<Point, int>();

                foreach (var linea in lineas)
                {
                    if (!gradoVertices.ContainsKey(linea[0])) gradoVertices[linea[0]] = 0;
                    if (!gradoVertices.ContainsKey(linea[1])) gradoVertices[linea[1]] = 0;

                    gradoVertices[linea[0]]++;
                    gradoVertices[linea[1]]++;
                }

                int verticesGradoImpar = gradoVertices.Values.Count(grado => grado % 2 == 1);


                return verticesGradoImpar == 0 || verticesGradoImpar == 2;
            }
        }
    }
}
