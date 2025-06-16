using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Padre_Hijo
{
  

    public partial class FrmFlower : Form
    {
        // Instancia de la clase Pentagon
        private Pentagon pentagon;

        public FrmFlower()
        {
            InitializeComponent();
            pentagon = new Pentagon();
        }

        private void btn_graph_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener la dimensión del TextBox
                float dimension = float.Parse(txt_dimension.Text);

                // Limpiar el canvas antes de dibujar
                pentagon.ClearCanvas(pictureBox1);

                // Dibujar el pentágono con la dimensión especificada
                pentagon.PlotShape(pictureBox1, dimension);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un número válido para la dimensión.",
                                "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}