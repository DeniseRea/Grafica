using System;
using System.Windows.Forms;
using Figuras;

namespace Padre_Hijo
{
    public partial class FrmCuadrado : Form
    {
        private static FrmCuadrado cuadrado;
        Square cuadrado1 = new Square(); // Instancia de la clase Square

        private FrmCuadrado()
        {
            InitializeComponent();

            // Configurar el rango del TrackBar
            track_tama.Minimum = 1;
            track_tama.Maximum = 10;
            track_tama.Value = 1;

            track_tama.SmallChange = 1;
            track_tama.LargeChange = 2;
        }

        public static FrmCuadrado Get_Form()
        {
            if (cuadrado == null || cuadrado.IsDisposed)
            {
                cuadrado = new FrmCuadrado();
            }
            return cuadrado;
        }

        private void FrmCuadrado_Load(object sender, EventArgs e)
        {
            // Dibujar con el valor inicial del trackbar
            float scale = track_tama.Value;
            cuadrado1.PlotShape(pictureBox1, scale);
        }

        private void actualizarTam(object sender, EventArgs e)
        {
            // Limpiar el canvas antes de redibujar
            cuadrado1.ClearCanvas(pictureBox1);

            // Obtener el factor de escala actual
            float scale = track_tama.Value;
            cuadrado1.PlotShape(pictureBox1, scale);
        }
    }
}