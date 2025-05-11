using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Circle
    {
        protected double Radius { get; set; }  // Radio del círculo

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        // Constructor por defecto
        public Circle()
        {
            
        }


        public double CalculateArea()
        {
            // Área del círculo = π * r²
            return Math.PI * Radius * Radius;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del círculo = 2 * π * r
            return 2 * Math.PI * Radius;
        }

        public double CalculateDiameter()
        {
            // Diámetro = 2 * radio
            return 2 * Radius;
        }

        public void clean(TextBox radius, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            radius.Text = "";
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