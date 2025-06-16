using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Padre_Hijo
{
    public partial class FrmHilogram : Form
    {
        private static FrmHilogram _instance;
        private int numberOfLines = 50; // Valor predeterminado

        public static FrmHilogram Get_Form()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new FrmHilogram();
            }
            return _instance;
        }

        public FrmHilogram()
        {
            InitializeComponent();

            // Configurar el PictureBox
            pc_Graph.BackColor = Color.White;
            pc_Graph.Paint += Pc_Graph_Paint;

            // Si tienes trackBar, configúralo aquí
            // trackBar.Minimum = 10;
            // trackBar.Maximum = 150;
            // trackBar.Value = numberOfLines;
        }

        private void Pc_Graph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawColorfulStringArt(g, numberOfLines);
        }

        private void DrawColorfulStringArt(Graphics g, int lines)
        {
            int width = pc_Graph.Width;
            int height = pc_Graph.Height;

            // Usar todo el espacio disponible (cuadrado)
            int size = Math.Min(width, height);

            // Centrar el cuadrado en el PictureBox
            int xOffset = (width - size) / 2;
            int yOffset = (height - size) / 2;

            // Espacio entre líneas
            float xSpacing = (float)size / lines;
            float ySpacing = (float)size / lines;

            // Crear puntos en los cuatro lados del cuadrado
            Point[] topPoints = new Point[lines + 1];    // Puntos en el borde superior
            Point[] rightPoints = new Point[lines + 1];  // Puntos en el borde derecho
            Point[] bottomPoints = new Point[lines + 1]; // Puntos en el borde inferior
            Point[] leftPoints = new Point[lines + 1];   // Puntos en el borde izquierdo

            // Crear puntos equidistantes en cada lado del cuadrado
            for (int i = 0; i <= lines; i++)
            {
                // Puntos en el borde superior (de izquierda a derecha)
                topPoints[i] = new Point(
                    xOffset + (int)(i * xSpacing),
                    yOffset);

                // Puntos en el borde derecho (de arriba a abajo)
                rightPoints[i] = new Point(
                    xOffset + size,
                    yOffset + (int)(i * ySpacing));

                // Puntos en el borde inferior (de derecha a izquierda)
                bottomPoints[i] = new Point(
                    xOffset + size - (int)(i * xSpacing),
                    yOffset + size);

                // Puntos en el borde izquierdo (de abajo a arriba)
                leftPoints[i] = new Point(
                    xOffset,
                    yOffset + size - (int)(i * ySpacing));
            }

            // Definir colores brillantes
            Color redColor = Color.FromArgb(200, 255, 0, 0);     // Rojo
            Color blueColor = Color.FromArgb(200, 30, 144, 255); // Azul
            Color greenColor = Color.FromArgb(200, 0, 180, 0);   // Verde
            Color yellowColor = Color.FromArgb(200, 255, 200, 0); // Amarillo

            // Patrón rojo (esquina superior izquierda): Superior a Izquierdo
            using (Pen redPen = new Pen(redColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(redPen, topPoints[i], leftPoints[i]);
                }
            }

            // Patrón azul (esquina superior derecha): Superior a Derecho
            using (Pen bluePen = new Pen(blueColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(bluePen, topPoints[i], rightPoints[lines - i]);
                }
            }

            // Patrón verde (esquina inferior derecha): Inferior a Derecho
            using (Pen greenPen = new Pen(greenColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(greenPen, bottomPoints[i], rightPoints[i]);
                }
            }

            // Patrón amarillo (esquina inferior izquierda): Inferior a Izquierdo
            using (Pen yellowPen = new Pen(yellowColor, 1))
            {
                for (int i = 0; i <= lines; i++)
                {
                    g.DrawLine(yellowPen, bottomPoints[i], leftPoints[lines - i]);
                }
            }

            // Dibujar el borde del cuadrado
            using (Pen borderPen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(borderPen, xOffset, yOffset, size, size);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // No se necesita acción
        }

        private void grp_Graph_Enter(object sender, EventArgs e)
        {
            // No se necesita acción
        }

        private void FrmHilogram_Load(object sender, EventArgs e)
        {
            // Puedes inicializar el formulario aquí si es necesario
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            // Simplemente invalidamos el PictureBox para forzar su redibujado
            pc_Graph.Invalidate();
        }
    }
}