using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Rhomboid
    {
        protected double BaseLength { get; set; }    // Base del romboide
        protected double LateralSide { get; set; }   // Lado lateral del romboide
        protected double Height { get; set; }        // Altura del romboide

        public Rhomboid(double baseLength, double lateralSide, double height)
        {
            this.BaseLength = baseLength;
            this.LateralSide = lateralSide;
            this.Height = height;
        }

        // Constructor por defecto
        public Rhomboid()
        {
        }

        public double CalculateArea()
        {
            // Área del romboide = base * altura
            return BaseLength * Height;
        }

        public double CalculatePerimeter()
        {
            // Perímetro del romboide = 2 * (base + lado lateral)
            return 2 * (BaseLength + LateralSide);
        }

        public void clean(TextBox baseLength, TextBox lateralSide, TextBox height, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            baseLength.Text = "";
            lateralSide.Text = "";
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