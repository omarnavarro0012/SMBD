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
    public partial class Modifica : Form
    {
        #region Entidad
        char[] NombreEntidadA = new char[35];
        public char[] nombreEntidadA { get { return NombreEntidadA; } set { NombreEntidadA = value; } }

        char[] NombreEntidadN = new char[35];
        public char[] nombreEntidadN { get { return NombreEntidadN; } set { NombreEntidadN = value; } }
        #endregion
        List<Entidad> ModEntidades;

        public Modifica()
        {
            InitializeComponent();
        }
        public Modifica(object lista)
        {
            ModEntidades = (List<Entidad>)lista;
            InitializeComponent();
        }

        private void Modifica_Load(object sender, EventArgs e)
        {
            foreach(Entidad entidad in ModEntidades)
            {
                entidadesList.Items.Add(ConvertirNombre(entidad.nombre));
            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            nombreEntidadA = entidadesList.SelectedItem.ToString().ToCharArray();

            if (NuevoNombre.Text != "")
            { nombreEntidadN = NuevoNombre.Text.ToCharArray(); }
            this.Close();
        }

        public string ConvertirNombre(char[] name)
        {
            string regresada = "";

            for (int i = 0; i < name.Length; i++)
            {
                regresada += name[i];
            }
            return regresada;
        }
    }
}
