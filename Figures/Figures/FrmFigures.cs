using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public partial class FrmFigures : Form
    {
        public FrmFigures()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ToolS_pentagono_Click(object sender, EventArgs e)
        {
            FrmPentagono pentagono=FrmPentagono.Get_Form();
            pentagono.MdiParent = this;
            pentagono.Show();
        }

        private void FrmFigures_Load(object sender, EventArgs e)
        {

        }

        private void ToolS_Hexagono_Click(object sender, EventArgs e)
        {
            FrmHexagono hexagono = FrmHexagono.Get_Form();
            hexagono.MdiParent = this;
            hexagono.Show();
        }

        private void ToolS_Heptagono_Click(object sender, EventArgs e)
        {
            FrmHeptagono heptagono = FrmHeptagono.Get_Form();
            heptagono.MdiParent = this;
            heptagono.Show();
        }

        private void ToolS_Octagono_Click(object sender, EventArgs e)
        {
            FrmOctagono octagono = FrmOctagono.Get_Form();
            octagono.MdiParent = this;
            octagono.Show();
        }

        private void ToolS_Eneagono_Click(object sender, EventArgs e)
        {
            FrmEneagono eneagono = FrmEneagono.Get_Form();
            eneagono.MdiParent = this;
            eneagono.Show();
        }

        private void ToolS_Decagono_Click(object sender, EventArgs e)
        {
            FrmDecagono decagono = FrmDecagono.Get_Form();
            decagono.MdiParent = this;
            decagono.Show();
        }

        private void ToolS_Triangulo_Click(object sender, EventArgs e)
        {
            FrmTriangulo triangulo = FrmTriangulo.Get_Form();
            triangulo.MdiParent = this;
            triangulo.Show();
        }

        private void ToolS_Rectangulo_Click(object sender, EventArgs e)
        {
            FrmRectangulo rectangulo = FrmRectangulo.Get_Form();
            rectangulo.MdiParent = this;
            rectangulo.Show();
        }

        private void ToolS_Rombo_Click(object sender, EventArgs e)
        {
            FrmRombo rombo = FrmRombo.Get_Form();
            rombo.MdiParent = this;
            rombo.Show();
        }

        private void ToolS_Romboide_Click(object sender, EventArgs e)
        {
            FrmRomboide romboide = FrmRomboide.Get_Form();
            romboide.MdiParent = this;
            romboide.Show();
        }

        private void ToolS_Trapecio_Click(object sender, EventArgs e)
        {
            FrmTrapecio trapecio = FrmTrapecio.Get_Form();
            trapecio.MdiParent = this;
            trapecio.Show();
        }

        private void ToolS_Trapezoide_Click(object sender, EventArgs e)
        {
            FrmTrapezoide trapezoide = FrmTrapezoide.Get_Form();
            trapezoide.MdiParent = this;
            trapezoide.Show();
        }

        private void ToolS_Deltoide_Click(object sender, EventArgs e)
        {
            FrmDeltoide deltoide = FrmDeltoide.Get_Form();
            deltoide.MdiParent = this;
            deltoide.Show();
        }

        private void tool_Square_Click(object sender, EventArgs e)
        {
            FrmCuadrado cuadrado = FrmCuadrado.Get_Form();
            cuadrado.MdiParent = this;
            cuadrado.Show();
        }

        private void ToolS_Circulo_Click(object sender, EventArgs e)
        {
            FrmCirculo circulo = FrmCirculo.Get_Form();
            circulo.MdiParent = this;
            circulo.Show();
        }

        private void ToolS_Ovalo_Click(object sender, EventArgs e)
        {
            FrmOvalo ovalo = FrmOvalo.Get_Form();
            ovalo.MdiParent = this;
            ovalo.Show();
        }

        private void ToolS_Elipse_Click(object sender, EventArgs e)
        {
            FrmElipse elipse = FrmElipse.Get_Form();
            elipse.MdiParent = this;
            elipse.Show();
        }
    }
}
