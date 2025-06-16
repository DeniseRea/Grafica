using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrmRegularFigures
{
    class FloodFill
    {
        private Color fillColor;
        private Color borderColor;
        private Bitmap bitmap;
        private Graphics graphics;

        public FloodFill(Color fillColor, Color borderColor)
        {
            this.fillColor = fillColor;
            this.borderColor = borderColor;
        }

        public void Fill(PictureBox pictureBox, Point clickPoint)
        {
            if (pictureBox.Image == null)
                return;

            Bitmap originalBitmap = (Bitmap)pictureBox.Image;
            
            using (bitmap = new Bitmap(originalBitmap))
            using (graphics = Graphics.FromImage(bitmap))
            {
                Color targetColor = bitmap.GetPixel(clickPoint.X, clickPoint.Y);
                
                if (targetColor.ToArgb() == fillColor.ToArgb())
                    return;

                FloodFillIterative(clickPoint.X, clickPoint.Y, targetColor, fillColor);
                pictureBox.Image = new Bitmap(bitmap); // Crear copia para el PictureBox
            } // Aquí se liberan automáticamente bitmap y graphics
        }

        private void FloodFillIterative(int x, int y, Color targetColor, Color replacementColor)
        {
            if (targetColor.ToArgb() == replacementColor.ToArgb())
                return;

            Stack<Point> stack = new Stack<Point>();
            
            try
            {
                stack.Push(new Point(x, y));

                while (stack.Count > 0)
                {
                    Point point = stack.Pop();
                    int px = point.X;
                    int py = point.Y;

                    if (px < 0 || px >= bitmap.Width || py < 0 || py >= bitmap.Height)
                        continue;

                    Color currentColor = bitmap.GetPixel(px, py);
                    if (currentColor.ToArgb() != targetColor.ToArgb())
                        continue;

                    bitmap.SetPixel(px, py, replacementColor);

                    // Agregar píxeles adyacentes al stack
                    stack.Push(new Point(px + 1, py));
                    stack.Push(new Point(px - 1, py));
                    stack.Push(new Point(px, py + 1));
                    stack.Push(new Point(px, py - 1));
                }
            }
            finally
            {
                // Liberar explícitamente el stack
                stack.Clear();
                stack = null;
                
                // Forzar recolección de basura si el stack era muy grande
                if (GC.GetTotalMemory(false) > 50 * 1024 * 1024) // Si hay más de 50MB en memoria
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }
    }
}
