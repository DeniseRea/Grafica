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
    public partial class FrmTriangulo : Form
    {
        private static FrmTriangulo triangulo;
        Triangle triangle = new Triangle(1, 1, 1); // Valores temporales
        Validation validation = new Validation();
        
        public FrmTriangulo()
        {
            InitializeComponent();
        }
        
        public static FrmTriangulo Get_Form()
        {
            if (triangulo == null || triangulo.IsDisposed)
            {
                triangulo = new FrmTriangulo();
            }
            return triangulo;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tengan valores válidos
            if (validation.IsValid(txt_CatetoA) && validation.IsValid(txt_CatetoB) && validation.IsValid(txt_CatetoC))
            {
                double sideA = Convert.ToDouble(txt_CatetoA.Text);
                double sideB = Convert.ToDouble(txt_CatetoB.Text);
                double sideC = Convert.ToDouble(txt_CatetoC.Text);
                
                // Verificar si los lados forman un triángulo válido
                if (!Triangle.IsValidTriangle(sideA, sideB, sideC))
                {
                    MessageBox.Show("Los valores proporcionados no pueden formar un triángulo válido.\n" +
                                    "La suma de dos lados debe ser mayor que el tercer lado.",
                                    "Triángulo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Crear el triángulo y calcular
                triangle = new Triangle(sideA, sideB, sideC);
                
                // Mostrar resultados
                txt_Area.Text = triangle.CalculateArea().ToString("F2");
                txt_Perimeter.Text = triangle.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            triangle.clean(txt_CatetoA, txt_CatetoB, txt_CatetoC, txt_Area, txt_Perimeter);
            txt_CatetoA.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            triangle.close(this);
        }
    }
}
