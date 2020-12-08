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
    public partial class EliminaEntidad : Form
    {
        private List<Entidad> entidades;

        public string EntidadSeleccionada;
        public EliminaEntidad()
        {
            InitializeComponent();
        }

        public EliminaEntidad(object Entidades)
        {
            entidades = (List<Entidad>)Entidades;
            InitializeComponent();
        }

        private void EliminaEntidad_Load(object sender, EventArgs e)
        {
            foreach (Entidad entidad in entidades)
            {
                comboBoxE.Items.Add(ConvertirNombre(entidad.nombre));
            }
        }

        private void comboBoxE_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadSeleccionada = comboBoxE.SelectedItem.ToString();
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

        private void eliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
