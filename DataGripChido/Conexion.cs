using System;
using System.Windows.Forms;

namespace DataGripChido
{
    public partial class Conexion : Form
    {
        /// <summary>
        /// Regresa la string que representa el host (computadora) a la que se conecta para usar la base de datos ej: localhost, 148.213.20.112
        /// </summary>
        public string Host { get { return txtHost.Text; } }

        /// <summary>
        /// Nombre de la base de datos a la que se conectara
        /// </summary>
        public string BaseDatos { get { return txtBaseDatos.Text; } }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Usuario { get { return txtUsuario.Text; } }

        /// <summary>
        /// Contrasenia
        /// </summary>
        public string Contrasena { get { return txtContrasena.Text; } }

        /// <summary>
        /// Puerto en el que esta escuchando el servicio de la base de datos
        /// </summary>
        public string Puerto { get { return txtPuerto.Text; } }

        /// <summary>
        /// Algunos servidores ultilizan un capa de seguridad extra para conectarse
        /// a la base de datos, por lo que activan el certificado SSL,
        /// en el caso de PostgreSQL es necesario decirle explicitamente que debe
        /// utilizar esa tecnologia, por lo que abra un valor booleano 
        /// para determinar si lo usa o no.
        /// </summary>
        public bool SSL{ get { return ckbSSL.Checked; } }

        public Conexion(string host, string baseDatos, string usuario, string contrasenia, string puerto, bool ssl)
        {
            InitializeComponent();
            txtHost.Text = host;
            txtBaseDatos.Text = baseDatos;
            txtUsuario.Text = usuario;
            txtContrasena.Text = contrasenia;
            txtPuerto.Text = puerto;
            ckbSSL.Checked = ssl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
