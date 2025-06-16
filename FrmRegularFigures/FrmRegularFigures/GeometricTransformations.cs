using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmRegularFigures
{
    public static class GeometricTransformations
    {
        public static void Zoom(ref List<PointF> points, float zoomFactor, PointF center)
        {
            for (int i = 0; i < points.Count; i++)
            {
                // Calcular el vector desde el centro al punto
                float dx = points[i].X - center.X;
                float dy = points[i].Y - center.Y;

                // Aplicar el factor de zoom
                dx *= zoomFactor;
                dy *= zoomFactor;

                // Calcular la nueva posición del punto
                points[i] = new PointF(
                    center.X + dx,
                    center.Y + dy
                );
            }
        }

        public static void RotateClockwise(ref List<PointF> points, float angle, PointF center)
        {
            float radians = angle * (float)Math.PI / 180;
            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].X - center.X;
                float y = points[i].Y - center.Y;
                points[i] = new PointF(
                    center.X + (x * (float)Math.Cos(radians) + y * (float)Math.Sin(radians)),
                    center.Y + (-x * (float)Math.Sin(radians) + y * (float)Math.Cos(radians))
                );
            }
        }

        public static void RotateCounterclockwise(ref List<PointF> points, float angle, PointF center)
        {
            float radians = angle * (float)Math.PI / 180;
            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].X - center.X;
                float y = points[i].Y - center.Y;
                points[i] = new PointF(
                    center.X + (x * (float)Math.Cos(radians) - y * (float)Math.Sin(radians)),
                    center.Y + (x * (float)Math.Sin(radians) + y * (float)Math.Cos(radians))
                );
            }
        }

        public static void Translate(ref List<PointF> points, float dx, float dy)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new PointF(points[i].X + dx, points[i].Y + dy);
            }
        }
    }
}
