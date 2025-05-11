using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
     class Validation
    {
        public Validation() { }
        
        public bool IsNumeric( TextBox text)
        {
            string value=text.Text;
            double number;
            return double.TryParse(value, out number);
        }

        public bool IsEmpty(TextBox text)
        {
            string value = text.Text;
            return string.IsNullOrEmpty(value);
        }

        /*
         use:just one textbox
         */
        public bool IsValid(TextBox text)
        {
            if (IsEmpty(text))
            {
                MessageBox.Show("El campo no puede estar vacío");
                return false;
            }
            if (!IsNumeric(text))
            {
                MessageBox.Show("El campo debe ser un número");
                return false;
            }
            return true;
        }

        /**
         use: two textbox
         */
        public bool IsValid(TextBox text1, TextBox text2)
        {
            if (IsEmpty(text1) || IsEmpty(text2))
            {
                MessageBox.Show("Los campos no pueden estar vacíos");
                return false;
            }
            if (!IsNumeric(text1) || !IsNumeric(text2))
            {
                MessageBox.Show("Los campos deben ser números");
                return false;
            }
            if (!isPositive(text1)  || !isPositive(text2))
            {
                MessageBox.Show("Los números deben ser positivos");
                return false;
            }
            
            return true;
        }

        public bool isPositive(TextBox text)
        {
            double number = Convert.ToDouble(text.Text);
            if (number <= 0)
            {
                
                return false;
            }
            return true;
        }
    }
}
