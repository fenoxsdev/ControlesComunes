using System;
using System.Windows.Forms;

namespace ControlesComunes
{
    public partial class RangoFechas : UserControl
    {
        private string desde;
        private string hasta;
        public string Desde
        {
            get
            {
                fecha f = new fecha(dtpDesde.Value);
                desde = f.fechaQuery();
                return desde;
            }
            set { desde = value;
                dtpDesde.Value = new DateTime(Convert.ToInt32( desde.Substring(0,4)), Convert.ToInt32(desde.Substring(4, 2)), Convert.ToInt32(desde.Substring(6, 2))); }
        }
        public string Hasta
        {
            get
            {
                fecha f = new fecha(dtpHasta.Value);
                hasta = f.fechaQuery();
                return hasta;
            }

            set { hasta = value;
                  dtpHasta.Value = new DateTime(Convert.ToInt32(hasta.Substring(0, 4)), Convert.ToInt32(hasta.Substring(4, 2)), Convert.ToInt32(hasta.Substring(6, 2)));
            }
        }
        public RangoFechas()
        {
            InitializeComponent();
        }
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpDesde.Value, dtpHasta.Value) > 0)
            {
                dtpHasta.Value = dtpDesde.Value;
            };

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpDesde.Value, dtpHasta.Value) > 0)
            {
                dtpDesde.Value = dtpHasta.Value;
            };

        }
    }
}
