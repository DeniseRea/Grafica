using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public class Rectangle
    {
        protected double Length { get; set; }
        protected double Width { get; set; }

        public Rectangle(double length, double width)
        {
            this.Length = length;
            this.Width = width;
        }

        // Constructor por defecto
        public Rectangle()
        {
        }


        public double CalculateArea()
        {
            return Length * Width;
        }

        public double CalculatePerimeter()
        {
            return 2 * (Length + Width);
        }

        public void clean(TextBox length, TextBox width, TextBox area, TextBox perimeter)
        {
            // Limpiar los campos de texto
            length.Text = "";
            width.Text = "";
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
