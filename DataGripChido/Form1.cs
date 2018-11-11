using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataGripChido
{
    public partial class Form1 : Form
    {
        #region Propiedades
        /// <summary>
        /// Representa el host (computadora) a la que se conecta para usar la base de datos ej: localhost, 148.213.20.112
        /// </summary>
        private string _host = "";

        private string _baseDatos = "";

        private string _usuario = "";

        private string _contrasenia = "";

        /// <summary>
        /// Puerto en el que esta escuchando el servicio de la base de datos
        /// </summary>
        private string _puerto = "";

        private MySqlConnection _mySQLConexion;

        private bool isConectado = false;

        private bool onRead = false;

        private string tipoConexion = "MySQL";
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void ConetarseMySQL()
        {
            // Inizializa el formulario de la conexion con los valores actulalez 
            // de las propiedades, para que si quiere editar los valores de la conexion
            // no reedite todo.
            using (Conexion frmConexion = new Conexion(
                "y06qcehxdtkegbeb.cbetxkdyhwsb.us-east-1.rds.amazonaws.com",
                "w93riiyygmp8180s",
                "tqrtf3w57x49s63n",
                "q5b6jc0m6hj3zgez",
                "3306"
            ))
            {
                // En caso de se precione el boton 'Aceptar', el resultado del dialogo
                // sera OK y entrara al if, si se cierra la ventana o presiona cancelar
                // no hara nada, mas que ocultar la etiqueta.
                if (frmConexion.ShowDialog() == DialogResult.OK)
                {
                    _host = frmConexion.Host;
                    _usuario = frmConexion.Usuario;
                    _baseDatos = frmConexion.BaseDatos;
                    _contrasenia = frmConexion.Contrasena;
                    _puerto = frmConexion.Puerto;
                    try
                    {
                        // Inizializa un objeto de conexion a una base de datos
                        // MySQL. Su constructor resive una string con los datos
                        // para iniciar la conexion. (Esto no abre la conexion,
                        // solo prepara el objeto).
                        _mySQLConexion = new MySqlConnection(
                            "SERVER=" + this._host + ";" +
                            "PORT=" + this._puerto + ";" +
                            "DATABASE=" + this._baseDatos + ";" +
                            "UID=" + this._usuario + ";" +
                            "PASSWORD=" + this._contrasenia + ";"
                        );

                        // Trata de abrir la conexion a la base de datos, en caso
                        // de error lanza una excepcion.
                        _mySQLConexion.Open();

                        // Se abrio la conexion y establece isConectado a verdadero
                        isConectado = true;

                        lblConexion.Text = "Conexión Exitosa";
                        lblConexion.ForeColor = System.Drawing.Color.Green;
                        lblConexion.Visible = true;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblConexion.Text = "Conexión Fallida";
                        lblConexion.ForeColor = System.Drawing.Color.Red;
                        lblConexion.Visible = true;

                        // fallo la conexion asi que isConectado es falso
                        isConectado = false;
                    }
                }
                else
                {
                    lblConexion.Visible = false;
                }
            }
        }

        private void EjecutarMySQL()
        {
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                MySqlCommand command = new MySqlCommand(txtQuery.Text, _mySQLConexion);

                MySqlDataReader reader = command.ExecuteReader();
                onRead = true;

                int registrosRecuperados = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string column = reader.GetName(i);
                        txtResultado.Text += column + ": " + reader[column] + Environment.NewLine;
                    }

                    txtResultado.Text += Environment.NewLine;

                    registrosRecuperados++;
                }

                reader.Close();

                onRead = false;

                MessageBox.Show(
                    "Sentecia ejectuda correctamente." + Environment.NewLine
                    + "Rigistros afectados: " +
                        (reader.RecordsAffected > 0 ? reader.RecordsAffected : 0)
                        + Environment.NewLine
                    + "Registros Recuperados: " + registrosRecuperados,
                    "Consulta exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }

        private void btnConectarse_Click(object sender, EventArgs e)
        {
            if (tipoConexion == "MySQL")
                ConetarseMySQL();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "";

            // Verifica que este la conexion abierta, de lo contrario
            // no hace nada.
            if (isConectado && !onRead)
            {
                if (tipoConexion == "MySQL")
                    EjecutarMySQL();
            }
            else
                MessageBox.Show("No hay conexion a ninguna base de datos", "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
