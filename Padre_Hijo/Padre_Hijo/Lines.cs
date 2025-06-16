using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Padre_Hijo
{
    public class Lines
    {
        private int numberOfLines;
        private PictureBox pictureBox;

        /// <summary>
        /// Constructor para la clase Lines
        /// </summary>
        /// <param name="numberOfLines">Cantidad de líneas para el patrón</param>
        /// <param name="pictureBox">PictureBox donde se dibujará</param>
        public Lines(int numberOfLines, PictureBox pictureBox)
        {
            this.numberOfLines = numberOfLines;
            this.pictureBox = pictureBox;

            // Configurar el PictureBox para el dibujo
            pictureBox.Paint += PictureBox_Paint;
        }

        /// <summary>
        /// Maneja el evento Paint del PictureBox
        /// </summary>
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawStringArt(g, numberOfLines);
        }

        /// <summary>
        /// Dibuja el patrón de string art con el número especificado de líneas
        /// </summary>
        /// <param name="lines">Número de líneas a utilizar</param>
        public void DrawStringArt(int lines)
        {
            // Forzar la actualización del PictureBox para activar el evento Paint
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Método principal que dibuja el patrón de string art
        /// </summary>
        /// <param name="g">Objeto Graphics donde dibujar</param>
        /// <param name="lines">Número de líneas a utilizar</param>
        private void DrawStringArt(Graphics g, int lines)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;

            // Usar todo el espacio disponible
            int size = Math.Min(width, height);

            // Centrar el cuadrado en el PictureBox
            int xOffset = (width - size) / 2;
            int yOffset = (height - size) / 2;

            // Espacio entre líneas
            float xSpacing = (float)size / lines;
            float ySpacing = (float)size / lines;

            // Crear puntos en los cuatro lados del cuadrado
            Point[] topPoints = new Point[lines + 1];    // Puntos en el borde superior
            Point[] rightPoints = new Point[lines + 1];  // Puntos en el borde derecho
            Point[] bottomPoints = new Point[lines + 1]; // Puntos en el borde inferior
            Point[] leftPoints = new Point[lines + 1];   // Puntos en el borde izquierdo

            // Crear puntos equidistantes en cada lado del cuadrado
            for (int i = 0; i <= lines; i++)
            {
                // Puntos en el borde superior (de izquierda a derecha)
                topPoints[i] = new Point(
                    xOffset + (int)(i * xSpacing),
                    yOffset);

                // Puntos en el borde derecho (de arriba a abajo)
                rightPoints[i] = new Point(
                    xOffset + size,
                    yOffset + (int)(i * ySpacing));

                // Puntos en el borde inferior (de izquierda a derecha)
                bottomPoints[i] = new Point(
                    xOffset + (int)(i * xSpacing),
                    yOffset + size);

                // Puntos en el borde izquierdo (de abajo a arriba)
                leftPoints[i] = new Point(
                    xOffset,
                    yOffset + size - (int)(i * ySpacing));
            }

            // Definir colores semitransparentes para los diferentes patrones
            Color redColor = Color.FromArgb(150, 255, 0, 0);   // Rojo
            Color greenColor = Color.FromArgb(150, 0, 180, 0); // Verde
            Color blueColor = Color.FromArgb(150, 0, 0, 255);  // Azul

            // Dibujar patrón rojo: Superior a Inferior
            using (Pen redPen = new Pen(redColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(redPen, topPoints[i], bottomPoints[lines - i]);
                }
            }

            // Dibujar patrón verde: Izquierdo a Derecho
            using (Pen greenPen = new Pen(greenColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(greenPen, leftPoints[i], rightPoints[lines - i]);
                }
            }

            // Dibujar patrón azul: Superior a Derecho y Inferior a Izquierdo
            using (Pen bluePen = new Pen(blueColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(bluePen, topPoints[i], rightPoints[i]);
                    g.DrawLine(bluePen, bottomPoints[i], leftPoints[i]);
                }
            }

            // Dibujar el borde del cuadrado
            using (Pen borderPen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(borderPen, xOffset, yOffset, size, size);
            }
        }

        /// <summary>
        /// Dibuja un patrón string art simplificado de un solo color (azul)
        /// </summary>
        public void DrawSimpleStringArt(int lines)
        {
            pictureBox.Paint -= PictureBox_Paint;
            pictureBox.Paint += (sender, e) => {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int width = pictureBox.Width;
                int height = pictureBox.Height;

                // Usar todo el espacio disponible
                int size = Math.Min(width, height);

                // Centrar el cuadrado en el PictureBox
                int xOffset = (width - size) / 2;
                int yOffset = (height - size) / 2;

                // Espacio entre líneas
                float xSpacing = (float)size / lines;
                float ySpacing = (float)size / lines;

                // Crear puntos en los dos lados del cuadrado
                Point[] bottomPoints = new Point[lines + 1]; // Puntos en el borde inferior
                Point[] leftPoints = new Point[lines + 1];   // Puntos en el borde izquierdo

                // Crear puntos equidistantes en cada lado del cuadrado
                for (int i = 0; i <= lines; i++)
                {
                    // Puntos en el borde inferior (de izquierda a derecha)
                    bottomPoints[i] = new Point(
                        xOffset + (int)(i * xSpacing),
                        yOffset + size);

                    // Puntos en el borde izquierdo (de abajo a arriba)
                    leftPoints[i] = new Point(
                        xOffset,
                        yOffset + size - (int)(i * ySpacing));
                }

                // Color azul semitransparente para todas las líneas
                Color lineColor = Color.FromArgb(150, 0, 0, 255); // Azul semitransparente

                // Dibujar líneas cruzando del borde inferior al borde izquierdo
                using (Pen pen = new Pen(lineColor, 1))
                {
                    for (int i = 0; i <= lines; i++)
                    {
                        g.DrawLine(pen, bottomPoints[i], leftPoints[lines - i]);
                    }
                }

                // Dibujar el borde del cuadrado
                using (Pen borderPen = new Pen(Color.Black, 2))
                {
                    g.DrawRectangle(borderPen, xOffset, yOffset, size, size);
                }
            };
            pictureBox.Invalidate();
        }
    }
}