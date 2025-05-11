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
    public partial class FrmOvalo : Form
    {
        private static FrmOvalo ovalo;
        Oval oval = new Oval(); // Valores por defecto
        Validation validation = new Validation();

        public FrmOvalo()
        {
            InitializeComponent();
        }

        public static FrmOvalo Get_Form()
        {
            if (ovalo == null || ovalo.IsDisposed)
            {
                ovalo = new FrmOvalo();
            }
            return ovalo;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que los campos tengan valores válidos
            if (validation.IsValid(txt_SideM, txt_Side_m))
            {
                double semiMajorAxis = Convert.ToDouble(txt_SideM.Text);
                double semiMinorAxis = Convert.ToDouble(txt_Side_m.Text);

                // Verificar que el semieje mayor sea mayor que el menor
                if (semiMajorAxis < semiMinorAxis)
                {
                    MessageBox.Show("El semieje mayor debe ser mayor o igual que el semieje menor.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el óvalo y calcular
                oval = new Oval(semiMajorAxis, semiMinorAxis);

                // Mostrar resultados
                txt_Area.Text = oval.CalculateArea().ToString("F2");
                txt_Perimeter.Text = oval.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            oval.clean(txt_SideM, txt_Side_m, txt_Area, txt_Perimeter);
            txt_SideM.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            oval.close(this);
        }
    }
}