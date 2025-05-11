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
    public partial class FrmDeltoide : Form
    {
        private static FrmDeltoide deltoide;
        Deltoid deltoid = new Deltoid(); // Valores por defecto
        Validation validation = new Validation();

        public FrmDeltoide()
        {
            InitializeComponent();
        }

        public static FrmDeltoide Get_Form()
        {
            if (deltoide == null || deltoide.IsDisposed)
            {
                deltoide = new FrmDeltoide();
            }
            return deltoide;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tengan valores válidos
            if (validation.IsValid(txt_sideA) && 
                validation.IsValid(txt_sideB) && 
                validation.IsValid(txt_diagMay) && 
                validation.IsValid(txt_diagMen))
            {
                double shortSide = Convert.ToDouble(txt_sideA.Text);
                double longSide = Convert.ToDouble(txt_sideB.Text);
                double diagonalP = Convert.ToDouble(txt_diagMay.Text);
                double diagonalS = Convert.ToDouble(txt_diagMen.Text);

                // Validar restricciones geométricas del deltoide
                if (longSide <= shortSide)
                {
                    MessageBox.Show("El lado largo debe ser mayor que el lado corto.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (shortSide + longSide <= diagonalP)
                {
                    MessageBox.Show("La diagonal principal debe ser menor que la suma de los lados.",
                        "Diagonal inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (2 * shortSide <= diagonalS || 2 * longSide <= diagonalS)
                {
                    MessageBox.Show("La diagonal secundaria no cumple con las propiedades geométricas de un deltoide.",
                        "Diagonal inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el deltoide y calcular
                deltoid = new Deltoid(shortSide, longSide, diagonalP, diagonalS);

                // Mostrar resultados
                txt_Area.Text = deltoid.CalculateArea().ToString("F2");
                txt_Perimeter.Text = deltoid.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            deltoid.clean(txt_sideA, txt_sideB, txt_diagMay, txt_diagMen, txt_Area, txt_Perimeter);
            txt_sideA.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            deltoid.close(this);
        }
    }
}