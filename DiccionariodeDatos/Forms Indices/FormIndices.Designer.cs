namespace DiccionariodeDatos
{
    partial class Indices
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
            this.DataIndiceP = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataIndiceP)).BeginInit();
            this.SuspendLayout();
            // 
            // DataIndiceP
            // 
            this.DataIndiceP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataIndiceP.Location = new System.Drawing.Point(26, 31);
            this.DataIndiceP.Name = "DataIndiceP";
            this.DataIndiceP.Size = new System.Drawing.Size(349, 386);
            this.DataIndiceP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Indice Primario";
            // 
            // Indices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataIndiceP);
            this.Name = "Indices";
            this.Text = "Indices";
            this.Load += new System.EventHandler(this.Indices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataIndiceP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView DataIndiceP;
        private System.Windows.Forms.Label label1;
    }
}