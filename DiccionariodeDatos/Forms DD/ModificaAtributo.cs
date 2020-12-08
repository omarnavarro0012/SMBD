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
    public partial class ModificaAtributo : Form
    {
        private char[] NombreA = new char[35];//nombre del nodo.
        public char[] nombreA { get { return NombreA; } set { NombreA = value; } }

        public int longitud;

        public char tipoDato = new char();

        public int tipoindice;

        private List<Entidad> entidades;

        public string EntidadSeleccionada;
        public string AtributoSeleccionado;


        public ModificaAtributo(object Entidades)
        {
            entidades = (List<Entidad>)Entidades;
            InitializeComponent();
        }

        public ModificaAtributo()
        {
            InitializeComponent();
        }
        private void ModificaAtributo_Load(object sender, EventArgs e)
        {
            foreach (Entidad entidad in entidades)
            {
                comboBoxE.Items.Add(ConvertirNombre(entidad.nombre));
            }
            tipodato.Items.Add("Entero corto");
            tipodato.Items.Add("Cadena");
            typeIndice.Items.Add("0 Sin tipo");
            typeIndice.Items.Add("1 Clave Busqueda");
            typeIndice.Items.Add("2 Indice Primario");
            typeIndice.Items.Add("3 Indice Secundario");
            typeIndice.Items.Add("4 Arbol Primario");
            typeIndice.Items.Add("5 Arbol Secundario");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nuevonombre.Text != "" && longi.Text != "" && tipodato.SelectedIndex != -1 && typeIndice.SelectedIndex != -1)
            {
                nombreA = nuevonombre.Text.ToCharArray();
                longitud = Convert.ToInt32(longi.Text);
                //tipoindice = typeIndice.SelectedIndex;
            }
            else
            {
                MessageBox.Show("LLenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Close();
        }


        private void tipodato_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tipodato.SelectedIndex)
            {
                case 0:
                    tipoDato = 'E';
                    longitud = 4;
                    //Longi.Enabled = false;
                    longi.Text = longitud.ToString();
                    longi.Enabled = false;
                    longi.Refresh();
                    break;
                
                case 1:
                    longi.Enabled = true;
                    longi.ResetText();
                    longi.Refresh();
                    tipoDato = 'C';
                    break;
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
            comboBoxA.Items.Clear();

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

        private void typeIndice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoindice = typeIndice.SelectedIndex;
        }
    }
}
