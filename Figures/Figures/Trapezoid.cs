using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Trapezoid
    {
        protected double LargBase { get; set; }    // Base mayor del trapezoide
        protected double ShortBase { get; set; }   // Base menor del trapezoide
        protected double LeftSide { get; set; }    // Lado izquierdo del trapezoide
        protected double RightSide { get; set; }   // Lado derecho del trapezoide
        protected double Diagonal { get; set; }    // Diagonal del trapezoide
        protected double Height { get; private set; } // Altura del trapezoide

        public Trapezoid(double largBase, double shortBase, double leftSide, double rightSide, double diagonal)
        {
            // Validar antes de asignar valores
            if (IsValidTrapezoid(largBase, shortBase, leftSide, rightSide, diagonal))
            {
                LargBase = largBase;
                ShortBase = shortBase;
                LeftSide = leftSide;
                RightSide = rightSide;
                Diagonal = diagonal;
                CalculateHeight();
            }
        }

        // Constructor por defecto
        public Trapezoid()
        {
            LargBase = 4;
            ShortBase = 2;
            LeftSide = 2.5;
            RightSide = 2.5;
            Diagonal = 4.5;
            CalculateHeight();
        }

        // Método para validar si un trapezoide es válido
        public static bool IsValidTrapezoid(double largBase, double shortBase, double leftSide, double rightSide, double diagonal)
        {
            // Verificar que todos los valores sean positivos
            if (largBase <= 0 || shortBase <= 0 || leftSide <= 0 || rightSide <= 0 || diagonal <= 0)
            {
                return false;
            }
            
            // Verificar que la base mayor sea efectivamente mayor que la base menor
            if (largBase <= shortBase)
            {
                return false;
            }
            
            // Verificar que la diagonal sea posible (debe ser mayor que la diferencia de bases y menor que la suma de lados)
            double minDiagonal = Math.Abs(largBase - shortBase);
            double maxDiagonal = leftSide + rightSide;
            
            if (diagonal < minDiagonal || diagonal > maxDiagonal)
            {
                return false;
            }
            
            // Verificar que los lados laterales sean posibles usando la ley de los triángulos
            if (leftSide + shortBase <= largBase || rightSide + shortBase <= largBase)
            {
                return false;
            }
            
            return true;
        }

        // Método para calcular la altura del trapezoide
        private void CalculateHeight()
        {
            // Usar la fórmula del área de un triángulo a partir de los lados
            // Dividir el trapezoide en dos triángulos usando la diagonal
            
            double s1 = (LeftSide + ShortBase + Diagonal) / 2; // Semiperímetro del primer triángulo
            double area1 = Math.Sqrt(s1 * (s1 - LeftSide) * (s1 - ShortBase) * (s1 - Diagonal));
            
            double s2 = (RightSide + LargBase + Diagonal) / 2; // Semiperímetro del segundo triángulo
            double area2 = Math.Sqrt(s2 * (s2 - RightSide) * (s2 - LargBase) * (s2 - Diagonal));
            
            // Calcular la altura a partir del área total
            double totalArea = area1 + area2;
            Height = 2 * totalArea / (LargBase + ShortBase);
        }

        public double CalculateArea()
        {
            // Área del trapezoide = ((Base mayor + Base menor) * altura) / 2
            return ((LargBase + ShortBase) * Height) / 2;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del trapezoide = suma de todos los lados
            return LargBase + ShortBase + LeftSide + RightSide;
        }

        public double GetHeight()
        {
            return Height;
        }

        public void clean(TextBox largBase, TextBox shortBase, TextBox leftSide, TextBox rightSide, 
                          TextBox diagonal, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            largBase.Text = "";
            shortBase.Text = "";
            leftSide.Text = "";
            rightSide.Text = "";
            diagonal.Text = "";
            //height.Text = "";
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