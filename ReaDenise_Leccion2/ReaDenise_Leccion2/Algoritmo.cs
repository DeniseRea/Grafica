using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaDenise_Leccion2
{
    public abstract class Algoritmo
    {
      
        public Point PuntoInicial { get; set; }
        public Point PuntoFinal { get; set; }
        public int Radio { get; set; }

        public Point[] Puntos { get; protected set; }

        public abstract void CalcularPuntos();

   
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
                        
                        int escaladoX = punto.X * escala;
                        int escaladoY = punto.Y * escala;

                        
                        int pixelX = centroX + escaladoX;
                        int pixelY = centroY - escaladoY; // Invertir Y para que crezca hacia arriba

                        
                        int x = pixelX - escala / 2;
                        int y = pixelY - escala / 2;

                        
                        if (x >= 0 && x + escala < bmp.Width &&
                            y >= 0 && y + escala < bmp.Height)
                        {
               
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
        public void clean(TextBox xStart, TextBox yStart, TextBox xFinal, TextBox yFinal, TextBox output, TextBox radius)
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
