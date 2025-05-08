using System;
using System.Windows.Forms;

namespace Padre_Hijo
{
    public partial class FrmCuadrado : Form
    {
        private static FrmCuadrado cuadrado;

        
        private FrmCuadrado()
        {
            InitializeComponent();
        }

        
        /**
         * instancia unica     
         */
        public static FrmCuadrado Get_Form()
        {
            if (cuadrado == null || cuadrado.IsDisposed)
            {
                cuadrado = new FrmCuadrado();
            }
            return cuadrado;
        }
    }
}