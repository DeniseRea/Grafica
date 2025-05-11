using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    class Triangle
    {
        protected double SideA { get; set; }
        protected double SideB { get; set; }
        protected double SideC { get; set; }

        public Triangle(double sideA, double sideB, double sideC)
        {
            // Validar antes de asignar valores
            if (IsValidTriangle(sideA, sideB, sideC))
            {
                
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
            }

        }

        // Método para validar si un triángulo es válido
        public static bool IsValidTriangle(double sideA, double sideB, double sideC)
        {
            // Verificar que los lados sean positivos
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                return false;
            }

            // Verificar la desigualdad triangular
            if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
            {
                return false;
            }

            return true;
        }

        public double CalculateArea()
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }

        public double CalculatePerimeter()
        {
            return SideA + SideB + SideC;
        }

        public void clean(TextBox sideA, TextBox sideB, TextBox sideC, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            sideA.Text = "";
            sideB.Text = "";
            sideC.Text = "";
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
