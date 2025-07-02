/**
 * Clase Rombo
 * 
 * Representa un rombo alargado que puede dibujarse en un PictureBox.
 * Incluye funcionalidades para dibujar el borde y relleno, detectar
 * si un punto está dentro del rombo y animar el proceso de relleno.
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
        /**
         * Propiedades del algoritmo de relleno
         */
        public Color ColorRelleno { get; set; }
        public bool EstaActivo { get; private set; }

        /**
         * Variables para controlar la animación del relleno
         */
        private Timer animacionTimer;
        private Queue<Point> puntosParaPintar;
        private Bitmap bmpAnimacion;
        private PictureBox pictureBoxAnimacion;
        private Color colorAnimacion;
        private Color colorOriginal;

        /**
         * Polígono personalizado creado por el usuario
         */
        private PolygonDrawer polygonDrawer;

        /**
         * Constructor
         */
        public FloodFill(PictureBox pictureBox)
        {
            pictureBoxAnimacion = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            ColorRelleno = Color.FromArgb(255, 168, 38);
            EstaActivo = false;

            // Inicializar el dibujador de polígonos
            polygonDrawer = new PolygonDrawer(pictureBox);
            polygonDrawer.ColorRelleno = ColorRelleno;

            // Configurar evento cuando el polígono se complete
            polygonDrawer.PolygonCompleted += OnPolygonCompleted;
        }

        /**
         * Inicia el modo de creación de polígono
         */
        public void IniciarCreacionPoligono()
        {
            LimpiarPictureBox(pictureBoxAnimacion);
            polygonDrawer.IniciarCreacion();
            EstaActivo = true;
        }

        /**
         * Evento que se dispara cuando se completa el polígono
         */
        private void OnPolygonCompleted(object sender, PolygonCompletedEventArgs e)
        {
            // El polígono está listo para ser rellenado
            // Se puede hacer clic dentro para aplicar flood fill
        }

        /**
         * Procesa un clic para flood fill (solo si hay un polígono completo)
         */
        public bool ProcesarClicFloodFill(Point puntoClick, Color colorInundacion, int intervalo = 10)
        {
            if (!polygonDrawer.EstaCompleto)
                return false;

            if (polygonDrawer.ContienePunto(puntoClick))
            {
                FloodFillAnimado(puntoClick.X, puntoClick.Y, colorInundacion, intervalo);
                return true;
            }
            return false;
        }

        /**
         * Implementa el algoritmo de inundación (flood fill) con animación
         */
        private void FloodFillAnimado(int x, int y, Color colorRelleno, int intervalo = 10)
        {
            if (pictureBoxAnimacion.Image == null) return;

            bmpAnimacion = pictureBoxAnimacion.Image as Bitmap;
            colorOriginal = bmpAnimacion.GetPixel(x, y);

            if (colorOriginal.ToArgb() == colorRelleno.ToArgb()) return;

            colorAnimacion = colorRelleno;
            puntosParaPintar = new Queue<Point>();
            ObtenerPuntosParaInundacion(bmpAnimacion, x, y, colorOriginal, puntosParaPintar);

            if (animacionTimer == null)
            {
                animacionTimer = new Timer();
                animacionTimer.Tick += AnimacionTimer_Tick;
            }

            animacionTimer.Interval = intervalo;
            animacionTimer.Start();

            polygonDrawer.EstaRelleno = true;
            polygonDrawer.ColorRelleno = colorRelleno;
        }

        /**
         * Calcula todos los puntos a pintar en el proceso de inundación
         */
        private void ObtenerPuntosParaInundacion(Bitmap bmp, int x, int y, Color targetColor, Queue<Point> puntos)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();
                int px = p.X;
                int py = p.Y;

                if (px < 0 || px >= bmp.Width || py < 0 || py >= bmp.Height)
                    continue;

                if (bmp.GetPixel(px, py).ToArgb() != targetColor.ToArgb())
                    continue;

                puntos.Enqueue(new Point(px, py));
                bmp.SetPixel(px, py, Color.FromArgb(0, 0, 0, 1));

                stack.Push(new Point(px, py + 1));
                stack.Push(new Point(px + 1, py));
                stack.Push(new Point(px, py - 1));
                stack.Push(new Point(px - 1, py));
            }

            // Redibujar solo el borde del polígono
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                polygonDrawer.DibujarBorde(pictureBoxAnimacion);
            }
        }

        /**
         * Manejador del evento Tick del Timer de animación
         */
        private void AnimacionTimer_Tick(object sender, EventArgs e)
        {
            int puntosAPintar = 50;

            if (puntosParaPintar.Count == 0)
            {
                animacionTimer.Stop();
                polygonDrawer.DibujarBorde(pictureBoxAnimacion);
                return;
            }

            for (int i = 0; i < puntosAPintar && puntosParaPintar.Count > 0; i++)
            {
                Point p = puntosParaPintar.Dequeue();
                bmpAnimacion.SetPixel(p.X, p.Y, colorAnimacion);
            }

            pictureBoxAnimacion.Refresh();
        }

        /**
         * Completa el polígono actual
         */
        public void CompletarPoligono()
        {
            polygonDrawer.CompletarPoligono();
        }

        /**
         * Cancela la creación del polígono actual
         */
        public void CancelarPoligono()
        {
            polygonDrawer.CancelarCreacion();
            EstaActivo = false;
        }

        /**
         * Limpia el PictureBox estableciendo todos los píxeles en blanco
         */
        public static void LimpiarPictureBox(PictureBox pictureBox)
        {
            if (pictureBox == null) return;

            Bitmap bmp = pictureBox.Image as Bitmap;
            if (bmp == null)
            {
                bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                pictureBox.Image = bmp;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }

            pictureBox.Refresh();
        }

        /**
         * Propiedades de acceso al polígono
         */
        public bool TienePoligonoCompleto => polygonDrawer?.EstaCompleto ?? false;
        public int NumeroVertices => polygonDrawer?.Vertices?.Count ?? 0;
    }
}
