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
    public partial class FrmHeptagono : Form
    {
        private static FrmHeptagono heptagono;
        Heptagon heptagono_ = new Heptagon();
        Validation validation = new Validation();
        public FrmHeptagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmHeptagono Get_Form()
        {
            if (heptagono == null || heptagono.IsDisposed)
            {
                heptagono = new FrmHeptagono();
            }
            return heptagono;
        }

        private void FrmHeptagono_Load(object sender, EventArgs e)
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
                heptagono_ = new Heptagon(7, base_, height);
                heptagono_.CalculateArea();
                heptagono_.CalculatePerimeter();
                txt_Area.Text = heptagono_.getArea().ToString();
                txt_Perimeter.Text = heptagono_.getPerimeter().ToString();
                txt_height.Text = heptagono_.getHeight().ToString();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            heptagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter, txt_height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            heptagono_.close(this);
        }
    }
}
