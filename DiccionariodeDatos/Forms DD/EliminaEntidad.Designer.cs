namespace DiccionariodeDatos
{
    partial class EliminaEntidad
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
            this.eliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxE = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // eliminar
            // 
            this.eliminar.Location = new System.Drawing.Point(69, 81);
            this.eliminar.Name = "eliminar";
            this.eliminar.Size = new System.Drawing.Size(75, 23);
            this.eliminar.TabIndex = 16;
            this.eliminar.Text = "Eliminar";
            this.eliminar.UseVisualStyleBackColor = true;
            this.eliminar.Click += new System.EventHandler(this.eliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Entidades";
            // 
            // comboBoxE
            // 
            this.comboBoxE.FormattingEnabled = true;
            this.comboBoxE.Location = new System.Drawing.Point(12, 26);
            this.comboBoxE.Name = "comboBoxE";
            this.comboBoxE.Size = new System.Drawing.Size(207, 21);
            this.comboBoxE.TabIndex = 12;
            this.comboBoxE.SelectedIndexChanged += new System.EventHandler(this.comboBoxE_SelectedIndexChanged);
            // 
            // EliminaEntidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 120);
            this.Controls.Add(this.eliminar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxE);
            this.Name = "EliminaEntidad";
            this.Text = "EliminaEntidad";
            this.Load += new System.EventHandler(this.EliminaEntidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button eliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxE;
    }
}