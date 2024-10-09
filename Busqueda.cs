using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ControlesComunes
{
    public partial class Busqueda : UserControl
    {
        private string codigo;
        private string nombre;
        public string conexion { get; set; }
        public string CodTabla { get; set; }
        public string NomTabla { get; set; }
        public string Tabla { get; set; }
        public string Condicion { get; set; }

        // Evento público para notificar que ha cambiado el contenido de cualquiera de los TextBox
        public event EventHandler ItemSeleccionado;

        funciones cFx =new funciones();

        public string Codigo
        {
            get
            {
                codigo=txtCodigo.Text;
                return codigo;
            }
            set
            {
                Data td = new Data();
                codigo = value;
                td=Carga(codigo);
                txtCodigo.Text=td.Codigo;
                txtNombre.Text = td.Descripcion;
            }
        }
        public string Nombre
        {
            get
            {
                codigo = txtNombre.Text;
                return nombre;
            }
            set
            {
                nombre = value;
                txtNombre.Text=nombre;
            }
        }

        public Busqueda()
        {
            InitializeComponent();
        
        }
        public Busqueda(string conexion, string codigo, string nombre, string tabla, string condicion)
        {
            InitializeComponent();
            this.conexion = conexion;
            CodTabla = codigo;
            NomTabla = nombre;
            Tabla = tabla;
            Condicion = condicion;
        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFx.soloAlfaNumericos(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFx.soloAlfaNumericos(e);
        }


        private void btnBusca_Click(object sender, EventArgs e)
        {
            Resultado fRs = new Resultado(conexion, CodTabla, NomTabla, Tabla, Condicion);
            fRs.ShowDialog();
            if (fRs.codigo != null)
            {
                txtCodigo.Text = fRs.codigo.ToString();

            }
            if (fRs.descripcion != null)
            {
                txtNombre.Text = fRs.descripcion.ToString();
            }
        }

        private void Busqueda_Load(object sender, EventArgs e)
        {

        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (txtCodigo.Text !="")
                txtNombre.Text=Carga(txtCodigo.Text).Descripcion.ToString();
        }
        private Data Carga(string Palabra="")
        {
            Data td = new Data();
            SqlConnection cnn;

            if (conexion != null)
            {
                cnn = new SqlConnection(conexion);
                cnn.Open();
                SqlCommand command;
                SqlDataReader reader;
                string sql;

                sql = "select " + CodTabla + " as Codigo, " + NomTabla + " as Descripcion from " + Tabla + " where " + CodTabla + " = '" + Palabra +"'" ;
                command = new SqlCommand(sql, cnn);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    td = new Data();
                    td.Codigo = reader["Codigo"].ToString();
                    td.Descripcion = reader["Descripcion"].ToString();
                }
                else
                {
                    td.Codigo = "";
                    td.Descripcion = "";
                }

                reader.Close();
                command.Dispose();
                cnn.Close();
            }

            return td;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Disparar el evento ContenidoCambiado para notificar al exterior
            ItemSeleccionado.Invoke(this, EventArgs.Empty);
        }
    }
}

