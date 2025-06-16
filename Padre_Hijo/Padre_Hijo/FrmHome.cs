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
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void toolStrip_Cuadrado_Click(object sender, EventArgs e)
        {
            //FrmCuadrado cuadrado=new FrmCuadrado();
            FrmCuadrado cuadrado = FrmCuadrado.Get_Form();
            //aqui le decimos que el formulario Home es el padre
            cuadrado.MdiParent = this; 
            cuadrado.Show();
        }

        private void toolStrip_Lineas_Click(object sender, EventArgs e)
        {
            FrmHilogram hilogram = new FrmHilogram();
            hilogram.MdiParent = this;
            hilogram.Show();

        }

        private void ToolStrip_Cuadrangulos_Click(object sender, EventArgs e)
        {

        }

        private void florMargaritaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFlower frm= new FrmFlower();
            frm.Show();
        }
    }
}
