namespace DiccionariodeDatos
{
    partial class ModificaAtributo
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
            this.comboBoxE = new System.Windows.Forms.ComboBox();
            this.modificar = new System.Windows.Forms.Button();
            this.comboBoxA = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.typeIndice = new System.Windows.Forms.ComboBox();
            this.tipodato = new System.Windows.Forms.ComboBox();
            this.longi = new System.Windows.Forms.TextBox();
            this.nuevonombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxE
            // 
            this.comboBoxE.FormattingEnabled = true;
            this.comboBoxE.Location = new System.Drawing.Point(12, 28);
            this.comboBoxE.Name = "comboBoxE";
            this.comboBoxE.Size = new System.Drawing.Size(157, 21);
            this.comboBoxE.TabIndex = 0;
            this.comboBoxE.SelectedIndexChanged += new System.EventHandler(this.comboBoxE_SelectedIndexChanged);
            // 
            // modificar
            // 
            this.modificar.Location = new System.Drawing.Point(12, 165);
            this.modificar.Name = "modificar";
            this.modificar.Size = new System.Drawing.Size(169, 81);
            this.modificar.TabIndex = 1;
            this.modificar.Text = "Modificar";
            this.modificar.UseVisualStyleBackColor = true;
            this.modificar.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxA
            // 
            this.comboBoxA.FormattingEnabled = true;
            this.comboBoxA.Location = new System.Drawing.Point(12, 107);
            this.comboBoxA.Name = "comboBoxA";
            this.comboBoxA.Size = new System.Drawing.Size(157, 21);
            this.comboBoxA.TabIndex = 2;
            this.comboBoxA.SelectedIndexChanged += new System.EventHandler(this.comboBoxA_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Entidades";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Atributos de Entidad";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.typeIndice);
            this.groupBox1.Controls.Add(this.tipodato);
            this.groupBox1.Controls.Add(this.longi);
            this.groupBox1.Controls.Add(this.nuevonombre);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(187, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 269);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modificacion";
            // 
            // typeIndice
            // 
            this.typeIndice.FormattingEnabled = true;
            this.typeIndice.Location = new System.Drawing.Point(11, 214);
            this.typeIndice.Name = "typeIndice";
            this.typeIndice.Size = new System.Drawing.Size(211, 21);
            this.typeIndice.TabIndex = 7;
            this.typeIndice.SelectedIndexChanged += new System.EventHandler(this.typeIndice_SelectedIndexChanged);
            // 
            // tipodato
            // 
            this.tipodato.FormattingEnabled = true;
            this.tipodato.Location = new System.Drawing.Point(16, 110);
            this.tipodato.Name = "tipodato";
            this.tipodato.Size = new System.Drawing.Size(206, 21);
            this.tipodato.TabIndex = 6;
            this.tipodato.SelectedIndexChanged += new System.EventHandler(this.tipodato_SelectedIndexChanged);
            // 
            // longi
            // 
            this.longi.Location = new System.Drawing.Point(16, 170);
            this.longi.Name = "longi";
            this.longi.Size = new System.Drawing.Size(206, 20);
            this.longi.TabIndex = 5;
            // 
            // nuevonombre
            // 
            this.nuevonombre.Location = new System.Drawing.Point(13, 53);
            this.nuevonombre.Name = "nuevonombre";
            this.nuevonombre.Size = new System.Drawing.Size(209, 20);
            this.nuevonombre.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Tipo Indice";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Longitud";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tipo de Dato";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nuevo Nombre Atributo";
            // 
            // ModificaAtributo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 308);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxA);
            this.Controls.Add(this.modificar);
            this.Controls.Add(this.comboBoxE);
            this.Name = "ModificaAtributo";
            this.Text = "ModificaAtributo";
            this.Load += new System.EventHandler(this.ModificaAtributo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxE;
        private System.Windows.Forms.Button modificar;
        private System.Windows.Forms.ComboBox comboBoxA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nuevonombre;
        private System.Windows.Forms.ComboBox tipodato;
        private System.Windows.Forms.TextBox longi;
        private System.Windows.Forms.ComboBox typeIndice;
    }
}