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
    public partial class FrmHexagono : Form
    {
        private static FrmHexagono hexagono;
        Hexagon hexagono_ = new Hexagon();
        Validation validation = new Validation();
        public FrmHexagono()
        {
            InitializeComponent();
        }

        private void lbl_mainTitle_Click(object sender, EventArgs e)
        {

        }

        public static FrmHexagono Get_Form()
        {
            if (hexagono == null || hexagono.IsDisposed)
            {
                hexagono = new FrmHexagono();
            }
            return hexagono;
        }

        private void FrmHexagono_Load(object sender, EventArgs e)
        {

        }

        private void btn_Calculate_Click_1(object sender, EventArgs e)
        {
            double base_ = 0;
            double height = 0;

            if (validation.IsValid(txt_radious, txt_Base))
            {
                base_ = Convert.ToDouble(txt_Base.Text);
                height = Convert.ToDouble(txt_radious.Text);
                hexagono_ = new Hexagon(6, base_, height);
                hexagono_.CalculateArea();
                hexagono_.CalculatePerimeter();
                txt_Area.Text = hexagono_.getArea().ToString();
                txt_Perimeter.Text = hexagono_.getPerimeter().ToString();
                txt_height.Text = hexagono_.getHeight().ToString();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            hexagono_.clean(txt_Base, txt_radious, txt_Area, txt_Perimeter, txt_height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            hexagono_.close(this);
        }
    }
}
