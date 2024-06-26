﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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

    }
}

