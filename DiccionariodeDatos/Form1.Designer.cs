namespace DiccionariodeDatos
{
    partial class Diccionario
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Diccionario));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Nuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Abrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Guardar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Cerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nuevaentidad = new System.Windows.Forms.TextBox();
            this.EliminarEntidad = new System.Windows.Forms.Button();
            this.ModificarEntidad = new System.Windows.Forms.Button();
            this.AgregarEntidad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EntidadesList = new System.Windows.Forms.ComboBox();
            this.EliminarAtributo = new System.Windows.Forms.Button();
            this.ModificarAtributo = new System.Windows.Forms.Button();
            this.AgregarAtributo = new System.Windows.Forms.Button();
            this.GridEntidad = new System.Windows.Forms.DataGridView();
            this.GridAtributo = new System.Windows.Forms.DataGridView();
            this.AgRegi = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridEntidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridAtributo)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Nuevo,
            this.toolStripSeparator3,
            this.Abrir,
            this.toolStripSeparator4,
            this.guardarToolStripMenuItem,
            this.toolStripSeparator1,
            this.Guardar,
            this.toolStripSeparator2,
            this.Cerrar});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // Nuevo
            // 
            this.Nuevo.Name = "Nuevo";
            this.Nuevo.Size = new System.Drawing.Size(152, 22);
            this.Nuevo.Text = "Nuevo";
            this.Nuevo.Click += new System.EventHandler(this.Nuevo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // Abrir
            // 
            this.Abrir.Name = "Abrir";
            this.Abrir.Size = new System.Drawing.Size(152, 22);
            this.Abrir.Text = "Abrir";
            this.Abrir.Click += new System.EventHandler(this.Abrir_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // Guardar
            // 
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(152, 22);
            this.Guardar.Text = "Guardar Como";
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // Cerrar
            // 
            this.Cerrar.Name = "Cerrar";
            this.Cerrar.Size = new System.Drawing.Size(152, 22);
            this.Cerrar.Text = "Cerrar";
            this.Cerrar.Click += new System.EventHandler(this.Cerrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nuevaentidad);
            this.groupBox1.Controls.Add(this.EliminarEntidad);
            this.groupBox1.Controls.Add(this.AgregarEntidad);
            this.groupBox1.Controls.Add(this.ModificarEntidad);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entidad";
            // 
            // nuevaentidad
            // 
            this.nuevaentidad.Location = new System.Drawing.Point(107, 22);
            this.nuevaentidad.Name = "nuevaentidad";
            this.nuevaentidad.Size = new System.Drawing.Size(136, 20);
            this.nuevaentidad.TabIndex = 3;
            this.nuevaentidad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nuevaentidad_KeyUp);
            // 
            // EliminarEntidad
            // 
            this.EliminarEntidad.Location = new System.Drawing.Point(356, 22);
            this.EliminarEntidad.Name = "EliminarEntidad";
            this.EliminarEntidad.Size = new System.Drawing.Size(94, 24);
            this.EliminarEntidad.TabIndex = 2;
            this.EliminarEntidad.Text = "Eliminar Entidad";
            this.EliminarEntidad.UseVisualStyleBackColor = true;
            this.EliminarEntidad.Click += new System.EventHandler(this.EliminarEntidad_Click);
            // 
            // ModificarEntidad
            // 
            this.ModificarEntidad.Location = new System.Drawing.Point(249, 19);
            this.ModificarEntidad.Name = "ModificarEntidad";
            this.ModificarEntidad.Size = new System.Drawing.Size(101, 31);
            this.ModificarEntidad.TabIndex = 1;
            this.ModificarEntidad.Text = "Modificar Entidad";
            this.ModificarEntidad.UseVisualStyleBackColor = true;
            this.ModificarEntidad.Click += new System.EventHandler(this.ModificarEntidad_Click);
            // 
            // AgregarEntidad
            // 
            this.AgregarEntidad.Location = new System.Drawing.Point(6, 19);
            this.AgregarEntidad.Name = "AgregarEntidad";
            this.AgregarEntidad.Size = new System.Drawing.Size(95, 32);
            this.AgregarEntidad.TabIndex = 0;
            this.AgregarEntidad.Text = " Agregar Entidad";
            this.AgregarEntidad.UseVisualStyleBackColor = true;
            this.AgregarEntidad.Click += new System.EventHandler(this.AgregarEntidad_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.EntidadesList);
            this.groupBox2.Controls.Add(this.EliminarAtributo);
            this.groupBox2.Controls.Add(this.ModificarAtributo);
            this.groupBox2.Controls.Add(this.AgregarAtributo);
            this.groupBox2.Location = new System.Drawing.Point(478, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 91);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Atributo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Entidades";
            // 
            // EntidadesList
            // 
            this.EntidadesList.FormattingEnabled = true;
            this.EntidadesList.Location = new System.Drawing.Point(67, 19);
            this.EntidadesList.Name = "EntidadesList";
            this.EntidadesList.Size = new System.Drawing.Size(134, 21);
            this.EntidadesList.TabIndex = 7;
            this.EntidadesList.Text = "Entidades";
            this.EntidadesList.SelectedIndexChanged += new System.EventHandler(this.EntidadesList_SelectedIndexChanged);
            // 
            // EliminarAtributo
            // 
            this.EliminarAtributo.Location = new System.Drawing.Point(412, 17);
            this.EliminarAtributo.Name = "EliminarAtributo";
            this.EliminarAtributo.Size = new System.Drawing.Size(96, 22);
            this.EliminarAtributo.TabIndex = 2;
            this.EliminarAtributo.Text = "Eliminar Atributo";
            this.EliminarAtributo.UseVisualStyleBackColor = true;
            this.EliminarAtributo.Click += new System.EventHandler(this.EliminarAtributo_Click);
            // 
            // ModificarAtributo
            // 
            this.ModificarAtributo.Location = new System.Drawing.Point(309, 16);
            this.ModificarAtributo.Name = "ModificarAtributo";
            this.ModificarAtributo.Size = new System.Drawing.Size(97, 24);
            this.ModificarAtributo.TabIndex = 1;
            this.ModificarAtributo.Text = "Modificar Atributo";
            this.ModificarAtributo.UseVisualStyleBackColor = true;
            this.ModificarAtributo.Click += new System.EventHandler(this.ModificarAtributo_Click);
            // 
            // AgregarAtributo
            // 
            this.AgregarAtributo.Location = new System.Drawing.Point(207, 16);
            this.AgregarAtributo.Name = "AgregarAtributo";
            this.AgregarAtributo.Size = new System.Drawing.Size(96, 24);
            this.AgregarAtributo.TabIndex = 0;
            this.AgregarAtributo.Text = "Agregar Atributo";
            this.AgregarAtributo.UseVisualStyleBackColor = true;
            this.AgregarAtributo.Click += new System.EventHandler(this.AgregarAtributo_Click);
            // 
            // GridEntidad
            // 
            this.GridEntidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridEntidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridEntidad.Location = new System.Drawing.Point(12, 98);
            this.GridEntidad.Name = "GridEntidad";
            this.GridEntidad.Size = new System.Drawing.Size(325, 271);
            this.GridEntidad.TabIndex = 4;
            // 
            // GridAtributo
            // 
            this.GridAtributo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridAtributo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridAtributo.Location = new System.Drawing.Point(478, 124);
            this.GridAtributo.Name = "GridAtributo";
            this.GridAtributo.Size = new System.Drawing.Size(508, 269);
            this.GridAtributo.TabIndex = 5;
            // 
            // AgRegi
            // 
            this.AgRegi.Location = new System.Drawing.Point(12, 375);
            this.AgRegi.Name = "AgRegi";
            this.AgRegi.Size = new System.Drawing.Size(325, 25);
            this.AgRegi.TabIndex = 6;
            this.AgRegi.Text = "Agregar Registros";
            this.AgRegi.UseVisualStyleBackColor = true;
            this.AgRegi.Click += new System.EventHandler(this.AgRegi_Click);
            // 
            // Diccionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 414);
            this.Controls.Add(this.AgRegi);
            this.Controls.Add(this.GridAtributo);
            this.Controls.Add(this.GridEntidad);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Diccionario";
            this.Text = "Diccionario de Datos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridEntidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridAtributo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Abrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Guardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Cerrar;
        private System.Windows.Forms.ToolStripMenuItem Nuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EliminarEntidad;
        private System.Windows.Forms.Button ModificarEntidad;
        private System.Windows.Forms.Button AgregarEntidad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button EliminarAtributo;
        private System.Windows.Forms.Button ModificarAtributo;
        private System.Windows.Forms.Button AgregarAtributo;
        private System.Windows.Forms.DataGridView GridEntidad;
        private System.Windows.Forms.DataGridView GridAtributo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EntidadesList;
        private System.Windows.Forms.TextBox nuevaentidad;
        private System.Windows.Forms.Button AgRegi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
    }
}

