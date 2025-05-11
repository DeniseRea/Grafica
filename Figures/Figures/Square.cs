using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Square
    {
        protected double Side { get; set; }  // Lado del cuadrado

        public Square(double side)
        {
            this.Side = side;
        }

        // Constructor por defecto
        public Square()
        {
            
        }

        // Método para validar si un cuadrado es válido

        public double CalculateArea()
        {
            // Área del cuadrado = lado²
            return Side * Side;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del cuadrado = 4 * lado
            return 4 * Side;
        }

        public void clean(TextBox side, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            side.Text = "";
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