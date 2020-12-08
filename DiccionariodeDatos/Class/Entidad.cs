using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariodeDatos
{
    [Serializable]
    class Entidad
    {
        byte[] ID = new byte[5];
        public byte[] id { get { return ID; } set { ID = value; } }
        char[] Nombre = new char[35];
        public char[] nombre { get { return Nombre; } set { Nombre = value; } }
        long DirEntidad;
        public long dirEntidad { get { return DirEntidad; } set { DirEntidad = value; } }
        long DirAtributo;
        public long dirAtributo { get { return DirAtributo; } set { DirAtributo = value; } }
        long DirDatos;
        public long dirDatos { get { return DirDatos; } set { DirDatos = value; } }
        long DirSigEntidad;
        public long dirSigEntidad { get { return DirSigEntidad; } set { DirSigEntidad = value; } }
        private List<Registro> archivoDatos;
        public List<Registro> Datos { get{ return archivoDatos; } set{ archivoDatos = value; }}
        // Contiene la ruta del archivo de datos.
        private string RutaAtributos;
        public string rutaArchDatos { get { return RutaAtributos; } set { RutaAtributos = value; } }
        //Archivo de Datos
        private FileStream archivo;
        public FileStream ArchivoDato {  get{ return archivo;}  set{ archivo = value;} }
        public List<Atributo> Atributos;
        public int numAtributo=00001;
        public int indexCveBusqueda;
        public List<int> indicesPrimarios;
        public List<int> indicesSecundarios;
        public List<int> ArbolPrimario;
        public List<int> ArbolSecu;
        public Entidad()
        {
            Atributos = new List<Atributo>();
            Datos = new List<Registro>();
        }
        public Entidad(byte[] id,char[] name, long dEnt, long dAtr, long dDat, long dSig)
        {
            this.id = id;
            nombre = name;
            dirEntidad = dEnt;
            dirAtributo = dAtr;
            dirDatos = dDat;
            dirSigEntidad = dSig;
            Atributos = new List<Atributo>();
            archivoDatos = new List<Registro>();
        }
        public Entidad(char[] name)
        {
            nombre = name;
            Atributos = new List<Atributo>();
        }
        public string NombreEntidad()
        {
            string regresada = "";

            for (int i = 0; i < nombre.Length; i++)
            {
                if (nombre[i] != '\0')
                { regresada += nombre[i]; }
            }
            return regresada;
        }
        public string GetID()
        {
            //string regresada = "";
            string regresada = String.Concat(id.Select(x => x.ToString("X2")).ToArray());
            return regresada;
        }
        public int ObtenerRegistro(long direccion)
        {
            int index = 0;
            for(int i = 0; i< Datos.Count; i++)
            {
                if(Datos[i].dirReG == direccion)
                {
                    index = i;
                }
            }
            return index;
        }
        public bool ExisteCveBusqueda()
        {
            bool existe = false;
            for(int i =0;i<this.Atributos.Count;i++)
            {
                if(Atributos[i].tipoIndice == 1)
                {
                    indexCveBusqueda = i;
                    existe = true;
                }
            }
            return existe;
        }
        public bool ExistenIndicesyClave()
        {
            indicesPrimarios = new List<int>();
            indicesSecundarios = new List<int>();
            ArbolPrimario = new List<int>();
            ArbolSecu = new List<int>();

            bool existe = false;
            for (int i = 0; i < Atributos.Count; i++)
            {
                if (ExisteCveBusqueda() == true)
                {
                    switch(Atributos[i].tipoIndice)
                    {
                        case 2:
                            indicesPrimarios.Add(i);
                        existe = true;
                            break;
                        case 3:
                            indicesSecundarios.Add(i);
                            existe = true;
                            break;
                        case 4:
                            ArbolPrimario.Add(i);
                            existe = true;
                            break;
                        case 5:
                            ArbolSecu.Add(i);
                            existe = true;
                            break;
                    }
                }
            }
            return existe;
        }
        public bool ExistenIndicesSinCVE()
        {
            indicesPrimarios = new List<int>();
            indicesSecundarios = new List<int>();
            ArbolPrimario = new List<int>();
            ArbolSecu = new List<int>();

            bool existe = false;
            for (int i = 0; i < Atributos.Count; i++)
            {
                switch (Atributos[i].tipoIndice)
                {
                    case 2:
                        if (ExisteCveBusqueda() == false)
                        {
                            indicesPrimarios.Add(i);
                            existe = true;
                        }
                        break;
                    case 3:
                        if (ExisteCveBusqueda() == false)
                        {
                            indicesSecundarios.Add(i);
                            existe = true;
                        }
                        break;
                    case 4:
                        if (ExisteCveBusqueda() == false)
                        {
                            ArbolPrimario.Add(i);
                            existe = true;
                        }
                        break;
                    case 5:
                        if (ExisteCveBusqueda() == false)
                        {
                            ArbolSecu.Add(i);
                            existe = true;
                        }
                        break;
                }
            }
            return existe;
        }
        public void ordenarIndices()
        {
            indicesPrimarios = new List<int>();
            indicesSecundarios = new List<int>();
            ArbolPrimario = new List<int>();
            ArbolSecu = new List<int>();
            for (int i = 0; i < Atributos.Count; i++)
            {
                switch (Atributos[i].tipoIndice)
                {
                    case 2:
                        indicesPrimarios.Add(i);
                        break;
                    case 3:
                        indicesSecundarios.Add(i);
                        break;
                    case 4:
                        ArbolPrimario.Add(i);
                        break;
                    case 5:
                        ArbolSecu.Add(i);
                     break;
                }
            }
        }
        public void EliminarReg(int indice)
        {

        }

        public bool ExistePK()
        {
            bool existe = false;
            foreach (Atributo atributo in Atributos)
            {
                if(atributo.tipodato == 1)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    }
}
