using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public partial class FrmCirculo : Form
    {
        private static FrmCirculo circulo;
        Circle circle = new Circle(); // Valores por defecto
        Validation validation = new Validation();

        public FrmCirculo()
        {
            InitializeComponent();
        }

        public static FrmCirculo Get_Form()
        {
            if (circulo == null || circulo.IsDisposed)
            {
                circulo = new FrmCirculo();
            }
            return circulo;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que el campo tenga un valor válido
            if (validation.IsValid(txt_radius))
            {
                double radius = Convert.ToDouble(txt_radius.Text);

                // Crear el círculo y calcular
                circle = new Circle(radius);

                // Mostrar resultados
                txt_Area.Text = circle.CalculateArea().ToString("F2");
                txt_Perimeter.Text = circle.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            circle.clean(txt_radius, txt_Area, txt_Perimeter);
            txt_radius.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            circle.close(this);
        }
    }
}