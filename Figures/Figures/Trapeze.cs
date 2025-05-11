using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Trapeze
    {
        protected double BaseMayor { get; set; }    // Base mayor del trapecio
        protected double BaseMenor { get; set; }    // Base menor del trapecio
        protected double LadoIzquierdo { get; set; } // Lado izquierdo del trapecio
        protected double LadoDerecho { get; set; }   // Lado derecho del trapecio
        protected double Altura { get; set; }        // Altura del trapecio

        public Trapeze(double baseMayor, double baseMenor, double ladoIzquierdo, double ladoDerecho, double altura)
        {
            // Validar antes de asignar valores
            if (IsValidTrapeze(baseMayor, baseMenor, ladoIzquierdo, ladoDerecho, altura))
            {
                BaseMayor = baseMayor;
                BaseMenor = baseMenor;
                LadoIzquierdo = ladoIzquierdo;
                LadoDerecho = ladoDerecho;
                Altura = altura;
            }
        }

        // Constructor por defecto
        public Trapeze()
        {
            BaseMayor = 2;
            BaseMenor = 1;
            LadoIzquierdo = Math.Sqrt(1.25);
            LadoDerecho = Math.Sqrt(1.25);
            Altura = 1;
        }

        // Método para validar si un trapecio es válido
        public static bool IsValidTrapeze(double baseMayor, double baseMenor, double ladoIzquierdo, double ladoDerecho, double altura)
        {
            
            // Verificar que la base mayor sea efectivamente mayor que la base menor
            if (baseMayor <= baseMenor)
            {
                return false;
            }
            
            // Verificar que los lados laterales sean lo suficientemente largos para la altura
            // Se usa el teorema de Pitágoras para esta verificación
            double distanciaIzq = (baseMayor - baseMenor) / 2;
            double distanciaDer = (baseMayor - baseMenor) / 2;
            
            if (Math.Sqrt(distanciaIzq * distanciaIzq + altura * altura) > ladoIzquierdo)
            {
                return false;
            }
            
            if (Math.Sqrt(distanciaDer * distanciaDer + altura * altura) > ladoDerecho)
            {
                return false;
            }
            
            return true;
        }

        public double CalculateArea()
        {
            // Área del trapecio = ((Base mayor + Base menor) * altura) / 2
            return ((BaseMayor + BaseMenor) * Altura) / 2;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del trapecio = Base mayor + Base menor + Lado izquierdo + Lado derecho
            return BaseMayor + BaseMenor + LadoIzquierdo + LadoDerecho;
        }

        public void clean(TextBox baseMayor, TextBox baseMenor, TextBox ladoIzquierdo, TextBox ladoDerecho, TextBox altura, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            baseMayor.Text = "";
            baseMenor.Text = "";
            ladoIzquierdo.Text = "";
            ladoDerecho.Text = "";
            altura.Text = "";
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
