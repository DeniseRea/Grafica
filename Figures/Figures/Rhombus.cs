using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Rhombus
    {
        protected double Side { get; set; }    // Lado del rombo
        protected double Height { get; set; }  // Altura del rombo

        public Rhombus(double side, double height)
        {
            this.Side = side;
            this.Height = height;
        }

        // Constructor por defecto
        public Rhombus()
        {
        }

        public double CalculateArea()
        {
            // Área del rombo = lado * altura
            return Side * Height;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del rombo = 4 * lado
            return 4 * Side;
        }

        public void clean(TextBox side, TextBox height, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            side.Text = "";
            height.Text = "";
            area.Text = "";
            perimeter.Text = "";
        }

        public void close(Form form)
        {
            // Cerrar el formulario
            form.Close();
        }
    }
}
