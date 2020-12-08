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
    public partial class FormIndiceSecundario : Form
    {
        private List<Ordenadas> listaSecundario;
        char tipoD;
        int RowClick;
        public FormIndiceSecundario()
        {
            InitializeComponent();
        }
        public FormIndiceSecundario(object ordenadas,char tipo)
        {
            listaSecundario = (List<Ordenadas>)ordenadas;
            tipoD = tipo;
            InitializeComponent();
        }
        private void FormIndiceSecundario_Load(object sender, EventArgs e)
        {
            crearColumnasBloquePrincipal();
            ActualizaDataBloquePrincipal();
        }
        public void crearColumnasBloquePrincipal()
        {

            BloquePrincipal.Rows.Clear();
            BloquePrincipal.Columns.Clear();
            BloquePrincipal.Columns.Add("Clave", "Clave");
            BloquePrincipal.Columns.Add("DirRegistro", "DirRegistro");
            //BloquePrincipal.Columns.Add(columna);
            BloquePrincipal.Columns[0].ReadOnly = true;
            BloquePrincipal.Columns[BloquePrincipal.Columns.Count - 1].ReadOnly = true;
        }
        private void ActualizaDataBloquePrincipal()
        {
            try
            {
                BloquePrincipal.Rows.Clear();
                for (int i = 0; i < listaSecundario.Count; i++)
                {
                    int rowCount = BloquePrincipal.Rows.Count - 1;
                    int colCount = BloquePrincipal.Columns.Count - 1;
                    BloquePrincipal.Rows.Add(1);
                    for (int j = 0; j < 1; j++)
                    {
                        if (tipoD== 'E')
                        {
                            BloquePrincipal.Rows[rowCount].Cells[j].Value = Convert.ToString(listaSecundario[i].entero);
                        }
                        else
                        {
                            if (tipoD == 'C')
                            {
                                BloquePrincipal.Rows[rowCount].Cells[j].Value = listaSecundario[i].cadena;
                            }
                        }
                    }
                    BloquePrincipal.Rows[rowCount].Cells[1].Value = Convert.ToString(listaSecundario[i].dir);
                }
            }
            catch (System.NullReferenceException) { }
        }
        private void BloquePrincipal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowClick = e.RowIndex;
            crearColumnasBloques();
            ActualizaDataBloques(RowClick);
        }
        public void crearColumnasBloques()
        {

            dataBloques.Rows.Clear();
            dataBloques.Columns.Clear();
            dataBloques.Columns.Add("Direccion", "Direccion");
            //BloquePrincipal.Columns.Add(columna);
            /*dataBloques.Columns.Add("DirRegistro", "DirRegistro");
            dataBloques.Columns[0].ReadOnly = true;
            dataBloques.Columns[BloquePrincipal.Columns.Count - 1].ReadOnly = true;*/
        }
        private void ActualizaDataBloques(int bloque)
        {
            try
            {
                dataBloques.Rows.Clear();
                for (int i = 0; i < listaSecundario.Count; i++)
                {
                    if (i == bloque)
                    {
                        for (int j = 0; j < listaSecundario[i].lista.Count; j++)
                        {
                            int rowCount = dataBloques.Rows.Count - 1;
                            int colCount = dataBloques.Columns.Count - 1;
                            dataBloques.Rows.Add(1);
                            dataBloques.Rows[rowCount].Cells[0].Value = Convert.ToString(listaSecundario[i].lista[j].dir);
                        }
                    }
                    
                }
            }
            catch (System.NullReferenceException) { }
        }    
    }
}