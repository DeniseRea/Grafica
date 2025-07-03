/**
 * Clase FloodFill
 * 
 * Representa un algoritmo de relleno por inundación (Flood Fill) animado,
 * con soporte para trabajar dentro de un polígono dibujado por el usuario.
 * Soporta la detección de clics, cálculo de puntos válidos y relleno progresivo.
 * 
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public class FloodFill
    {
        public Color ColorRelleno { get; set; }
        public bool EstaActivo { get; private set; }

        private Timer animacionTimer;
        private Queue<Point> puntosParaPintar;
        private Bitmap bmpAnimacion;
        private PictureBox pictureBoxAnimacion;
        private Color colorAnimacion;
        private Color colorOriginal;

        public PolygonDrawer polygonDrawer;

        public FloodFill(PictureBox pictureBox)
        {
            pictureBoxAnimacion = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            ColorRelleno = Color.FromArgb(255, 168, 38);
            EstaActivo = false;

            polygonDrawer = new PolygonDrawer(pictureBoxAnimacion);
            polygonDrawer.ColorRelleno = ColorRelleno;
            polygonDrawer.PolygonCompleted += OnPolygonCompleted;
        }

        public void IniciarCreacionPoligono()
        {
            if (pictureBoxAnimacion.Image == null)
            {
                pictureBoxAnimacion.Image = new Bitmap(pictureBoxAnimacion.Width, pictureBoxAnimacion.Height);
                using (Graphics g = Graphics.FromImage(pictureBoxAnimacion.Image))
                {
                    g.Clear(Color.White);
                }
            }

            polygonDrawer.IniciarCreacion();
            EstaActivo = true;
        }

        private void OnPolygonCompleted(object sender, PolygonCompletedEventArgs e)
        {
            // Evento opcional cuando se termina de dibujar el polígono
        }

        public bool ProcesarClicFloodFill(Point puntoClick, Color colorInundacion, int intervalo = 10)
        {
            if (!polygonDrawer.EstaCompleto)
            {
                System.Diagnostics.Debug.WriteLine("❌ Polígono no está completo");
                return false;
            }

            if (!polygonDrawer.ContienePunto(puntoClick))
            {
                System.Diagnostics.Debug.WriteLine($"❌ Punto ({puntoClick.X}, {puntoClick.Y}) fuera del polígono");
                return false;
            }

            if (pictureBoxAnimacion.Image == null)
            {
                System.Diagnostics.Debug.WriteLine("❌ Imagen es null");
                return false;
            }

            if (puntoClick.X < 0 || puntoClick.X >= pictureBoxAnimacion.Image.Width ||
                puntoClick.Y < 0 || puntoClick.Y >= pictureBoxAnimacion.Image.Height)
            {
                System.Diagnostics.Debug.WriteLine("❌ Punto fuera de límites de imagen");
                return false;
            }

            System.Diagnostics.Debug.WriteLine($"✅ Iniciando flood fill en ({puntoClick.X}, {puntoClick.Y})");
            FloodFillAnimado(pictureBoxAnimacion, puntoClick.X, puntoClick.Y, colorInundacion, intervalo);
            return true;
        }

        // ✅ FloodFillAnimado actualizado: recibe el PictureBox como parámetro
        public void FloodFillAnimado(PictureBox pictureBox, int x, int y, Color colorRelleno, int intervalo = 10)
        {
            if (pictureBox == null || pictureBox.Image == null)
                return;

            pictureBoxAnimacion = pictureBox;
            bmpAnimacion = pictureBox.Image as Bitmap;

            colorOriginal = bmpAnimacion.GetPixel(x, y);
            if (colorOriginal.ToArgb() == colorRelleno.ToArgb())
                return;

            colorAnimacion = colorRelleno;
            puntosParaPintar = new Queue<Point>();
            ObtenerPuntosParaInundacion(x, y, colorOriginal);

            if (animacionTimer != null)
            {
                animacionTimer.Stop();
                animacionTimer.Dispose();
            }

            animacionTimer = new Timer();
            animacionTimer.Interval = intervalo;
            animacionTimer.Tick += AnimacionTimer_Tick;
            animacionTimer.Start();

            polygonDrawer.EstaRelleno = true;
            polygonDrawer.ColorRelleno = colorRelleno;
        }

        private void ObtenerPuntosParaInundacion(int x, int y, Color targetColor)
        {
            Stack<Point> stack = new Stack<Point>();
            HashSet<Point> visitados = new HashSet<Point>();
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();
                int px = p.X;
                int py = p.Y;

                if (px < 0 || px >= bmpAnimacion.Width || py < 0 || py >= bmpAnimacion.Height)
                    continue;

                if (visitados.Contains(p))
                    continue;

                if (!polygonDrawer.ContienePunto(p))
                    continue;

                Color colorActual = bmpAnimacion.GetPixel(px, py);
                if (colorActual.ToArgb() != targetColor.ToArgb())
                    continue;

                visitados.Add(p);
                puntosParaPintar.Enqueue(p);

                stack.Push(new Point(px + 1, py));
                stack.Push(new Point(px - 1, py));
                stack.Push(new Point(px, py + 1));
                stack.Push(new Point(px, py - 1));
            }
        }

        private void AnimacionTimer_Tick(object sender, EventArgs e)
        {
            int puntosAPintar = 100;

            if (puntosParaPintar.Count == 0)
            {
                animacionTimer.Stop();

                using (Graphics g = Graphics.FromImage(bmpAnimacion))
                {
                    using (Pen pen = new Pen(polygonDrawer.ColorBorde, 2))
                    {
                        if (polygonDrawer.Vertices.Count >= 3)
                        {
                            Point[] vertices = polygonDrawer.Vertices.ToArray();
                            g.DrawPolygon(pen, vertices);
                        }
                    }
                }

                pictureBoxAnimacion.Refresh();
                return;
            }

            for (int i = 0; i < puntosAPintar && puntosParaPintar.Count > 0; i++)
            {
                Point p = puntosParaPintar.Dequeue();
                bmpAnimacion.SetPixel(p.X, p.Y, colorAnimacion);
            }

            pictureBoxAnimacion.Refresh();
        }

        public void CompletarPoligono()
        {
            polygonDrawer.CompletarPoligono();
        }

        public void CancelarPoligono()
        {
            polygonDrawer.CancelarCreacion();
            EstaActivo = false;
        }

        public static void LimpiarPictureBox(PictureBox pictureBox)
        {
            if (pictureBox == null) return;

            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.Clear(Color.White);
            }

            pictureBox.Refresh();
        }

        public bool TienePoligonoCompleto => polygonDrawer?.EstaCompleto ?? false;
        public int NumeroVertices => polygonDrawer?.Vertices?.Count ?? 0;
    }
}
