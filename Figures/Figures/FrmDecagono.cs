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
    public partial class FrmDecagono : Form
    {
        private static FrmDecagono decagono;
        Decagono decagono_ = new Decagono();
        Validation validation = new Validation();
        
        public FrmDecagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmDecagono Get_Form()
        {
            if (decagono == null || decagono.IsDisposed)
            {
                decagono = new FrmDecagono();
            }
            return decagono;
        }

        private void FrmDecagono_Load(object sender, EventArgs e)
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
                decagono_ = new Decagono(10, base_, height);
                decagono_.CalculateArea();
                decagono_.CalculatePerimeter();
                txt_Area.Text = decagono_.getArea().ToString();
                txt_Perimeter.Text = decagono_.getPerimeter().ToString();
                txt_height.Text = decagono_.getHeight().ToString();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            decagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter, txt_height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            decagono_.close(this);
        }
    }
}
