using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AlgoritmoRecorte
{
    internal class CohenSutherland
    {
        private List<(Point start, Point end, bool isInside)> lineSegments;
        private Rectangle clipRegion;
        private Size canvas;
        private Cortar clipper;

        public CohenSutherland(Size canvasSize)
        {
            canvas = canvasSize;
            lineSegments = new List<(Point start, Point end, bool isInside)>();
            clipRegion = new Rectangle(
                canvas.Width / 3,
                canvas.Height / 3,
                canvas.Width / 3,
                canvas.Height / 3
            );
            clipper = new Cortar(clipRegion);
        }

        public bool RemoveExternalSegmentsAtPoint(Point clickPoint, int tolerance = 5)
        {
            bool removedAny = false;
            lineSegments.RemoveAll(segment =>
            {
                if (!segment.isInside && IsPointNearLine(clickPoint, segment.start, segment.end, tolerance))
                {
                    removedAny = true;
                    return true;
                }
                return false;
            });
            return removedAny;
        }

        private bool IsPointNearLine(Point point, Point lineStart, Point lineEnd, int tolerance)
        {
            float deltaX = lineEnd.X - lineStart.X;
            float deltaY = lineEnd.Y - lineStart.Y;

            if (deltaX == 0 && deltaY == 0)
            {
                return Distance(point, lineStart) <= tolerance;
            }

            float lineLength = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float distance = Math.Abs(
                (deltaY * point.X - deltaX * point.Y + lineEnd.X * lineStart.Y - lineEnd.Y * lineStart.X) /
                lineLength
            );

            float minX = Math.Min(lineStart.X, lineEnd.X) - tolerance;
            float maxX = Math.Max(lineStart.X, lineEnd.X) + tolerance;
            float minY = Math.Min(lineStart.Y, lineEnd.Y) - tolerance;
            float maxY = Math.Max(lineStart.Y, lineEnd.Y) + tolerance;

            return distance <= tolerance &&
                   point.X >= minX && point.X <= maxX &&
                   point.Y >= minY && point.Y <= maxY;
        }

        public void AddLine(Point start, Point end)
        {
            lineSegments.Add((start, end, true));
        }

        public void ClipLines()
        {
            lineSegments = clipper.ClipLines(lineSegments);
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public List<(Point start, Point end)> GetClippedLineSegments()
        {
            return lineSegments.Select(l => (l.start, l.end)).ToList();
        }

        public Rectangle GetClipRegion()
        {
            return clipRegion;
        }

        public void Draw(Graphics g)
        {
            using (Pen insidePen = new Pen(Color.Black, 2))
            using (Pen outsidePen = new Pen(Color.Pink, 2))
            {
                foreach (var line in lineSegments)
                {
                    if (!line.start.IsEmpty && !line.end.IsEmpty)
                    {
                        Pen pen = line.isInside ? insidePen : outsidePen;
                        g.DrawLine(pen, line.start, line.end);
                    }
                }
            }
        }

        public void DrawGrid(Graphics g, Size canvasSize)
        {
            int cellWidth = canvasSize.Width / 3;
            int cellHeight = canvasSize.Height / 3;

            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                for (int i = 1; i < 3; i++)
                {
                    int y = i * cellHeight;
                    g.DrawLine(gridPen, 0, y, canvasSize.Width, y);
                }

                for (int i = 1; i < 3; i++)
                {
                    int x = i * cellWidth;
                    g.DrawLine(gridPen, x, 0, x, canvasSize.Height);
                }
            }
        }
    }
}
