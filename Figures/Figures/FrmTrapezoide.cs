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
    public partial class FrmTrapezoide : Form
    {
        private static FrmTrapezoide trapezoide;
        Trapezoid trapezoid = new Trapezoid(); // Valores por defecto
        Validation validation = new Validation();

        public FrmTrapezoide()
        {
            InitializeComponent();
        }

        public static FrmTrapezoide Get_Form()
        {
            if (trapezoide == null || trapezoide.IsDisposed)
            {
                trapezoide = new FrmTrapezoide();
            }
            return trapezoide;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tengan valores válidos
            if (validation.IsValid(txt_SideA) && 
                validation.IsValid(txt_sideB) && 
                validation.IsValid(txt_sideC) && 
                validation.IsValid(txt_sideD) &&
                validation.IsValid(txt_Diagonal))
            {
                double largBase = Convert.ToDouble(txt_SideA.Text);
                double shortBase = Convert.ToDouble(txt_sideB.Text);
                double leftSide = Convert.ToDouble(txt_sideC.Text);
                double rightSide = Convert.ToDouble(txt_sideD.Text);
                double diagonal = Convert.ToDouble(txt_Diagonal.Text);

                // Validar restricciones geométricas
                if (largBase <= shortBase)
                {
                    MessageBox.Show("La base mayor debe ser mayor que la base menor.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que la diagonal sea posible
                double minDiagonal = Math.Abs(largBase - shortBase);
                double maxDiagonal = leftSide + rightSide;
                
                if (diagonal < minDiagonal || diagonal > maxDiagonal)
                {
                    MessageBox.Show("La diagonal debe ser mayor que " + minDiagonal.ToString("F2") + 
                                   " y menor que " + maxDiagonal.ToString("F2") + ".",
                        "Diagonal inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Validar que los lados laterales sean posibles
                if (leftSide + shortBase <= largBase || rightSide + shortBase <= largBase)
                {
                    MessageBox.Show("Los lados laterales no cumplen con las restricciones geométricas para formar un trapezoide.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el trapezoide y calcular
                trapezoid = new Trapezoid(largBase, shortBase, leftSide, rightSide, diagonal);

                // Mostrar resultados
                
                txt_Area.Text = trapezoid.CalculateArea().ToString("F2");
                txt_Perimeter.Text = trapezoid.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            trapezoid.clean(txt_SideA, txt_sideB, txt_sideC, txt_sideC, 
                           txt_Diagonal,txt_Area, txt_Perimeter);
            txt_SideA.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            trapezoid.close(this);
        }
    }
}