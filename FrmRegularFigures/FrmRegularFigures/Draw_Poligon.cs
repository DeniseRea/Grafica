using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System;


namespace FrmRegularFigures
{
    class Draw_Poligon
    {
        int numLados;
        float radio;
        private Graphics mGraph;
        private const float SF = 20;
        private Pen mPen;

        // Propiedad para permitir el acceso a los puntos del polígono desde fuera
        public PointF[] PolygonPoints { get; private set; }

        //coloes
        /*private readonly Color[] colores = {
                Color.Red, Color.Yellow, Color.Green,
                Color.HotPink, Color.Blue, Color.Cyan
            };*/

        public Draw_Poligon()
        {
            numLados = 0;
            radio = 0;
            mPen = new Pen(Color.Black, 2);
        }

        public Draw_Poligon(int numLados, float radio)
        {
            this.numLados = numLados;
            this.radio = radio;
            mPen = new Pen(Color.Black, 2);
        }

        public void PlotPolygon(PictureBox picCanvas)
        {
            if (numLados <= 0 || radio <= 0)
                return;

            // Crear un nuevo bitmap y gráfico en memoria
            Bitmap bitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
            mGraph = Graphics.FromImage(bitmap);
            mGraph.Clear(Color.White);
            // Comentar esta línea para evitar antialiasing
            // mGraph.SmoothingMode = SmoothingMode.AntiAlias;

            float centerX = picCanvas.Width / 2;
            float centerY = picCanvas.Height / 2;

            PointF[] points = new PointF[numLados];

            // Calcular los puntos del polígono
            for (int i = 0; i < numLados; i++)
            {
                float angle = (float)(2 * Math.PI * i / numLados);

                points[i] = new PointF(
                    centerX + radio * SF * (float)Math.Cos(angle),
                    centerY + radio * SF * (float)Math.Sin(angle)
                );
            }

            // Guardar los puntos para acceso externo
            PolygonPoints = points;

            // Dibujar el contorno del polígono
            mGraph.DrawPolygon(mPen, points);

            // Asignar el bitmap al PictureBox
            picCanvas.Image = bitmap;
        }

        // Método para manejar el clic en el polígono y aplicar FloodFill


        public void clean(PictureBox pc, TextBox tx1, TextBox tx2)
        {
            pc.Refresh();
            tx1.Text = "";
            tx2.Text = "";
        }
    }
}

