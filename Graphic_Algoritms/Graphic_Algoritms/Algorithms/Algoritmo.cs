/**
 * Clase abstracta Algoritmo
 * 
 * Define la estructura base para los algoritmos de dibujo gráfico.
 * Proporciona funcionalidad común como el manejo de puntos, transformación
 * de coordenadas y dibujo en un PictureBox.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public abstract class Algoritmo
    {
        /**
         * Punto inicial para el algoritmo (o centro en caso del círculo)
         */
        public Point PuntoInicial { get; set; }
        
        /**
         * Punto final para algoritmos de línea
         */
        public Point PuntoFinal { get; set; }
        
        /**
         * Radio para el algoritmo de círculo
         */
        public int Radio { get; set; }
        
        /**
         * Array de puntos calculados por el algoritmo
         */
        public Point[] Puntos { get; protected set; }

        /**
         * Método abstracto que cada algoritmo debe implementar
         * para calcular los puntos según su propio procedimiento
         */
        public abstract void CalcularPuntos();

        /**
         * Dibuja los puntos calculados en un PictureBox
         * Aplica transformaciones para que el centro sea el origen (0,0)
         */
        public virtual void Dibujar(PictureBox pictureBox, Color color, int escala = 10)
        {
            if (Puntos == null || pictureBox == null) return;

            Bitmap bmp = pictureBox.Image as Bitmap ?? new Bitmap(pictureBox.Width, pictureBox.Height);

            //centro
            int centroX = bmp.Width / 2;
            int centroY = bmp.Height / 2;

            using (Graphics g = Graphics.FromImage(bmp))
            {

                g.Clear(Color.White);

                //ejes 
                using (Pen ejesPen = new Pen(Color.LightGray, 1))
                {
                    g.DrawLine(ejesPen, 0, centroY, bmp.Width, centroY);
                    g.DrawLine(ejesPen, centroX, 0, centroX, bmp.Height);
                }

                using (SolidBrush brush = new SolidBrush(color))
                {
                    foreach (var punto in Puntos)
                    {
                        // Aplicar escala a las coordenadas
                        int escaladoX = punto.X * escala;
                        int escaladoY = punto.Y * escala;

                        // Transformar las coordenadas para que (0,0) sea el centro
                        int pixelX = centroX + escaladoX;
                        int pixelY = centroY - escaladoY; // Invertir Y para que crezca hacia arriba

                        // Calcular la posición del cuadrado centrado en el punto
                        int x = pixelX - escala / 2;
                        int y = pixelY - escala / 2;

                        // Verificar que el cuadrado esté dentro de los límites del bitmap
                        if (x >= 0 && x + escala < bmp.Width &&
                            y >= 0 && y + escala < bmp.Height)
                        {
                            // Dibujar un cuadrado en lugar de un solo píxel
                            g.FillRectangle(brush, x, y, escala, escala);
                        }
                    }
                }
            }

            pictureBox.Image = bmp;
            pictureBox.Refresh();
        }

        /**
         * Limpia el área de dibujo
         */
        public void clear(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
            pictureBox.Refresh();
        }

        /**
         * Limpia los campos de texto de la interfaz
         */
        public void clean(TextBox xStart, TextBox yStart,TextBox xFinal, TextBox yFinal, TextBox output, TextBox radius)
        {
            xStart.Text = string.Empty;
            yStart.Text = string.Empty;
            xFinal.Text = string.Empty;
            yFinal.Text = string.Empty;
            output.Text = string.Empty;
            radius.Text = string.Empty;
        }
    }
}
