using System.Drawing;
using System.Windows.Forms;
using System;

public class Pentagon
{
    // Atributos
    private float mRadius;   // Radio del pentágono (distancia del centro a los vértices)
    private float mApothem;  // Apotema del pentágono (distancia del centro a cualquier lado)
    private Graphics mGraph;
    private Pen mPen;

    // Constructor
    public Pentagon()
    {
        mRadius = 5.0f; // Tamaño base del pentágono
        // Calcular el apotema inicial
        CalculateApothem();
    }

    // Método para calcular el apotema
    private void CalculateApothem()
    {
        // Para un pentágono regular, apotema = radio * cos(π/5)
        mApothem = mRadius * (float)Math.Cos(Math.PI / 5);
    }

    // Método para obtener el valor del apotema
    public float GetApothem()
    {
        return mApothem;
    }

    // Método para obtener el apotema escalado
    public float GetScaledApothem(float scale)
    {
        return mApothem * scale;
    }

    public void PlotShape(PictureBox picCanvas, float dimension)
    {
        mGraph = picCanvas.CreateGraphics();
        mPen = new Pen(Color.DarkGreen, 3);

        // Calcular el centro del PictureBox
        float centerX = picCanvas.Width / 2.0f;
        float centerY = picCanvas.Height / 2.0f;

        // Usar la dimensión como factor de escala
        float scaledRadius = mRadius * dimension;
        // Calcular el apotema escalado
        float scaledApothem = mApothem * dimension;

        // Crear un array para almacenar los puntos del pentágono
        Point[] points = new Point[5];

        // Calcular los vértices del pentágono regular con la punta hacia abajo
        for (int i = 0; i < 5; i++)
        {
            // Cada vértice está separado por 72 grados (2π/5 radianes)
            // Sumamos 180 grados para orientar la punta hacia abajo
            double angle = Math.PI + i * 2 * Math.PI / 5;

            // Convertir coordenadas polares a cartesianas
            float x = centerX + scaledRadius * (float)Math.Cos(angle);
            float y = centerY + scaledRadius * (float)Math.Sin(angle);

            points[i] = new Point((int)x, (int)y);
        }

        // Dibujar el pentágono conectando los vértices
        mGraph.DrawPolygon(mPen, points);

        // Opcional: Dibujar el apotema para visualización
        // Este código dibuja una línea desde el centro hasta la mitad del lado inferior
        double apothemAngle = Math.PI + Math.PI / 5; // Ángulo perpendicular al lado inferior
        float apothemX = centerX + scaledApothem * (float)Math.Cos(apothemAngle);
        float apothemY = centerY + scaledApothem * (float)Math.Sin(apothemAngle);

        // Dibujar la línea del apotema con un color diferente
        using (Pen apothemPen = new Pen(Color.Red, 1))
        {
            mGraph.DrawLine(apothemPen, centerX, centerY, apothemX, apothemY);
        }
    }

    public void ClearCanvas(PictureBox picCanvas)
    {
        picCanvas.Refresh();
    }
}