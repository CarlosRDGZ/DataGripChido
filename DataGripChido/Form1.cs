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
        private string host = "localhost";

        private string baseDatos = "information_schema";

        private string usuario = "root";

        private string contrasena = "root";

        /// <summary>
        /// Puerto en el que esta escuchando el servicio de la base de datos
        /// </summary>
        private string puerto = "3306";

        /// <summary>
        /// Determina si es necesario certificado SSL para la conexión
        /// </summary>
        private bool ssl = false;

        /// <summary>
        /// Objecto que maneja la conexión a base de datos MySQL
        /// </summary>
        private MySqlConnection mySQLConexion;

        /// <summary>
        /// Objecto que maneja la conexión a base de datos MySQL
        /// </summary>
        private NpgsqlConnection pgConexion;

        /// <summary>
        /// Esta propiedad booleana se utiliza para saber si exite una conexión activa
        /// a una base de datos o no.
        /// </summary>
        private bool isConectado = false;

        /// <summary>
        /// Esta propiedad solo esta en falso mientras se estan recuperando los datos de la
        /// base de datos, de manera que si trata de presionar otra vez el boton de 'Ejecutar'
        /// no haga nada si esta es veradero
        /// </summary>
        private bool onRead = false;
        #endregion
        
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Se engarga de tratar de generar una conexion a bases de datos MySQL
        /// </summary>
        private void ConetarseMySQL()
        {
            // Inizializa el formulario de la conexión con los valores actulalez 
            // de las propiedades, para que si quiere editar los valores de la conexion
            // no reedite todo.
            using (Conexion frmConexion = new Conexion(
                host,
                baseDatos,
                usuario,
                contrasena,
                puerto,
                ssl
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
                    ssl = frmConexion.SSL;
                    try
                    {
                        // Inizializa un objeto de conexión a una base de datos
                        // MySQL. Su constructor resive una string con los datos
                        // para iniciar la conexión. (Esto no abre la conexión,
                        // solo prepara el objeto).
                        mySQLConexion = new MySqlConnection(
                            "SERVER=" + host + ";" +
                            "PORT=" + puerto + ";" +
                            "DATABASE=" + baseDatos + ";" +
                            "UID=" + usuario + ";" +
                            "PASSWORD=" + contrasena + ";"
                        );

                        // Trata de abrir la conexión a la base de datos, en caso
                        // de error lanza una excepcion.
                        mySQLConexion.Open();

                        // Se abrio la conexión y establece isConectado a verdadero
                        isConectado = true;

                        lblConexion.Text = "Conexión Exitosa";
                        lblConexion.ForeColor = System.Drawing.Color.Green;
                        lblConexion.Visible = true;
                    }
                    catch (MySqlException ex)
                    {
                        // Aparece ventana de error
                        MessageBox.Show(ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        lblConexion.Text = "Conexión Fallida";
                        lblConexion.ForeColor = System.Drawing.Color.Red;
                        lblConexion.Visible = true;

                        // fallo la conexión asi que isConectado es falso
                        isConectado = false;
                    }
                }
                else
                {
                    lblConexion.Visible = false;
                }
            }
        }
        
        /// <summary>
        /// Se engarga de tratar de generar una conexion a bases de datos PostgreSQL
        /// </summary>
        private void ConetarsePostgreSQL()
        {
            // Inizializa el formulario de la conexión con los valores actulalez 
            // de las propiedades, para que si quiere editar los valores de la conexión
            // no reedite todo.
            using (Conexion frmConexion = new Conexion(
                host,
                baseDatos,
                usuario,
                contrasena,
                puerto,
                ssl
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
                    ssl = frmConexion.SSL;
                    try
                    {
                        // Inizializa un objeto de conexión a una base de datos
                        // PostgreSQL. Su constructor resive una string con los datos
                        // para iniciar la conexión. (Esto no abre la conexión,
                        // solo prepara el objeto).
                        // En servidores con segurirdad, la string de conexion
                        // require de 2 parametros adicionales. Si se agregan o no se
                        // determina con la propiedad boolena 'ssl'.
                        pgConexion = new NpgsqlConnection(
                            "SERVER=" + host + ";" +
                            "PORT=" + puerto + ";" +
                            "DATABASE=" + baseDatos + ";" +
                            "UID=" + usuario + ";" +
                            "PASSWORD=" + contrasena + ";" + (
                                ssl ? "SSL Mode=Require;Trust Server Certificate=true;" : ""
                            )
                        );

                        // Trata de abrir la conexión a la base de datos, en caso
                        // de error lanza una excepcion.
                        pgConexion.Open();

                        // Se abrio la conexión y establece isConectado a verdadero
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

                        // fallo la conexión asi que isConectado es falso
                        isConectado = false;
                    }
                }
                else
                {
                    lblConexion.Visible = false;
                }
            }
        }

        /// <summary>
        /// Ejecuta sentencia de consulta en base de dato MySQL
        /// </summary>
        private void EjecutarMySQL()
        {
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand(txtQuery.Text, mySQLConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                MySqlDataReader reader = command.ExecuteReader();
                onRead = true;
                                
                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cilcla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
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

                // Mensaje de ejecucion terminada
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



        public void SqlMenu(string sql)
        {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand(sql, mySQLConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                MySqlDataReader reader = command.ExecuteReader();
                onRead = true;

                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (reader.Read())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];
   
                    tvDb.Nodes.Add(bd); //+= Environment.NewLine;
                    

                    registrosRecuperados++;
                }

                reader.Close();

                foreach (TreeNode node in tvDb.Nodes)
                {
                    CrearHijos(node);
                    Console.WriteLine(node.Text);
                }
                onRead = false;
            }
            catch (Exception ex)
            {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }

        public void CrearHijos(TreeNode node)
        {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand("SELECT table_name " +
                    "FROM INFORMATION_SCHEMA.tables " +
                    "WHERE table_schema = '" + node.Text + "'", mySQLConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                MySqlDataReader reader = command.ExecuteReader();
                onRead = true;

                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (reader.Read())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    node.Nodes.Add(bd);
                    //tvDb.Nodes[NodoPocision].Nodes.Add(bd); //+= Environment.NewLine;

                    registrosRecuperados++;
                }

                reader.Close();

                foreach (TreeNode taquito in node.Nodes)
                {
                    CrearNietos(node, taquito);
                }
                onRead = false;
            }
            catch (Exception ex)
            {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }

        public void CrearNietos(TreeNode abuelo, TreeNode padre)
        {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand("SELECT column_name " +
                    "FROM INFORMATION_SCHEMA.columns " +
                    "WHERE table_schema = '" + abuelo.Text + "'" +
                    "AND table_name = '" + padre.Text + "'", mySQLConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                MySqlDataReader reader = command.ExecuteReader();
                onRead = true;

                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (reader.Read())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    padre.Nodes.Add(bd);
                    //tvDb.Nodes[NodoPocision].Nodes.Add(bd); //+= Environment.NewLine;

                    registrosRecuperados++;
                }

                reader.Close();
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
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                NpgsqlCommand command = new NpgsqlCommand(txtQuery.Text, pgConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                NpgsqlDataReader reader = command.ExecuteReader();
                onRead = true;

                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cilcla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
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
            // Limpiar los objetos de conexion, por si existia una conexión previa
            if (mySQLConexion != null)
            {
                // cierra conexión
                mySQLConexion.Close();
                // detruye objeto
                mySQLConexion = null;
            }

            if (pgConexion != null)
            {
                // cierra conexión
                pgConexion.Close();
                // detruye objeto
                pgConexion = null;
            }

            // limpia resultados y query
            txtQuery.Text = txtResultado.Text = "";



            if (cmbSGBDR.Text == "MySQL")
            {
                ConetarseMySQL();
                SqlMenu("SELECT table_schema " +
                    "FROM INFORMATION_SCHEMA.tables " +
                    "GROUP BY table_schema");
            }
            else if (cmbSGBDR.Text == "PostgreSQL")
                ConetarsePostgreSQL();
            else
                MessageBox.Show("Gestor de base de datos no soportado", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "";

            // Verifica que este la conexión abierta, de lo contrario
            // no hace nada.
            if (isConectado && !onRead)
            {
                if (cmbSGBDR.Text == "MySQL")
                    EjecutarMySQL();
                else if (cmbSGBDR.Text == "PostgreSQL")
                    EjecutarPostgreSQL();
            }
            else
                MessageBox.Show("No hay conexión a ninguna base de datos", "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
