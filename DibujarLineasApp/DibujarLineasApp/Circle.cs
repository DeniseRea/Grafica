using System;
using System.Drawing;
using System.Windows.Forms;

namespace DibujarLineasApp
{
    class Circle
    {
        int x;
        int y;
        int radius = 3;
        private Graphics grph;
        private Pen pen;
        public Circle(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Circle()
        {
            this.x = 0;
            this.y = 0;
        }

        public void DrawCircle(PictureBox picCanvas)
        {
            grph = picCanvas.CreateGraphics();
            pen = new Pen(Color.Blue, 3);

            // Dibuja el círculo
            grph.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
        }

        public void ClearCircle(PictureBox picCanvas)
        {
            // Borra el círculo dibujado
            picCanvas.Refresh();
        }
    }
}
