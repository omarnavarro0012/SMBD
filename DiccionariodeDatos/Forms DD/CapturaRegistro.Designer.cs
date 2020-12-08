namespace DiccionariodeDatos
{
    partial class CapturaRegistro
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
            this.comboBoxEntidades = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataCaptura = new System.Windows.Forms.DataGridView();
            this.DataVisualReg = new System.Windows.Forms.DataGridView();
            this.Agregar = new System.Windows.Forms.Button();
            this.botonIndice = new System.Windows.Forms.Button();
            this.Modificar = new System.Windows.Forms.Button();
            this.Eliminar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.IndicesPrimariosCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listaIndiceSECU = new System.Windows.Forms.ComboBox();
            this.boton_IndiceSecu = new System.Windows.Forms.Button();
            this.muestraArbolP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboArbol = new System.Windows.Forms.ComboBox();
            this.ArbolSEC = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboArbolS = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataCaptura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataVisualReg)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEntidades
            // 
            this.comboBoxEntidades.FormattingEnabled = true;
            this.comboBoxEntidades.Location = new System.Drawing.Point(15, 25);
            this.comboBoxEntidades.Name = "comboBoxEntidades";
            this.comboBoxEntidades.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEntidades.TabIndex = 0;
            this.comboBoxEntidades.SelectedIndexChanged += new System.EventHandler(this.comboBoxEntidades_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entidades";
            // 
            // DataCaptura
            // 
            this.DataCaptura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataCaptura.Location = new System.Drawing.Point(15, 61);
            this.DataCaptura.Name = "DataCaptura";
            this.DataCaptura.Size = new System.Drawing.Size(773, 92);
            this.DataCaptura.TabIndex = 2;
            this.DataCaptura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataCaptura_KeyUp);
            // 
            // DataVisualReg
            // 
            this.DataVisualReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataVisualReg.Location = new System.Drawing.Point(15, 203);
            this.DataVisualReg.Name = "DataVisualReg";
            this.DataVisualReg.Size = new System.Drawing.Size(773, 200);
            this.DataVisualReg.TabIndex = 3;
            this.DataVisualReg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataVisualReg_CellClick);
            // 
            // Agregar
            // 
            this.Agregar.Location = new System.Drawing.Point(12, 159);
            this.Agregar.Name = "Agregar";
            this.Agregar.Size = new System.Drawing.Size(100, 23);
            this.Agregar.TabIndex = 4;
            this.Agregar.Text = "Agregar Registro";
            this.Agregar.UseVisualStyleBackColor = true;
            this.Agregar.Click += new System.EventHandler(this.Agregar_Click);
            // 
            // botonIndice
            // 
            this.botonIndice.Location = new System.Drawing.Point(341, 23);
            this.botonIndice.Name = "botonIndice";
            this.botonIndice.Size = new System.Drawing.Size(85, 23);
            this.botonIndice.TabIndex = 5;
            this.botonIndice.Text = "Muestra Indice";
            this.botonIndice.UseVisualStyleBackColor = true;
            this.botonIndice.Click += new System.EventHandler(this.botonIndice_Click);
            // 
            // Modificar
            // 
            this.Modificar.Location = new System.Drawing.Point(118, 160);
            this.Modificar.Name = "Modificar";
            this.Modificar.Size = new System.Drawing.Size(104, 22);
            this.Modificar.TabIndex = 6;
            this.Modificar.Text = "Modificar Registro";
            this.Modificar.UseVisualStyleBackColor = true;
            this.Modificar.Click += new System.EventHandler(this.Modificar_Click);
            // 
            // Eliminar
            // 
            this.Eliminar.Location = new System.Drawing.Point(228, 160);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(98, 22);
            this.Eliminar.TabIndex = 7;
            this.Eliminar.Text = "Eliminar Regristro";
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Indices Primarios:";
            // 
            // IndicesPrimariosCombo
            // 
            this.IndicesPrimariosCombo.FormattingEnabled = true;
            this.IndicesPrimariosCombo.Location = new System.Drawing.Point(273, 23);
            this.IndicesPrimariosCombo.Name = "IndicesPrimariosCombo";
            this.IndicesPrimariosCombo.Size = new System.Drawing.Size(62, 21);
            this.IndicesPrimariosCombo.TabIndex = 9;
            this.IndicesPrimariosCombo.SelectedIndexChanged += new System.EventHandler(this.IndicesPrimariosCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Indices Secundarios:";
            // 
            // listaIndiceSECU
            // 
            this.listaIndiceSECU.FormattingEnabled = true;
            this.listaIndiceSECU.Location = new System.Drawing.Point(567, 25);
            this.listaIndiceSECU.Name = "listaIndiceSECU";
            this.listaIndiceSECU.Size = new System.Drawing.Size(69, 21);
            this.listaIndiceSECU.TabIndex = 11;
            this.listaIndiceSECU.SelectedIndexChanged += new System.EventHandler(this.listaIndiceSECU_SelectedIndexChanged);
            // 
            // boton_IndiceSecu
            // 
            this.boton_IndiceSecu.Location = new System.Drawing.Point(642, 25);
            this.boton_IndiceSecu.Name = "boton_IndiceSecu";
            this.boton_IndiceSecu.Size = new System.Drawing.Size(90, 23);
            this.boton_IndiceSecu.TabIndex = 12;
            this.boton_IndiceSecu.Text = "Muestra Indice";
            this.boton_IndiceSecu.UseVisualStyleBackColor = true;
            this.boton_IndiceSecu.Click += new System.EventHandler(this.boton_IndiceSecu_Click);
            // 
            // muestraArbolP
            // 
            this.muestraArbolP.Location = new System.Drawing.Point(477, 161);
            this.muestraArbolP.Name = "muestraArbolP";
            this.muestraArbolP.Size = new System.Drawing.Size(95, 23);
            this.muestraArbolP.TabIndex = 5;
            this.muestraArbolP.Text = "Muestra Arbol";
            this.muestraArbolP.UseVisualStyleBackColor = true;
            this.muestraArbolP.Click += new System.EventHandler(this.muestraArbolP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(338, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Arbol Primario";
            // 
            // comboArbol
            // 
            this.comboArbol.FormattingEnabled = true;
            this.comboArbol.Location = new System.Drawing.Point(409, 161);
            this.comboArbol.Name = "comboArbol";
            this.comboArbol.Size = new System.Drawing.Size(62, 21);
            this.comboArbol.TabIndex = 9;
            this.comboArbol.SelectedIndexChanged += new System.EventHandler(this.comboArbol_SelectedIndexChanged);
            // 
            // ArbolSEC
            // 
            this.ArbolSEC.Location = new System.Drawing.Point(708, 161);
            this.ArbolSEC.Name = "ArbolSEC";
            this.ArbolSEC.Size = new System.Drawing.Size(80, 23);
            this.ArbolSEC.TabIndex = 5;
            this.ArbolSEC.Text = "Muestra Arbol";
            this.ArbolSEC.UseVisualStyleBackColor = true;
            this.ArbolSEC.Click += new System.EventHandler(this.ArbolSEC_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(583, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Arbol Sec";
            // 
            // comboArbolS
            // 
            this.comboArbolS.FormattingEnabled = true;
            this.comboArbolS.Location = new System.Drawing.Point(640, 160);
            this.comboArbolS.Name = "comboArbolS";
            this.comboArbolS.Size = new System.Drawing.Size(62, 21);
            this.comboArbolS.TabIndex = 9;
            this.comboArbolS.SelectedIndexChanged += new System.EventHandler(this.comboArbolS_SelectedIndexChanged);
            // 
            // CapturaRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.boton_IndiceSecu);
            this.Controls.Add(this.listaIndiceSECU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboArbolS);
            this.Controls.Add(this.comboArbol);
            this.Controls.Add(this.IndicesPrimariosCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Eliminar);
            this.Controls.Add(this.Modificar);
            this.Controls.Add(this.ArbolSEC);
            this.Controls.Add(this.muestraArbolP);
            this.Controls.Add(this.botonIndice);
            this.Controls.Add(this.Agregar);
            this.Controls.Add(this.DataVisualReg);
            this.Controls.Add(this.DataCaptura);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxEntidades);
            this.Name = "CapturaRegistro";
            this.Text = "CapturaRegistro";
            this.Load += new System.EventHandler(this.CapturaRegistro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataCaptura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataVisualReg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxEntidades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataCaptura;
        private System.Windows.Forms.DataGridView DataVisualReg;
        private System.Windows.Forms.Button Agregar;
        private System.Windows.Forms.Button botonIndice;
        private System.Windows.Forms.Button Modificar;
        private System.Windows.Forms.Button Eliminar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox IndicesPrimariosCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox listaIndiceSECU;
        private System.Windows.Forms.Button boton_IndiceSecu;
        private System.Windows.Forms.Button muestraArbolP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboArbol;
        private System.Windows.Forms.Button ArbolSEC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboArbolS;
    }
}