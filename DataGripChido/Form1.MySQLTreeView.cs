using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace DataGripChido
{
    public partial class Form1
    {
        public async System.Threading.Tasks.Task SqlMenuAsync(string sql)
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
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (await reader.ReadAsync())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    tvDb.Nodes.Add(bd);
                }

                reader.Close();

                foreach (TreeNode node in tvDb.Nodes)
                    await CrearHijosMySQLAsync(node);

                lblConexion.Text = "Conexión Exitosa";
                lblConexion.ForeColor = System.Drawing.Color.Green;
                lblConexion.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task CrearHijosMySQLAsync(TreeNode node)
        {
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
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (await reader.ReadAsync())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    node.Nodes.Add(bd);
                }

                reader.Close();

                foreach (TreeNode taquito in node.Nodes)
                    await CrearNietosMySQLAsync(node, taquito);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task CrearNietosMySQLAsync(TreeNode abuelo, TreeNode padre)
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
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                // En un ciclo, mientras existan registros, se podran recuperar
                // los registros (si es que la consulta devolvio registros).
                // Mientras lea un registro se cicla el numero de columnas
                // del registro y por cada iteracion se agrega a la string de
                // resultado el registro.
                while (await reader.ReadAsync())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    padre.Nodes.Add(bd);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
