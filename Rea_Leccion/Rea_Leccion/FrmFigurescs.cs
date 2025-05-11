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
    public partial class FrmFigurescs : Form
    {
        public FrmFigurescs()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void FrmFigurescs_Load(object sender, EventArgs e)
        {

        }

        private void figurasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pentagono_Click(object sender, EventArgs e)
        {
            FrmPentagono pentagono = new FrmPentagono();
            pentagono.MdiParent = this;
            pentagono.Show();
        }

        private void hexagono_Click(object sender, EventArgs e)
        {

        }
    }
}
