using System;
using System.Collections.Generic;
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
        //private string host = "y06qcehxdtkegbeb.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";

        //private string baseDatos = "w93riiyygmp8180s";

        //private string usuario = "tqrtf3w57x49s63n";

        //private string contrasena = "q5b6jc0m6hj3zgez";

        private string host = "ec2-54-221-234-62.compute-1.amazonaws.com";

        private string baseDatos = "d7mdj4u8hovsc2";

        private string usuario = "jesnsvrksrsqdo";

        private string contrasena = "55ebaa3ba100c294330de045be60aa1646b5cfeb3d9d7b056108b2f56f96d6d1";

        /// <summary>
        /// Puerto en el que esta escuchando el servicio de la base de datos
        /// </summary>
        private string puerto = "5432";

        /// <summary>
        /// Determina si es necesario certificado SSL para la conexión
        /// </summary>
        private bool ssl = true;

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
            tvDb.DoubleClick += new EventHandler(TreeView_Click);
        }

        /// <summary>
        /// Se engarga de tratar de generar una conexion a bases de datos MySQL
        /// </summary>
        private bool ConetarseMySQL()
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
                        return true;
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
                        return false;
                    }
                }
                else
                {
                    lblConexion.Visible = false;
                    return false;
                }
            }
        }
        
        /// <summary>
        /// Se engarga de tratar de generar una conexion a bases de datos PostgreSQL
        /// </summary>
        private bool ConetarsePostgreSQL()
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
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblConexion.Text = "Conexión Fallida";
                        lblConexion.ForeColor = System.Drawing.Color.Red;
                        lblConexion.Visible = true;

                        // fallo la conexión asi que isConectado es falso
                        isConectado = false;
                        return false;
                    }
                }
                else
                {
                    lblConexion.Visible = false;
                    return false;
                }
            }
        }

        /// <summary>
        /// Ejecuta sentencia de consulta en base de dato MySQL
        /// </summary>
        private async System.Threading.Tasks.Task EjecutarMySQLAsync(string query = null)
        {
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                if (query == null)
                    query = txtQuery.Text;
                MySqlCommand command = new MySqlCommand(query, mySQLConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync();
                onRead = true;
                                
                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cilcla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (await reader.ReadAsync())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (dataGrid.ColumnCount < reader.FieldCount)
                        {
                            DataGridViewColumn col = new DataGridViewColumn();
                            col.CellTemplate = new DataGridViewTextBoxCell();
                            string column = reader.GetName(i);
                            col.HeaderText = column;
                            dataGrid.Columns.Add(col);
                        }
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = reader[i];
                        row.Cells.Add(cell);
                        cell.ReadOnly = true;
                    }
                    dataGrid.Rows.Add(row);

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
            }
        }

        private async System.Threading.Tasks.Task EjecutarPostgreSQLAsync(string query = null)
        {
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                if (query == null)
                    query = txtQuery.Text;
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                NpgsqlCommand command = new NpgsqlCommand(query, pgConexion);

                // Objeto para recuperar datos de la consulta.
                // No es un array, la unica manera de saber si hay o no
                // datos es llamar a Read. Estructura tipo lista.
                NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync();
                onRead = true;

                // Variable para el control de cuantos registros se recuperaron
                int registrosRecuperados = 0;

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cilcla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.

                while (await reader.ReadAsync())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (dataGrid.ColumnCount < reader.FieldCount)
                        {
                            DataGridViewColumn col = new DataGridViewColumn();
                            col.CellTemplate = new DataGridViewTextBoxCell();
                            string column = reader.GetName(i);
                            col.HeaderText = column;
                            dataGrid.Columns.Add(col);
                        }

                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = reader[i];
                        row.Cells.Add(cell);
                        cell.ReadOnly = true;
                    }

                    dataGrid.Rows.Add(row);

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
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();
            tvDb.Nodes.Clear();



            if (cmbSGBDR.Text == "MySQL")
            {
                if (ConetarseMySQL())
                    SqlMenuAsync("SELECT table_schema FROM INFORMATION_SCHEMA.tables GROUP BY table_schema");
            }
            else if (cmbSGBDR.Text == "PostgreSQL")
            {
                if (ConetarsePostgreSQL())
                    PostSqlMenuAsync("select table_schema from information_schema.tables group by table_schema");
            }   
            else
                MessageBox.Show("Gestor de base de datos no soportado", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Verifica que este la conexión abierta, de lo contrario
            // no hace nada.
            if (isConectado && !onRead)
            {
                if (cmbSGBDR.Text == "MySQL")
                    EjecutarMySQLAsync();
                else if (cmbSGBDR.Text == "PostgreSQL")
                    EjecutarPostgreSQLAsync();
            }
            else
                MessageBox.Show("No hay conexión a ninguna base de datos", "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void TreeView_Click(object sender, EventArgs e)
        {
            TreeView node = (TreeView)sender;
            
            if (node.SelectedNode != null)
            {
                if (node.SelectedNode.Name == "table")
                {

                    if (mySQLConexion != null)
                    {
                        using (Formulario form = new Formulario(node.SelectedNode, mySQLConexion, "MySQL"))
                        {
                            form.ShowDialog();
                        }
                    }
                    else if (pgConexion != null)
                    {
                        using (Formulario form = new Formulario(node.SelectedNode,pgConexion, "PSQL"))
                        {
                            form.ShowDialog();
                        }
                    }
                    else
                        return;
                }
            }
        }
    }
}
