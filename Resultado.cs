using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ControlesComunes
{
    public partial class Resultado : Form
    {
        public string codigo;
        public string descripcion;
        public string conexion { get; set; }
        public string CodTabla { get; set; }
        public string NomTabla { get; set; }
        public string Tabla { get; set; }
        public string Condicion { get; set; }
        public Resultado(string conexion, string codigo, string nombre, string tabla, string condicion)
        {
            InitializeComponent();
            this.conexion = conexion;
            CodTabla = codigo;
            NomTabla = nombre;
            Tabla = tabla;
            Condicion = condicion;
        }

        private void Resultado_Load(object sender, EventArgs e)
        {
            dgvResultado.DataSource = Cargadt();
            dgvResultado.Columns[1].Width = 300;

        }

        private List<Data> Carga(string PartePalabra="")
        {
            Data td = new Data();
            List<Data> Registros = new List<Data>();
            SqlConnection cnn;

            if (conexion != null)
            {
                cnn = new SqlConnection(conexion);
                cnn.Open();
                SqlCommand command;
                SqlDataReader reader;
                string sql;

                sql = "select " + CodTabla + " as Codigo, " + NomTabla + " as Descripcion from " + Tabla;
                if(Condicion  != null)
                {
                    sql += " where " + Condicion;
                    if (PartePalabra != "")
                    {
                        sql += " AND ";
                    }                
                }
                else
                {
                    if (PartePalabra != "")
                    {
                        sql += " WHERE ";
                    }
                }
                if (PartePalabra != "")
                {
                    sql += " (" + CodTabla + " like '%" + PartePalabra + "%' OR " + NomTabla + " like '%" + PartePalabra + "%')";
                }


                command = new SqlCommand(sql, cnn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    td = new Data();
                    td.Codigo = reader["Codigo"].ToString();
                    td.Descripcion = reader["Descripcion"].ToString();
                    Registros.Add(td);
                }

                reader.Close();
                command.Dispose();
                cnn.Close();
            }

            return Registros;
        }

        private DataTable Cargadt(string PartePalabra = "")
        {

            SqlConnection cnn;
            DataTable dt = new DataTable();

            if (conexion != null)
            {
                cnn = new SqlConnection(conexion);
                cnn.Open();
                string sql;

                sql = "select " + CodTabla + " as Codigo, " + NomTabla + " as Descripcion from " + Tabla;
                if (Condicion != null)
                {
                    sql += " where " + Condicion;
                    if (PartePalabra != "")
                    {
                        sql += " AND ";
                    }
                }
                else
                {
                    if (PartePalabra != "")
                    {
                        sql += " WHERE ";
                    }
                }
                if (PartePalabra != "")
                {
                    sql += " (" + CodTabla + " like '%" + PartePalabra + "%' OR " + NomTabla + " like '%" + PartePalabra + "%')";
                }


                SqlDataAdapter adaptador = new SqlDataAdapter(sql, cnn);
                adaptador.Fill(dt);
                //dgvResultado.DataSource = dt;
                cnn.Close();
            }

            return dt;
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int posicion = dgvResultado.CurrentRow.Index;
            codigo = dgvResultado[0, posicion].Value.ToString();
            descripcion = dgvResultado[1, posicion].Value.ToString();
            this.Hide();

        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            dgvResultado.DataSource = Carga(txtBusqueda.Text);

        }
    }
}
