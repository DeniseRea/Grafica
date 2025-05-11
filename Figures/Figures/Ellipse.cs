using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Ellipse
    {
        protected double SemiMajor { get; set; }  // Semieje mayor (a)
        protected double SemiMinor { get; set; }  // Semieje menor (b)

        public Ellipse(double semiMajor, double semiMinor)
        {
            // Validar antes de asignar valores
            if (IsValidEllipse(semiMajor, semiMinor))
            {
                SemiMajor = semiMajor;
                SemiMinor = semiMinor;
            }
        }

        // Constructor por defecto
        public Ellipse()
        {
        }

        // Método para validar si una elipse es válida
        public static bool IsValidEllipse(double semiMajor, double semiMinor)
        {
            // Verificar que el semieje mayor sea efectivamente mayor o igual al menor
            if (semiMajor < semiMinor)
            {
                return false;
            }
            
            return true;
        }

        public double CalculateArea()
        {
            // Área de la elipse = π * a * b  (donde a y b son los semiejes)
            return Math.PI * SemiMajor * SemiMinor;
        }

        public double CalculatePerimeter()
        {
            // Aproximación de Ramanujan para el perímetro de una elipse
            // P ≈ π * (a + b) * (1 + (3h/(10 + √(4-3h))))
            // donde h = (a-b)²/(a+b)²
            
            double a = SemiMajor;
            double b = SemiMinor;
            
            double h = Math.Pow(a - b, 2) / Math.Pow(a + b, 2);
            double perimeter = Math.PI * (a + b) * (1 + (3 * h / (10 + Math.Sqrt(4 - 3 * h))));
            
            return perimeter;
        }

        // Método para calcular la excentricidad
        public double CalculateEccentricity()
        {
            // Excentricidad = c/a donde c = √(a² - b²)
            double c = Math.Sqrt(Math.Pow(SemiMajor, 2) - Math.Pow(SemiMinor, 2));
            return c / SemiMajor;
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