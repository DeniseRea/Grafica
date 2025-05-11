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
    public partial class FrmRomboide : Form
    {
        private static FrmRomboide romboide;
        Rhomboid rhomboid = new Rhomboid(); // Valores por defecto
        Validation validation = new Validation();

        public FrmRomboide()
        {
            InitializeComponent();
        }

        public static FrmRomboide Get_Form()
        {
            if (romboide == null || romboide.IsDisposed)
            {
                romboide = new FrmRomboide();
            }
            return romboide;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tengan valores válidos
            if (validation.IsValid(txt_sideA) && validation.IsValid(txt_sideB) && validation.IsValid(txt_height))
            {
                double baseLength = Convert.ToDouble(txt_sideB.Text);
                double lateralSide = Convert.ToDouble(txt_sideA.Text);
                double height = Convert.ToDouble(txt_height.Text);

                // Validar restricciones geométricas
                if (height > lateralSide)
                {
                    MessageBox.Show("La altura no puede ser mayor que el lado lateral.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el romboide y calcular
                rhomboid = new Rhomboid(baseLength, lateralSide, height);

                // Mostrar resultados
                txt_Area.Text = rhomboid.CalculateArea().ToString("F2");
                txt_Perimeter.Text = rhomboid.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            rhomboid.clean(txt_sideA, txt_sideB, txt_height, txt_Area, txt_Perimeter);
            txt_sideA.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            rhomboid.close(this);
        }
    }
}
