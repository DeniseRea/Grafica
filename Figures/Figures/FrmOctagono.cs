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
    public partial class FrmOctagono : Form
    {
        private static FrmOctagono octagono;
        Octagon octagono_ = new Octagon();
        Validation validation = new Validation();
        
        public FrmOctagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmOctagono Get_Form()
        {
            if (octagono == null || octagono.IsDisposed)
            {
                octagono = new FrmOctagono();
            }
            return octagono;
        }

        private void FrmOctagono_Load(object sender, EventArgs e)
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
                octagono_ = new Octagon(8, base_, height);
                octagono_.CalculateArea();
                octagono_.CalculatePerimeter();
                txt_Area.Text = octagono_.getArea().ToString();
                txt_Perimeter.Text = octagono_.getPerimeter().ToString();
                txt_height.Text = octagono_.getHeight().ToString();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            octagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter, txt_height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            octagono_.close(this);
        }
    }
}
