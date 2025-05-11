using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rea_Leccion
{
     abstract class Figura
    {
        int numlados;
        float medida;
        float radio;
        Graphics graph;
        Pen pen;

        public Figura(int numlados, float medida)
        {
            this.numlados = numlados;
            this.medida = medida;
        }

        public double calcularPerimetro()
        {
            return numlados * medida;
        }

        public void dibujarFigura(PictureBox pic)
        {

        }
    }
}
