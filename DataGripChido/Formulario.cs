using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Npgsql;

namespace DataGripChido
{
    public partial class Formulario : Form
    {
        private string table;
        private List<TextBox> fields = new List<TextBox>();
        private List<Label> lables = new List<Label>();
        private MySqlConnection mysql;
        private NpgsqlConnection psql;
        private List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        public Formulario(TreeNode table, object conn, string tipo)
        {
            InitializeComponent();
            this.table = table.Text;
            CreateFields(table);
            Console.WriteLine("Constructor");
            try
            {
                if (tipo == "MySQL")
                {
                    mysql = (MySqlConnection)conn;
                    GetValuesMySQL();
                }
                else if (tipo == "PSQL")
                {
                    psql = (NpgsqlConnection)conn;
                    GetValuesPsql();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void CreateFields (TreeNode table)
        {
            int y = 10;
            foreach (TreeNode column in table.Nodes)
            {
                var lbl = new Label();
                lables.Add(lbl);
                lbl.AutoSize = true;
                lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Location = new System.Drawing.Point(20, y);
                lbl.Text = column.Text;

                var txt = new TextBox();
                fields.Add(txt);
                txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                txt.Location = new System.Drawing.Point(20, y + 20);
                txt.Width = panel1.Width - txt.Location.X - 10;
                txt.Name = column.Text;
                panel1.Controls.Add(lbl);
                panel1.Controls.Add(txt);

                y += 60;
            }
            panel1.PerformLayout();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string[] FiledsString()
        {
            int length = fields.Count;
            string[] strs = new string[length];
            for (int i = 0; i < length; i++)
                strs[i] = fields[i].Text;
            return strs;
        }

        private async System.Threading.Tasks.Task GetValuesMySQL()
        {
            try
            {
                MySqlCommand command = new MySqlCommand($"SELECT * FROM {this.table}", mysql);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string column = reader.GetName(i);
                        obj[column] = (string)reader[i];
                    }

                    if (data.Count == 0)
                    {
                        foreach (var txt in fields)
                        {
                            Console.WriteLine(txt.Name);
                            Console.WriteLine(data.Count);
                        }
                    }
                    data.Add(obj);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async System.Threading.Tasks.Task GetValuesPsql()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM {this.table}", psql);
                NpgsqlDataReader reader = (NpgsqlDataReader)await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var obj = new Dictionary<string, string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string column = reader.GetName(i);
                        obj[column] = (string)reader[i];
                    }
                    data.Add(obj);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
