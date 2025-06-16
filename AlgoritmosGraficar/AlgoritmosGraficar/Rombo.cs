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
    public class Rombo
    {
        /**
         * Propiedades básicas del rombo
         */
        public Point Centro { get; set; }      // Centro del rombo
        public int Tamaño { get; set; }        // Tamaño del rombo (ancho)
        public Color ColorBorde { get; set; }  // Color del borde
        public Color ColorRelleno { get; set; } // Color del relleno
        public bool EstaRelleno { get; set; }  // Indica si el rombo está relleno

        /**
         * Variables para controlar la animación del relleno
         */
        private Timer animacionTimer;          // Temporizador para controlar la animación
        private Queue<Point> puntosParaPintar; // Cola de puntos para pintar
        private Bitmap bmpAnimacion;           // Bitmap donde se dibuja
        private PictureBox pictureBoxAnimacion; // PictureBox que muestra la animación
        private Color colorAnimacion;          // Color para la animación
        private Color colorOriginal;           // Color original de los píxeles

        /**
         * Constructor por defecto
         * Inicializa un rombo con valores predeterminados
         */
        public Rombo()
        {
            Centro = new Point(0, 0);
            Tamaño = 50;
            ColorBorde = Color.Black;
            ColorRelleno = Color.FromArgb(255, 168, 38); // Color naranja predeterminado
            EstaRelleno = false;
        }

        /**
         * Constructor con parámetros
         * Permite crear un rombo con valores específicos
         */
        public Rombo(Point centro, int tamaño, Color colorBorde, Color colorRelleno, bool estaRelleno)
        {
            Centro = centro;
            Tamaño = tamaño;
            ColorBorde = colorBorde;
            ColorRelleno = colorRelleno;
            EstaRelleno = estaRelleno;
        }

        /**
         * Calcula las coordenadas de los vértices del rombo alargado
         * basándose en el centro y tamaño
         */
        public Point[] CalcularVertices()
        {
            int anchura = Tamaño;
            int altura = Tamaño * 2; // Factor de alargamiento vertical
            
            Point[] vertices = new Point[4];
            vertices[0] = new Point(Centro.X, Centro.Y - altura/2); // Superior
            vertices[1] = new Point(Centro.X + anchura/2, Centro.Y); // Derecho
            vertices[2] = new Point(Centro.X, Centro.Y + altura/2); // Inferior
            vertices[3] = new Point(Centro.X - anchura/2, Centro.Y); // Izquierdo
            return vertices;
        }

        /**
         * Verifica si un punto está dentro del rombo
         * Utiliza el algoritmo de punto en polígono
         */
        public bool ContienePunto(Point punto)
        {
            Point[] vertices = CalcularVertices();
            bool dentro = false;
            int j = vertices.Length - 1;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (((vertices[i].Y > punto.Y) != (vertices[j].Y > punto.Y)) &&
                    (punto.X < (vertices[j].X - vertices[i].X) * (punto.Y - vertices[i].Y) /
                    (vertices[j].Y - vertices[i].Y) + vertices[i].X))
                {
                    dentro = !dentro;
                }
                j = i;
            }

            return dentro;
        }

        /**
         * Dibuja únicamente el borde del rombo en un PictureBox
         */
        public void DibujarBorde(PictureBox pictureBox)
        {
            if (pictureBox == null) return;

            // Obtener los vértices del rombo alargado
            Point[] vertices = CalcularVertices();

            // Crear o utilizar la imagen existente del PictureBox
            Bitmap bmp = pictureBox.Image as Bitmap;
            if (bmp == null)
            {
                bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                pictureBox.Image = bmp;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Dibujar solo el borde
                using (Pen pen = new Pen(ColorBorde, 2))
                {
                    g.DrawPolygon(pen, vertices);
                }
            }

            // Refrescar el PictureBox
            pictureBox.Refresh();
        }

        /**
         * Dibuja el rombo completo (borde y relleno si está activado)
         */
        public void Dibujar(PictureBox pictureBox)
        {
            if (pictureBox == null) return;

            // Obtener los vértices del rombo alargado
            Point[] vertices = CalcularVertices();

            // Crear o utilizar la imagen existente del PictureBox
            Bitmap bmp = pictureBox.Image as Bitmap;
            if (bmp == null)
            {
                bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
                pictureBox.Image = bmp;
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Si el rombo debe estar relleno
                if (EstaRelleno)
                {
                    using (SolidBrush brush = new SolidBrush(ColorRelleno))
                    {
                        g.FillPolygon(brush, vertices);
                    }
                }

                // Dibujar el borde
                using (Pen pen = new Pen(ColorBorde, 2))
                {
                    g.DrawPolygon(pen, vertices);
                }
            }

            // Refrescar el PictureBox
            pictureBox.Refresh();
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
         * Implementa el algoritmo de inundación (flood fill) con animación
         * Comienza desde un punto (x,y) y rellena con un color específico
         */
        public void FloodFillAnimado(PictureBox pictureBox, int x, int y, Color colorRelleno, int intervalo = 10)
        {
            if (pictureBox == null || pictureBox.Image == null) return;

            pictureBoxAnimacion = pictureBox;
            bmpAnimacion = pictureBox.Image as Bitmap;
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

            EstaRelleno = true;
            ColorRelleno = colorRelleno;
        }

        /**
         * Calcula todos los puntos a pintar en el proceso de inundación
         * y los almacena en una cola para la animación
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

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                DibujarBorde(pictureBoxAnimacion);
            }
        }

        /**
         * Manejador del evento Tick del Timer de animación
         * Pinta un conjunto de puntos en cada intervalo
         */
        private void AnimacionTimer_Tick(object sender, EventArgs e)
        {
            int puntosAPintar = 50;

            if (puntosParaPintar.Count == 0)
            {
                animacionTimer.Stop();
                DibujarBorde(pictureBoxAnimacion);
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
         * Procesa un clic del usuario y comienza la animación si
         * el clic ocurrió dentro del rombo
         */
        public bool ProcesarClicAnimado(Point puntoClick, PictureBox pictureBox, Color colorInundacion, int intervalo = 10)
        {
            if (ContienePunto(puntoClick))
            {
                FloodFillAnimado(pictureBox, puntoClick.X, puntoClick.Y, colorInundacion, intervalo);
                return true;
            }
            return false;
        }
    }
}
