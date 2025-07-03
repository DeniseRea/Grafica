using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphic_Algoritms
{
    /// <summary>
    /// Canvas mejorado para mantener el fondo mientras se dibujan elementos
    /// </summary>
    public class EnhancedCanvas
    {
        private PictureBox pictureBox;
        private Bitmap fondoBase;
        private Graphics graphicsFondo;

        public EnhancedCanvas(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            InicializarCanvas();
        }

        /// <summary>
        /// Inicializa el canvas con dimensiones adecuadas
        /// </summary>
        private void InicializarCanvas()
        {
            if (pictureBox.Width > 0 && pictureBox.Height > 0)
            {
                CrearFondoBase();
            }
        }

        /// <summary>
        /// Crea el fondo base del canvas
        /// </summary>
        public void CrearFondoBase()
        {
            fondoBase?.Dispose();
            fondoBase = new Bitmap(Math.Max(pictureBox.Width, 800), Math.Max(pictureBox.Height, 600));

            using (Graphics g = Graphics.FromImage(fondoBase))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }

            ActualizarCanvas();
        }

        /// <summary>
        /// Establece una imagen de fondo personalizada
        /// </summary>
        public void EstablecerFondo(Image fondo)
        {
            if (fondoBase != null && fondo != null)
            {
                using (Graphics g = Graphics.FromImage(fondoBase))
                {
                    g.Clear(Color.White);
                    g.DrawImage(fondo, 0, 0, fondoBase.Width, fondoBase.Height);
                }
                ActualizarCanvas();
            }
        }

        /// <summary>
        /// Obtiene los graphics para dibujar en el fondo
        /// </summary>
        public Graphics ObtenerGraphicsFondo()
        {
            if (fondoBase == null)
            {
                CrearFondoBase();
            }

            var g = Graphics.FromImage(fondoBase);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            return g;
        }

        /// <summary>
        /// Actualiza la visualización del canvas
        /// </summary>
        public void ActualizarCanvas()
        {
            if (fondoBase != null)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = new Bitmap(fondoBase);
                pictureBox.Invalidate();
            }
        }

        /// <summary>
        /// Limpia el canvas
        /// </summary>
        public void Limpiar()
        {
            if (fondoBase != null)
            {
                using (Graphics g = Graphics.FromImage(fondoBase))
                {
                    g.Clear(Color.White);
                }
                ActualizarCanvas();
            }
        }

        /// <summary>
        /// Redimensiona el canvas si es necesario
        /// </summary>
        public void Redimensionar()
        {
            if (pictureBox.Width != fondoBase?.Width || pictureBox.Height != fondoBase?.Height)
            {
                CrearFondoBase();
            }
        }

        /// <summary>
        /// Obtiene una copia del fondo actual
        /// </summary>
        public Bitmap ObtenerFondo()
        {
            return fondoBase != null ? new Bitmap(fondoBase) : null;
        }

        public void Dispose()
        {
            graphicsFondo?.Dispose();
            fondoBase?.Dispose();
        }
    }
}
