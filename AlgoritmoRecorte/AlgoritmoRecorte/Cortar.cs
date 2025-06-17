using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AlgoritmoRecorte
{
    internal class Cortar
    {
        private Rectangle clipRegion;

        public Cortar(Rectangle clipRegion)
        {
            this.clipRegion = clipRegion;
        }

        public List<(Point start, Point end, bool isInside)> ClipLines(List<(Point start, Point end, bool isInside)> lineSegments)
        {
            var newSegments = new List<(Point start, Point end, bool isInside)>();

            foreach (var line in lineSegments)
            {
                if (IsCompletelyOutside(line.start, line.end))
                {
                    newSegments.Add((line.start, line.end, false));
                    continue;
                }

                if (IsCompletelyInside(line.start, line.end))
                {
                    newSegments.Add((line.start, line.end, true));
                    continue;
                }

                var intersections = FindIntersections(line.start, line.end);
                if (intersections.Count == 0)
                {
                    newSegments.Add((line.start, line.end, false));
                }
                else
                {
                    intersections.Sort((a, b) => Distance(line.start, a).CompareTo(Distance(line.start, b)));
                    intersections.Insert(0, line.start);
                    intersections.Add(line.end);

                    for (int i = 0; i < intersections.Count - 1; i++)
                    {
                        Point midPoint = new Point(
                            (intersections[i].X + intersections[i + 1].X) / 2,
                            (intersections[i].Y + intersections[i + 1].Y) / 2
                        );
                        bool isInside = IsPointInside(midPoint);
                        newSegments.Add((intersections[i], intersections[i + 1], isInside));
                    }
                }
            }

            return newSegments;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private bool IsCompletelyInside(Point p1, Point p2)
        {
            return clipRegion.Contains(p1) && clipRegion.Contains(p2);
        }

        private List<Point> FindIntersections(Point start, Point end)
        {
            var intersections = new List<Point>();

            TryIntersectHorizontal(start, end, clipRegion.Top, intersections);
            TryIntersectHorizontal(start, end, clipRegion.Bottom, intersections);
            TryIntersectVertical(start, end, clipRegion.Left, intersections);
            TryIntersectVertical(start, end, clipRegion.Right, intersections);

            return intersections.Distinct().Where(p => IsPointOnLineSegment(start, end, p)).ToList();
        }

        private void TryIntersectHorizontal(Point start, Point end, int y, List<Point> intersections)
        {
            if (start.Y == end.Y) return;

            float x = start.X + (float)(end.X - start.X) * (y - start.Y) / (end.Y - start.Y);
            if (x >= clipRegion.Left && x <= clipRegion.Right)
            {
                intersections.Add(new Point((int)x, y));
            }
        }

        private void TryIntersectVertical(Point start, Point end, int x, List<Point> intersections)
        {
            if (start.X == end.X) return;

            float y = start.Y + (float)(end.Y - start.Y) * (x - start.X) / (end.X - start.X);
            if (y >= clipRegion.Top && y <= clipRegion.Bottom)
            {
                intersections.Add(new Point(x, (int)y));
            }
        }

        private bool IsPointOnLineSegment(Point start, Point end, Point test)
        {
            double d1 = Distance(start, test);
            double d2 = Distance(test, end);
            double lineLen = Distance(start, end);
            const double EPSILON = 0.1;
            return Math.Abs(d1 + d2 - lineLen) < EPSILON;
        }

        private bool IsPointInside(Point p)
        {
            return clipRegion.Contains(p);
        }

        private bool IsCompletelyOutside(Point p1, Point p2)
        {
            int code1 = ComputeRegionCode(p1, clipRegion.Left, clipRegion.Right, clipRegion.Top, clipRegion.Bottom);
            int code2 = ComputeRegionCode(p2, clipRegion.Left, clipRegion.Right, clipRegion.Top, clipRegion.Bottom);
            return (code1 & code2) != 0;
        }

        private int ComputeRegionCode(Point p, int xmin, int xmax, int ymin, int ymax)
        {
            int code = 0;
            if (p.X < xmin) code |= 1;
            if (p.X > xmax) code |= 2;
            if (p.Y < ymin) code |= 4;
            if (p.Y > ymax) code |= 8;
            return code;
        }
    }
}
