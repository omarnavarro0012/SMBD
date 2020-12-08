using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiccionariodeDatos
{
    public partial class ArbolSecundario : Form
    {
        List<Hoja> Arbol = new List<Hoja>();
        public ArbolSecundario(object tree)
        {
            Arbol = (List<Hoja>)tree;
            InitializeComponent();
        }
        public ArbolSecundario()
        {
            InitializeComponent();
            crearColumnas();
        }

        private void ArbolSecundario_Load(object sender, EventArgs e)
        {
            crearColumnas();
            ActualizaDataDatos();
        }

        public void crearColumnas()
        {
            DataArbolSec.Rows.Clear();
            DataArbolSec.Columns.Clear();
            DataArbolSec.Columns.Add("TipoHoja", "TipoHoja");
            DataArbolSec.Columns.Add("DireccionHoja", "DireccionHoja");
            DataArbolSec.Columns.Add("1", "P1");
            DataArbolSec.Columns.Add("1", "K1");
            DataArbolSec.Columns.Add("2", "P2");
            DataArbolSec.Columns.Add("2", "K2");
            DataArbolSec.Columns.Add("3", "P3");
            DataArbolSec.Columns.Add("3", "K3");
            DataArbolSec.Columns.Add("4", "P4");
            DataArbolSec.Columns.Add("4", "K4");
            DataArbolSec.Columns.Add("P5/DirSIG", "P5/DirSIG");
        }

        private void ActualizaDataDatos()
        {
            try
            {
                DataArbolSec.Rows.Clear();
                for (int i = 0; i < Arbol.Count; i++)
                {
                    int rowCount = DataArbolSec.Rows.Count - 1;
                    int columIndex = 0;
                    DataArbolSec.Rows.Add(1);

                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].tipoHoja;
                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].dirnodo;
                    if (Arbol[i].tipoHoja == 'H')
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            try
                            {
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER;
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero;
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                columIndex--;
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = -1;
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = -1;
                            }
                        }
                        DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].dirsignodo;
                    }
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            try
                            {
                                if (j == 0)
                                {
                                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirIZQ;
                                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero;
                                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER;
                                }
                                else
                                {
                                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero;
                                    DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER;
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                columIndex--;
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = -1;
                                DataArbolSec.Rows[rowCount].Cells[columIndex++].Value = -1;
                            }
                        }
                    }

                }

            }
            catch (System.NullReferenceException) { }
        }

        private void ActualizaDataBloques(int columna,int renglon)
        {
            try
            {
                DataBloque.Rows.Clear();
                for (int i = 0; i < Arbol.Count; i++)
                {
                    if (i == renglon)
                    {
                        for (int j = 0; j < Arbol[i].ListPointKey.Count; j++)
                        {
                            if(columna-1 == j )
                            {
                                for (int k = 0; k < Arbol[i].ListPointKey[j].lista.Count; k++)
                                {
                                    int rowCount = DataBloque.Rows.Count - 1;
                                    int colCount = DataBloque.Columns.Count - 1;
                                    DataBloque.Rows.Add(1);
                                    DataBloque.Rows[rowCount].Cells[0].Value = Convert.ToString(Arbol[i].ListPointKey[j].lista[k].dir);
                                }
                                break;
                            }
                        }
                        break;
                    }

                }
            }
            catch (System.NullReferenceException) { }
        }

        private void DataArbolSec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;
            try { 
            string colun = DataArbolSec.Columns[column].Name;
            column = Convert.ToInt32(colun);
                try
                {
                    if ((char)DataArbolSec.Rows[row].Cells[0].Value == 'H')
                    {
                        crearColumnasBloques();
                        ActualizaDataBloques(column, row);
                    }
                }
                catch (System.NullReferenceException) {
                    MessageBox.Show("Celda Sin VALORES","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        } catch (FormatException) { MessageBox.Show("Celda Sin VALORES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
}

        public void crearColumnasBloques()
        {

            DataBloque.Rows.Clear();
            DataBloque.Columns.Clear();
            DataBloque.Columns.Add("Direccion", "Direccion");
            //BloquePrincipal.Columns.Add(columna);
            /*dataBloques.Columns.Add("DirRegistro", "DirRegistro");
            dataBloques.Columns[0].ReadOnly = true;
            dataBloques.Columns[BloquePrincipal.Columns.Count - 1].ReadOnly = true;*/
        }
    }
}
