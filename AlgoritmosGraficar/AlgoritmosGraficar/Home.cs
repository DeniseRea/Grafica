using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
             this.IsMdiContainer = true;
        }

        private void algoritmosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGraficar graficarForm = FrmGraficar.Instancia;
            graficarForm.MdiParent = this; // Establecer el formulario padre
            graficarForm.Show(); // Mostrar el formulario
        }

        private void pintadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInundacion inundacionForm = FrmInundacion.Instancia;
            inundacionForm.MdiParent = this; 
            inundacionForm.Show(); 
        }
    }
}
