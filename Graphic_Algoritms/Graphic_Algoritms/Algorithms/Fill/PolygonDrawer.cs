/**
 * Clase PolygonDrawer
 * 
 * Permite crear polígonos personalizados mediante clics del usuario.
 * Los polígonos creados pueden ser utilizados para algoritmos de relleno
 * como FloodFill.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public class PolygonDrawer
    {
        /**
         * Propiedades del polígono
         */
        public List<Point> Vertices { get; private set; }
        public Color ColorBorde { get; set; }
        public Color ColorRelleno { get; set; }
        public bool EstaCompleto { get; private set; }
        public bool EstaRelleno { get; set; }

        /**
         * Variables para controlar el proceso de dibujo
         */
        private PictureBox pictureBox;
        private bool modoCreacion;
        private Point puntoTemporal;
        private bool mostrarPuntoTemporal;

        /**
         * Eventos para notificar cuando el polígono está completo
         */
        public event EventHandler<PolygonCompletedEventArgs> PolygonCompleted;

        /**
         * Constructor
         */
        public PolygonDrawer(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            Vertices = new List<Point>();
            ColorBorde = Color.Black;
            ColorRelleno = Color.FromArgb(255, 168, 38);
            EstaCompleto = false;
            EstaRelleno = false;
            modoCreacion = false;
            mostrarPuntoTemporal = false;

            ConfigurarEventos();
        }

        /**
         * Configura los eventos del PictureBox
         */
        private void ConfigurarEventos()
        {
            pictureBox.MouseClick += PictureBox_MouseClick;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.Paint += PictureBox_Paint;
        }

        /**
         * Inicia el modo de creación de polígono
         */
        public void IniciarCreacion()
        {
            Vertices.Clear();
            EstaCompleto = false;
            EstaRelleno = false;
            modoCreacion = true;
            pictureBox.Invalidate();
        }

        /**
         * Finaliza la creación del polígono
         */
        /*public void CompletarPoligono()
        {
            if (Vertices.Count >= 3)
            {
                EstaCompleto = true;
                modoCreacion = false;
                mostrarPuntoTemporal = false;

                // Disparar evento de polígono completado
                PolygonCompleted?.Invoke(this, new PolygonCompletedEventArgs(this));

                pictureBox.Invalidate();
            }
        }*/

        /**
 * Finaliza la creación del polígono
 */
        public void CompletarPoligono()
        {
            if (Vertices.Count >= 3)
            {
                EstaCompleto = true;
                modoCreacion = false;
                mostrarPuntoTemporal = false;

                // ✅ NUEVO: Desactivar eventos del mouse cuando se completa
                pictureBox.MouseClick -= PictureBox_MouseClick;
                pictureBox.MouseMove -= PictureBox_MouseMove;

                // Disparar evento de polígono completado
                PolygonCompleted?.Invoke(this, new PolygonCompletedEventArgs(this));

                pictureBox.Invalidate();
            }
        }


        /**
         * Cancela la creación del polígono
         */
        public void CancelarCreacion()
        {
            Vertices.Clear();
            EstaCompleto = false;
            modoCreacion = false;
            mostrarPuntoTemporal = false;
            pictureBox.Invalidate();
        }

        /**
         * Maneja los clics del mouse para agregar vértices
         */
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!modoCreacion) return;

            if (e.Button == MouseButtons.Left)
            {
                // Agregar vértice
                Vertices.Add(e.Location);
                pictureBox.Invalidate();
            }
            else if (e.Button == MouseButtons.Right && Vertices.Count >= 3)
            {
                // Completar polígono con clic derecho
                CompletarPoligono();
            }
        }

        /**
         * Maneja el movimiento del mouse para mostrar preview
         */
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!modoCreacion) return;

            puntoTemporal = e.Location;
            mostrarPuntoTemporal = Vertices.Count > 0;
            pictureBox.Invalidate();
        }

        /**
         * Dibuja el polígono en el PictureBox
         */
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Vertices.Count == 0) return;

            Graphics g = e.Graphics;

            // Dibujar polígono completo
            if (EstaCompleto)
            {
                DibujarPoligonoCompleto(g);
            }
            else if (modoCreacion)
            {
                DibujarPoligonoEnCreacion(g);
            }
        }

        /**
         * Dibuja el polígono cuando está completo
         */
        private void DibujarPoligonoCompleto(Graphics g)
        {
            if (Vertices.Count < 3) return;

            Point[] puntos = Vertices.ToArray();

            // Dibujar relleno si está activado
            if (EstaRelleno)
            {
                using (SolidBrush brush = new SolidBrush(ColorRelleno))
                {
                    g.FillPolygon(brush, puntos);
                }
            }

            // Dibujar borde
            using (Pen pen = new Pen(ColorBorde, 2))
            {
                g.DrawPolygon(pen, puntos);
            }

            // Dibujar vértices
            DibujarVertices(g);
        }

        /**
         * Dibuja el polígono mientras se está creando
         */
        private void DibujarPoligonoEnCreacion(Graphics g)
        {
            // Dibujar líneas entre vértices existentes
            if (Vertices.Count > 1)
            {
                using (Pen pen = new Pen(ColorBorde, 2))
                {
                    for (int i = 0; i < Vertices.Count - 1; i++)
                    {
                        g.DrawLine(pen, Vertices[i], Vertices[i + 1]);
                    }
                }
            }

            // Dibujar línea desde último vértice hasta posición actual del mouse
            if (mostrarPuntoTemporal && Vertices.Count > 0)
            {
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(pen, Vertices[Vertices.Count - 1], puntoTemporal);

                    // Mostrar línea de cierre si hay más de 2 vértices
                    if (Vertices.Count > 2)
                    {
                        g.DrawLine(pen, puntoTemporal, Vertices[0]);
                    }
                }
            }

            // Dibujar vértices
            DibujarVertices(g);
        }

        /**
         * Dibuja los vértices del polígono
         */
        private void DibujarVertices(Graphics g)
        {
            foreach (Point vertice in Vertices)
            {
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    g.FillEllipse(brush, vertice.X - 3, vertice.Y - 3, 6, 6);
                }
            }
        }

        /**
         * Verifica si un punto está dentro del polígono
         */
        public bool ContienePunto(Point punto)
        {
            if (!EstaCompleto || Vertices.Count < 3) return false;

            bool dentro = false;
            int j = Vertices.Count - 1;

            for (int i = 0; i < Vertices.Count; i++)
            {
                if (((Vertices[i].Y > punto.Y) != (Vertices[j].Y > punto.Y)) &&
                    (punto.X < (Vertices[j].X - Vertices[i].X) * (punto.Y - Vertices[i].Y) /
                    (Vertices[j].Y - Vertices[i].Y) + Vertices[i].X))
                {
                    dentro = !dentro;
                }
                j = i;
            }

            return dentro;
        }

        /**
         * Dibuja solo el borde del polígono
         */
        public void DibujarBorde(PictureBox pictureBox)
        {
            if (!EstaCompleto || Vertices.Count < 3) return;

            Bitmap bmp = pictureBox.Image as Bitmap;
            if (bmp == null)
            {
                bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                pictureBox.Image = bmp;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (Pen pen = new Pen(ColorBorde, 2))
                {
                    Point[] puntos = Vertices.ToArray();
                    g.DrawPolygon(pen, puntos);
                }
            }

            pictureBox.Refresh();
        }

        /**
         * Limpia el polígono
         */
        public void Limpiar()
        {
            Vertices.Clear();
            EstaCompleto = false;
            EstaRelleno = false;
            modoCreacion = false;
            mostrarPuntoTemporal = false;
            pictureBox.Invalidate();
        }
    }

    /**
     * Argumentos del evento cuando se completa un polígono
     */
    public class PolygonCompletedEventArgs : EventArgs
    {
        public PolygonDrawer Polygon { get; }

        public PolygonCompletedEventArgs(PolygonDrawer polygon)
        {
            Polygon = polygon;
        }
    }
}
