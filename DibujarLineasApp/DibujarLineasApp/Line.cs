using System;
using System.Drawing;
using System.Windows.Forms;

namespace DibujarLineasApp
{
    class Line
    {
        private int startX;
        private int startY;
        private int endX;
        private int endY;
        private Graphics grph;
        private Pen pen;

        public Line(int startX, int startY, int endX, int endY)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }

        public Line()
        {
            this.startX = 0;
            this.startY = 0;
            this.endX = 0;
            this.endY = 0;
        }

        public void DrawLine(PictureBox picCanvas)
        {
            grph = picCanvas.CreateGraphics();
            pen = new Pen(Color.Red, 3);
            grph.DrawLine(pen, startX, startY, endX, endY);
        }
    }
}