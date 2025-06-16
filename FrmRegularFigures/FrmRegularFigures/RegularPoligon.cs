using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmRegularFigures
{
    
    public partial class RegularPoligon : Form
    {
        public RegularPoligon()
        {
            InitializeComponent();
        }



// Variable para mantener referencia al objeto Draw_Poligon activo
private Draw_Poligon dibuja;

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(txt_num.Text);
            float radio = Convert.ToSingle(txt_radius.Text);
            // Guarda la referencia al objeto dibuja para poder usarlo en otros métodos
            dibuja = new Draw_Poligon(n, radio);
            dibuja.PlotPolygon(pictureBox1);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            dibuja = new Draw_Poligon();
            dibuja.clean(pictureBox1, txt_num, txt_radius);
        }

        // Añade este manejador de eventos para el clic del mouse en el PictureBox
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Verificar que existe un polígono dibujado y que el PictureBox tiene una imagen
            if (dibuja != null && !string.IsNullOrEmpty(txt_num.Text) &&
                !string.IsNullOrEmpty(txt_radius.Text) && pictureBox1.Image != null)
            {
                // Crear una instancia de FloodFill y aplicar el relleno
                FloodFill floodFill = new FloodFill(Color.LightBlue, Color.Black);
                floodFill.Fill(pictureBox1, e.Location);
            }
        }

        private void RegularPoligon_Load(object sender, EventArgs e)
        {
            // Suscribe el manejador de eventos al evento MouseClick del PictureBox
            pictureBox1.MouseClick += pictureBox1_MouseClick;
        }
        
    }
}
