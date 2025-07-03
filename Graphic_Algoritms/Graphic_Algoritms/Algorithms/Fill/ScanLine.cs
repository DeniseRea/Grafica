/**
 * Clase ScanLine
 * 
 * Implementa el algoritmo de relleno por líneas de barrido (Scan Line Fill).
 * Este algoritmo es más eficiente que Flood Fill para polígonos complejos,
 * ya que rellena línea por línea usando una tabla de aristas activas.
 * 
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public class ScanLine
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

        /**
         * Polígono personalizado creado por el usuario
         */
        private PolygonDrawer polygonDrawer;

        /**
         * Estructura para representar una arista del polígono
         */
        private class Arista
        {
            public int YMin { get; set; }       // Y mínimo de la arista
            public int YMax { get; set; }       // Y máximo de la arista
            public float X { get; set; }        // Intersección X actual
            public float DeltaX { get; set; }   // Incremento en X por cada Y

            public Arista(Point p1, Point p2)
            {
                if (p1.Y == p2.Y) return; // Línea horizontal, ignorar

                if (p1.Y < p2.Y)
                {
                    YMin = p1.Y;
                    YMax = p2.Y;
                    X = p1.X;
                }
                else
                {
                    YMin = p2.Y;
                    YMax = p1.Y;
                    X = p2.X;
                }

                DeltaX = (float)(p2.X - p1.X) / (p2.Y - p1.Y);
            }

            public void ActualizarX()
            {
                X += DeltaX;
            }
        }

        /**
         * Constructor
         */
        public ScanLine(PictureBox pictureBox)
        {
            pictureBoxAnimacion = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            ColorRelleno = Color.FromArgb(100, 200, 255); // Azul claro
            EstaActivo = false;

            // Inicializar el dibujador de polígonos
            polygonDrawer = new PolygonDrawer(pictureBox);
            polygonDrawer.ColorRelleno = ColorRelleno;
            polygonDrawer.PolygonCompleted += OnPolygonCompleted;
        }

        /**
         * Inicia el modo de creación de polígono
         */
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

        /**
         * Evento que se dispara cuando se completa el polígono
         */
        private void OnPolygonCompleted(object sender, PolygonCompletedEventArgs e)
        {
            // El polígono está listo para ser rellenado
        }

        /**
         * Procesa un clic para scan line fill
         */
        public bool ProcesarClicScanLine(Color colorRelleno, int intervalo = 100)
        {
            if (!polygonDrawer.EstaCompleto)
                return false;

            if (pictureBoxAnimacion.Image == null)
                return false;

            if (polygonDrawer.Vertices.Count < 3)
                return false;

            ScanLineAnimado(colorRelleno, intervalo);
            return true;
        }

        /**
         * Implementa el algoritmo de scan line con animación
         */
        public void ScanLineAnimado(Color colorRelleno, int intervalo = 100)
        {
            if (pictureBoxAnimacion.Image == null) return;

            bmpAnimacion = (Bitmap)pictureBoxAnimacion.Image;
            colorAnimacion = colorRelleno;
            puntosParaPintar = new Queue<Point>();

            // Calcular todas las líneas de relleno
            CalcularLineasRelleno();

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

        /**
         * Calcula todas las líneas de relleno usando el algoritmo Scan Line
         */
        private void CalcularLineasRelleno()
        {
            var vertices = polygonDrawer.Vertices;
            if (vertices.Count < 3) return;

            // Crear lista de aristas
            List<Arista> todasLasAristas = new List<Arista>();
            for (int i = 0; i < vertices.Count; i++)
            {
                Point p1 = vertices[i];
                Point p2 = vertices[(i + 1) % vertices.Count];

                if (p1.Y != p2.Y) // Ignorar aristas horizontales
                {
                    todasLasAristas.Add(new Arista(p1, p2));
                }
            }

            // Encontrar Y mínimo y máximo
            int yMin = todasLasAristas.Min(a => a.YMin);
            int yMax = todasLasAristas.Max(a => a.YMax);

            // Procesar cada línea de barrido
            for (int y = yMin; y <= yMax; y++)
            {
                // Encontrar aristas activas para esta línea
                List<Arista> aristasActivas = todasLasAristas
                    .Where(a => a.YMin <= y && a.YMax > y)
                    .OrderBy(a => a.X)
                    .ToList();

                // Rellenar entre pares de intersecciones
                for (int i = 0; i < aristasActivas.Count - 1; i += 2)
                {
                    if (i + 1 < aristasActivas.Count)
                    {
                        int xInicio = (int)Math.Round(aristasActivas[i].X);
                        int xFin = (int)Math.Round(aristasActivas[i + 1].X);

                        // Agregar puntos de la línea horizontal
                        for (int x = xInicio; x <= xFin; x++)
                        {
                            if (x >= 0 && x < bmpAnimacion.Width && y >= 0 && y < bmpAnimacion.Height)
                            {
                                puntosParaPintar.Enqueue(new Point(x, y));
                            }
                        }
                    }
                }

                // Actualizar X para la siguiente línea
                foreach (var arista in aristasActivas)
                {
                    arista.ActualizarX();
                }
            }
        }

        /**
         * Manejador del evento Tick del Timer de animación
         */
        private void AnimacionTimer_Tick(object sender, EventArgs e)
        {
            int puntosAPintar = 150; // Más puntos por línea para mejor visualización

            if (puntosParaPintar.Count == 0)
            {
                animacionTimer.Stop();

                // Redibujar el borde al final
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

            // Pintar puntos línea por línea
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
         * Limpia el PictureBox
         */
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

        /**
         * Propiedades de acceso al polígono
         */
        public bool TienePoligonoCompleto => polygonDrawer?.EstaCompleto ?? false;
        public int NumeroVertices => polygonDrawer?.Vertices?.Count ?? 0;
    }
}
