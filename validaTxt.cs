using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlesComunes
{
    internal class funciones
    {
        string CaracteresValidos = "@.";
        public void soloNumeros(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else { e.Handled = true; };
        }
        public void soloAlfaNumericos(KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsSeparator(e.KeyChar) || CaracteresValidos.Contains(e.KeyChar))
            {
                e.Handled = false;
            }
            else { e.Handled = true; };
        }


        // Method to check the Email ID
        public bool isValidoEmail(string inputEmail)
        {

            // This Pattern is use to verify the email
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            return re.IsMatch(inputEmail);

        }

        public double ValorSegunRegion(string valor)
        {
            double precioUnit;
            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
                precioUnit = Convert.ToDouble(valor.Replace('.', ','));//
            else
                precioUnit = Convert.ToDouble(valor.Replace(',', '.'));
            return precioUnit;
        }


    }
}
