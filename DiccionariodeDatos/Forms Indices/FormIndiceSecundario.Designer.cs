namespace DiccionariodeDatos
{
    partial class FormIndiceSecundario
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
            this.BloquePrincipal = new System.Windows.Forms.DataGridView();
            this.dataBloques = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BloquePrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBloques)).BeginInit();
            this.SuspendLayout();
            // 
            // BloquePrincipal
            // 
            this.BloquePrincipal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BloquePrincipal.Location = new System.Drawing.Point(12, 30);
            this.BloquePrincipal.Name = "BloquePrincipal";
            this.BloquePrincipal.Size = new System.Drawing.Size(318, 408);
            this.BloquePrincipal.TabIndex = 0;
            this.BloquePrincipal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BloquePrincipal_CellClick);
            // 
            // dataBloques
            // 
            this.dataBloques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataBloques.Location = new System.Drawing.Point(336, 30);
            this.dataBloques.Name = "dataBloques";
            this.dataBloques.Size = new System.Drawing.Size(338, 408);
            this.dataBloques.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bloque Principal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bloque de : ";
            // 
            // FormIndiceSecundario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataBloques);
            this.Controls.Add(this.BloquePrincipal);
            this.Name = "FormIndiceSecundario";
            this.Text = "FormIndiceSecundario";
            this.Load += new System.EventHandler(this.FormIndiceSecundario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BloquePrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBloques)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BloquePrincipal;
        private System.Windows.Forms.DataGridView dataBloques;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}