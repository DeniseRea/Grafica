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
    public partial class FrmCuadrado : Form
    {
        private static FrmCuadrado cuadrado;
        Square square = new Square(); // Valores por defecto
        Validation validation = new Validation();

        public FrmCuadrado()
        {
            InitializeComponent();
        }

        public static FrmCuadrado Get_Form()
        {
            if (cuadrado == null || cuadrado.IsDisposed)
            {
                cuadrado = new FrmCuadrado();
            }
            return cuadrado;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que el campo tenga un valor válido
            if (validation.IsValid(txt_Side))
            {
                double side = Convert.ToDouble(txt_Side.Text);
                // Crear el cuadrado y calcular
                square = new Square(side);

                // Mostrar resultados
                txt_Area.Text = square.CalculateArea().ToString("F2");
                txt_Perimeter.Text = square.CalculatePerimeter().ToString("F2");
            }
            else
            {
                MessageBox.Show("El lado debe ser un valor positivo.",
                        "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            square.clean(txt_Side, txt_Area, txt_Perimeter);
            txt_Side.Focus();
        }


        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            square.close(this);
        }
    }
}