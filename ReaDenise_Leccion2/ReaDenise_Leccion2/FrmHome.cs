using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaDenise_Leccion2
{
    public partial class FrmHome : Form
    {
        private Bitmap lienzo;
        private Timer timerAnimacion;
        private List<Point[]> lineasActuales;
        private List<List<Point>> puntosBresenham;
        private int lineaActual;
        private int puntoActual;
        private Color colorActual;

        public FrmHome()
        {
            InitializeComponent();
            InicializarLienzo();

            timerAnimacion = new Timer();
            timerAnimacion.Interval = 5;
            timerAnimacion.Tick += TimerAnimacion_Tick;
        }

        private void InicializarLienzo()
        {
            lienzo = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = lienzo;

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
            }
        }

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (lineaActual < lineasActuales.Count)
            {
                if (puntoActual == 0 && puntosBresenham[lineaActual] == null)
                {
                    Point[] puntos = lineasActuales[lineaActual];
                    if (puntos.Length >= 2)
                    {
                        puntosBresenham[lineaActual] = algoritmo.DibujarLineaBresenham(puntos[0], puntos[1]);
                    }
                    else
                    {
                        lineaActual++;
                        return;
                    }
                }

                List<Point> puntosLinea = puntosBresenham[lineaActual];

                if (puntoActual < puntosLinea.Count)
                {
                    Point punto = puntosLinea[puntoActual];
                    if (punto.X >= 0 && punto.X < lienzo.Width &&
                        punto.Y >= 0 && punto.Y < lienzo.Height)
                    {
                        lienzo.SetPixel(punto.X, punto.Y, colorActual);
                    }

                    puntoActual++;

                    int puntosADibujarPorTick = Math.Max(1, puntosLinea.Count / 100);
                    for (int i = 1; i < puntosADibujarPorTick && (puntoActual + i) < puntosLinea.Count; i++)
                    {
                        punto = puntosLinea[puntoActual + i - 1];
                        if (punto.X >= 0 && punto.X < lienzo.Width &&
                            punto.Y >= 0 && punto.Y < lienzo.Height)
                        {
                            lienzo.SetPixel(punto.X, punto.Y, colorActual);
                        }
                        puntoActual++;
                    }
                }
                else
                {
                    lineaActual++;
                    puntoActual = 0;
                }

                pictureBox1.Invalidate();
            }
            else
            {
                timerAnimacion.Stop();
            }
        }

        private void IniciarAnimacion(List<Point[]> lineas, Color color)
        {
            timerAnimacion.Stop();

            lineasActuales = lineas;
            colorActual = color;
            lineaActual = 0;
            puntoActual = 0;

            puntosBresenham = new List<List<Point>>();
            for (int i = 0; i < lineas.Count; i++)
            {
                puntosBresenham.Add(null);
            }

            timerAnimacion.Start();
        }


        private void btn_Pentagono_Click(object sender, EventArgs e)
        {
            InicializarLienzo();

            Point centro = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            int radio = (int)(Math.Min(pictureBox1.Width, pictureBox1.Height) * 0.4);

            List<Point[]> lineasPentagono = algoritmo.Figuras.PentagramaPentagono(centro, radio);

            IniciarAnimacion(lineasPentagono, Color.Black);
        }

        private void btn_Malla_Click(object sender, EventArgs e)
        {
            InicializarLienzo();

            int tamañoCelda = 50;
            int anchoTotal = tamañoCelda * 4;
            int altoTotal = tamañoCelda * 4;

            Point esquina = new Point(
                (pictureBox1.Width - anchoTotal) / 2,
                (pictureBox1.Height - altoTotal) / 2
            );

            List<Point[]> lineasMalla = algoritmo.Figuras.Malla4x4(esquina, tamañoCelda);

            IniciarAnimacion(lineasMalla, Color.Black);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            timerAnimacion.Stop();
            InicializarLienzo();
            lineasActuales = null;
            puntosBresenham = null;
        }
    }
    

     
}
