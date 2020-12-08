using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiccionariodeDatos
{
    public partial class CapturaRegistro : Form
    {
        private List<Entidad> Entidades;
        static string EntidadSeleccionada="";
        int indiceEntidadSeleccionada;
        int NumAtributosReg;
        string directorio;
        public string nombreArchivo;
        Entidad seleccionada;
        int numeroCol;
        int numeroRow;
        string nombreIndice;
        string nombreIndiceSecu;
        string nombreArbolP;
        string nombreArbolS;
        static Registro registroViejo = new Registro();
        FileStream archivoDD;

        public CapturaRegistro(object entidades,string Directorio,FileStream archivo)
        {
            archivoDD = archivo;
            Entidades = (List<Entidad>)entidades;
            directorio = Directorio;
            InitializeComponent();
        }
        private void CapturaRegistro_Load(object sender, EventArgs e)
        {
            foreach(Entidad ent in Entidades)
            {
                comboBoxEntidades.Items.Add(ent.NombreEntidad());
                ent.ordenarIndices();
               
                for (int i = 0; i < ent.indicesSecundarios.Count; i++)
                {
                    listaIndiceSECU.Items.Add(ent.Atributos[ent.indicesSecundarios[i]].NombreAtributo());
                }
                foreach (Atributo atributo in ent.Atributos)
                {
                    switch(atributo.tipoIndice)
                    {
                        case 2://Indice Primario
                            string Nombre = directorio + "\\" + atributo.GetID() + "_" + "Primario" + ".idx";
                            atributo.rutaIndice = Nombre;
                            
                            if (atributo.dirIndice != -1)
                            {
                                Archivo.LeerArchivoIndice(Nombre, atributo.archivoIndice, atributo, atributo.tipodato);
                                IndicesPrimariosCombo.Items.Clear();
                                for (int i = 0; i < ent.indicesPrimarios.Count; i++)
                                {
                                    IndicesPrimariosCombo.Items.Add(ent.Atributos[ent.indicesPrimarios[i]].NombreAtributo());
                                }
                            }
                        break;
                        case 3://Indice Secundario
                            Nombre = directorio + "\\" + atributo.GetID() + "_" + "Secundario" + ".idx";
                            atributo.rutaIndice = Nombre;
                            if (atributo.dirIndice != -1)
                            {
                                Archivo.LeerArchivoIndiceSecundario(Nombre, atributo.archivoIndice, atributo, atributo.tipodato);
                                listaIndiceSECU.Items.Clear();
                                ent.ordenarIndices();
                                for (int i = 0; i < ent.indicesSecundarios.Count; i++)
                                {
                                    listaIndiceSECU.Items.Add(ent.Atributos[ent.indicesSecundarios[i]].NombreAtributo());
                                }
                            }
                        break;
                        case 4://Arbol Primario
                            Nombre = directorio + "\\" + atributo.GetID() + "_" + "ArbolPrimario" + ".idx";
                            atributo.rutaIndice = Nombre;
                            if (atributo.dirIndice != -1)
                            {
                                if (atributo.Arbol.Count == 0)
                                { Archivo.LeerArbolPrimario(atributo, atributo.rutaIndice, atributo.archivoIndice); }
                                comboArbol.Items.Clear();
                                ent.ordenarIndices();
                                for (int i = 0; i < ent.ArbolPrimario.Count; i++)
                                {
                                    comboArbol.Items.Add(ent.Atributos[ent.ArbolPrimario[i]].NombreAtributo());
                                }
                            }
                            break;
                        case 5://Arbol Secundario
                            Nombre = directorio + "\\" + atributo.GetID() + "_" + "ArbolSecundario" + ".idx";
                            atributo.rutaIndice = Nombre;
                            if (atributo.dirIndice != -1)
                            {
                                //Funcion para leer el arbol si tiene alguno.
                                if (atributo.Arbol.Count == 0)
                                {
                                    Archivo.LeerArbolSecundario(atributo, atributo.rutaIndice, atributo.archivoIndice); 
                                }
                                comboArbolS.Items.Clear();
                                ent.ordenarIndices();
                                for (int i = 0; i < ent.ArbolSecu.Count; i++)
                                {
                                    comboArbolS.Items.Add(ent.Atributos[ent.ArbolSecu[i]].NombreAtributo());
                                }
                            }
                            break;
                    }
                }
            }
        }
        private void comboBoxEntidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataVisualReg.Rows.Clear();
            seleccionada = new Entidad();
            EntidadSeleccionada = (string)comboBoxEntidades.SelectedItem;
            string cad = "";
            for (int i = 0; i < EntidadSeleccionada.Length; i++)
            {
                if(EntidadSeleccionada[i] != '\0')
                { cad += EntidadSeleccionada[i]; }
            }

            EntidadSeleccionada = cad;

            foreach(Entidad en in Entidades)
            {
                if(en.NombreEntidad() == EntidadSeleccionada)
                {
                    seleccionada = en;
                }
            }
            indiceEntidadSeleccionada = comboBoxEntidades.SelectedIndex;

            foreach(Entidad enti in Entidades)
            {
                if(enti.NombreEntidad() == EntidadSeleccionada)
                {
                    if(enti.dirDatos == -1)
                    {
                        enti.rutaArchDatos = directorio + "\\" + enti.GetID() + ".dat"; 
                        Archivo.CreaArchivoDatos(enti.rutaArchDatos, enti.ArchivoDato);
                        crearColumnas();
                        ActualizaDataDatos(enti);
                    }else
                    {
                        enti.rutaArchDatos = directorio + "\\" + enti.GetID() + ".dat";
                        crearColumnas();
                        ActualizaDataDatos(enti);
                    }
                }
            }
            
            Entidad entReg = null;
            foreach (Entidad ent in Entidades)
            {
                if (ent.NombreEntidad() == EntidadSeleccionada)
                {
                    entReg = ent;
                    IndicesPrimariosCombo.Items.Clear();
                    listaIndiceSECU.Items.Clear();
                    comboArbol.Items.Clear();
                    comboArbolS.Items.Clear();
                    foreach (Atributo atributo in ent.Atributos)
                    {
                        switch (atributo.tipoIndice)
                        {
                            case 2://Indice Primario
                                string Nombre = directorio + "\\" + atributo.GetID() + "_" + "Primario" + ".idx";
                                atributo.rutaIndice = Nombre;

                                if (atributo.dirIndice != -1)
                                {
                                    
                                    Archivo.LeerArchivoIndice(Nombre, atributo.archivoIndice, atributo, atributo.tipodato);
                                    IndicesPrimariosCombo.Items.Clear();
                                    for (int i = 0; i < ent.indicesPrimarios.Count; i++)
                                    {
                                        IndicesPrimariosCombo.Items.Add(ent.Atributos[ent.indicesPrimarios[i]].NombreAtributo());
                                    }
                                }
                                break;
                            case 3://Indice Secundario
                                Nombre = directorio + "\\" + atributo.GetID() + "_" + "Secundario" + ".idx";
                                atributo.rutaIndice = Nombre;
                                if (atributo.dirIndice != -1)
                                {
                                    Archivo.LeerArchivoIndiceSecundario(Nombre, atributo.archivoIndice, atributo, atributo.tipodato);
                                    listaIndiceSECU.Items.Clear();
                                    ent.ordenarIndices();
                                    for (int i = 0; i < ent.indicesSecundarios.Count; i++)
                                    {
                                        listaIndiceSECU.Items.Add(ent.Atributos[ent.indicesSecundarios[i]].NombreAtributo());
                                    }
                                }
                                break;
                            case 4://Arbol Primario
                                Nombre = directorio + "\\" + atributo.GetID() + "_" + "ArbolPrimario" + ".idx";
                                atributo.rutaIndice = Nombre;
                                if (atributo.dirIndice != -1)
                                {
                                    if (atributo.Arbol.Count == 0)
                                    { Archivo.LeerArbolPrimario(atributo, atributo.rutaIndice, atributo.archivoIndice); }
                                    comboArbol.Items.Clear();
                                    ent.ordenarIndices();
                                    for (int i = 0; i < ent.ArbolPrimario.Count; i++)
                                    {
                                        comboArbol.Items.Add(ent.Atributos[ent.ArbolPrimario[i]].NombreAtributo());
                                    }
                                }
                                break;
                            case 5://Arbol Secundario
                                Nombre = directorio + "\\" + atributo.GetID() + "_" + "ArbolSecundario" + ".idx";
                                atributo.rutaIndice = Nombre;
                                if (atributo.dirIndice != -1)
                                {
                                    //Funcion para leer el arbol si tiene alguno.
                                    if (atributo.Arbol.Count == 0)
                                    {
                                        Archivo.LeerArbolSecundario(atributo, atributo.rutaIndice, atributo.archivoIndice);
                                    }
                                    comboArbolS.Items.Clear();
                                    ent.ordenarIndices();
                                    for (int i = 0; i < ent.ArbolSecu.Count; i++)
                                    {
                                        comboArbolS.Items.Add(ent.Atributos[ent.ArbolSecu[i]].NombreAtributo());
                                    }
                                }
                                break;
                        }
                    }
                    break;
                    //DireccionarRegistrosEntidad(ent);
                }
            }
        }
        private void Agregar_Click(object sender, EventArgs e)
        {
            Registro reg = new Registro();
            Entidad ent = new Entidad();
            bool escribe = true;

            reg.registros = new List<object>();

            for (int i = 0; i < NumAtributosReg; i++)
            {
                try
                {
                    if (Entidades[indiceEntidadSeleccionada].Atributos[i].tipodato == 'E')
                    {
                        int entero = Convert.ToInt32((object)DataCaptura.Rows[0].Cells[i].Value);
                        reg.registros.Add(entero);
                    }
                    else
                    {
                        if (Entidades[indiceEntidadSeleccionada].Atributos[i].tipodato == 'C')
                        {
                            string cadena = ConvertirNombre(DataCaptura.Rows[0].Cells[i].Value.ToString().ToCharArray());
                            if (Entidades[indiceEntidadSeleccionada].Atributos[i].longitud >= cadena.Length)
                            {
                                reg.registros.Add(cadena);
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Informacion Incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    escribe = false;
                }

            }
            if (escribe == true)
            {
                foreach (Entidad e1 in Entidades)
                {
                    bool encontrada = false;
                    if (e1.NombreEntidad() == EntidadSeleccionada)
                    {
                        ent = e1;
                        encontrada = true;
                    }
                    if(encontrada ==true)
                    {

                        reg.dirReG = Archivo.EncuentraDireccionRegistros(ent.rutaArchDatos, ent.ArchivoDato);
                        ent.Datos.Add(reg);
                        Archivo.GuardaRegistro(ent, reg);

                        if (ent.Datos.Count == 1 && (ent.ExistenIndicesyClave() == true || ent.ExistenIndicesSinCVE() == true) )
                        {
                            for (int i = 0; i < ent.Atributos.Count; i++)
                            {
                                switch (ent.Atributos[i].tipoIndice)
                                {
                                    case 2://Indice Primario
                                        string Nombre = directorio + "\\" + ent.Atributos[i].GetID() + "_" + "Primario" + ".idx";
                                        Archivo.CreaArchivoIndicePrimario(Nombre, ent.Atributos[i].archivoIndice);
                                        ent.Atributos[i].rutaIndice = Nombre;
                                        //ent.Atributos[i].datosIndiceS = new List<Ordenadas>();
                                        ent.Atributos[i].ordenadaC = new List<Ordenadas>();
                                        ent.Atributos[i].ordenadaE = new List<Ordenadas>();
                                        break;
                                    case 3://Indice Secundario
                                        Nombre = directorio + "\\" + ent.Atributos[i].GetID() + "_" + "Secundario" + ".idx";
                                        Archivo.CreaArchivoIndiceSecundario(Nombre, ent.Atributos[i].archivoIndice);
                                        ent.Atributos[i].rutaIndice = Nombre;
                                        ent.Atributos[i].datosIndiceS = new List<Ordenadas>();
                                        break;
                                    case 4://Arbol Primario
                                        Nombre = directorio + "\\" + ent.Atributos[i].GetID() + "_" + "ArbolPrimario" + ".idx";
                                        Archivo.CreaArchivoArbolP(Nombre, ent.Atributos[i].archivoIndice);
                                        ent.Atributos[i].rutaIndice = Nombre;
                                        ent.Atributos[i].Arbol = new List<Hoja>();
                                        break;
                                    case 5://Arbol SecundarioCreaArchivoArbolP
                                        Nombre = directorio + "\\" + ent.Atributos[i].GetID() + "_" + "ArbolSecundario" + ".idx";
                                        Archivo.CreaArchivoArbolP(Nombre, ent.Atributos[i].archivoIndice);
                                        ent.Atributos[i].rutaIndice = Nombre;
                                        ent.Atributos[i].Arbol = new List<Hoja>();
                                        break;
                                }
                            }
                        }


                        if (ent.ExisteCveBusqueda() == true)
                        {
                            if (ent.ExistenIndicesyClave() == true)
                            {
                                Archivo.DireccionamientoRegistrosCveBusquedaIndices(ent, ent.indexCveBusqueda, nombreArchivo, directorio,archivoDD,reg,1,null);
                            }
                            else
                            {
                                if (ent.ExisteCveBusqueda() == true)
                                {
                                    Archivo.DireccionamientoRegistrosCveBusqueda(ent, ent.indexCveBusqueda, nombreArchivo, directorio,archivoDD);
                                }
                            }
                        }
                        else
                        {
                            if (ent.ExisteCveBusqueda() == false && ent.ExistenIndicesSinCVE() == true)
                            {
                                Archivo.DireccionamientoRegistrosIndice(ent, ent.Datos, nombreArchivo, directorio, archivoDD,reg,1,null);
                            }
                            else
                            {
                                Archivo.DireccionamientoRegistros(ent, ent.Datos, nombreArchivo, directorio, archivoDD);
                            }
                        }

                        ActualizaDataDatos(ent);
                        
                        DataCaptura.Rows.Clear();
                    }
                }
                IndicesPrimariosCombo.Items.Clear();
                listaIndiceSECU.Items.Clear();
                comboArbol.Items.Clear();
                comboArbolS.Items.Clear();
                //checar los null
                for (int i = 0; i < ent.indicesPrimarios.Count; i++)
                {
                    IndicesPrimariosCombo.Items.Add(ent.Atributos[ent.indicesPrimarios[i]].NombreAtributo());
                }
                for (int i = 0; i < ent.indicesSecundarios.Count; i++)
                {
                    listaIndiceSECU.Items.Add(ent.Atributos[ent.indicesSecundarios[i]].NombreAtributo());
                }
                for (int i = 0; i < ent.ArbolPrimario.Count; i++)
                {
                    comboArbol.Items.Add(ent.Atributos[ent.ArbolPrimario[i]].NombreAtributo());
                }
                for (int i = 0; i < ent.ArbolSecu.Count; i++)
                {
                    comboArbolS.Items.Add(ent.Atributos[ent.ArbolSecu[i]].NombreAtributo());
                }
            }
        }       
        private void DireccionarRegistrosEntidad(Entidad ent,Registro reg,int llamada,Registro regViejo)
        {
            if (ent.ExisteCveBusqueda() == true)
            {
                if (ent.ExistenIndicesyClave() == true)
                {
                    Archivo.DireccionamientoRegistrosCveBusquedaIndices(ent, ent.indexCveBusqueda, nombreArchivo, directorio,archivoDD, reg, llamada,regViejo);
                }
                else
                {
                    if (ent.ExisteCveBusqueda() == true)
                    {
                        Archivo.DireccionamientoRegistrosCveBusqueda(ent, ent.indexCveBusqueda, nombreArchivo, directorio, archivoDD);
                    }
                }
            }
            else
            {
                if (ent.ExisteCveBusqueda() == false && ent.ExistenIndicesSinCVE() == true)
                {
                    Archivo.DireccionamientoRegistrosIndice(ent, ent.Datos, nombreArchivo, directorio, archivoDD,reg,llamada,regViejo);
                }
                else
                {
                    Archivo.DireccionamientoRegistros(ent, ent.Datos, nombreArchivo, directorio, archivoDD);
                }
            }
        }
        private void ActualizaDataDatos(Entidad e)
        {
            DataVisualReg.Rows.Clear();
            for (int i = 0; i < e.Datos.Count; i++)
            {
                int rowCount = DataVisualReg.Rows.Count - 1;
                int colCount = DataVisualReg.Columns.Count - 1;
                DataVisualReg.Rows.Add(1);
                DataVisualReg.Rows[rowCount].Cells[0].Value = e.Datos[i].dirReG;
                for (int j = 0; j < NumAtributosReg; j++)
                {
                    if (e.Atributos[j].tipodato == 'E')
                    {
                        DataVisualReg.Rows[rowCount].Cells[j + 1].Value = Convert.ToString(e.Datos[i].registros[j]);
                    }
                    else
                    {
                        if (e.Atributos[j].tipodato == 'C')
                        {
                            DataVisualReg.Rows[rowCount].Cells[j + 1].Value = (string)e.Datos[i].registros[j];
                        }
                    }
                }
                DataVisualReg.Rows[rowCount].Cells[colCount].Value = e.Datos[i].dirSigR;
            }

        }
        public void crearColumnas()
        {

            DataVisualReg.Rows.Clear();
            DataVisualReg.Columns.Clear();

            DataCaptura.Rows.Clear();
            DataCaptura.Columns.Clear();


            Entidad en = null;
            int numAtributos = 0;
            foreach (Entidad ent in Entidades)
            {
                if (ent.NombreEntidad() == EntidadSeleccionada)
                {
                    en = ent;
                    numAtributos = ent.Atributos.Count;
                    NumAtributosReg = ent.Atributos.Count;
                }
            }
            DataVisualReg.Columns.Add("DirRegistro", "DirRegistro");
            DataCaptura.Columns.Add("", "");
            for (int i = 0; i < numAtributos; i++)
            {
                DataGridViewColumn columna = new DataGridViewColumn();
                columna.Name = en.Atributos[i].NombreAtributo();
                columna.HeaderText = en.Atributos[i].NombreAtributo();
                columna.ReadOnly = false;
                columna.CellTemplate = DataVisualReg.Columns[0].CellTemplate;
                DataVisualReg.Columns.Add(columna);

                DataGridViewColumn columna2 = new DataGridViewColumn();
                columna2.Name = en.Atributos[i].NombreAtributo();
                columna2.HeaderText = en.Atributos[i].NombreAtributo();
                columna2.CellTemplate = DataCaptura.Columns[0].CellTemplate;
                DataCaptura.Columns.Add(columna2);
            }
            DataVisualReg.Columns.Add("DirRegistroSig", "DirRegistroSig");
            DataVisualReg.Columns[0].ReadOnly = true;
            DataVisualReg.Columns[DataVisualReg.Columns.Count-1].ReadOnly = true;
            DataCaptura.Columns.RemoveAt(0);
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
        private void DataVisualReg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            numeroCol = -1;
            numeroRow = -1;
            numeroCol = DataVisualReg.CurrentCell.ColumnIndex;
            numeroRow = DataVisualReg.CurrentCell.RowIndex;
        }
        private void Modificar_Click(object sender, EventArgs e)
        {
            int numeroEntidad = -1;
            int numeroREG = -1;
            long direccion = -1;
            List<object> r = new List<object>();
            long dirS = -1;
            
            for (int j = 0; j < Entidades.Count; j++)
            {
                bool encontrado = false;
                for (int i = 0; i < Entidades[j].Datos.Count; i++)
                {
                    
                    if(Entidades[j].Datos[i].dirReG == Convert.ToInt64(DataVisualReg.Rows[numeroRow].Cells[0].Value))
                    {
                        numeroEntidad = j;
                        numeroREG = i;
                        /*registroViejo = new Registro();
                        registroViejo = Entidades[j].Datos[i];*/
                        direccion = Entidades[j].Datos[i].dirReG;
                        r = Entidades[j].Datos[i].registros;
                        dirS = Entidades[j].Datos[i].dirSigR;
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado == true)
                {
                    registroViejo = new Registro();
                    registroViejo.dirReG = direccion;
                    registroViejo.registros = r;
                    registroViejo.dirSigR = dirS;
                    break; }
            }

            for (int i = 0; i < DataVisualReg.Columns.Count; i++)
            {
                Entidades[numeroEntidad].Datos[numeroREG].dirReG = (long)DataVisualReg.Rows[numeroRow].Cells[i].Value;
                i++;
                Entidades[numeroEntidad].Datos[numeroREG].registros = new List<object>();
                foreach (Atributo a in Entidades[numeroEntidad].Atributos)
                {
                    if(a.tipodato == 'E')
                    {
                        Entidades[numeroEntidad].Datos[numeroREG].registros.Add(Convert.ToInt32(DataVisualReg.Rows[numeroRow].Cells[i].Value));
                        i++;
                    }
                    else
                    {
                        if(a.tipodato == 'C')
                        {
                            Entidades[numeroEntidad].Datos[numeroREG].registros.Add(Convert.ToString(DataVisualReg.Rows[numeroRow].Cells[i].Value));
                            i++;
                        }
                    }
                }
                Entidades[numeroEntidad].Datos[numeroREG].dirSigR = (long)DataVisualReg.Rows[numeroRow].Cells[i].Value;
                Archivo.ModificaRegristro(Entidades[numeroEntidad].ArchivoDato, Entidades[numeroEntidad].rutaArchDatos, Entidades[numeroEntidad], Entidades[numeroEntidad].Datos[numeroREG]);
                DireccionarRegistrosEntidad(Entidades[numeroEntidad], Entidades[numeroEntidad].Datos[numeroREG], 2,registroViejo);
                ActualizaDataDatos(Entidades[numeroEntidad]);
            }
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            int numeroEntidad = -1;
            int numeroREG = -1;
            Registro reg = new Registro();
            for (int j = 0; j < Entidades.Count; j++)
            {
                for (int i = 0; i < Entidades[j].Datos.Count; i++)
                {
                    try
                    {
                        if (Entidades[j].Datos[i].dirReG == Convert.ToInt64(DataVisualReg.Rows[numeroRow].Cells[0].Value))
                        {
                            numeroEntidad = j;
                            numeroREG = i;
                            reg = Entidades[j].Datos[i];
                            Archivo.ElimarReg(Entidades[numeroEntidad], Entidades[j].Datos[i].dirReG);
                            Entidades[numeroEntidad].Datos.RemoveAt(numeroREG);
                            DireccionarRegistrosEntidad(Entidades[numeroEntidad],reg,3,null);
                            break;
                        }
                    }
                    catch (System.ArgumentOutOfRangeException) {
                        numeroCol = DataVisualReg.CurrentCell.ColumnIndex;
                        numeroRow = DataVisualReg.CurrentCell.RowIndex;
                        i = 0;
                    }
                }
            }

            //Entidades[numeroEntidad].Datos.RemoveAt(numeroREG);
            ActualizaDataDatos(Entidades[numeroEntidad]);

        }
        private void IndicesPrimariosCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            nombreIndice = (string)IndicesPrimariosCombo.SelectedItem;
        }
        private void botonIndice_Click(object sender, EventArgs e)
        {
            if (nombreIndice != "")
            {
                Indices indices = new Indices();
                foreach (Entidad entidad in Entidades)
                {
                    if (entidad.NombreEntidad() == EntidadSeleccionada)
                    {
                        foreach (Atributo atributo in entidad.Atributos)
                        {
                            if (atributo.NombreAtributo() == nombreIndice)
                            {
                                if (atributo.tipodato == 'E')
                                {
                                    indices = new Indices(atributo.ordenadaE, atributo.tipodato);
                                    indices.ShowDialog();
                                    break;
                                }
                                else
                                {
                                    if (atributo.tipodato == 'C')
                                    {
                                        indices = new Indices(atributo.ordenadaC, atributo.tipodato);
                                        indices.ShowDialog();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                //Indices indices = new Indices();
                //indices.ShowDialog();
            }
        }
        private void listaIndiceSECU_SelectedIndexChanged(object sender, EventArgs e)
        {
            nombreIndiceSecu = (string)listaIndiceSECU.SelectedItem;
        }
        private void boton_IndiceSecu_Click(object sender, EventArgs e)
        {
            if (nombreIndiceSecu != "")
            {
                FormIndiceSecundario indiceSecundario = new FormIndiceSecundario();
                foreach(Entidad ent in Entidades)
                {
                    if ( ent.NombreEntidad() == EntidadSeleccionada)
                    {
                        foreach (Atributo ar in ent.Atributos)
                        {
                            if (ar.NombreAtributo() == nombreIndiceSecu)
                            {
                                if (ar.tipodato == 'E')
                                {
                                    indiceSecundario = new FormIndiceSecundario(ar.datosIndiceS, ar.tipodato);
                                    indiceSecundario.ShowDialog();
                                    break;
                                }
                                if (ar.tipodato == 'C')
                                {
                                    indiceSecundario = new FormIndiceSecundario(ar.datosIndiceS, ar.tipodato);
                                    indiceSecundario.ShowDialog();
                                    break;
                                }
                            }
                        }
                    }
                }

                //indiceSecundario.ShowDialog();
            }
        }
        private void comboArbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            nombreArbolP = (string)comboArbol.SelectedItem;
        }
        private void muestraArbolP_Click(object sender, EventArgs e)
        {
            if (nombreArbolP != "")
            {
                ArbolPrimario arbolprimario = new ArbolPrimario();
                foreach (Entidad ent in Entidades)
                {
                    if (ent.NombreEntidad() == EntidadSeleccionada)
                    {
                        foreach (Atributo ar in ent.Atributos)
                        {
                            if (ar.NombreAtributo() == nombreArbolP)
                            {
                                arbolprimario = new ArbolPrimario(ar.Arbol);
                                arbolprimario.ShowDialog();
                                break;
                            }
                        }
                    }
                }

               //arbolprimario.ShowDialog();
            }
        }
        private void comboArbolS_SelectedIndexChanged(object sender, EventArgs e)
        {
            nombreArbolS = (string)comboArbolS.SelectedItem;
        }
        private void ArbolSEC_Click(object sender, EventArgs e)
        {
            if (nombreArbolS != "")
            {
                ArbolSecundario arbolSecundario = new ArbolSecundario();
                foreach (Entidad ent in Entidades)
                {
                    if (ent.NombreEntidad() == EntidadSeleccionada)
                    {
                        foreach (Atributo ar in ent.Atributos)
                        {
                            if (ar.NombreAtributo() == nombreArbolS)
                            {
                                arbolSecundario = new ArbolSecundario(ar.Arbol);
                                arbolSecundario.ShowDialog();
                                break;
                            }
                        }
                    }
                }
                
            }
        }
        private void DataCaptura_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Agregar_Click(this, null);
            }
        }
    }
}
