using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rea_Leccion
{
    class Pentagono 
    {
        float lado;
        float medida;
        float radio;
        Graphics graph;
        Pen pen;

        public float CalcularArea()
        {
            lado = 2 * radio * (float)Math.Sin(Math.PI / 5); 
            float apotema = radio * (float)Math.Cos(Math.PI / 5); 
            return (5 * lado * apotema) / 2;
        }

        public float CalcularPerimetro()
        {
            lado = 2 * radio * (float)Math.Sin(Math.PI / 5);
            return 5 * lado;
        }

        public void Dibujar(PictureBox pictureBox)
        {
            PointF centro = new PointF(pictureBox.Width / 2, pictureBox.Height / 2);
             graph = pictureBox.CreateGraphics();
            
            pen = new Pen(Color.Blue, 3);
            PointF[] puntos = CalcularVerticesPentagono(centro, radio);
            graph.DrawPolygon(pen, puntos);
            
        }

        private PointF[] CalcularVerticesPentagono(PointF centro, float radio)
        {
            PointF[] puntos = new PointF[5];
            double anguloCentral = 2 * Math.PI / 5;

            for (int i = 0; i < 5; i++)
            {
                double angulo = i * anguloCentral - Math.PI / 2; // Rotar para que un vértice esté hacia arriba
                float x = centro.X + (float)(radio * Math.Cos(angulo));
                float y = centro.Y + (float)(radio * Math.Sin(angulo));
                puntos[i] = new PointF(x, y);
            }

            return puntos;
        }
    }
}
