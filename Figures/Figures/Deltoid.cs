using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Deltoid
    {
        protected double ShortSide { get; set; }   // Lado corto del deltoide
        protected double LongSide { get; set; }    // Lado largo del deltoide
        protected double DiagonalP { get; set; }   // Diagonal principal (la que une vértices de lados diferentes)
        protected double DiagonalS { get; set; }   // Diagonal secundaria (la que une vértices de lados iguales)

        public Deltoid(double shortSide, double longSide, double diagonalP, double diagonalS)
        {
            // Validar antes de asignar valores
            if (IsValidDeltoid(shortSide, longSide, diagonalP, diagonalS))
            {
                ShortSide = shortSide;
                LongSide = longSide;
                DiagonalP = diagonalP;
                DiagonalS = diagonalS;
            }
        }

        // Constructor por defecto
        public Deltoid()
        {
            ShortSide = 3;
            LongSide = 5;
            DiagonalP = 8;
            DiagonalS = 4;
        }

        // Método para validar si un deltoide es válido
        public static bool IsValidDeltoid(double shortSide, double longSide, double diagonalP, double diagonalS)
        {
            // Verificar que todos los valores sean positivos
            if (shortSide <= 0 || longSide <= 0 || diagonalP <= 0 || diagonalS <= 0)
            {
                return false;
            }
            
            // Verificar que el lado largo sea mayor que el lado corto
            if (longSide <= shortSide)
            {
                return false;
            }
            
            // Verificar que las diagonales sean posibles
            // En un deltoide, cada diagonal forma dos triángulos
            
            // Para la diagonal principal:
            // Verificamos si puede formar triángulos con los lados
            if (shortSide + longSide <= diagonalP)
            {
                return false;
            }
            
            // Para la diagonal secundaria:
            // Verificamos si puede formar triángulos con los lados
            if (2 * shortSide <= diagonalS || 2 * longSide <= diagonalS)
            {
                return false;
            }
            
            return true;
        }

        public double CalculateArea()
        {
            // Área del deltoide = (Diagonal principal * Diagonal secundaria) / 2
            return (DiagonalP * DiagonalS) / 2;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del deltoide = 2 * (Lado corto + Lado largo)
            return 2 * (ShortSide + LongSide);
        }

        public void clean(TextBox shortSide, TextBox longSide, TextBox diagonalP, TextBox diagonalS, 
                          TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            shortSide.Text = "";
            longSide.Text = "";
            diagonalP.Text = "";
            diagonalS.Text = "";
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