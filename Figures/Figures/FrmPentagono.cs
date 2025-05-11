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
    public partial class FrmPentagono : Form
    {
        private static FrmPentagono pentagono;
        Pentagon pentagono_ = new Pentagon();
        Validation validation = new Validation();
        public FrmPentagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmPentagono Get_Form()
        {
            if (pentagono == null || pentagono.IsDisposed)
            {
                pentagono = new FrmPentagono();
            }
            return pentagono;
        }

        private void FrmPentagono_Load(object sender, EventArgs e)
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
                pentagono_ = new Pentagon(5, base_, height);
                pentagono_.CalculateArea();
                pentagono_.CalculatePerimeter();
                txt_Area.Text = pentagono_.getArea().ToString();
                txt_Perimeter.Text = pentagono_.getPerimeter().ToString();
                txt_height.Text = pentagono_.getHeight().ToString();
            }

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            pentagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter,txt_height);

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            pentagono_.close(this);
        }
    }
}
