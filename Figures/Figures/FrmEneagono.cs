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
    public partial class FrmEneagono : Form
    {
        private static FrmEneagono eneagono;
        Eneagono eneagono_ = new Eneagono();
        Validation validation = new Validation();
        public FrmEneagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmEneagono Get_Form()
        {
            if (eneagono == null || eneagono.IsDisposed)
            {
                eneagono = new FrmEneagono();
            }
            return eneagono;
        }

        private void FrmEneagono_Load(object sender, EventArgs e)
        {

        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            double base_ = 0;
            double height = 0;

            if (validation.IsValid(txt_radious, txt_Base))
            {
                base_ = Convert.ToDouble(txt_Base.Text);
                height = Convert.ToDouble(txt_radious.Text);
                eneagono_ = new Eneagono(9, base_, height);
                eneagono_.CalculateArea();
                eneagono_.CalculatePerimeter();
                txt_Area.Text = eneagono_.getArea().ToString();
                txt_Perimeter.Text = eneagono_.getPerimeter().ToString();
                txt_height.Text = eneagono_.getHeight().ToString();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            eneagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter, txt_height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            eneagono_.close(this);
        }
    }
}
