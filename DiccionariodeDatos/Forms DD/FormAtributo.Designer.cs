namespace DiccionariodeDatos
{
    partial class FormAtributo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.entidadesCombo = new System.Windows.Forms.ComboBox();
            this.FKlabel = new System.Windows.Forms.Label();
            this.Aceptar = new System.Windows.Forms.Button();
            this.comboTipodato = new System.Windows.Forms.ComboBox();
            this.tipoindiceCombo = new System.Windows.Forms.ComboBox();
            this.Longi = new System.Windows.Forms.TextBox();
            this.nombreatributo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.entidadesCombo);
            this.groupBox1.Controls.Add(this.FKlabel);
            this.groupBox1.Controls.Add(this.Aceptar);
            this.groupBox1.Controls.Add(this.comboTipodato);
            this.groupBox1.Controls.Add(this.tipoindiceCombo);
            this.groupBox1.Controls.Add(this.Longi);
            this.groupBox1.Controls.Add(this.nombreatributo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 276);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nuevo Atributo";
            // 
            // entidadesCombo
            // 
            this.entidadesCombo.FormattingEnabled = true;
            this.entidadesCombo.Location = new System.Drawing.Point(115, 209);
            this.entidadesCombo.Name = "entidadesCombo";
            this.entidadesCombo.Size = new System.Drawing.Size(217, 21);
            this.entidadesCombo.TabIndex = 9;
            this.entidadesCombo.Visible = false;
            // 
            // FKlabel
            // 
            this.FKlabel.AutoSize = true;
            this.FKlabel.Location = new System.Drawing.Point(23, 212);
            this.FKlabel.Name = "FKlabel";
            this.FKlabel.Size = new System.Drawing.Size(76, 13);
            this.FKlabel.TabIndex = 8;
            this.FKlabel.Text = "Clave Foranea";
            this.FKlabel.Visible = false;
            // 
            // Aceptar
            // 
            this.Aceptar.Location = new System.Drawing.Point(0, 251);
            this.Aceptar.Name = "Aceptar";
            this.Aceptar.Size = new System.Drawing.Size(354, 25);
            this.Aceptar.TabIndex = 7;
            this.Aceptar.Text = "Aceptar";
            this.Aceptar.UseVisualStyleBackColor = true;
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            // 
            // comboTipodato
            // 
            this.comboTipodato.FormattingEnabled = true;
            this.comboTipodato.Location = new System.Drawing.Point(115, 73);
            this.comboTipodato.Name = "comboTipodato";
            this.comboTipodato.Size = new System.Drawing.Size(217, 21);
            this.comboTipodato.TabIndex = 6;
            this.comboTipodato.SelectedIndexChanged += new System.EventHandler(this.ComboTipodato_SelectedIndexChanged);
            // 
            // tipoindiceCombo
            // 
            this.tipoindiceCombo.FormattingEnabled = true;
            this.tipoindiceCombo.Location = new System.Drawing.Point(115, 165);
            this.tipoindiceCombo.Name = "tipoindiceCombo";
            this.tipoindiceCombo.Size = new System.Drawing.Size(217, 21);
            this.tipoindiceCombo.TabIndex = 5;
            this.tipoindiceCombo.SelectedIndexChanged += new System.EventHandler(this.tipoindiceCombo_SelectedIndexChanged);
            // 
            // Longi
            // 
            this.Longi.Location = new System.Drawing.Point(115, 116);
            this.Longi.Name = "Longi";
            this.Longi.Size = new System.Drawing.Size(217, 20);
            this.Longi.TabIndex = 4;
            // 
            // nombreatributo
            // 
            this.nombreatributo.Location = new System.Drawing.Point(115, 31);
            this.nombreatributo.Name = "nombreatributo";
            this.nombreatributo.Size = new System.Drawing.Size(217, 20);
            this.nombreatributo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tipo de Indice:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Longitud:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo de Dato:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Atributo:";
            // 
            // FormAtributo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 297);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAtributo";
            this.Text = "FormAtributo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAtributo_FormClosed);
            this.Load += new System.EventHandler(this.FormAtributo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nombreatributo;
        private System.Windows.Forms.ComboBox comboTipodato;
        private System.Windows.Forms.ComboBox tipoindiceCombo;
        private System.Windows.Forms.Button Aceptar;
        private System.Windows.Forms.TextBox Longi;
        private System.Windows.Forms.ComboBox entidadesCombo;
        private System.Windows.Forms.Label FKlabel;
    }
}