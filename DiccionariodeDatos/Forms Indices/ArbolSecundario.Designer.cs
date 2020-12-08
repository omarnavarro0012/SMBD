namespace DiccionariodeDatos
{
    partial class ArbolSecundario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataArbolSec = new System.Windows.Forms.DataGridView();
            this.DataBloque = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataArbolSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataBloque)).BeginInit();
            this.SuspendLayout();
            // 
            // DataArbolSec
            // 
            this.DataArbolSec.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataArbolSec.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DataArbolSec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataArbolSec.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataArbolSec.Location = new System.Drawing.Point(0, 0);
            this.DataArbolSec.Name = "DataArbolSec";
            this.DataArbolSec.Size = new System.Drawing.Size(678, 213);
            this.DataArbolSec.TabIndex = 0;
            this.DataArbolSec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataArbolSec_CellClick);
            // 
            // DataBloque
            // 
            this.DataBloque.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataBloque.BackgroundColor = System.Drawing.Color.Black;
            this.DataBloque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataBloque.DefaultCellStyle = dataGridViewCellStyle1;
            this.DataBloque.Location = new System.Drawing.Point(157, 219);
            this.DataBloque.Name = "DataBloque";
            this.DataBloque.Size = new System.Drawing.Size(275, 219);
            this.DataBloque.TabIndex = 1;
            // 
            // ArbolSecundario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 450);
            this.Controls.Add(this.DataBloque);
            this.Controls.Add(this.DataArbolSec);
            this.Name = "ArbolSecundario";
            this.Text = "ArbolSecundario";
            this.Load += new System.EventHandler(this.ArbolSecundario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataArbolSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataBloque)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataArbolSec;
        private System.Windows.Forms.DataGridView DataBloque;
    }
}