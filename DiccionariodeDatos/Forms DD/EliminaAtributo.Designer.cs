namespace DiccionariodeDatos
{
    partial class EliminaAtributo
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxA = new System.Windows.Forms.ComboBox();
            this.comboBoxE = new System.Windows.Forms.ComboBox();
            this.eliminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Atributos de Entidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Entidades";
            // 
            // comboBoxA
            // 
            this.comboBoxA.FormattingEnabled = true;
            this.comboBoxA.Location = new System.Drawing.Point(12, 108);
            this.comboBoxA.Name = "comboBoxA";
            this.comboBoxA.Size = new System.Drawing.Size(207, 21);
            this.comboBoxA.TabIndex = 8;
            this.comboBoxA.SelectedIndexChanged += new System.EventHandler(this.comboBoxA_SelectedIndexChanged);
            // 
            // comboBoxE
            // 
            this.comboBoxE.FormattingEnabled = true;
            this.comboBoxE.Location = new System.Drawing.Point(12, 29);
            this.comboBoxE.Name = "comboBoxE";
            this.comboBoxE.Size = new System.Drawing.Size(207, 21);
            this.comboBoxE.TabIndex = 6;
            this.comboBoxE.SelectedIndexChanged += new System.EventHandler(this.comboBoxE_SelectedIndexChanged);
            // 
            // eliminar
            // 
            this.eliminar.Location = new System.Drawing.Point(72, 181);
            this.eliminar.Name = "eliminar";
            this.eliminar.Size = new System.Drawing.Size(75, 23);
            this.eliminar.TabIndex = 11;
            this.eliminar.Text = "Eliminar";
            this.eliminar.UseVisualStyleBackColor = true;
            this.eliminar.Click += new System.EventHandler(this.eliminar_Click);
            // 
            // EliminaAtributo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 216);
            this.Controls.Add(this.eliminar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxA);
            this.Controls.Add(this.comboBoxE);
            this.Name = "EliminaAtributo";
            this.Text = "EliminaAtributo";
            this.Load += new System.EventHandler(this.EliminaAtributo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxA;
        private System.Windows.Forms.ComboBox comboBoxE;
        private System.Windows.Forms.Button eliminar;
    }
}