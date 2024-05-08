using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlesComunes
{
    internal class fecha
    {
        DateTime miFecha;
        public fecha(DateTime xFecha)
        {
            miFecha = xFecha;
        }
        public string fechaQuery()
        {
            string mes = new string('0',2  ) + miFecha.Month.ToString();
            string dia = new string('0',2) + miFecha.Day.ToString();
            return miFecha.Year + mes.Substring(mes.Length-2) + dia.Substring(dia.Length-2);
        }
        public string Mes()
        {
            string mes = new string('0', 2) + miFecha.Month.ToString();
            return mes.Substring(mes.Length-2);
        }

        public string MesNombre() {

            switch (miFecha.Month)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo"; 
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default:
                    return "";
                    
            }
                
        }

        public string Año() {
            return miFecha.Year.ToString();
        }

        public string Dia()
        {
            return new string('0', 2) + miFecha.Day.ToString();
        }
    }
}
