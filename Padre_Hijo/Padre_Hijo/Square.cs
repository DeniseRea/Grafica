using System;
using System.Drawing;
using System.Windows.Forms;

namespace Figuras
{
    class Square
    {
        //atributos
        private float mSide;
        private Graphics mGraph;
        private Pen mPen;

        //constructor
        public Square()
        {
            mSide = 5.0f; // Tamaño base del lado del cuadrado
        }

        public void InitilizeData(PictureBox picCanvas)
        {
            picCanvas.Refresh();
        }

        public void PlotShape(PictureBox picCanvas, float scale)
        {
            mGraph = picCanvas.CreateGraphics();
            mPen = new Pen(Color.Blue, 3);

            // Calcular el centro del PictureBox
            float centerX = picCanvas.Width / 2.0f;
            float centerY = picCanvas.Height / 2.0f;

            // Calcular el tamaño escalado
            float scaledSide = mSide * scale;

            // Calcular la posición para que el cuadrado quede centrado
            float x = centerX - (scaledSide / 2.0f);
            float y = centerY - (scaledSide / 2.0f);

            // Dibujar el cuadrado centrado con el tamaño escalado
            mGraph.DrawRectangle(mPen, x, y, scaledSide, scaledSide);
        }

        public void CloseForm(Form objForm)
        {
            //cerrar el formulario
            objForm.Close();
        }

        public void ClearCanvas(PictureBox picCanvas)
        {
            picCanvas.Refresh();
        }
    }
}