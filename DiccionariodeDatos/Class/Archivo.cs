using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiccionariodeDatos
{
    class Archivo
    {
        public Archivo()
        {

        }

        #region Archivo
        public static void CreaArchivo(string nombreArchivo, FileStream archivo)
        {
            archivo = File.Create(nombreArchivo);
            archivo.Close();
        }
        public static List<Entidad> Leer(long cabecera, string Nombrearchivo, string direccionGen)
        {
            //long cabecera = LeeCabecera(Nombrearchivo);

            List<Entidad> eaux = new List<Entidad>();
            Entidad nueva;
            FileStream archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.Read, FileShare.None);
            //BinaryReader reader = new BinaryReader(archivo);
            long direccion = cabecera;

            while (direccion != -1)
            {
                archivo.Seek(direccion, SeekOrigin.Begin);
                BinaryReader reader = new BinaryReader(archivo);
                nueva = new Entidad();
                nueva.id = reader.ReadBytes(5);
                char[] nom = new char[35];
                /*for (int i = 0; i < 35; i++)
                {
                    byte n = reader.ReadByte();
                    char c = Convert.ToChar(n);
                    nom[i] = c;
                }*/
                nom = reader.ReadChars(35);
                nueva.nombre = nom;
                nueva.dirEntidad = reader.ReadInt64();
                nueva.dirAtributo = reader.ReadInt64();
                nueva.dirDatos = reader.ReadInt64();
                nueva.dirSigEntidad = reader.ReadInt64();
                eaux.Add(nueva);
                //Checar los atributos
                if (nueva.dirAtributo != -1)
                {
                    LeeAtributos(nueva, archivo);
                }
                if (nueva.dirDatos != -1)//Modificar Entidad con la direccion del Archivo de datos;
                {
                    string nombreArch = direccionGen + "\\" + nueva.GetID() + ".dat";
                    LeerRegistros(nombreArch, nueva.ArchivoDato, nueva);
                }
                direccion = nueva.dirSigEntidad;
            }
            archivo.Close();
            return eaux;
        }
        public static int TamañoArchivo(string Nombrearchivo)
        {
            int tamaño = 0;
            FileStream archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.Write);
            tamaño = (int)archivo.Length;
            archivo.Close();
            return tamaño;
        }
        public static int TamañoArchivoS(string Nombrearchivo, FileStream archivo)
        {
            //archivo.Close();
            archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            int tamaño = 0;
            /*archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.Write);*/
            tamaño = (int)archivo.Length;
            archivo.Close();
            return tamaño;
        }
        public static string ConvertirNombre(char[] name)
        {
            string regresada = "";

            for (int i = 0; i < name.Length; i++)
            {
                regresada += name[i];
            }
            return regresada;
        }
        #endregion

        #region Entidad
        public static void GuardaCabecera(FileStream archivo, long cabecera)
        {
            //FileStream archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.Write, FileShare.None);
            BinaryWriter writer = new BinaryWriter(archivo);
            writer.Write(cabecera);
            archivo.Close();
        }
        public static void ActualizaCabecera(FileStream archivo, string Nombrearchivo, long direccion)
        {
            archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryWriter writer = new BinaryWriter(archivo);
            writer.Write(direccion);
            archivo.Close();
        }
        public static long LeeCabecera(string Nombrearchivo)
        {
            FileStream archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(archivo);
            long dir = -1;
            archivo.Seek(0, SeekOrigin.Begin);
            dir = reader.ReadInt64();
            archivo.Close();
            return dir;
        }
        public static void EscribirEntidad(string Nombrearchivo, FileStream archivo, Entidad entidad)
        {
            //FileStream archivo = new FileStream(Nombrearchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            long dir = Archivo.EncuentraDireccion(Nombrearchivo, archivo);
            archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.Write, FileShare.None);
            archivo.Seek(dir, SeekOrigin.Begin);
            BinaryWriter bw = new BinaryWriter(archivo);
            //entidad.dirEntidad = TamañoArchivoS(archivo);
            /*byte[] bytes = new byte[5];
            for (int i = 0; i < 5; i++)
            { bytes[i] = Convert.ToByte(entidad.id[i]); }
            bw.Write(bytes);*/
            bw.Write(entidad.id);
            char[] chars = new char[35];
            for (int i = 0; i < 35; i++)
            {
                try
                {
                    if (entidad.nombre[i] != '\0')
                    {
                        chars[i] = entidad.nombre[i];
                    }
                }
                catch (System.IndexOutOfRangeException) { chars[i] = '\0'; }
            }
            bw.Write(chars);
            bw.Write(entidad.dirEntidad);
            bw.Write(entidad.dirAtributo);
            bw.Write(entidad.dirAtributo);
            bw.Write(entidad.dirSigEntidad);

            archivo.Close();
        }
        public static long EncuentraDireccion(string nombreArchivo, FileStream archivo)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            long dir = -1;
            //archivo = File.OpenRead(nombreArchivo);
            dir = archivo.Length;
            archivo.Close();
            return dir;
        }
        public static void DireccionamientoEntidades(string nombreArchivo, List<Entidad> entidades)
        {
            for (int i = 0; i < entidades.Count; i++)
            {
                try
                { entidades[i].dirSigEntidad = entidades[i + 1].dirEntidad; }
                catch (ArgumentOutOfRangeException)
                {
                    entidades[i].dirSigEntidad = -1;
                }
                ModificaEntidad(entidades[i], nombreArchivo);
            }
        }
        public static void ModificaEntidad(Entidad entidad, string path)
        {
            FileStream archivo = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(entidad.dirEntidad, SeekOrigin.Begin);
            BinaryWriter writer = new BinaryWriter(archivo);
            
            writer.Write(entidad.id);
            char[] chars = new char[35];
            for (int i = 0; i < 35; i++)
            {
                try
                {
                    if (entidad.nombre[i] != '\0')
                    {
                        chars[i] = entidad.nombre[i];
                    }
                }
                catch (System.IndexOutOfRangeException) { chars[i] ='\0'; }
            }
            writer.Write(chars);
            writer.Write(entidad.dirEntidad);
            writer.Write(entidad.dirAtributo);
            writer.Write(entidad.dirDatos);
            writer.Write(entidad.dirSigEntidad);
            archivo.Close();
        }
        #endregion

        #region Atributo
        public static void EscribirAtributo(string Nombrearchivo, FileStream archivo, Entidad entidad, Atributo atributo)
        {
            long dir = TamañoArchivoS(Nombrearchivo, archivo);
            archivo = File.Open(Nombrearchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(dir, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);
            //byte[] bytes = new byte[5];
            /*for (int i = 0; i < 5; i++)
            {
                try
                {
                    if (entidad.nombre[i] != '\0')
                    {
                        bytes[i] = Convert.ToByte(atributo.id[i]);
                    }
                }
                catch (System.IndexOutOfRangeException) { bytes[i] = Convert.ToByte('\0'); }
            }*/
            bw.Write(atributo.id);
            char[] chars = new char[35];
            for (int i = 0; i < 35; i++)
            {
                try
                {
                    if (atributo.nombre[i] != '\0')
                    {
                        chars[i] = atributo.nombre[i];
                    }
                }
                catch (System.IndexOutOfRangeException) { chars[i] = '\0'; }
            }
            bw.Write(chars);//valor=35
            bw.Write(atributo.tipodato);//valor=1
            bw.Write(atributo.longitud);//valor=4
            bw.Write(atributo.dirAtributo);//valor=8
            bw.Write(atributo.tipoIndice);//valor=4
            bw.Write(atributo.dirIndice);//valor=8
            bw.Write(atributo.dirSigA);//valor=8
            archivo.Close();
        }
        public static void LeeAtributos(Entidad e, FileStream archivo)
        {
            e.Atributos = new List<Atributo>();
            long dir = e.dirAtributo;
            // FileStream archivo = new FileStream(Narchivo, FileMode.Open, FileAccess.ReadWrite);
            while (dir != -1)
            {
                archivo.Seek(dir, SeekOrigin.Begin);
                BinaryReader reader = new BinaryReader(archivo);
                byte[] id = reader.ReadBytes(5);
                char[] nombre = reader.ReadChars(35);
                char tipoDato = reader.ReadChar();
                int longitud = reader.ReadInt32();
                long dirAtr = reader.ReadInt64();
                int tipoInd = reader.ReadInt32();
                long dirInd = reader.ReadInt64();
                long dirSigA = reader.ReadInt64();
                Atributo a = new Atributo(id, nombre, dirAtr, tipoDato, longitud, tipoInd, dirInd, dirSigA);
                e.Atributos.Add(a);
                dir = dirSigA;
            }
        }
        /// <summary>
        /// Funcion para modificar el atributo, en donde el atributo puede que tenga algunas modificaciones.
        /// Esta funcion hace la modificacion desde el archivo de Datos y en el diccionario.
        /// </summary>
        /// <param name="atributo">Atributo a modificar.</param>
        /// <param name="path">Nombre del archivo.</param>
        /// <param name="archivo">Objeto del archivo.</param>
        public static void ModificaAtributo(Atributo atributo, string path, FileStream archivo)
        {
            archivo = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(atributo.dirAtributo, SeekOrigin.Begin);
            BinaryWriter writer = new BinaryWriter(archivo);

            writer.Write(atributo.id);
            char[] chars = new char[35];
            for (int i = 0; i < 35; i++)
            {
                try
                {
                    if (atributo.nombre[i] != '\0')
                    {
                        chars[i] = atributo.nombre[i];
                    }
                }
                catch (System.IndexOutOfRangeException) { chars[i] = '\0'; }
            }
            writer.Write(chars);//valor=35
            

            writer.Write(atributo.tipodato);//valor=1
            writer.Write(atributo.longitud);//valor=4
            writer.Write(atributo.dirAtributo);//valor=8
            writer.Write(atributo.tipoIndice);//valor=4
            writer.Write(atributo.dirIndice);//valor=8
            writer.Write(atributo.dirSigA);//valor=8
            archivo.Close();
        }
        #endregion

        #region Registro
        public static void CreaArchivoDatos(string nombreArchivo, FileStream archivo)
        {
            archivo = File.Create(nombreArchivo);
            archivo.Close();
        }
        public static void GuardaRegistro(Entidad ent, Registro reg)
        {
            ent.ArchivoDato = File.Open(ent.rutaArchDatos, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            ent.ArchivoDato.Seek(ent.ArchivoDato.Length, SeekOrigin.Begin);

            BinaryWriter writer = new BinaryWriter(ent.ArchivoDato);

            writer.Write(reg.dirReG);

            for (int i = 0; i < ent.Atributos.Count; i++)
            {
                if (ent.Atributos[i].tipodato == 'E')
                {
                    try
                    {
                        if (ent.Atributos[i].longitud == 4)
                        {
                            int dato = Convert.ToInt32((object)reg.registros[i]);
                            writer.Write(dato);
                        }
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Informacion Invalida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    if (ent.Atributos[i].tipodato == 'C')
                    {
                        byte[] bytes = new byte[ent.Atributos[i].longitud];
                        string cadena = Convert.ToString(reg.registros[i]);
                        for (int j = 0; j < ent.Atributos[i].longitud; j++)
                        {
                            try
                            {
                                if (cadena[j] != '\0')
                                {
                                    bytes[j] = Convert.ToByte(cadena[j]);
                                }
                            }
                            catch (System.IndexOutOfRangeException) {
                                bytes[j] = Convert.ToByte('\0');
                            }
                        }

                        writer.Write(bytes);
                    }
                }
            }
            writer.Write(reg.dirSigR);
            ent.ArchivoDato.Close();
        }
        public static void LeerRegistros(string nombreArchivo, FileStream archivo, Entidad entidad)
        {
            entidad.Datos = new List<Registro>();
            long dir = entidad.dirDatos;
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryReader reader = new BinaryReader(archivo);
            while (dir != -1)
            {
                archivo.Seek(dir, SeekOrigin.Begin);
                Registro Reg = new Registro();
                long dirREG = reader.ReadInt64();
                Reg.dirReG = dirREG;
                for (int i = 0; i < entidad.Atributos.Count; i++)
                {
                    if (entidad.Atributos[i].tipodato == 'E')
                    {
                        if (entidad.Atributos[i].longitud == 4)
                        {
                            //int dato = Int32.Parse(reg.registros[i].ToString());
                            int enteroCorto = reader.ReadInt32();
                            Reg.registros.Add(enteroCorto);
                        }
                    }
                    else
                    {
                        if (entidad.Atributos[i].tipodato == 'C')
                        {
                            char[] cad = reader.ReadChars(entidad.Atributos[i].longitud);
                            Reg.registros.Add(ConvertirNombre(cad));
                        }
                    }
                }
                long dirSigREG = reader.ReadInt64();

                Reg.dirSigR = dirSigREG;
                dir = Reg.dirSigR;
                entidad.Datos.Add(Reg);

            }
            archivo.Close();
        }
        public static long EncuentraDireccionRegistros(string path, FileStream archivo)
        {
            long dir = -1;
            archivo = File.Open(path, FileMode.Open);
            dir = archivo.Length;
            archivo.Close();
            return dir;
        }
        public static void ModificaRegristro(FileStream archivo, string nombreArchivo, Entidad entidad, Registro reg)
        {
            long dirDatos = reg.dirReG;
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(dirDatos, SeekOrigin.Begin);

            BinaryWriter writer = new BinaryWriter(archivo);
            writer.Write(reg.dirReG);

            for (int i = 0; i < entidad.Atributos.Count; i++)
            {
                if (entidad.Atributos[i].tipodato == 'E')
                {
                    if (entidad.Atributos[i].longitud == 4)
                    {
                        int dato = Int32.Parse(reg.registros[i].ToString());
                        writer.Write(dato);
                    }
                }
                else
                {
                    if (entidad.Atributos[i].tipodato == 'C')
                    {
                        byte[] bytes = new byte[entidad.Atributos[i].longitud];
                        char[] cadena = reg.registros[i].ToString().ToCharArray();
                        for (int j = 0; j < entidad.Atributos[i].longitud; j++)
                        {
                            try
                            {
                                if (cadena[j] != '\0')
                                {
                                    bytes[j] = Convert.ToByte(cadena[j]);
                                }
                            }
                            catch (System.IndexOutOfRangeException) { bytes[j] = Convert.ToByte('\0'); }
                        }

                        writer.Write(bytes);
                    }
                }
            }
            writer.Write(reg.dirSigR);

            archivo.Close();
        }
        public static void DireccionamientoRegistros(Entidad entidad, List<Registro> registros, string nombreArchivo, string directorio, FileStream archivoDD)
        {
            for (int i = 0; i < registros.Count; i++)
            { foreach (Registro r in entidad.Datos)
                {
                    if (r.dirReG == registros[i].dirReG)
                    {
                        try
                        {
                            r.dirSigR = registros[i + 1].dirReG;
                            i++;
                            ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, r);
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            r.dirSigR = -1;
                            ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, r);
                        }
                    }
                }
            }
            try { 
            if (entidad.dirDatos != entidad.Datos[0].dirReG)
            { entidad.dirDatos = entidad.Datos[0].dirReG;
                ModificaEntidad(entidad, nombreArchivo); }
            }
            catch (System.ArgumentOutOfRangeException) {
                entidad.dirDatos = -1;
                ModificaEntidad(entidad, nombreArchivo);
            }
        }
        public static void DireccionamientoRegistrosCveBusqueda(Entidad entidad, int CveBusqueda, string nombreArchivo, string directorio, FileStream archivoDD)
        {
            List<Ordenadas> ordenadaE = new List<Ordenadas>();
            List<Ordenadas> ordenadaC = new List<Ordenadas>();
            List<Registro> registrosOrdenados = new List<Registro>();
            List<Ordenadas> DatosOrdenados = new List<Ordenadas>();


            //Agregamos a un lista Auxiliar y ordenamos
            foreach (Registro r in entidad.Datos)
            {
                if (entidad.Atributos[CveBusqueda].tipodato == 'E')
                {
                    Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[CveBusqueda]), r.dirReG);
                    DatosOrdenados.Add(nuevo);
                    DatosOrdenados = DatosOrdenados.OrderBy(o => o.entero).ToList();
                }
                else
                {
                    if (entidad.Atributos[CveBusqueda].tipodato == 'C')
                    {
                        Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[CveBusqueda]), r.dirReG);
                        DatosOrdenados.Add(nuevo);
                        DatosOrdenados = DatosOrdenados.OrderBy(o => o.cadena).ToList();
                    }
                }
            }

            for (int i = 0; i < DatosOrdenados.Count; i++)
            {
                try
                { DatosOrdenados[i].dirSig = DatosOrdenados[i + 1].dir; }
                catch (System.ArgumentOutOfRangeException) { DatosOrdenados[i].dirSig = -1; }
            }

            //Ordenamos logicamente.
            for (int i = 0; i < entidad.Datos.Count; i++)
            {
                Registro nuevo = new Registro();
                for (int j = 0; j < DatosOrdenados.Count; j++)
                {
                    if (entidad.Datos[i].dirReG == DatosOrdenados[j].dir)
                    {
                        nuevo.dirReG = entidad.Datos[i].dirReG;
                        nuevo.registros = new List<object>();
                        nuevo.registros = entidad.Datos[i].registros;
                        nuevo.dirSigR = DatosOrdenados[j].dirSig;
                        ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, nuevo);
                        registrosOrdenados.Add(nuevo);
                        break;
                    }
                }
            }
            entidad.Datos = registrosOrdenados;
            entidad.dirDatos = DatosOrdenados[0].dir;
            ModificaEntidad(entidad, nombreArchivo);
        }
        public static void DireccionamientoRegistrosIndice(Entidad entidad, List<Registro> registros, string nombreArchivo, string directorio, FileStream archivoDD, Registro nReg, int llamada,Registro regviejo)
        {
            List<Ordenadas> ordenadaE = new List<Ordenadas>();
            List<Ordenadas> ordenadaC = new List<Ordenadas>();


            for (int i = 0; i < registros.Count; i++)
            {
                foreach (Registro r in entidad.Datos)
                {
                    if (r.dirReG == registros[i].dirReG)
                    {
                        try
                        {
                            r.dirSigR = registros[i + 1].dirReG;
                            //i++;
                            ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, r);
                            break;
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            r.dirSigR = -1;
                            ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, r);
                            break;
                        }
                    }
                }
            }
            if (entidad.Datos.Count > 0)
            {
                entidad.dirDatos = entidad.Datos[0].dirReG;
                ModificaEntidad(entidad, nombreArchivo);

                for (int i = 0; i < entidad.Atributos.Count; i++)
                {
                    switch (entidad.Atributos[i].tipoIndice)
                    {
                        case 2://indice Primario.
                            switch(llamada)
                            {
                                case 1:

                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)//cambiar i por otra variable
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaE[0].dir*/ 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);

                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaE.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaE[j].entero, ordenadaE[j].dir, paso);
                                            }
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaC[0].dir*/0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaC.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaC[j].cadena, ordenadaC[j].dir, paso);
                                            }
                                        }
                                    }
                                    break;
                                case 2:


                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)//cambiar i por otra variable
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaE[0].dir*/ 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);

                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaE.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaE[j].entero, ordenadaE[j].dir, paso);
                                            }
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaC[0].dir*/0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaC.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, 
                                                    entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaC[j].cadena, ordenadaC[j].dir, paso);
                                            }
                                        }
                                    }
                                    break;
                                case 3:

                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaE[0].dir*/ 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            Archivo.BorrarEnArchivoPrimario(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice,
                                                entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice, 
                                                entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaC[0].dir*/0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            Archivo.BorrarEnArchivoPrimario(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice,
                                                entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                        }
                                    }
                                    break;
                            }                           
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 3://indice Secundario.
                               
                            switch (llamada)
                            {
                                case 1:
                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);
                                    //falta actualizar en el archivo.
                                    break;
                                case 2:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, regviejo);
                                    
                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);
                                    
                                    break;
                                case 3:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG,entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                            }
                            ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 4://Arbol primario
                            /*foreach (Registro r in entidad.Datos)
                            {*/
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    //entidad.Atributos[i].Borrar(entidad.Atributos[i].Arbol, Convert.ToInt32(nReg.registros[i]), entidad.Atributos[i]);
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            //}
                            break;
                        case 5://Arbol secundario
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    //                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                    }
                }
                //ModificaEntidad(entidad, nombreArchivo);
            }
            else
            {
                entidad.dirDatos = -1;
                ModificaEntidad(entidad, nombreArchivo);

                for (int i = 0; i < entidad.Atributos.Count; i++)
                {
                    switch (entidad.Atributos[i].tipoIndice)
                    {
                        case 2://indice Primario.
                            if(ordenadaC.Count ==0  || ordenadaE.Count==0)
                            {
                                Archivo.CreaArchivoIndicePrimario(entidad.Atributos[entidad.indicesPrimarios[i]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[i]].archivoIndice);
                                entidad.Atributos[entidad.indicesPrimarios[i]].dirIndice = -1;
                                ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 3://indice Secundario.
                            switch (llamada)
                            {
                                case 1:
                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);
                                    //falta actualizar en el archivo.
                                    break;
                                case 2:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, regviejo);

                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                                case 3:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                            }
                            ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 4://Arbol primario
                            /*foreach (Registro r in entidad.Datos)
                            {*/
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    //entidad.Atributos[i].Borrar(entidad.Atributos[i].Arbol, Convert.ToInt32(nReg.registros[i]), entidad.Atributos[i]);
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            //}
                            break;
                        case 5://Arbol secundario
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                    }
                }

            }
        }
        /// <summary>
        /// Funcion de Direccionamiento de Registros que contiene una clave de busqueda
        /// y ademas contiene indices que pueden ser primarios , secundarios o algun 
        /// tipo de arbol.
        /// </summary>
        /// <param name="entidad">Nombre y objeto en donde se encuentran los Registros</param>
        /// <param name="CveBusqueda">Atributo que es clave de busqueda para su ordenacion</param>
        /// <param name="nombreArchivo">Nombre del archivo se encuentra en la entidad</param>
        /// <param name="directorio">Directorio del archivo</param>
        /// <param name="archivoDD">Objeto del archivo de datos</param>
        /// <param name="nReg">Objeto del registro en el que se aplica la operacion.</param>
        /// <param name="llamada">Tipo de llamada: 1 Agregar, 2 Modificar y 3 Eliminar</param>
        public static void DireccionamientoRegistrosCveBusquedaIndices(Entidad entidad, int CveBusqueda, string nombreArchivo, string directorio, FileStream archivoDD, Registro nReg, int llamada,Registro regviejo)
        {
            List<Ordenadas> ordenadaE = new List<Ordenadas>();
            List<Ordenadas> ordenadaC = new List<Ordenadas>();
            List<Registro> registrosOrdenados = new List<Registro>();
            List<Ordenadas> DatosOrdenados = new List<Ordenadas>();
            

            //Agregamos a un lista Auxiliar y ordenamos
            foreach (Registro r in entidad.Datos)
            {
                if (entidad.Atributos[CveBusqueda].tipodato == 'E')
                {
                    Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[CveBusqueda]), r.dirReG);
                    DatosOrdenados.Add(nuevo);
                    DatosOrdenados = DatosOrdenados.OrderBy(o => o.entero).ToList();
                }
                else
                {
                    if (entidad.Atributos[CveBusqueda].tipodato == 'C')
                    {
                        Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[CveBusqueda]), r.dirReG);
                        DatosOrdenados.Add(nuevo);
                        DatosOrdenados = DatosOrdenados.OrderBy(o => o.cadena).ToList();
                    }
                }
            }

            for (int i = 0; i < DatosOrdenados.Count; i++)
            {
                try
                { DatosOrdenados[i].dirSig = DatosOrdenados[i + 1].dir; }
                catch (System.ArgumentOutOfRangeException) { DatosOrdenados[i].dirSig = -1; }
            }

            //Ordenamos logicamente.
            for (int i = 0; i < entidad.Datos.Count; i++)
            {
                Registro nuevo = new Registro();
                for (int j = 0; j < DatosOrdenados.Count; j++)
                {
                    if (entidad.Datos[i].dirReG == DatosOrdenados[j].dir)
                    {
                        nuevo.dirReG = entidad.Datos[i].dirReG;
                        nuevo.registros = new List<object>();
                        nuevo.registros = entidad.Datos[i].registros;
                        nuevo.dirSigR = DatosOrdenados[j].dirSig;
                        ModificaRegristro(entidad.ArchivoDato, entidad.rutaArchDatos, entidad, nuevo);
                        registrosOrdenados.Add(nuevo);
                        break;
                    }
                }
            }
            if (registrosOrdenados.Count > 0)
            {
                entidad.Datos = registrosOrdenados;
                entidad.dirDatos = DatosOrdenados[0].dir;
                ModificaEntidad(entidad, nombreArchivo);


                //Ciclo para Crear listas para indices

                for (int i = 0; i < entidad.Atributos.Count; i++)
                {
                    switch (entidad.Atributos[i].tipoIndice)
                    {
                        case 2://indice Primario.
                            switch (llamada)
                            {
                                case 1:

                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)//cambiar i por otra variable
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaE[0].dir*/ 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);

                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaE.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaE[j].entero, ordenadaE[j].dir, paso);
                                            }
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaC[0].dir*/0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaC.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaC[j].cadena, ordenadaC[j].dir, paso);
                                            }
                                        }
                                    }
                                    break;
                                case 2:


                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)//cambiar i por otra variable
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaE[0].dir*/ 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);

                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaE.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaE[j].entero, ordenadaE[j].dir, paso);
                                            }
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = /*entidad.Atributos[entidad.indicesPrimarios[i]].ordenadaC[0].dir*/0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            int step = (entidad.Atributos[entidad.indicesPrimarios[h]].longitud + 8);
                                            for (int j = 0; j < ordenadaC.Count; j++)
                                            {
                                                int paso = step * j;
                                                EscribirenIndice(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                    entidad.Atributos[entidad.indicesPrimarios[h]], ordenadaC[j].cadena, ordenadaC[j].dir, paso);
                                            }
                                        }
                                    }
                                    break;
                                case 3:

                                    foreach (Registro r in entidad.Datos)
                                    {
                                        if (entidad.Atributos[i].tipodato == 'E' &&
                                            entidad.Atributos[i].ExisteDatoEntero(ordenadaE, Convert.ToInt32(r.registros[i]), out int x) == false)//Checamos que no se repita el dato.
                                        {
                                            Ordenadas nuevo = new Ordenadas(Convert.ToInt32(r.registros[i]), r.dirReG);
                                            ordenadaE.Add(nuevo);
                                            ordenadaE = ordenadaE.OrderBy(o => o.entero).ToList();
                                        }
                                        else
                                        {
                                            if (entidad.Atributos[i].tipodato == 'C' &&
                                                entidad.Atributos[i].ExisteDatoCadena(ordenadaC, Convert.ToString(r.registros[i]), out int y) == false)//Checamos que no se repita el dato.
                                            {
                                                Ordenadas nuevo = new Ordenadas(Convert.ToString(r.registros[i]), r.dirReG);
                                                ordenadaC.Add(nuevo);
                                                ordenadaC = ordenadaC.OrderBy(o => o.cadena).ToList();
                                            }
                                        }
                                    }
                                    if (ordenadaE.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaE = ordenadaE;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice =  0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            Archivo.BorrarEnArchivoPrimario(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice,
                                                entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                        }
                                    }
                                    if (ordenadaC.Count != 0)
                                    {
                                        for (int h = 0; h < entidad.indicesPrimarios.Count; h++)
                                        {
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = new List<Ordenadas>();
                                            entidad.Atributos[entidad.indicesPrimarios[h]].ordenadaC = ordenadaC;
                                            entidad.Atributos[entidad.indicesPrimarios[h]].dirIndice = 0;
                                            ModificaAtributo(entidad.Atributos[entidad.indicesPrimarios[h]], nombreArchivo, archivoDD);
                                            Archivo.BorrarEnArchivoPrimario(entidad.Atributos[entidad.indicesPrimarios[h]].rutaIndice,
                                                entidad.Atributos[entidad.indicesPrimarios[h]].archivoIndice,
                                                entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                        }
                                    }
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 3://indice Secundario.

                            switch (llamada)
                            {
                                case 1:
                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);
                                    //falta actualizar en el archivo.
                                    break;
                                case 2:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, regviejo);

                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                                case 3:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                            }
                            ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 4://Arbol primario
                            /*foreach (Registro r in entidad.Datos)
                            {*/
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    //entidad.Atributos[i].Borrar(entidad.Atributos[i].Arbol, Convert.ToInt32(nReg.registros[i]), entidad.Atributos[i]);
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            //}
                            break;
                        case 5://Arbol secundario
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    //                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                    }
                }
            }
            else
            {
                  entidad.dirDatos = -1;
                    ModificaEntidad(entidad, nombreArchivo);

                    for (int i = 0; i < entidad.Atributos.Count; i++)
                    {
                    switch (entidad.Atributos[i].tipoIndice)
                    {
                        case 2://indice Primario.
                            if (ordenadaC.Count == 0 || ordenadaE.Count == 0)
                            {
                                Archivo.CreaArchivoIndicePrimario(entidad.Atributos[entidad.indicesPrimarios[i]].rutaIndice, entidad.Atributos[entidad.indicesPrimarios[i]].archivoIndice);
                                entidad.Atributos[entidad.indicesPrimarios[i]].dirIndice = -1;
                                ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 3://indice Secundario.
                            switch (llamada)
                            {
                                case 1:
                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);
                                    //falta actualizar en el archivo.
                                    break;
                                case 2:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, regviejo);

                                    entidad.Atributos[i].AgregarRegistroEnSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                                case 3:
                                    BorrarEnArchivo(nombreArchivo, archivoDD, entidad.Atributos[i], nReg.registros[i], nReg.dirReG, entidad.Atributos[i].tipodato);
                                    entidad.Atributos[i].BorrarRegistroSecundario(entidad, entidad.Atributos[i], i, nReg);

                                    break;
                            }
                            ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                        case 4://Arbol primario
                            /*foreach (Registro r in entidad.Datos)
                            {*/
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].Inserta(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    //entidad.Atributos[i].Borrar(entidad.Atributos[i].Arbol, Convert.ToInt32(nReg.registros[i]), entidad.Atributos[i]);
                                    entidad.Atributos[i].Borrar(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbol(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            //}
                            break;
                        case 5://Arbol secundario
                            switch (llamada)
                            {
                                case 1://Agregar a el arbol
                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 2://modificacion
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(regviejo.registros[i]), regviejo.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);

                                    entidad.Atributos[i].InsertaSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i], nombreArchivo, archivoDD);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                                case 3://eliminar
                                    entidad.Atributos[i].BorrarSECU(Convert.ToInt32(nReg.registros[i]), nReg.dirReG, entidad.Atributos[i].Arbol, entidad.Atributos[i]);
                                    ActualizarArbolSECU(entidad.Atributos[i].rutaIndice, entidad.Atributos[i].archivoIndice, entidad.Atributos[i].Arbol);
                                    entidad.Atributos[i].dirIndice = entidad.Atributos[i].RegresaRaiz();
                                    ModificaAtributo(entidad.Atributos[i], nombreArchivo, archivoDD);
                                    break;
                            }
                            ModificaEntidad(entidad, nombreArchivo);
                            break;
                    }
                    }

            }
            
        }
        public static void ElimarReg(Entidad entidad, long direccion)
        {
            foreach (Atributo item in entidad.Atributos)
            {
                for (int i = 0; i < item.datosIndiceS.Count; i++)
                {
                    for (int j = 0; j < item.datosIndiceS[i].lista.Count; j++)
                    {
                        if (item.datosIndiceS[i].lista[j].dir == direccion)
                        {
                            item.datosIndiceS[i].lista.RemoveAt(j--);
                        }
                        if (item.datosIndiceS[i].lista.Count == 0)
                        {
                            item.datosIndiceS.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Indice Primario
        public static void CreaArchivoIndicePrimario(string nombreArchivo, FileStream archivo)
        {
            archivo = File.Create(nombreArchivo);
            archivo.SetLength(2048);
            archivo.Close();
        }
        /// <summary>
        ///Funcion para escribir en el archivo de indice primario
        ///un elemento entero.
        /// </summary>
        /// <param NombreArchivo="">Ruta del archivo</param>
        /// <param archivo="">Objeto de la clase Atributo donde se almacenan los indices</param>
        public static void EscribirenIndice(string NombreArchivo, FileStream archivo, Atributo atributo, int elementoI, long dir, int paso)
        {
            archivo = File.Open(NombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(paso, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);
                bw.Write(Convert.ToInt32(elementoI));
            if (dir == 0)
            {
                dir += 1;
                bw.Write(dir);
            }else
            {
                bw.Write(dir);
            }

            archivo.Close();
        }
        public static void BorrarEnArchivoPrimario(string nombreArchivo, FileStream archivo, Atributo atributo, object clave, long direccion, char tipo)
        {

            archivo = File.Open(atributo.rutaIndice, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            BinaryWriter bw = new BinaryWriter(archivo);

            //long posision = 0;
            if (tipo == 'E')
            {
                for (int i = 0; i < atributo.ordenadaE.Count; i++)
                {
                    bw.Write(atributo.ordenadaE[i].entero);
                    if(atributo.ordenadaE[i].dir == 0)
                    { 
                        bw.Write(atributo.ordenadaE[i].dir+1);
                    }
                    else
                    {
                        bw.Write(atributo.ordenadaE[i].dir);
                    }

                    if (i + 1 == atributo.ordenadaE.Count)
                    {
                        long dir = archivo.Position;
                        int ultimoD = br.ReadInt32();
                        long ultimoDi = br.ReadInt64();
                        if (ultimoDi != 0 && ultimoD != 0)
                        {
                            archivo.Seek(dir, SeekOrigin.Begin);
                            bw.Write((long)0);
                            bw.Write((int)0);
                        }
                        //archivo.Seek(posision, SeekOrigin.Begin);

                    }
                }
            }
            else
            {
                for (int i = 0; i < atributo.ordenadaC.Count; i++)
                {
                    byte[] bytes = new byte[atributo.longitud];
                    for (int j = 0; j < atributo.longitud; j++)
                    {
                        try
                        {
                            if (atributo.ordenadaC[i].cadena[j] != '\0')
                            {
                                bytes[j] = Convert.ToByte(atributo.ordenadaC[i].cadena[j]);
                            }
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            bytes[j] = Convert.ToByte('\0');
                        }
                    }
                    bw.Write(bytes);
                    bw.Write(atributo.ordenadaC[i].dir);
                    if (i + 1 == atributo.ordenadaE.Count)
                    {
                        long dir = archivo.Position;
                        char[] ultimoD = br.ReadChars(atributo.longitud);
                        long ultimoDi = br.ReadInt64();
                        if (ultimoDi != 0 && ultimoD.Length != 0)
                        {
                            archivo.Seek(dir, SeekOrigin.Begin);
                            bw.Write((long)0);
                            bw.Write((int)0);
                        }
                        //chivo.Seek(posision, SeekOrigin.Begin);
                    }
                }
            }

            archivo.Close();

        }
        /// <summary>
        ///Funcion para escribir en el archivo de indice primario
        ///un elemento cadena.
        /// </summary>
        /// <param NombreArchivo="">Ruta del archivo</param>
        /// <param archivo="">Objeto de la clase Atributo donde se almacenan los indices</param>
        public static void EscribirenIndice(string NombreArchivo, FileStream archivo, Atributo atributo, string elementoI, long dir, int paso)
        {
            //paso es para saber cuanto vamos ir avanzando en el archivo cuando se escribe un dato.
            //long dir = TamañoArchivoS(NombreArchivo, archivo);
            archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //long dire = archivo.Length;
            archivo.Seek(paso, SeekOrigin.Begin);
            BinaryWriter bw = new BinaryWriter(archivo);
            byte[] bytes = new byte[atributo.longitud];
            string cadena = elementoI;
            for (int j = 0; j < atributo.longitud; j++)
            {
                try
                {
                    if (cadena[j] != '\0')
                    {
                        bytes[j] = Convert.ToByte(cadena[j]);
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    bytes[j] = Convert.ToByte('\0');
                }
            }
            bw.Write(bytes);
            if (dir == 0)
            {
                dir += 1;
                bw.Write(dir);
            }
            archivo.Close();
        }
        public static void LeerArchivoIndice(string NombreArchivo, FileStream archivo, Atributo atributo, char tipoDato)
        {
            archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(atributo.dirIndice, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            atributo.ordenadaE = new List<Ordenadas>();
            atributo.ordenadaC = new List<Ordenadas>();

            if (tipoDato == 'E')
            {
                long salir = -1;
                do
                {
                    int dato = -1;
                    long direccion = -1;
                    try {
                        dato = br.ReadInt32();
                        direccion = br.ReadInt64();
                        if(direccion == 1)
                        {
                            direccion = 0;
                            salir = direccion + 1;
                        }
                        else
                        {
                            if (direccion == 0)
                            {
                                salir = direccion;
                            }
                            else
                            {
                                salir = direccion;
                            }
                        }
                    } catch (System.IO.EndOfStreamException) { }
                    //dato = br.ReadInt32();
                    Ordenadas or = new Ordenadas(dato, direccion);
                    if (direccion != 1)
                    {
                        if(salir !=0)
                        {
                            atributo.ordenadaE.Add(or);
                        }
                    }
                } while (salir != 0);
            }
            else
            { 
                do
                {
                try {
                    string dato = "";
                    dato = br.ReadChars(atributo.longitud).ToString();
                    long direccion = -1;
                    direccion = br.ReadInt64();
                    Ordenadas or = new Ordenadas(dato, direccion);
                    if (dato != "" && direccion != -1)
                    {
                        atributo.ordenadaE.Add(or);
                    }
                    } catch (System.IO.EndOfStreamException) { break; }
                } while (br.ReadChars(atributo.longitud) != null);
            }
            archivo.Close();
        }
        #endregion

        #region Indice Secundario
        /// <summary>
        ///Funcion para crear un archivo de indice secundario con su bloque principal.
        ///De tamaño 2048 Bytes
        /// </summary>
        /// <param NombreArchivo="">Ruta del archivo</param>
        /// <param archivo="">Objeto de la clase Atributo donde se almacenan los indices</param>
        public static void CreaArchivoIndiceSecundario(string nombreArchivo, FileStream archivo)
        {
            archivo = File.Create(nombreArchivo);
            archivo.SetLength(2048);
            archivo.Close();
        }
        /// <summary>
        ///Funcion para crear un bloque en el archivo secundario que nos devuelve una direccion de el inicio del bloque
        ///para asignarla a el bloque primario.
        /// </summary>
        /// <param NombreArchivo="">Ruta del archivo</param>
        /// <param archivo="">Objeto de la clase Atributo donde se almacenan los indices</param>
        public static long CrearBloque(string nombreArchivo, FileStream archivo)
        {
            long inicioBloque = UltimaPosicion(nombreArchivo, archivo);
            archivo = File.Open(nombreArchivo, FileMode.Open);
            //archivo.Seek(inicioBloque,SeekOrigin.Begin);
            archivo.SetLength(inicioBloque + 2048);
            archivo.Close();
            return inicioBloque;
        }
        public static long UltimaPosicion(string nombreArchivo, FileStream archivo)
        {
            long direccion = -1;
            archivo = File.Open(nombreArchivo, FileMode.Open);
            direccion = archivo.Length;
            archivo.Close();
            return direccion;
        }
        public static void EscribirIndiceSecundarioBloquePrincipal(string nombreArchivo, FileStream archivo, List<Ordenadas> listaPrincipal,char tipo, int longitud, object clave, long dirDatos, long dirBloqueP)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            //BinaryReader br = new BinaryReader(archivo);
            BinaryWriter bw = new BinaryWriter(archivo);
            //long direccion = 0;
            //archivo.Seek(0, SeekOrigin.Begin);
            foreach (Ordenadas or in listaPrincipal)
            {
                bw.Write(or.dir);
                if (tipo == 'E')
                {
                    bw.Write(or.entero);
                }
                else
                {
                    if (tipo == 'C')
                    {
                        char[] chars = new char[longitud];
                        //string cadena = Convert.ToString(or.cadena);
                        for (int j = 0; j < longitud; j++)
                        {
                            try
                            {
                                if (or.cadena[j] != '\0')
                                {
                                    chars[j] = Convert.ToChar(or.cadena[j]);
                                }
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                chars[j] = Convert.ToChar('\0');
                            }
                        }

                        bw.Write(chars);
                    }
                }
            }
            archivo.Close();
        }
        public static void EscribirIndiceSecundarioBloques(string nombreArchivo, FileStream archivo, List<Ordenadas> listadeCVE, long dirBloqueP)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            BinaryWriter bw = new BinaryWriter(archivo);
            archivo.Seek(dirBloqueP, SeekOrigin.Begin);

            foreach (Ordenadas orde in listadeCVE)
            {
                if (orde.dir == 0)
                {
                    long aux = orde.dir + 1;
                    bw.Write(aux);
                }
                else { bw.Write(orde.dir); }
            }
            archivo.Close();
        }
        /// <summary>
        /// hacer antes de borrar en fisico primero en memoria 
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="archivo"></param>
        /// <param name="atributo"></param>
        /// <param name="clave"></param>
        /// <param name="claveS"></param>
        /// <param name="tipo"></param>
        public static void BorrarEnArchivo(string nombreArchivo,FileStream archivo,Atributo atributo, object clave,long direccion, char tipo)
        {
            
            archivo = File.Open(atributo.rutaIndice, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            BinaryWriter bw = new BinaryWriter(archivo);

            long posision = 0;
            for (int i = 0; i < atributo.datosIndiceS.Count; i++)
            {
                //escribir y ir a direccion de bloque. 
                bw.Write(atributo.datosIndiceS[i].dir);
                if (tipo == 'E')
                {
                    bw.Write(atributo.datosIndiceS[i].entero);
                }
                else
                {
                    if (tipo == 'C')
                    {
                        char[] chars = new char[atributo.longitud];
                        //string cadena = Convert.ToString(or.cadena);
                        for (int j = 0; j < atributo.longitud; j++)
                        {
                            try
                            {
                                if (atributo.datosIndiceS[i].cadena[j] != '\0')
                                {
                                    chars[j] = Convert.ToChar(atributo.datosIndiceS[i].cadena[j]);
                                }
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                chars[j] = Convert.ToChar('\0');
                            }
                        }

                        bw.Write(chars);
                    }
                
                }
                posision = archivo.Position;
                archivo.Seek(atributo.datosIndiceS[i].dir,SeekOrigin.Begin);

                for (int j = 0; j < atributo.datosIndiceS[i].lista.Count; j++)
                {
                    //escribir todos los elementos.
                    if(atributo.datosIndiceS[i].lista[j].dir == 0)
                    {
                        bw.Write(atributo.datosIndiceS[i].lista[j].dir+1);
                    }
                    else
                    {
                        bw.Write(atributo.datosIndiceS[i].lista[j].dir);
                    }

                    //checar en el despues del siguiente si hay aun elementos , de ser asi borrar escribiendo cero.
                    if(j+1 == atributo.datosIndiceS[i].lista.Count)
                    {
                        long dir = archivo.Position;
                        long ultimo = br.ReadInt64();
                        if(ultimo != 0)
                        {
                            archivo.Seek(dir, SeekOrigin.Begin);
                            bw.Write((long)0);
                        }
                        archivo.Seek(posision, SeekOrigin.Begin);
                    }
                }


                //checar el ultimo
                if (i + 1 == atributo.datosIndiceS.Count)
                {
                    long dir = archivo.Position;
                    long ultimoDi = br.ReadInt64();
                    int ultimoD= br.ReadInt32();
                    if (ultimoDi != 0 && ultimoD != 0)
                    {
                        archivo.Seek(dir, SeekOrigin.Begin);
                        bw.Write((long)0);
                        bw.Write((int)0);
                    }
                    archivo.Seek(posision, SeekOrigin.Begin);
                }
            }
            archivo.Close();

        }
        public static bool ExisteBloque(string nombreArchivo, FileStream archivo)
        {
            bool existe = false;
            archivo = File.Open(nombreArchivo, FileMode.Open);
            BinaryReader br = new BinaryReader(archivo);

            return existe;
        }
        public static void LeerArchivoIndiceSecundario(string NombreArchivo, FileStream archivo, Atributo atributo, char tipoDato)
        {
            archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            atributo.datosIndiceS = new List<Ordenadas>();

            if (tipoDato == 'E')
            {
                int salir = 0;
                do
                {
                    int dato = -1;
                    long direccion = -1;
                    try
                    {
                        direccion = br.ReadInt64();
                        dato = br.ReadInt32();
                    }
                    catch (System.IO.EndOfStreamException) { }
                    long ultima = archivo.Position;
                    salir = dato;
                    Ordenadas or = new Ordenadas(dato, direccion);
                    if (dato != 0 && direccion != 0)
                    {
                        or.lista = new List<Ordenadas>();
                        long salirBloque = -1;
                        archivo.Seek(direccion, SeekOrigin.Begin);
                        do
                        {
                            long dir = -1;
                            try
                            {
                                dir = br.ReadInt64();
                                salirBloque = dir;
                                if (dir == 1)
                                {
                                    dir -= 1;
                                }
                            }
                            catch (System.IO.EndOfStreamException) { }
                            //long ultima = archivo.Position;

                            Ordenadas orBloque = new Ordenadas(dir);
                            if (salirBloque == 1 && dir == 0)
                            {
                                //salirBloque = dir;
                                or.lista.Add(orBloque);
                            }
                            if (dir != 0)
                            {
                                if (ExistedirREG(or.lista, dir) == false)
                                { or.lista.Add(orBloque); }
                            }
                        } while (salirBloque != 0);

                        atributo.datosIndiceS.Add(or);
                        archivo.Seek(ultima, SeekOrigin.Begin);
                    }
                } while (salir != 0);
            }
            else
            {
                long direccion;
                do
                {
                    string dato = "";
                    direccion = -1;
                    try
                    {
                        direccion = br.ReadInt64();
                        dato = CharArraytoString(br.ReadChars(atributo.longitud));
                    }
                    catch (System.IO.EndOfStreamException) { }
                    long ultima = archivo.Position;
                    //salir = dato;
                    Ordenadas or = new Ordenadas(dato, direccion);
                    if (direccion != 0)
                    {
                        or.lista = new List<Ordenadas>();
                        long salirBloque = -1;
                        archivo.Seek(direccion, SeekOrigin.Begin);
                        do
                        {
                            long dir = -1;
                            try
                            {
                                dir = br.ReadInt64();
                                salirBloque = dir;
                                if (dir == 1)
                                {
                                    dir -= 1;
                                }
                            }
                            catch (System.IO.EndOfStreamException) { }
                            //long ultima = archivo.Position;

                            Ordenadas orBloque = new Ordenadas(dir);
                            if (salirBloque == 1 && dir == 0)//identifico con el numero 1 que es el registro de la direccion 0.
                            {
                                //salirBloque = dir;
                                or.lista.Add(orBloque);
                            }
                            if (dir != 0)
                            {
                                if (ExistedirREG(or.lista, dir) == false)
                                { or.lista.Add(orBloque); }
                            }
                        } while (salirBloque != 0);

                        atributo.datosIndiceS.Add(or);
                        archivo.Seek(ultima, SeekOrigin.Begin);
                    }
                } while (direccion != 0);
            }
            /*atributo.dirIndice = atributo.datosIndiceS[0].dir;
            ModificaAtributo(atributo, NombreArchivo, archivo);*/
            archivo.Close();
        }
        public static bool ExistedirREG(List<Ordenadas> lista, long dir)
        {
            bool existe = false;
            foreach (Ordenadas a in lista)
            {
                if (a.dir == dir)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        public static string CharArraytoString(char[] nombre)
        {
            string n = "";
            int i = 0;
            foreach (char c in nombre)
            {
                n += c;
                i++;
            }
            return n;
        }
        #endregion

        #region Arbol Primario
        /// <summary>
        /// Funcion que nos crea un pedazo de memoria con el valor de el tamaño de la hoja 
        /// de un arbol 
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo que se obtiene de un objeto de la clase Atributo</param>
        /// <param name="archivo">Archivo que se obtiene de un objeto de la clase Atributo</param>
        /// <returns>Nos devuelve el inicio de donde se crea la hoja, para ponerlo de apuntador en el objeto HOJA</returns>
        public static long CreacionHojas(string nombreArchivo, FileStream archivo)
        {
            long inicioHoja = UltimaPosicion(nombreArchivo, archivo);
            archivo = File.Open(nombreArchivo, FileMode.Open);
            archivo.SetLength(inicioHoja + 65);
            archivo.Close();
            return inicioHoja;
        }
        /// <summary>
        /// Funcion para escribir en el archivo de indice Primario en
        /// donde usamos el parametro de a hoja para posicionarnos en el Archivo
        /// y poder sobre escribir los elementos de la hoja en el pedaso de memoria
        /// ya reservado.
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo que se obtiene de un objeto de la clase Atributo</param>
        /// <param name="archivo">Archivo que se obtiene de un objeto de la clase Atributo</param>
        /// <param name="hoja">Objeto que se va a escribir en el archivo</param>
        public static void EscribirEnHoja(string nombreArchivo, FileStream archivo, Hoja hoja)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    bw.Write(hoja.ListPointKey[i].dirDER);
                    bw.Write(hoja.ListPointKey[i].entero);
                }
                catch (System.ArgumentOutOfRangeException) {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
            bw.Write(hoja.dirsignodo);

            archivo.Close();
        }
        /// <summary>
        ///En esta funcion se escriben una lista de 5 ordenadas ya que la raiz no tiene un apuntador siguiente \n
        ///entonces este se sustituye por el primer apuntador en la lista de la raiz y el tamaño de la lista \n
        ///cambia a un grado 5. se usa para la raiz y los intermedios ya que tienen la misma estructura.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="archivo"></param>
        /// <param name="hoja"></param>
        public static void EscribirEnRaiz(string nombreArchivo, FileStream archivo, Hoja hoja)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (i == 0)
                    {
                        bw.Write(hoja.ListPointKey[i].dirIZQ);
                        bw.Write(hoja.ListPointKey[i].entero);
                        bw.Write(hoja.ListPointKey[i].dirDER);
                    }
                    else
                    {
                        bw.Write(hoja.ListPointKey[i].dirDER);
                        bw.Write(hoja.ListPointKey[i].entero);
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
            //bw.Write(hoja.p5);

            archivo.Close();
        }
        public static void CreaArchivoArbolP(string nombreArchivo, FileStream archivo)
        { 
            archivo = File.Create(nombreArchivo);
            archivo.Close();
        }
        public static void LeerArbolPrimario(Atributo atributo, string NombreArchivo, FileStream archivo)
        {
            archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(atributo.dirIndice, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            Hoja hoja = new Hoja();

            hoja.tipoHoja = br.ReadChar();
            hoja.dirnodo = br.ReadInt64();
            hoja.ListPointKey = new List<Ordenadas>();
            if(hoja.dirnodo == 0)
            {
                Hoja hoja1 = new Hoja();
                hoja1 = LeerHoja(archivo, br, atributo.dirIndice, atributo);
                atributo.Arbol.Add(hoja1);
            }
            else
            {
                if (hoja.tipoHoja == 'R' || hoja.tipoHoja == 'I')
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Ordenadas or = new Ordenadas();
                        if (i == 0)
                        {
                            or.dirIZQ = br.ReadInt64();
                            or.entero = br.ReadInt32();
                            or.dirDER = br.ReadInt64();
                            hoja.ListPointKey.Add(or);
                        }
                        else
                        {
                            or.dirDER = br.ReadInt64();
                            or.entero = br.ReadInt32();
                            if (or.dirDER != -1 && or.entero != -1)
                            { hoja.ListPointKey.Add(or); }
                        }
                    }
                    atributo.Arbol.Add(hoja);
                }
                for (int i = 0; i < 4; i++)
                {
                    Hoja hoja1 = new Hoja();
                    if (i == 0)
                    {
                        hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[i].dirIZQ, atributo);
                        if(hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                        }
                        hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                        if (hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                        }
                    }
                    else
                    {
                        try
                        {
                            hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                            if (hoja1.tipoHoja == 'H')
                            {
                                atributo.Arbol.Add(hoja1);
                            }

                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                        }
                    }

                }
            }
            archivo.Close();
        }
        public static Hoja LeerHoja(FileStream archivo, BinaryReader br, long direccion, Atributo atributo)
        {
            Hoja hoja = new Hoja();
            archivo.Seek(direccion, SeekOrigin.Begin);

            hoja.tipoHoja = br.ReadChar();
            hoja.dirnodo = br.ReadInt64();
            if (hoja.tipoHoja == 'I')
            {
                hoja.ListPointKey = new List<Ordenadas>();
                for (int i = 0; i < 4; i++)
                {
                    Ordenadas or = new Ordenadas();
                    if (i == 0)
                    {
                        or.dirIZQ = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        or.dirDER = br.ReadInt64();
                        hoja.ListPointKey.Add(or);
                    }
                    else
                    {
                        or.dirDER = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        if (or.dirDER != -1 && or.entero != -1)
                        { hoja.ListPointKey.Add(or); }
                    }
                }
                atributo.Arbol.Add(hoja);
                for (int i = 0; i < 4; i++)
                {
                    Hoja hoja1 = new Hoja();
                    if (i == 0)
                    {
                        hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[0].dirIZQ, atributo);
                        //atributo.Arbol[atributo.Arbol.Count - 2].dirsignodo = hoja1.dirnodo;
                        atributo.Arbol.Add(hoja1);
                        hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[0].dirDER, atributo);
                        atributo.Arbol.Add(hoja1);
                    }
                    else
                    {
                        try {
                            hoja1 = LeerHoja(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                            atributo.Arbol.Add(hoja1);
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                        }
                    }

                }
            }
            else
            {
                if (hoja.tipoHoja == 'H')
                {
                    hoja.ListPointKey = new List<Ordenadas>();
                    for (int i = 0; i < 4; i++)
                    {
                        Ordenadas ordenadas = new Ordenadas();
                        ordenadas.dirDER = br.ReadInt64();
                        ordenadas.entero = br.ReadInt32();
                        if (ordenadas.dirDER != -1 && ordenadas.entero != -1)
                        {
                            hoja.ListPointKey.Add(ordenadas);
                        }
                    }
                    hoja.dirsignodo = br.ReadInt64();
                }
            }
            return hoja;
        }
        public static void ActualizarArbol(string nombreArchivo, FileStream archivo, List<Hoja> arbol)
        {
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            foreach (Hoja hoja in arbol)
            {
                if (hoja.tipoHoja == 'H')
                { ActualizaHoja(archivo, hoja); }
                else
                {
                    ActualizaRaizeInter(archivo, hoja);
                }

            }
            archivo.Close();
        }
        public static void ActualizaHoja(FileStream archivo, Hoja hoja)
        {
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    bw.Write(hoja.ListPointKey[i].dirDER);
                    bw.Write(hoja.ListPointKey[i].entero);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
            bw.Write(hoja.dirsignodo);

        }
        public static void ActualizaRaizeInter(FileStream archivo, Hoja hoja)
        {
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (i == 0)
                    {
                        bw.Write(hoja.ListPointKey[i].dirIZQ);
                        bw.Write(hoja.ListPointKey[i].entero);
                        bw.Write(hoja.ListPointKey[i].dirDER);
                    }
                    else
                    {
                        bw.Write(hoja.ListPointKey[i].dirDER);
                        bw.Write(hoja.ListPointKey[i].entero);
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
        }
        #endregion

        #region Arbol Secundario
        public static void LeerArbolSecundario(Atributo atributo, string NombreArchivo, FileStream archivo)
        {
            archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            archivo.Seek(atributo.dirIndice, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(archivo);
            Hoja hoja = new Hoja();

            hoja.tipoHoja = br.ReadChar();
            hoja.dirnodo = br.ReadInt64();
            hoja.ListPointKey = new List<Ordenadas>();
            if (hoja.tipoHoja == 'R')
            {
                for (int i = 0; i < 4; i++)
                {
                    Ordenadas or = new Ordenadas();
                    if (i == 0)
                    {
                        or.dirIZQ = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        or.dirDER = br.ReadInt64();
                        hoja.ListPointKey.Add(or);
                    }
                    else
                    {
                        or.dirDER = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        if (or.dirDER != -1 && or.entero != -1)
                        { hoja.ListPointKey.Add(or); }
                    }
                }
                atributo.Arbol.Add(hoja);
                for (int i = 0; i < 4; i++)
            {
                Hoja hoja1 = new Hoja();
                if (i == 0)
                {
                    try
                    {
                        hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirIZQ, atributo);
                        if (hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                            LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                        }
                    }
                    catch (System.ArgumentOutOfRangeException) { }
                    //atributo.Arbol.Add(hoja1);
                    //LeerBloqueDeHoja(archivo,br,hoja1.ListPointKey);
                    try 
                    { 
                        hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                        if (hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                            LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                        }
                    
                    } catch (System.ArgumentOutOfRangeException) { }
                    //atributo.Arbol.Add(hoja1);
                    //LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                }
                else
                {
                    try
                    {
                        hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                        if (hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                            LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                        }
                        //atributo.Arbol.Add(hoja1);
                        //LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        
                    }
                }

            }
            }
            else
            {
                hoja = LeerHojaS(archivo, br, hoja.dirnodo, atributo);
                LeerBloqueDeHoja(archivo, br, hoja.ListPointKey);
                atributo.Arbol.Add(hoja);
            }
            archivo.Close();
        }
        public static Hoja LeerHojaS(FileStream archivo, BinaryReader br, long direccion, Atributo atributo)
        {
            Hoja hoja = new Hoja();
            archivo.Seek(direccion, SeekOrigin.Begin);

            hoja.tipoHoja = br.ReadChar();
            hoja.dirnodo = br.ReadInt64();
            if (hoja.tipoHoja == 'I')
            {
                hoja.ListPointKey = new List<Ordenadas>();
                for (int i = 0; i < 4; i++)
                {
                    Ordenadas or = new Ordenadas();
                    if (i == 0)
                    {
                        or.dirIZQ = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        or.dirDER = br.ReadInt64();
                        hoja.ListPointKey.Add(or);
                    }
                    else
                    {
                        or.dirDER = br.ReadInt64();
                        or.entero = br.ReadInt32();
                        if (or.dirDER != -1 && or.entero != -1)
                        { hoja.ListPointKey.Add(or); }
                    }
                }
                atributo.Arbol.Add(hoja);
                for (int i = 0; i < 4; i++)
                {
                    Hoja hoja1 = new Hoja();
                    if (i == 0)
                    {
                        hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirIZQ, atributo);
                        if(hoja1.tipoHoja == 'H')
                        {                            
                            atributo.Arbol.Add(hoja1);
                            LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                        }
                        hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                        if (hoja1.tipoHoja == 'H')
                        {
                            atributo.Arbol.Add(hoja1);
                            LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                        }
                    }
                    else
                    {
                        try
                        {
                            hoja1 = LeerHojaS(archivo, br, hoja.ListPointKey[i].dirDER, atributo);
                            if (hoja1.tipoHoja == 'H')
                            {
                                atributo.Arbol.Add(hoja1);
                                LeerBloqueDeHoja(archivo, br, hoja1.ListPointKey);
                            }
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                        }
                    }

                }
            }
            else
            {
                if (hoja.tipoHoja == 'H')
                {
                    hoja.ListPointKey = new List<Ordenadas>();
                    for (int i = 0; i < 4; i++)
                    {
                            Ordenadas ordenadas = new Ordenadas();
                            ordenadas.dirDER = br.ReadInt64();
                            ordenadas.entero = br.ReadInt32();
                            if (ordenadas.dirDER != -1 && ordenadas.entero != -1)
                            {
                                hoja.ListPointKey.Add(ordenadas);
                            }
                    }
                    hoja.dirsignodo = br.ReadInt64();
                    
                }
            }
            return hoja;
        }
        public static void LeerBloqueDeHoja(FileStream archivo, BinaryReader br, List<Ordenadas> lista)
        {
            //archivo = File.Open(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            //archivo.Seek(0, SeekOrigin.Begin);
            //BinaryReader br = new BinaryReader(archivo);
            foreach (Ordenadas or in lista)
            {
                or.lista = new List<Ordenadas>();
                long salirBloque = -1;
                archivo.Seek(or.dirDER, SeekOrigin.Begin);
                //archivo.Seek(direccion, SeekOrigin.Begin);
                do
                {
                    long dir = -1;
                    try
                    {
                        dir = br.ReadInt64();
                        salirBloque = dir;
                        if (dir == 1)
                        {
                            dir -= 1;
                        }
                    }
                    catch (System.IO.EndOfStreamException) { }
                    //long ultima = archivo.Position;

                    Ordenadas orBloque = new Ordenadas(dir);
                    if (salirBloque == 1 && dir == 0)
                    {
                        //salirBloque = dir;
                        or.lista.Add(orBloque);
                    }
                    if (dir != 0)
                    {
                        if (ExistedirREG(or.lista, dir) == false)
                        { or.lista.Add(orBloque); }
                        //break;
                    }
                } while (salirBloque != 0);
                //or.lista.Add(or);
                //break;
            }
        }
        public static void ActualizarArbolSECU(string nombreArchivo, FileStream archivo, List<Hoja> arbol)
        {
            
            archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            foreach (Hoja hoja in arbol)
            {
                if (hoja.tipoHoja == 'H')
                { ActualizaHojaSECU(archivo, hoja); }
                else
                {
                    ActualizaRaizeInterSECU(archivo, hoja);
                }

            }
            archivo.Close();
        }
        public static void ActualizaHojaSECU(FileStream archivo, Hoja hoja)
        {
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    bw.Write(hoja.ListPointKey[i].dirDER);
                    long pos = archivo.Position;
                    //guardamos la posicion anterior del archivo para regresar a escribir en el despues de actualizar el bloque.
                    ActualizarBloqueSECU(archivo, hoja.ListPointKey[i].lista, hoja.ListPointKey[i].dirDER);
                    archivo.Seek(pos, SeekOrigin.Begin);
                    bw.Write(hoja.ListPointKey[i].entero);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
            bw.Write(hoja.dirsignodo);

        }
        public static void ActualizaRaizeInterSECU(FileStream archivo, Hoja hoja)
        {
            archivo.Seek(hoja.dirnodo, SeekOrigin.Begin);

            BinaryWriter bw = new BinaryWriter(archivo);

            bw.Write(hoja.tipoHoja);
            bw.Write(hoja.dirnodo);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (i == 0)
                    {
                        bw.Write(hoja.ListPointKey[i].dirIZQ);
                        bw.Write(hoja.ListPointKey[i].entero);
                        bw.Write(hoja.ListPointKey[i].dirDER);
                    }
                    else
                    {
                        bw.Write(hoja.ListPointKey[i].dirDER);
                        bw.Write(hoja.ListPointKey[i].entero);
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    long x = -1;
                    bw.Write(x);
                    int y = -1;
                    bw.Write(y);
                }
            }
        }
        public static void ActualizarBloqueSECU(FileStream archivo, List<Ordenadas> listadeCVE, long dirBloqueP)
        {
            //archivo = File.Open(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            BinaryWriter bw = new BinaryWriter(archivo);
            BinaryReader br = new BinaryReader(archivo);
            archivo.Seek(dirBloqueP, SeekOrigin.Begin);

            for (int i = 0; i < listadeCVE.Count; i++)
            {
                if(listadeCVE[i].dir == 0)
                {
                    long aux = listadeCVE[i].dir + 1;
                    bw.Write(aux);
                }
                else
                {
                    bw.Write(listadeCVE[i].dir);
                }
                if(i+1 == listadeCVE.Count)
                {
                    long sobra = 0;
                    long dir = archivo.Position;
                    sobra = br.ReadInt64();
                    if(sobra != 0)
                    {
                        archivo.Seek(dir,SeekOrigin.Begin);
                        bw.Write((long)0);
                    }
                }
                
            }


            //archivo.Close();
        }
        #endregion
    }
}
