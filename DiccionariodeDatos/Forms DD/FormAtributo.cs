﻿using System;
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
    public partial class FormAtributo : Form
    {
        #region Variables
        private char[] NombreA = new char[35];//nombre del nodo.
        public char[] nombreA { get { return NombreA; } set { NombreA = value; } }

        public int longitud;

        public char tipoDato = new char();

        public int tipoindice;

        public bool lleno;

        private List<Entidad> Entidads;
        #endregion

        public FormAtributo(object Entidades)
        {
            Entidads = (List<Entidad>)Entidades;
            InitializeComponent();
        }

        private void FormAtributo_Load(object sender, EventArgs e)
        {
            comboTipodato.Items.Add("Entero corto");
            comboTipodato.Items.Add("Cadena");
            tipoindiceCombo.Items.Add("0 Sin tipo");
            tipoindiceCombo.Items.Add("1 Clave Primaria");
            tipoindiceCombo.Items.Add("2 Clave Foranea");
            tipoindiceCombo.Items.Add("3 Hash");
            
            foreach (Entidad enti in Entidads)
            {
                if (enti.ExistePK())
                {
                    entidadesCombo.Items.Add(enti.NombreEntidad());
                }
            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            if (nombreatributo.Text != "" && Longi.Text != "" && comboTipodato.SelectedIndex != -1 && tipoindiceCombo.SelectedIndex != -1)
            {
                nombreA = nombreatributo.Text.ToCharArray();
                longitud = Convert.ToInt32(Longi.Text);
                if (comboTipodato.SelectedIndex != -1 && (tipoindiceCombo.SelectedIndex != 4 && tipoindiceCombo.SelectedIndex != 5))
                {
                    tipoindice = tipoindiceCombo.SelectedIndex;
                    lleno = true;
                    this.Close();
                }
                else
                {
                    if (comboTipodato.SelectedIndex == 0 && (tipoindiceCombo.SelectedIndex == 4 || tipoindiceCombo.SelectedIndex == 5))
                    {
                        tipoindice = tipoindiceCombo.SelectedIndex;
                        lleno = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("EL tipo de dato para Arbol debe ser \'E\' ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("LLenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void ComboTipodato_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboTipodato.SelectedIndex)
            {
                case 0:
                    tipoDato = 'E';
                    longitud = 4;
                    Longi.Text = longitud.ToString();
                    Longi.Enabled = false;
                    Longi.Refresh();
                    break;
                    
                case 1:
                    Longi.Enabled = true;
                    Longi.ResetText();
                    Longi.Refresh();
                    tipoDato = 'C';
                    break;
            }
        }

        private void FormAtributo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing && lleno != true)
            {
                lleno = false;
            }
        }

        private void tipoindiceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipoindiceCombo.SelectedIndex == 2)
            {
                FKlabel.Visible = true;
                entidadesCombo.Visible = true;
            }
            else
            {
                FKlabel.Visible = false;
                entidadesCombo.Visible = false;
            }
        }
    }
}
