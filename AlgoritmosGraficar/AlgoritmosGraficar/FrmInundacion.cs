/**
 * Clase FrmInundacion
 * 
 * Formulario que implementa una demostración interactiva del algoritmo de inundación (flood fill).
 * Muestra un rombo en el centro del formulario y permite al usuario hacer clic dentro de él
 * para inicilar la animación del algoritmo de relleno.
 * 
 * Implementa el patrón Singleton para asegurar una única instancia del formulario.
 *
 * @author Denise Rea
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public partial class FrmInundacion : Form
    {
        private Rombo rombo;
        private Color colorRomboNaranja = Color.FromArgb(255, 168, 38);

        /**
         * Implementación del patrón Singleton para asegurar una única instancia
         */
        private static FrmInundacion instancia;

        /**
         * Propiedad para acceder a la instancia única del formulario
         * Si no existe o fue eliminada, crea una nueva instancia
         */
        public static FrmInundacion Instancia
        {
            get
            {
                if (instancia == null || instancia.IsDisposed)
                {
                    instancia = new FrmInundacion();
                }
                return instancia;
            }
        }

        /**
         * Constructor del formulario
         * Inicializa la interfaz y configura el rombo para la demostración
         */
        public FrmInundacion()
        {
            InitializeComponent();

            // Configurar como formulario hijo
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Inicializar el PictureBox y el rombo
            ConfigurarPictureBox();
            InicializarRombo();
            rombo.DibujarBorde(pictureBox1);

            // Asignar el evento de clic del mouse
            pictureBox1.MouseClick += PictureBox1_MouseClick;
        }

        /**
         * Configura el PictureBox creando un lienzo en blanco si no existe
         */
        private void ConfigurarPictureBox()
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.Clear(Color.White);
                }
                pictureBox1.Refresh();
            }
        }

        /**
         * Inicializa un rombo en el centro del PictureBox
         */
        private void InicializarRombo()
        {
            // Crear un nuevo rombo en el centro del PictureBox
            Point centro = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            rombo = new Rombo(centro, 137, Color.Black, colorRomboNaranja, false);
        }

        /**
         * Reinicia la demostración limpiando el PictureBox y redibujando el rombo vacío
         */
        private void btn_reset_Click(object sender, EventArgs e)
        {
            Rombo.LimpiarPictureBox(pictureBox1);
            InicializarRombo();
            rombo.DibujarBorde(pictureBox1);
        }

        /**
         * Maneja el evento de clic en el PictureBox
         * Si el clic ocurre dentro del rombo, inicia la animación de relleno
         */
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (rombo != null)
            {
                rombo.ProcesarClicAnimado(e.Location, pictureBox1, colorRomboNaranja, 10); 
            }
        }
    }
}
