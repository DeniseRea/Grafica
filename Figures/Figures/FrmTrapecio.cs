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
    public partial class FrmTrapecio : Form
    {
        private static FrmTrapecio trapecio;
        Trapeze trapeze = new Trapeze(); // Valores por defecto
        Validation validation = new Validation();

        public FrmTrapecio()
        {
            InitializeComponent();
        }

        public static FrmTrapecio Get_Form()
        {
            if (trapecio == null || trapecio.IsDisposed)
            {
                trapecio = new FrmTrapecio();
            }
            return trapecio;
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tengan valores válidos
            if (validation.IsValid(txt_BaseM) && 
                validation.IsValid(txt_baseMenor) && 
                validation.IsValid(txt_SideA) && 
                validation.IsValid(txt_sideC) && 
                validation.IsValid(txt_height))
            {
                double baseMayor = Convert.ToDouble(txt_BaseM.Text);
                double baseMenor = Convert.ToDouble(txt_baseMenor.Text);
                double ladoIzquierdo = Convert.ToDouble(txt_SideA.Text);
                double ladoDerecho = Convert.ToDouble(txt_sideC.Text);
                double altura = Convert.ToDouble(txt_height.Text);

                // Validar restricciones geométricas
                if (baseMayor <= baseMenor)
                {
                    MessageBox.Show("La base mayor debe ser mayor que la base menor.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que los lados laterales sean suficientemente largos
                double distanciaIzq = (baseMayor - baseMenor) / 2;
                double distanciaDer = (baseMayor - baseMenor) / 2;
                
                if (Math.Sqrt(distanciaIzq * distanciaIzq + altura * altura) > ladoIzquierdo)
                {
                    MessageBox.Show("El lado izquierdo no es lo suficientemente largo para la altura especificada.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (Math.Sqrt(distanciaDer * distanciaDer + altura * altura) > ladoDerecho)
                {
                    MessageBox.Show("El lado derecho no es lo suficientemente largo para la altura especificada.",
                        "Dimensiones inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el trapecio y calcular
                trapeze = new Trapeze(baseMayor, baseMenor, ladoIzquierdo, ladoDerecho, altura);

                // Mostrar resultados
                txt_Area.Text = trapeze.CalculateArea().ToString("F2");
                txt_Perimeter.Text = trapeze.CalculatePerimeter().ToString("F2");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            trapeze.clean(txt_BaseM, txt_baseMenor, txt_SideA, txt_sideC, txt_height, txt_Area, txt_Perimeter);
            txt_BaseM.Focus();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            trapeze.close(this);
        }
    }
}