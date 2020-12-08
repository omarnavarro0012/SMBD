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
    public partial class ArbolPrimario : Form
    {
        List<Hoja> Arbol = new List<Hoja>();

        public ArbolPrimario()
        {
            //InitializeComponent();
            //crearColumnas();
        }

        public ArbolPrimario(object ArbolN)
        {
            Arbol = (List<Hoja>)ArbolN;
            InitializeComponent();
        }

        private void ArbolPrimario_Load(object sender, EventArgs e)
        {
            crearColumnas();
            ActualizaDataDatos();
            List<Hoja> Arbol = new List<Hoja>();
        }

        public void crearColumnas()
        {
            DataArbol.Rows.Clear();
            DataArbol.Columns.Clear();
            DataArbol.Columns.Add("TipoHoja", "TipoHoja");
            DataArbol.Columns.Add("DireccionHoja", "DireccionHoja");
            DataArbol.Columns.Add("P1", "P1");
            DataArbol.Columns.Add("K1", "K1");
            DataArbol.Columns.Add("P2", "P2");
            DataArbol.Columns.Add("K2", "K2");
            DataArbol.Columns.Add("P3", "P3");
            DataArbol.Columns.Add("K3", "K3");
            DataArbol.Columns.Add("P4", "P4");
            DataArbol.Columns.Add("K4", "K4");
            DataArbol.Columns.Add("P5", "P5");
        }

        private void ActualizaDataDatos()
        {
            try
            {
                DataArbol.Rows.Clear();
                for (int i = 0; i < Arbol.Count; i++)
                {
                    int rowCount = DataArbol.Rows.Count - 1;
                    int columIndex = 0;
                    DataArbol.Rows.Add(1);

                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].tipoHoja;
                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].dirnodo;
                    if (Arbol[i].tipoHoja == 'H')
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            try
                            {
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER; 
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero; 
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                columIndex--;
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = -1; 
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = -1;
                            }
                        }
                        DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].dirsignodo;
                    }
                    else
                    {
                        for(int j = 0;j<4;j++)
                        {
                            try
                            {
                                if (j == 0)
                                {
                                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirIZQ;
                                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero;
                                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER;
                                }
                                else
                                {
                                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].entero;
                                    DataArbol.Rows[rowCount].Cells[columIndex++].Value = Arbol[i].ListPointKey[j].dirDER;
                                }
                            }
                            catch (System.ArgumentOutOfRangeException) {
                                columIndex--;
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = -1;
                                DataArbol.Rows[rowCount].Cells[columIndex++].Value = -1;
                            }
                        }
                    }

                }

            }
            catch (System.NullReferenceException) { }
        }


    }
}
