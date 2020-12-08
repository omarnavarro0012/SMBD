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
    public partial class EliminaAtributo : Form
    {

        private List<Entidad> entidades;

        public string EntidadSeleccionada;
        public string AtributoSeleccionado;


        public EliminaAtributo(object Entidades)
        {
            entidades = (List<Entidad>)Entidades;
            InitializeComponent();
        }

        public EliminaAtributo()
        {
            InitializeComponent();
        }

        private void EliminaAtributo_Load(object sender, EventArgs e)
        {
            foreach (Entidad entidad in entidades)
            {
                comboBoxE.Items.Add(ConvertirNombre(entidad.nombre));
            }
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

        private void comboBoxE_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadSeleccionada = comboBoxE.SelectedItem.ToString();

            foreach (Entidad entidad in entidades)
            {
                if (EntidadSeleccionada == ConvertirNombre(entidad.nombre))
                {
                    foreach (Atributo a in entidad.Atributos)
                    {
                        comboBoxA.Items.Add(ConvertirNombre(a.nombre));
                    }
                }
            }
        }

        private void comboBoxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtributoSeleccionado = comboBoxA.SelectedItem.ToString();
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
