using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DibujarLineasApp
{
    public partial class FrmLineas : Form
    {
        int xStart ;
        int yStart ;
        Circle circle;
        Line line;
        public FrmLineas()
        {
            InitializeComponent();
            circle = new Circle(xStart, yStart);
            circle.DrawCircle(pct_Graphic);
        }

        private void FrmLineas_Load(object sender, EventArgs e)
        {
        }

        private void pct_Graphic_MouseClick(object sender, MouseEventArgs e)
        {
            // Obtén las coordenadas del clic
            int xFinal = e.X;
            int yFinal = e.Y;
            // Crea un círculo con un radio fijo (por ejemplo, 20)
            circle= new Circle(xFinal, yFinal);
            circle.DrawCircle(pct_Graphic);
            line= new Line(xStart, yStart, xFinal, yFinal);
            line.DrawLine(pct_Graphic);
            // Actualiza las coordenadas de inicio para el siguiente clic   
            xStart = xFinal;
            yStart = yFinal;
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            // Resetea las coordenadas iniciales
            xStart = 0;
            yStart = 0;
            circle.ClearCircle(pct_Graphic);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            // Cierra el formulario
            this.Close();
        }
    }
}
