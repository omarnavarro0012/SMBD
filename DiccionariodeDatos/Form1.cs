using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DiccionariodeDatos
{
    public partial class Diccionario : Form
    {
        #region  Variables
        FileStream archivo;
        private string directorio;
        List<Entidad> Entidades;
        string nombreArchivo;
        long Cabeza = -1;
        static string EntidadSeleccionada="";
        static string entidadSelect = "";
        #endregion

        public Diccionario()
        {
            InitializeComponent();
        }

        #region Formulario 
        private void Form1_Load(object sender, EventArgs e)
        {
            directorio = Environment.CurrentDirectory + @"\Archivos";
            GridEntidad.AutoResizeColumns();
            GridEntidad.ColumnCount = 1;
            GridEntidad.Columns[0].Name = "Nombre";
            /*GridEntidad.Columns[0].Name = "IDEntidad";
            GridEntidad.Columns[2].Name = "Dir Entidad";
            GridEntidad.Columns[3].Name = "Dir Atributo";
            GridEntidad.Columns[4].Name = "Dir Datos";
            GridEntidad.Columns[5].Name = "Dir Siguiente Entidad";*/
            GridAtributo.AutoResizeColumns();
            GridAtributo.ColumnCount = 4;
            GridAtributo.Columns[0].Name = "Nombre Atributo";
            GridAtributo.Columns[1].Name = "Tipo Dato";
            GridAtributo.Columns[2].Name = "Longitud";
            GridAtributo.Columns[3].Name = "Tipo Indice";
            /*GridAtributo.Columns[4].Name = "Dir Atributo";
            GridAtributo.Columns[0].Name = "ID Atributo";
            GridAtributo.Columns[6].Name = "Dir Indice";
            GridAtributo.Columns[7].Name = "Dir Siguiente Atributo";*/
            Entidades = new List<Entidad>();

        }
        private void Nuevo_Click(object sender, EventArgs e)
        {
            Entidades = new List<Entidad>();
            GridEntidad.Rows.Clear();
            GridAtributo.Rows.Clear();
            EntidadesList.Items.Clear();
            entidadSelect = EntidadSeleccionada = "";


            SaveFileDialog saveFileDialog = new SaveFileDialog {Title= "Crear Nuevo archivo.", Filter = "Diccionario de Datos (*.dd)|*.dd" };
            saveFileDialog.InitialDirectory = directorio;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = saveFileDialog.FileName;
                archivo = new FileStream(nombreArchivo, FileMode.Create);
                Archivo.GuardaCabecera( archivo, Cabeza);
                archivo.Close();
            }
        }
        private void Abrir_Click(object sender, EventArgs e)
        {
            LeeDiccionario();           
        }
        private void LeeDiccionario()
        {
            OpenFileDialog open = new OpenFileDialog {Title = "Abrir Archivo", Filter = "Diccionario de Datos (*.dd)|*.dd" };
            if (open.ShowDialog() != DialogResult.OK)
                return;
            EntidadesList.Items.Clear();
            entidadSelect = EntidadSeleccionada = "";
            EntidadesList.Text = "";
            GridAtributo.Rows.Clear();
            nombreArchivo = open.FileName;
            Text = "Diccionario de Datos | " + nombreArchivo;
            Cabeza = Archivo.LeeCabecera(nombreArchivo);
            Entidades = Archivo.Leer(Cabeza, nombreArchivo,directorio);
            ActualizaDataEntidades();
        }

        private void Guardar_Click_1(object sender, EventArgs e)//guardar como
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "GuardarComo...",
                FileName = nombreArchivo,
                Filter = "Diccionario de Datos (*.dd)|*.dd",
                DefaultExt = "(*.dd) | *.dd",
                InitialDirectory = directorio,
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = save.FileName;
                if (nombreArchivo != archivo.Name)
                {
                    File.Copy(archivo.Name, nombreArchivo);
                }

            }
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "GuardarComo...",
                FileName = nombreArchivo,
                Filter = "Diccionario de Datos (*.dd)|*.dd",
                DefaultExt = "(*.dd) | *.dd",
                InitialDirectory = directorio,
                AddExtension = true
            };
            nombreArchivo = save.FileName;

        }
        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Archivo
        public void EscribirArchivo(string Nombrearchivo)
        {
            BinaryWriter bw;   
            FileStream archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.Write);
            bw = new BinaryWriter(archivo);
            foreach (Entidad entidad in Entidades)
            { 
                byte[] bytes  = new byte[5];
                for (int i = 0; i < 5; i++)
                { bytes[i] = Convert.ToByte(entidad.id[i]); }
                bw.Write(bytes);
                bw.Write(entidad.nombre);
                bw.Write(entidad.dirEntidad);
                bw.Write(entidad.dirAtributo);
                bw.Write(entidad.dirDatos);
                bw.Write(entidad.dirSigEntidad);

                foreach(Atributo atributo in entidad.Atributos)
                {
                    bw.Write(atributo.id);
                    bw.Write(atributo.nombre);
                    bw.Write(atributo.tipodato);
                    bw.Write(atributo.longitud);
                    bw.Write(atributo.dirAtributo);
                    bw.Write(atributo.tipoIndice);
                    bw.Write(atributo.dirIndice);
                    bw.Write(atributo.dirSigA);
                }
            }

            archivo.Close();
        }
        public void leerArchivo(FileStream archivo)
        {
            string nombreArchivo = archivo.Name;
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);

            long cab = br.ReadInt64();
            archivo.Close();
        }
        #endregion
               
        #region Entidades 
        private void AgregarEntidad_Click(object sender, EventArgs e)
        {
            try
            {
                char[] nombre = new char[35];
                nombre = nuevaentidad.Text.ToCharArray();
                long direccion = Archivo.EncuentraDireccion(nombreArchivo,archivo);

                if (ExisteEntidad(nombre) == false && nuevaentidad.Text != "")
                {
                    Entidad nueva = new Entidad(AsignarID(), nombre, direccion, -1, -1, -1);
                    Entidades.Add(nueva);

                    Archivo.EscribirEntidad(nombreArchivo, archivo, nueva);
                    Entidades = OrdenaNombre(Entidades);
                    Archivo.ActualizaCabecera(archivo, nombreArchivo, Entidades[0].dirEntidad);
                    ActualizaEntidades();
                    ActualizaDataEntidades();
                }
                else
                {
                    MessageBox.Show("Entidad Ya Existente o Nula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                nuevaentidad.Text = "";
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("Cree un Archivo para comenzar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nuevaentidad.Text = "";
                EntidadesList.Items.Clear();
            }
        }
        private void ActualizaEntidades()
        {
            for (int i = 0; i < Entidades.Count - 1; i++)
            {
                Entidades[i].dirSigEntidad = Entidades[i + 1].dirEntidad;
                Archivo.ModificaEntidad(Entidades[i], nombreArchivo);
            }
            int last = Entidades.Count - 1;
            Entidades[last].dirSigEntidad = -1;
            Archivo.ModificaEntidad(Entidades[last], nombreArchivo);
        }
        public bool ExisteEntidad(char[] name)
        {
            bool existe = false;
            if (Entidades.Count != 0)
            {
                foreach (Entidad enti in Entidades)
                {
                    if (ConvertirNombre(enti.nombre) == ConvertirNombre(name))
                    {
                        existe = true;
                    }
                }
            }
            return existe;

        }
        public string ConvertirNombre(char[] name)
        {
            string regresada = "";

            for(int i = 0; i<name.Length;i++)
            {
                if (name[i] != '\0')
                { regresada += name[i]; }
            }
            return regresada;
        }
        private void ModificarEntidad_Click(object sender, EventArgs e)
        {
            Modifica mod = new Modifica(Entidades);
            mod.ShowDialog();

            for (int i = 0; i < Entidades.Count; i++)
            {
                if(ConvertirNombre(Entidades[i].nombre) == ConvertirNombre(mod.nombreEntidadA))
                {
                    Entidades[i].nombre = mod.nombreEntidadN;
                    Archivo.ModificaEntidad(Entidades[i], nombreArchivo);
                }
            }
            Entidades = OrdenaNombre(Entidades);
            Archivo.DireccionamientoEntidades(nombreArchivo, Entidades);
            Archivo.ActualizaCabecera(archivo,nombreArchivo,Entidades[0].dirEntidad);
            ActualizaDataEntidades();
        }
        private byte[] AsignarID()
        {
            Random random = new Random();

            byte[] buffer = new byte[10 / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            return buffer;
        }
        private List<Entidad> OrdenaNombre(List<Entidad> entidads)
        {
            List<Entidad> aux = new List<Entidad>();
            aux = entidads;
            aux.Sort(new Compara_Letra());
            return aux;
        }
        class Compara_Letra : IComparer<Entidad>
        {
            public int Compare(Entidad x, Entidad y)
            {
                return x.NombreEntidad().CompareTo(y.NombreEntidad());
            }
        }
        public string ConvertirID(byte[] id)
        {
            string aux = String.Concat(id.Select(x => x.ToString("X2")).ToArray());

            return aux;
        }
        private void ActualizaDataEntidades()
        {
            GridEntidad.Rows.Clear();
            EntidadesList.Items.Clear();

            foreach (Entidad entidad in Entidades)
            {
                GridEntidad.Rows.Add(
                    /*ConvertirID(entidad.id),*/
                    ConvertirNombre(entidad.nombre)
                    /*entidad.dirEntidad,
                    entidad.dirAtributo,
                    entidad.dirDatos
                    entidad.dirSigEntidad*/
                );
                EntidadesList.Items.Add(ConvertirNombre(entidad.nombre));
            }
        }
        private void EliminarEntidad_Click(object sender, EventArgs e)
        {
            EliminaEntidad eliminaEntidad = new EliminaEntidad(Entidades);
            eliminaEntidad.ShowDialog();


            if (Entidades.Count > 0 )
            {
                EliminaEntidad(eliminaEntidad.EntidadSeleccionada);
            }
        }
        private void EliminaEntidad(string entidad)
        {
            bool borrado = false;
            int entidadseleccionada = 0;

            //Buscamos el indice de la entidad a borrar.
            for (int i = 0; i < Entidades.Count; i++)
            {
                if (Entidades[i].NombreEntidad() == NombreEntidad(entidad))
                {
                    entidadseleccionada = i;
                    break;
                }
            }

            if (entidadseleccionada != Entidades.Count - 1)//para ver si no es la ultima entidad de la lista
            {
                /*se recorre toda la lista y si en la entidad pasada a la entidadseleccionada esta su dir en su campo
                 * de DirSig significa que la que sigue es la entidad que se borrara y se hace el cambio de direcciones            
                 * en sus campos dirSig.
                 */
                for (int i = 0; i < Entidades.Count - 1; i++)
                {
                    if (Entidades[i].dirSigEntidad == Entidades[entidadseleccionada].dirEntidad)
                    {                                                                           
                        Entidades[i].dirSigEntidad = Entidades[entidadseleccionada].dirSigEntidad;
                        Archivo.ModificaEntidad(Entidades[i], nombreArchivo);
                        Entidades.Remove(Entidades[entidadseleccionada]);
                        borrado = true;
                    }
                }
            }
            else//para ver si es el ultimo elemento en la lista
            {
                if (Entidades[entidadseleccionada].dirEntidad == Entidades[Entidades.Count-1].dirEntidad)//serciorarnos que es la ultima entidad.
                {
                    if (Entidades.Count != 1)
                    {
                        Entidades[entidadseleccionada - 1].dirSigEntidad = -1;
                        Archivo.ModificaEntidad(Entidades[entidadseleccionada - 1], nombreArchivo);
                        Entidades.Remove(Entidades[entidadseleccionada]);
                        borrado = true;
                    }
                }
            }
            //Condicional para ver si solo es un elemento en la lista.
            if (Entidades.Count == 1 && borrado == false)
            {
                Cabeza = -1;
                Archivo.ActualizaCabecera(archivo, nombreArchivo, Cabeza);
                Entidades.RemoveAt(0);
            }

            if (borrado == false && Entidades.Count > 1 && Entidades[entidadseleccionada].dirEntidad == Entidades[0].dirEntidad)
            {
                Cabeza = Entidades[0].dirSigEntidad;
                Archivo.ActualizaCabecera(archivo, nombreArchivo, Cabeza);
                Entidades.RemoveAt(0);
            }
            ActualizaDataEntidades();
            ActualizaDataAtributos(entidad);
        }
        #endregion

        #region Atributos
        private void AgregarAtributo_Click(object sender, EventArgs e)
        {
            FormAtributo nuevoatri = new FormAtributo();
            nuevoatri.ShowDialog();
            if (EntidadSeleccionada != "")
            {
                entidadSelect = EntidadSeleccionada;
            }
            else {
                try
                {
                    EntidadSeleccionada = EntidadesList.SelectedItem.ToString();
                    for (int i = 0; i < EntidadSeleccionada.Length; i++)
                    {
                        if(EntidadSeleccionada[i] != '\0')
                        {
                            entidadSelect += EntidadSeleccionada[i];
                        }
                    }
                }
                catch (System.NullReferenceException) { }
                EntidadSeleccionada = entidadSelect;
            }
            if(nuevoatri.lleno == true)
            {
                foreach (Entidad entidad in Entidades)
            {
                    if (entidad.NombreEntidad() == this.ConvertirNombre(entidadSelect.ToCharArray()))//buscamos la entidad que se selecciono para agregar el atributo.
                {
                    if (entidad.ExisteCveBusqueda() == false)//checamos que solo haya una solo clave de busqueda.
                    {
                        long dir = Archivo.EncuentraDireccion(nombreArchivo, archivo);
                        Atributo nuevo = new Atributo(AsignarID(), nuevoatri.nombreA, dir, nuevoatri.tipoDato, nuevoatri.longitud,
                            nuevoatri.tipoindice, -1, -1);
                        if (entidad.dirAtributo == -1)
                        {
                            entidad.dirAtributo = nuevo.dirAtributo;
                            Archivo.ModificaEntidad(entidad, nombreArchivo);
                            ActualizaDataEntidades();
                        }
                        else
                        {
                            int last = entidad.Atributos.Count;
                            entidad.Atributos[last - 1].dirSigA = nuevo.dirAtributo;
                            Archivo.ModificaAtributo(entidad.Atributos[last - 1], nombreArchivo,archivo);
                        }
                        entidad.Atributos.Add(nuevo);
                        ActualizaDataAtributos(entidadSelect);
                        Archivo.EscribirAtributo(nombreArchivo, archivo, entidad, nuevo);
                    }else
                    {
                        if(entidad.ExisteCveBusqueda() ==true && nuevoatri.tipoindice != 1 )
                        {
                            long dir = Archivo.EncuentraDireccion(nombreArchivo, archivo);
                            Atributo nuevo = new Atributo(AsignarID(), nuevoatri.nombreA, dir, nuevoatri.tipoDato, nuevoatri.longitud,
                                nuevoatri.tipoindice, -1, -1);
                            if (entidad.dirAtributo == -1)
                            {
                                entidad.dirAtributo = nuevo.dirAtributo;
                                Archivo.ModificaEntidad(entidad, nombreArchivo);
                                ActualizaDataEntidades();
                            }
                            else
                            {
                                int last = entidad.Atributos.Count;
                                entidad.Atributos[last - 1].dirSigA = nuevo.dirAtributo;
                                Archivo.ModificaAtributo(entidad.Atributos[last - 1], nombreArchivo, archivo);
                            }
                            entidad.Atributos.Add(nuevo);
                            ActualizaDataAtributos(entidadSelect);
                            Archivo.EscribirAtributo(nombreArchivo, archivo, entidad, nuevo);
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un Atributo con Clave de Busqueda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        
                    }
                }
            }
            }
            else
            {
                MessageBox.Show("Informacion incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ActualizaDataAtributos(string ent)
        {
            GridAtributo.Rows.Clear();
            
            foreach (Entidad e in Entidades)
            {
                if (ent == e.NombreEntidad())
                {
                    if (e.dirAtributo != -1)
                    {
                        foreach (Atributo a in e.Atributos)
                        {
                            GridAtributo.Rows.Add(ConvertirID(a.id), ConvertirNombre(a.nombre), a.tipodato, a.longitud, a.dirAtributo, a.tipoIndice, a.dirIndice, a.dirSigA);
                        }
                    }
                }
            }
        }
        private void ModificarAtributo_Click(object sender, EventArgs e)
        {
            ModificaAtributo modificaAtributo = new ModificaAtributo(Entidades);
            modificaAtributo.ShowDialog();

            for(int i=0;i<Entidades.Count;i++)
            {
                if(ConvertirNombre(Entidades[i].nombre) == ConvertirNombre(modificaAtributo.EntidadSeleccionada.ToCharArray()))
                { 
                    for(int j=0;j<Entidades[i].Atributos.Count;j++)
                    {
                        if(ConvertirNombre(Entidades[i].Atributos[j].nombre) == ConvertirNombre(modificaAtributo.AtributoSeleccionado.ToCharArray()))
                        {
                            Entidades[i].Atributos[j].nombre = modificaAtributo.nombreA;
                            Entidades[i].Atributos[j].tipodato = modificaAtributo.tipoDato;
                            Entidades[i].Atributos[j].longitud = modificaAtributo.longitud;
                            Entidades[i].Atributos[j].tipoIndice = modificaAtributo.tipoindice;
                            Archivo.ModificaAtributo(Entidades[i].Atributos[j], nombreArchivo, archivo);
                            break;
                        }
                    }
                }
            }
            string cad = "";
            for (int i = 0; i < modificaAtributo.EntidadSeleccionada.Length; i++)
            {
                if (modificaAtributo.EntidadSeleccionada[i] != '\0')
                { cad += modificaAtributo.EntidadSeleccionada[i]; }
            }
            ActualizaDataAtributos(cad);
        }
        private void EliminarAtributo_Click(object sender, EventArgs e)
        {
            EliminaAtributo eliminaAtributo = new EliminaAtributo(Entidades);
            eliminaAtributo.ShowDialog();

            foreach(Entidad entidad in Entidades)
            {
                if (ConvertirNombre(entidad.nombre) == eliminaAtributo.EntidadSeleccionada)
                { 
                    for(int i=0;i<entidad.Atributos.Count;i++)
                    {
                        if(ConvertirNombre(entidad.Atributos[i].nombre) == ConvertirNombre(eliminaAtributo.AtributoSeleccionado.ToCharArray()))
                        {
                            EliminaAtributo(entidad, entidad.Atributos[i]);
                        }
                    }
                }

            }

        }
        private void EliminaAtributo(Entidad entidad,Atributo atributo)
        {
            bool borrado = false;
            long dir = atributo.dirAtributo;
            for (int i = 0; i < entidad.Atributos.Count - 1; i++)
            {
                if (dir == entidad.Atributos[i].dirSigA)
                {
                    entidad.Atributos[i].dirSigA = entidad.Atributos[i + 1].dirSigA;
                    Archivo.ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivo);
                    entidad.Atributos.Remove(entidad.Atributos[i + 1]);
                    dir = -1;
                    borrado = true;
                }
            }
            if (entidad.Atributos.Count == 1 && !borrado)
            {
                entidad.dirAtributo = -1;
                entidad.Atributos.RemoveAt(0);
                Archivo.ModificaEntidad(entidad, nombreArchivo);
                ActualizaDataEntidades();
            }
            if (!borrado && entidad.Atributos.Count > 1 && dir == entidad.Atributos[0].dirAtributo)
            {
                entidad.dirAtributo = atributo.dirSigA;
                Archivo.ModificaEntidad(entidad, nombreArchivo);
                entidad.Atributos.RemoveAt(0);
                ActualizaDataEntidades();
            }
            ActualizaDataAtributos(entidad.NombreEntidad());
        }
        private void EntidadesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EntidadSeleccionada = EntidadesList.SelectedItem.ToString();
                GridAtributo.Rows.Clear();
                string cad = "";
                for (int i = 0; i < EntidadSeleccionada.Length; i++)
                {
                    if (EntidadSeleccionada[i] != '\0')
                    { cad += EntidadSeleccionada[i]; }
                }
                ActualizaDataAtributos(cad);
            }
            catch (System.NullReferenceException) { }
        }
        #endregion

        #region Registros 
        private void AgRegi_Click(object sender, EventArgs e)
        {
            CapturaRegistro capturaRegistro = new CapturaRegistro(Entidades,directorio,archivo);
            capturaRegistro.nombreArchivo = nombreArchivo;
            capturaRegistro.ShowDialog();
            ActualizaDataEntidades();
            if (EntidadSeleccionada != "")
            {
                ActualizaDataAtributos(EntidadSeleccionada); 
            }
        }

        #endregion

        private void nuevaentidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                AgregarEntidad_Click(this, null); 
            }
        }

        public string NombreEntidad(string nombre)
        {
            string regresada = "";

            for (int i = 0; i < nombre.Length; i++)
            {
                if (nombre[i] != '\0')
                { regresada += nombre[i]; }
            }
            return regresada;
        }

    }
}