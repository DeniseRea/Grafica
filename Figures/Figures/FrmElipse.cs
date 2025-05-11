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
    public partial class FrmElipse : Form
    {
        private static FrmElipse elipse;
        Ellipse ellipse = new Ellipse(); // Valores por defecto
        Validation validation = new Validation();

        public FrmElipse()
        {
            InitializeComponent();
        }

        public static FrmElipse Get_Form()
        {
            if (elipse == null || elipse.IsDisposed)
            {
                elipse = new FrmElipse();
            }
            return elipse;
        }

        private void btn_Calculate_Click_1(object sender, EventArgs e)
        {
            // Validar que los campos tengan valores válidos
            if (validation.IsValid(txt_SideM, txt_Side_m))
            {
                double semiMajor = Convert.ToDouble(txt_SideM.Text);
                double semiMinor = Convert.ToDouble(txt_Side_m.Text);

                // Verificar que el semieje mayor sea mayor que el menor
                if (semiMajor < semiMinor)
                {
                    MessageBox.Show("El semieje mayor debe ser mayor o igual que el semieje menor.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear la elipse y calcular
                ellipse = new Ellipse(semiMajor, semiMinor);

                // Mostrar resultados
                txt_Area.Text = ellipse.CalculateArea().ToString("F2");
                txt_Perimeter.Text = ellipse.CalculatePerimeter().ToString("F2");

            }
        }

        private void btn_reset_Click_1(object sender, EventArgs e)
        {
            ellipse.clean(txt_SideM, txt_Side_m, txt_Area, txt_Perimeter);
            txt_SideM.Focus();
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            ellipse.close(this);
        }
    }
}