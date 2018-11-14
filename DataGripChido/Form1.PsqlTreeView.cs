using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Npgsql;

namespace DataGripChido
{
    public partial class Form1
    {
        public async System.Threading.Tasks.Task PostSqlMenuAsync(string sql)
        {
            //string sql = "select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = " + hola;
            // Crea una instancia de NpgsqlCommand, que es el objeto
            // que utiliza C# para poder interactuar con la base de datos
            // y ejecutar comandos (queries).
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql, pgConexion);
                NpgsqlDataReader reader = (NpgsqlDataReader) await command.ExecuteReaderAsync();
                while (await reader.ReadAsync()) {
                    string bd = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];
                    tvDb.Nodes.Add(bd);
                }

                reader.Close();
                foreach (TreeNode node in tvDb.Nodes)
                    await CrearBDPostgresAsync(node);

                lblConexion.Text = "Conexión Exitosa";
                lblConexion.ForeColor = System.Drawing.Color.Green;
                lblConexion.Visible = true;

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task CrearBDPostgresAsync (TreeNode schema)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select table_catalog " +
                            "from information_schema.tables " +
                            "where table_schema = '" + schema.Text + "' group by table_catalog", pgConexion);
                NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string bd = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        bd = (string)reader[i];

                    schema.Nodes.Add(bd);
                }

                reader.Close();

                foreach (TreeNode node in schema.Nodes)
                    await PostCrearHijosMySQLAsync(schema, node);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task PostCrearHijosMySQLAsync(TreeNode schema, TreeNode bd) {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select table_name " +
                                "from information_schema.tables " +
                            "where table_schema = '"+ schema.Text + "' and table_catalog = '" + bd.Text + "'", pgConexion);

                NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string table = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        table = (string)reader[i];

                    bd.Nodes.Add(table);
                }

                reader.Close();

                foreach (TreeNode padre in bd.Nodes)
                    await PostCrearNietosMySQLAsync(schema, bd, padre);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async System.Threading.Tasks.Task PostCrearNietosMySQLAsync(TreeNode schema, TreeNode abuelo, TreeNode padre)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select column_name " +
                "from information_schema.columns " +
                "where table_schema = '" + schema.Text + "' " +
                "and table_catalog = '" + abuelo.Text + "' " +
                "and table_name = '" + padre.Text + "' ", pgConexion);

                NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string column = "";

                    for (int i = 0; i < reader.FieldCount; i++)
                        column = (string)reader[i];

                    padre.Nodes.Add(column);
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
