using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Npgsql;

namespace DataGripChido
{
    public partial class Form1 : Form
    {
        #region Propiedades
        /// <summary>
        /// Representa el host (computadora) a la que se conecta para usar la base de datos ej: localhost, 148.213.20.112
        /// </summary>
        private string host = "";

        private string baseDatos = "";

        private string usuario = "";

        private string contrasena = "";

        /// <summary>
        /// Puerto en el que esta escuchando el servicio de la base de datos
        /// </summary>
        private string puerto = "";

        /// <summary>
        /// Objecto que maneja la conexion a base de datos MySQL
        /// </summary>
        private MySqlConnection mySQLConexion;

        /// <summary>
        /// Objecto que maneja la conexion a base de datos MySQL
        /// </summary>
        private NpgsqlConnection pgConexion;

        private bool isConectado = false;

        private bool onRead = false;
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
                    host = frmConexion.Host;
                    usuario = frmConexion.Usuario;
                    baseDatos = frmConexion.BaseDatos;
                    contrasena = frmConexion.Contrasena;
                    puerto = frmConexion.Puerto;
                    try
                    {
                        // Inizializa un objeto de conexion a una base de datos
                        // MySQL. Su constructor resive una string con los datos
                        // para iniciar la conexion. (Esto no abre la conexion,
                        // solo prepara el objeto).
                        mySQLConexion = new MySqlConnection(
                            "SERVER=" + host + ";" +
                            "PORT=" + puerto + ";" +
                            "DATABASE=" + baseDatos + ";" +
                            "UID=" + usuario + ";" +
                            "PASSWORD=" + contrasena + ";"
                        );

                        // Trata de abrir la conexion a la base de datos, en caso
                        // de error lanza una excepcion.
                        mySQLConexion.Open();

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

        private void ConetarsePostgreSQL()
        {
            // Inizializa el formulario de la conexion con los valores actulalez 
            // de las propiedades, para que si quiere editar los valores de la conexion
            // no reedite todo.
            using (Conexion frmConexion = new Conexion(
                //"y06qcehxdtkegbeb.cbetxkdyhwsb.us-east-1.rds.amazonaws.com",
                //"w93riiyygmp8180s",
                //"tqrtf3w57x49s63n",
                //"q5b6jc0m6hj3zgez",
                //"3306"
                "ec2-54-83-49-109.compute-1.amazonaws.com",
                "dam9f2r2oldmnh",
                "jrusfofxbutkva",
                "45f27eb3fe4bf151a72a21ee54b00bd4bf9decad34f51928663e9ff14e77a14e",
                "5432"
            ))
            {
                // En caso de se precione el boton 'Aceptar', el resultado del dialogo
                // sera OK y entrara al if, si se cierra la ventana o presiona cancelar
                // no hara nada, mas que ocultar la etiqueta.
                if (frmConexion.ShowDialog() == DialogResult.OK)
                {
                    host = frmConexion.Host;
                    usuario = frmConexion.Usuario;
                    baseDatos = frmConexion.BaseDatos;
                    contrasena = frmConexion.Contrasena;
                    puerto = frmConexion.Puerto;
                    try
                    {
                        // Inizializa un objeto de conexion a una base de datos
                        // MySQL. Su constructor resive una string con los datos
                        // para iniciar la conexion. (Esto no abre la conexion,
                        // solo prepara el objeto).
                        pgConexion = new NpgsqlConnection(
                            "SERVER=" + host + ";" +
                            "PORT=" + puerto + ";" +
                            "DATABASE=" + baseDatos + ";" +
                            "UID=" + usuario + ";" +
                            "PASSWORD=" + contrasena + ";" + (
                                frmConexion.SSL ?
                                    "SSL Mode=Require;Trust Server Certificate=true;" :
                                    ""
                            )
                        );

                        // Trata de abrir la conexion a la base de datos, en caso
                        // de error lanza una excepcion.
                        pgConexion.Open();

                        // Se abrio la conexion y establece isConectado a verdadero
                        isConectado = true;

                        lblConexion.Text = "Conexión Exitosa";
                        lblConexion.ForeColor = System.Drawing.Color.Green;
                        lblConexion.Visible = true;
                    }
                    catch (Exception ex)
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
                MySqlCommand command = new MySqlCommand(txtQuery.Text, mySQLConexion);

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

        private void EjecutarPostgreSQL()
        {
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(txtQuery.Text, pgConexion);

                NpgsqlDataReader reader = command.ExecuteReader();
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
            if (cmbSGBDR.Text == "MySQL")
                ConetarseMySQL();
            else if (cmbSGBDR.Text == "PostgreSQL")
                ConetarsePostgreSQL();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "";

            // Verifica que este la conexion abierta, de lo contrario
            // no hace nada.
            if (isConectado && !onRead)
            {
                if (cmbSGBDR.Text == "MySQL")
                    EjecutarMySQL();
                else if (cmbSGBDR.Text == "PostgreSQL")
                    EjecutarPostgreSQL();
            }
            else
                MessageBox.Show("No hay conexion a ninguna base de datos", "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
