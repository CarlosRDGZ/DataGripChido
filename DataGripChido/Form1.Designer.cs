namespace DataGripChido
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConectarse = new System.Windows.Forms.Button();
            this.lblConexion = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.lblResutlado = new System.Windows.Forms.Label();
            this.cmbSGBDR = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tvDb = new System.Windows.Forms.TreeView();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConectarse
            // 
            this.btnConectarse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectarse.Location = new System.Drawing.Point(805, 12);
            this.btnConectarse.Name = "btnConectarse";
            this.btnConectarse.Size = new System.Drawing.Size(97, 29);
            this.btnConectarse.TabIndex = 0;
            this.btnConectarse.Text = "Conectarse";
            this.btnConectarse.UseVisualStyleBackColor = true;
            this.btnConectarse.Click += new System.EventHandler(this.btnConectarse_Click);
            // 
            // lblConexion
            // 
            this.lblConexion.AutoSize = true;
            this.lblConexion.ForeColor = System.Drawing.Color.Green;
            this.lblConexion.Location = new System.Drawing.Point(811, 48);
            this.lblConexion.Name = "lblConexion";
            this.lblConexion.Size = new System.Drawing.Size(88, 13);
            this.lblConexion.TabIndex = 1;
            this.lblConexion.Text = "Conexión Exitosa";
            this.lblConexion.Visible = false;
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(349, 82);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(553, 95);
            this.txtQuery.TabIndex = 2;
            this.txtQuery.WordWrap = false;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuery.Location = new System.Drawing.Point(351, 63);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(73, 16);
            this.lblQuery.TabIndex = 3;
            this.lblQuery.Text = "SQL Query";
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEjecutar.Location = new System.Drawing.Point(814, 183);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(85, 27);
            this.btnEjecutar.TabIndex = 0;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // lblResutlado
            // 
            this.lblResutlado.AutoSize = true;
            this.lblResutlado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResutlado.Location = new System.Drawing.Point(351, 216);
            this.lblResutlado.Name = "lblResutlado";
            this.lblResutlado.Size = new System.Drawing.Size(70, 16);
            this.lblResutlado.TabIndex = 5;
            this.lblResutlado.Text = "Resultado";
            // 
            // cmbSGBDR
            // 
            this.cmbSGBDR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSGBDR.FormattingEnabled = true;
            this.cmbSGBDR.Items.AddRange(new object[] {
            "MySQL",
            "PostgreSQL"});
            this.cmbSGBDR.Location = new System.Drawing.Point(655, 17);
            this.cmbSGBDR.Name = "cmbSGBDR";
            this.cmbSGBDR.Size = new System.Drawing.Size(121, 24);
            this.cmbSGBDR.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(589, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "SGBDR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(346, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "DataGrip Chido";
            // 
            // tvDb
            // 
            this.tvDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDb.Location = new System.Drawing.Point(30, 21);
            this.tvDb.Name = "tvDb";
            this.tvDb.Size = new System.Drawing.Size(268, 391);
            this.tvDb.TabIndex = 9;
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(349, 235);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(548, 177);
            this.dataGrid.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 436);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.tvDb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSGBDR);
            this.Controls.Add(this.lblResutlado);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.lblConexion);
            this.Controls.Add(this.btnConectarse);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConectarse;
        private System.Windows.Forms.Label lblConexion;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Label lblResutlado;
        private System.Windows.Forms.ComboBox cmbSGBDR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvDb;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}

