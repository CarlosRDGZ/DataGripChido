using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataGripChido
{
    public partial class Formulario : Form
    {
        private TreeNode table;
        private List<TextBox> fields = new List<TextBox>();
        private List<Label> lables = new List<Label>();

        public Formulario(TreeNode table)
        {
            InitializeComponent();
            this.table = table;

            int y = label1.Location.Y + label1.Size.Height + 10;
            foreach(TreeNode column in table.Nodes)
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

    }
}
