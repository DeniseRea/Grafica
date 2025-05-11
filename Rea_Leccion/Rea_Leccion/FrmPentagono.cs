using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rea_Leccion
{
    public partial class FrmPentagono : Form
    {
        Pentagono pentagono= new Pentagono();
        public FrmPentagono()
        {
            InitializeComponent();
        }

        private void FrmPentagono_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            txt_area.Text = pentagono.CalcularArea().ToString("F2");
            txt_perimeter.Text = pentagono.CalcularPerimetro().ToString("F2");
            pentagono.Dibujar(pic_Grafico);
        }
    }
}
