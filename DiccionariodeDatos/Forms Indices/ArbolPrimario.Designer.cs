namespace DiccionariodeDatos
{
    partial class ArbolPrimario
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
            this.DataArbol = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataArbol)).BeginInit();
            this.SuspendLayout();
            // 
            // DataArbol
            // 
            this.DataArbol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataArbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataArbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataArbol.Location = new System.Drawing.Point(0, 0);
            this.DataArbol.Name = "DataArbol";
            this.DataArbol.Size = new System.Drawing.Size(670, 270);
            this.DataArbol.TabIndex = 0;
            // 
            // ArbolPrimario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 270);
            this.Controls.Add(this.DataArbol);
            this.Name = "ArbolPrimario";
            this.Text = "ArbolPrimario";
            this.Load += new System.EventHandler(this.ArbolPrimario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataArbol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataArbol;
    }
}