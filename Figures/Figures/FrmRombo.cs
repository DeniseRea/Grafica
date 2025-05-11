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
    public partial class FrmRombo : Form
    {
        private static FrmRombo rombo;
        Rhombus rhombus = new Rhombus(); // Valores por defecto
        Validation validation = new Validation();

        public FrmRombo()
        {
            InitializeComponent();
        }

        public static FrmRombo Get_Form()
        {
            if (rombo == null || rombo.IsDisposed)
            {
                rombo = new FrmRombo();
            }
            return rombo;
        }

        private void FrmRombo_Load(object sender, EventArgs e)
        {
            // Inicialización del formulario
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que ambos campos tengan valores válidos
            if (validation.IsValid(txt_Side, txt_height))
            {
                double side = Convert.ToDouble(txt_Side.Text);
                double height = Convert.ToDouble(txt_height.Text);

                // Crear el rombo y calcular
                rhombus = new Rhombus(side, height);

                // Mostrar resultados
                txt_Area.Text = rhombus.CalculateArea().ToString("F2");
                txt_Perimeter.Text = rhombus.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            rhombus.clean(txt_Side, txt_height, txt_Area, txt_Perimeter);
            txt_Side.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            rhombus.close(this);
        }
    }
}
