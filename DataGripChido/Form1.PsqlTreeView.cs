using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace DataGripChido {
    public partial class Form1 {
        public void PostSqlMenu(string sql) {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try {
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
                while (reader.Read()) {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    tvDb.Nodes.Add(bd); //+= Environment.NewLine;


                    registrosRecuperados++;
                }

                reader.Close();

                foreach (TreeNode node in tvDb.Nodes) {
                    PostCrearHijosMySQL(node);
                    Console.WriteLine(node.Text);
                }
                onRead = false;
            } catch (Exception ex) {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }

        public void PostCrearHijosMySQL(TreeNode node) {
            try {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand("select table_name " +
                                "from information_schema.tables" +
                            "where table_schema = 'public' and table_catalog = '" + node.Text + "'", mySQLConexion);


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
                while (reader.Read()) {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    node.Nodes.Add(bd);
                    //tvDb.Nodes[NodoPocision].Nodes.Add(bd); //+= Environment.NewLine;

                    registrosRecuperados++;
                }

                reader.Close();

                foreach (TreeNode padre in node.Nodes) {
                    PostCrearNietosMySQL(node, padre);
                }
                onRead = false;
            } catch (Exception ex) {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }

        public void PostCrearNietosMySQL(TreeNode abuelo, TreeNode padre) {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de MySQLCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try {
                // Objeto utlizado para poder ejecutar un comando de SQL,
                // recibe como paramtros (sentencia SQL, objeto de conexión a BD).
                MySqlCommand command = new MySqlCommand("select column_name " +
                "from information_schema.columns " +
                "where table_schema =  'public'" +
                "and table_catalog = '" + abuelo.Text + "' " +
                "and table_name = '" + padre.Text + "' ", mySQLConexion);


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
                while (reader.Read()) {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    padre.Nodes.Add(bd);
                    //tvDb.Nodes[NodoPocision].Nodes.Add(bd); //+= Environment.NewLine;

                    registrosRecuperados++;
                }

                reader.Close();
            } catch (Exception ex) {
                onRead = false;

                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtResultado.Text = ex.Message;
            }
        }
    }
}
