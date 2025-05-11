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
    public partial class FrmRectangulo : Form
    {
        private static FrmRectangulo rectangulo;
        Rectangle rectangle = new Rectangle(); // Valores por defecto
        Validation validation = new Validation();

        public FrmRectangulo()
        {
            InitializeComponent();
        }

        public static FrmRectangulo Get_Form()
        {
            if (rectangulo == null || rectangulo.IsDisposed)
            {
                rectangulo = new FrmRectangulo();
            }
            return rectangulo;
        }


        private void FrmRectangulo_Load(object sender, EventArgs e)
        {

        }

        private void btn_Calculate_Click_1(object sender, EventArgs e)
        {
            // Validar que ambos campos tengan valores válidos
            if (validation.IsValid(txt_height, txt_Base))
            {
                double length = Convert.ToDouble(txt_height.Text);
                double width = Convert.ToDouble(txt_Base.Text);

                // Verificar si las dimensiones son válidas
                if (!validation.IsValid(txt_height, txt_Base))
                {
                    MessageBox.Show("Las dimensiones del rectángulo deben ser positivas.",
                                    "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el rectángulo y calcular
                rectangle = new Rectangle(length, width);

                // Mostrar resultados
                txt_Area.Text = rectangle.CalculateArea().ToString("F2");
                txt_Perimeter.Text = rectangle.CalculatePerimeter().ToString("F2");
            }
        }
        private void btn_reset_Click_1(object sender, EventArgs e)
        {
            rectangle.clean(txt_height, txt_Base, txt_Area, txt_Perimeter);
            txt_Base.Focus();
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            rectangle.close(this);
        }
        }
    }

