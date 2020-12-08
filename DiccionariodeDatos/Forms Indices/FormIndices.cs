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
    public partial class Indices : Form
    {
        string Directorio;
        private List<Entidad> Entidades;
        private Entidad entidadSeleccionada;
        //private int NumAtributosReg;
        private List<Ordenadas> lista;
        private char tipoDato;
        public Indices(string directorio, object entidads, object seleccionada)
        {
            Directorio = directorio;
            Entidades = (List<Entidad>)entidads;
            entidadSeleccionada = (Entidad)seleccionada;
        }
        public Indices(object listaindexada,char tipo)
        {
            lista = (List<Ordenadas>)listaindexada;
            tipoDato = tipo;
            InitializeComponent();
        }
        public Indices()
        {
            InitializeComponent();
        }
        private void Indices_Load(object sender, EventArgs e)
        {
            //solo obtenemos los Nombres de los Atributos que tienen indice primario para poder mostrarlos cuando seleccionemos 
            //el atributo en un dropbox para poder mostrarlos, solo se muestran los primarios por el momento,
            //apartir de cada atributo se crearan las columnas, solamente con el nombre del atributo
            //Se usara la lista de cada atributo.lista de indices
            crearColumnas();
            ActualizaDataDatos();

        }
        public void crearColumnas()
        {

            DataIndiceP.Rows.Clear();
            DataIndiceP.Columns.Clear();
            DataIndiceP.Columns.Add("Clave", "Clave");
            DataIndiceP.Columns.Add("Direccion", "Direccion");

        }
        private void ActualizaDataDatos()
        {
            try
            {
                DataIndiceP.Rows.Clear();
                for (int i = 0; i < lista.Count; i++)
                {
                    int rowCount = DataIndiceP.Rows.Count - 1;
                    int colCount = DataIndiceP.Columns.Count - 1;
                    DataIndiceP.Rows.Add(1);
                    for (int j = 0; j < 1; j++)
                    {
                        if (tipoDato == 'E')
                        {
                            DataIndiceP.Rows[rowCount].Cells[j].Value = Convert.ToString(lista[i].entero);
                        }
                        else
                        {
                            if (tipoDato == 'C')
                            {
                                DataIndiceP.Rows[rowCount].Cells[j].Value = lista[i].cadena;
                            }
                        }
                    }
                    DataIndiceP.Rows[rowCount].Cells[1].Value = Convert.ToString(lista[i].dir);
                }
            }
            catch (System.NullReferenceException) { }
        }
    }
}
