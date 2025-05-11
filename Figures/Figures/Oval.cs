using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Oval
    {
        protected double SemiMajorAxis { get; set; }  // Semieje mayor
        protected double SemiMinorAxis { get; set; }  // Semieje menor

        public Oval(double semiMajorAxis, double semiMinorAxis)
        {
            this.SemiMajorAxis = semiMajorAxis;
            this.SemiMinorAxis = semiMinorAxis;
        }

        // Constructor por defecto
        public Oval()
        {
        }

        // Método para validar si una elipse es válida
        public static bool IsValidOval(TextBox semiMajor, TextBox semiMinor)
        {
            // Verificar que los semiejes sean números positivos
            if (double.TryParse(semiMajor.Text, out double a) && double.TryParse(semiMinor.Text, out double b))
            {
                if (a > b)
                {
                    return true;
                }
            }
            return false;
        }

        public double CalculateArea()
        {
            // Área de la elipse = π * a * b  (donde a y b son los semiejes)
            return Math.PI * SemiMajorAxis * SemiMinorAxis;
        }

        public double CalculatePerimeter()
        {
            // Aproximación de Ramanujan para el perímetro de una elipse
            // P ≈ π * (a + b) * (1 + (3h/(10 + √(4-3h))))
            // donde h = (a-b)²/(a+b)²
            
            double a = SemiMajorAxis;
            double b = SemiMinorAxis;
            
            double h = Math.Pow(a - b, 2) / Math.Pow(a + b, 2);
            double perimeter = Math.PI * (a + b) * (1 + (3 * h / (10 + Math.Sqrt(4 - 3 * h))));
            
            return perimeter;
        }

        public void clean(TextBox semiMajorAxis, TextBox semiMinorAxis, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            semiMajorAxis.Text = "";
            semiMinorAxis.Text = "";
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