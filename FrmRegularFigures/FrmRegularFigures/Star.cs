using FrmRegularFigures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

public class Star
{
    private List<PointF> originalPoints;
    private List<PointF> currentPoints;
    private float baseSize;
    private float currentScale = 1.0f;
    private float currentRotation = 0.0f;
    private PointF currentTranslation = PointF.Empty;

    public Star(float size = 5.0f)
    {
        baseSize = size;
        originalPoints = CalculateStarPoints(baseSize * 40f);
        currentPoints = new List<PointF>(originalPoints);
    }

    private List<PointF> CalculateStarPoints(float size)
    {
        List<PointF> points = new List<PointF>();
        float angleStep = 360.0f / 5;
        float outerRadius = size / 2;
        float innerRadius = outerRadius * 0.382f;

        for (int i = 0; i < 5; i++)
        {
            double outerAngle = (i * angleStep - 90) * Math.PI / 180;
            points.Add(new PointF(
                (float)(outerRadius * Math.Cos(outerAngle)),
                (float)(outerRadius * Math.Sin(outerAngle))));

            double innerAngle = ((i * angleStep + angleStep / 2) - 90) * Math.PI / 180;
            points.Add(new PointF(
                (float)(innerRadius * Math.Cos(innerAngle)),
                (float)(innerRadius * Math.Sin(innerAngle))));
        }

        return points;
    }

    public void Zoom(float zoomFactor)
    {
        currentScale = zoomFactor;
        ApplyTransformations();
    }

    public void RotateClockwise(float angle)
    {
        currentRotation -= angle;
        ApplyTransformations();
    }

    public void RotateCounterclockwise(float angle)
    {
        currentRotation += angle;
        ApplyTransformations();
    }

    public void Translate(float dx, float dy)
    {
        currentTranslation.X += dx;
        currentTranslation.Y += dy;
        ApplyTransformations();
    }

    private void ApplyTransformations()
    {
        // Reiniciamos los puntos a los originales
        currentPoints = new List<PointF>(originalPoints);

        // Aplicamos las transformaciones en orden correcto:
        // 1. Escala
        GeometricTransformations.Zoom(ref currentPoints, currentScale, PointF.Empty);

        // 2. Rotación
        GeometricTransformations.RotateCounterclockwise(ref currentPoints, currentRotation, PointF.Empty);

        // 3. Traslación
        GeometricTransformations.Translate(ref currentPoints, currentTranslation.X, currentTranslation.Y);
    }

    public void PlotShape(PaintEventArgs e, PictureBox picCanvas)
    {
        PointF center = new PointF(picCanvas.Width / 2, picCanvas.Height / 2);

        // Creamos una copia de los puntos transformados
        List<PointF> pointsToDraw = new List<PointF>(currentPoints);

        // Centramos en el PictureBox
        GeometricTransformations.Translate(ref pointsToDraw, center.X, center.Y);

        using (Pen pen = new Pen(Color.Blue, 2))
        {
            // Dibujar la estrella
            for (int i = 0; i < pointsToDraw.Count; i++)
            {
                int nextIndex = (i + 1) % pointsToDraw.Count;
                e.Graphics.DrawLine(pen, pointsToDraw[i], pointsToDraw[nextIndex]);
            }

            // Dibujar líneas al centro (opcional)
            foreach (PointF point in pointsToDraw)
            {
                e.Graphics.DrawLine(pen, center, point);
            }
        }
    }
}