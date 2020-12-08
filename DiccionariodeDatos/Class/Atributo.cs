using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariodeDatos
{
    [Serializable]
    class Atributo
    {
        byte[] ID = new byte[5];
        public byte[] id { get { return ID; } set { ID = value; } }
        char[] Nombre = new char[35];
        public char[] nombre { get { return Nombre; } set { Nombre = value; } }
        char Tipodato = new char();
        public char tipodato { get { return Tipodato; } set { Tipodato = value; } }
        int Longitud;
        public int longitud { get { return Longitud; } set { Longitud = value; } }
        long DirAtributo;
        public long dirAtributo { get { return DirAtributo; } set { DirAtributo = value; } }
        int TipoIndice;
        public int tipoIndice { get { return TipoIndice; } set { TipoIndice = value; } }
        long DirIndice;
        public long dirIndice { get { return DirIndice; } set { DirIndice = value; } }
        long DirSigA;
        public long dirSigA { get { return DirSigA; } set { DirSigA = value; } }
        private List<Ordenadas> archivoDatos;
        public List<Ordenadas> Datos { get { return archivoDatos; } set { archivoDatos = value; } }
        public List<Hoja> Arbol = new List<Hoja>();
        // Contiene la ruta del archivo de datos.
        private string RutaIndice;
        /// <summary>
        /// ruta del archivo
        /// </summary>
        public string rutaIndice { get { return RutaIndice; } set { RutaIndice = value; } }
        //Archivo de Datos
        private FileStream ArchivoIndice;
        /// <summary>
        /// objeto tipo FileStream que almacena el archivo de indicie
        /// </summary>
        public FileStream archivoIndice { get { return ArchivoIndice; } set { ArchivoIndice = value; } }
        /// <summary>
        /// lista donde se guarda el indice secundario
        /// </summary>
        public List<Ordenadas> datosIndiceS;//lista de secundario        
        //listas de primarios
        public List<Ordenadas> ordenadaE;
        public List<Ordenadas> ordenadaC;
        //lista de Hojas del arbol
        bool UNA = false;
        public Atributo(byte[] id, char[] name, char tipoDato, int longi, int tipoindice)
        {
            nombre = name;
            this.id = id;
            tipodato = tipoDato;
            longitud = longi;
            tipoIndice = tipoindice;
        }
        public Atributo(byte[] id, char[] name, long dAtr, char tDato, int longi, int tInd, long dInd, long dSig)
        {
            this.id = id;
            nombre = CambiaNombre(name);
            //nombre = name;
            dirAtributo = dAtr;
            tipodato = tDato;
            longitud = longi;
            tipoIndice = tInd;
            dirIndice = dInd;
            dirSigA = dSig;
            ordenadaC = new List<Ordenadas>();
            ordenadaE = new List<Ordenadas>();
            datosIndiceS = new List<Ordenadas>();
        }
        private char[] CambiaNombre(char[] name)
        {
            nombre = new char[35];
            int i = 0;
            foreach (char c in name)
            {
                nombre[i] = c;
                i++;
            }
            return nombre;
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
        public string NombreAtributo()
        {
            string regresada = "";

            for (int i = 0; i < nombre.Length; i++)
            {
                if (nombre[i] != '\0')
                    regresada += nombre[i];
            }
            //regresada = QuitarEspacios(regresada);
            return regresada;
        }
        public string QuitarEspacios(string cadena)
        {
            char[] nuevo = cadena.ToCharArray();

            for (int i = 0; i < nuevo.Length; i++)
            {
                if (nuevo[i] == '\0')
                {
                    // nuevo[i] = '';
                }
            }

            string nueva = "";
            for (int i = 0; i < nueva.Length; i++)
            {
                nueva += nuevo[i];
            }

            return nueva;
        }


        #region Indices Primario y Secundario
        /// <summary>
        /// Funcion para ver si hay un dato que ya este repetido en la lista donde se guardan los datos del indice
        /// </summary>
        /// <param name="lista">lista en donde buscaremos</param>
        /// <param name="dato">Tipo de dato Entero</param>
        public bool ExisteDatoEntero(List<Ordenadas> lista, int dato, out int indice)
        {
            bool exist = false;
            indice = 0;
            foreach (Ordenadas a in lista)
            {
                indice += 1;
                if (a.entero == dato)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        /// <summary>
        /// Funcion para ver si hay un dato que ya este repetido en la lista donde se guardan los datos del indice
        /// </summary>
        /// <param name="lista">lista en donde buscaremos</param>
        /// <param name="dato">Tipo de dato string</param>
        public bool ExisteDatoCadena(List<Ordenadas> lista, string dato,out int indice)
        {
            bool exist = false;
            indice = 0;
            foreach (Ordenadas a in lista)
            {
                indice += 1;
                if (a.cadena == dato)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        public string GetID()
        {
            //string regresada = "";
            string regresada = String.Concat(id.Select(x => x.ToString("X2")).ToArray());
            return regresada;
        }
        public bool ExisteCveEnSecundarioC(string cad)
        {
            bool existe = false;

            foreach (Ordenadas a in datosIndiceS)
            {
                if (a.cadena == cad)
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }
        public bool ExisteCveEnSecundarioE(int dato)
        {
            bool existe = false;

            foreach (Ordenadas a in datosIndiceS)
            {
                if (a.entero == dato)
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }
        public void AgregarBloquePrincipalE(int cve, long DireccionBloque, long dirReg)
        {
            for (int i = 0; i < datosIndiceS.Count; i++)
            {
                int index;
                if (ExistedirREGotraLista(datosIndiceS, dirReg, i, out index))
                {
                    for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                    {
                        if (i != index)
                        {
                            if (datosIndiceS[index].lista[j].dir == dirReg)
                            {
                                datosIndiceS[index].lista.RemoveAt(j);
                                datosIndiceS[j].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                                if (datosIndiceS[index].lista.Count == 0)
                                {
                                    datosIndiceS.RemoveAt(i);
                                    break;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            Ordenadas nueva = new Ordenadas(cve, DireccionBloque);
            nueva.lista = new List<Ordenadas>();
            nueva.lista.Add(new Ordenadas(dirReg));
            nueva.lista = nueva.lista.OrderBy(o => o.dir).ToList();
            datosIndiceS.Add(nueva);
            datosIndiceS = datosIndiceS.OrderBy(o => o.entero).ToList();
        }
        public void AgregarBloquePrincipalC(string cve, long DireccionBloque, long dirReg)
        {
            for (int i = 0; i < datosIndiceS.Count; i++)
            {
                int index;
                if (ExistedirREGotraLista(datosIndiceS, dirReg, i, out index))
                {
                    for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                    {
                        if (i != index)
                        {
                            if (datosIndiceS[index].lista[j].dir == dirReg)
                            {
                                datosIndiceS[index].lista.RemoveAt(j);
                                datosIndiceS[j].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                                if (datosIndiceS[index].lista.Count == 0)
                                {
                                    datosIndiceS.RemoveAt(i);
                                    break;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            Ordenadas nueva = new Ordenadas(cve, DireccionBloque);
            nueva.lista = new List<Ordenadas>();
            nueva.lista.Add(new Ordenadas(dirReg));
            nueva.lista = nueva.lista.OrderBy(o => o.dir).ToList();
            datosIndiceS.Add(nueva);
            datosIndiceS = datosIndiceS.OrderBy(o => o.cadena).ToList();
        }
        public void AgregarBloque(int cve, long dirReg)
        {
            int indice;
            if (ExisteDatoEntero(datosIndiceS, cve,out indice) == true)
            {
                if (ExistedirREG(datosIndiceS[indice - 1].lista, dirReg,out int n) == false)
                {
                    for (int i = 0; i < datosIndiceS.Count; i++)
                    {
                        if (datosIndiceS[i].entero == cve)
                        {
                            datosIndiceS[i].lista.Add(new Ordenadas(dirReg));
                            datosIndiceS[i].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                            int index;
                            if (ExistedirREGotraLista(datosIndiceS, dirReg, i, out index))
                            {
                                for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                                {
                                    if (i != index)
                                    {
                                        if (datosIndiceS[index].lista[j].dir == dirReg)
                                        {
                                            datosIndiceS[index].lista.RemoveAt(j);
                                            datosIndiceS[index].lista = datosIndiceS[index].lista.OrderBy(o => o.dir).ToList();
                                            if (datosIndiceS[index].lista.Count == 0)
                                            {
                                                datosIndiceS.RemoveAt(index);
                                                break;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            
        }
        public void AgregarBloque(string cve, long dirReg)
        {
            int indice;
            if(ExisteDatoCadena(datosIndiceS,cve,out indice) == true)
            {
                if(ExistedirREG(datosIndiceS[indice-1].lista,dirReg,out int n) == false)
                {
                    for (int i = 0; i < datosIndiceS.Count; i++)
                    {
                        if (datosIndiceS[i].cadena == cve)
                        {   
                            datosIndiceS[i].lista.Add(new Ordenadas(dirReg));
                            datosIndiceS[i].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                            int index;
                            if (ExistedirREGotraLista(datosIndiceS, dirReg, i, out index))
                            {
                                for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                                {
                                    if (i != index)
                                    {
                                        if (datosIndiceS[index].lista[j].dir == dirReg)
                                        {
                                            datosIndiceS[index].lista.RemoveAt(j);
                                            datosIndiceS[index].lista = datosIndiceS[index].lista.OrderBy(o => o.dir).ToList();
                                            if (datosIndiceS[index].lista.Count == 0)
                                            {
                                                datosIndiceS.RemoveAt(i);
                                                break;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// funcion para ver si existe la direccion de memoria en la lista.
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool ExistedirREG(List<Ordenadas> lista, long dir, out int indice)
        {
            bool existe = false;
            indice = 0;
            foreach (Ordenadas a in lista)
            {
                indice += 1;
                if (a.dir == dir)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        public bool ExistedirREGotraLista(List<Ordenadas> lista, long dir,int indice,out int index)
        {
            bool existe = false;
            index = -1;
            for (int i = 0; i < lista.Count; i++)            
            {
                if (i != indice)
                {
                    foreach (Ordenadas b in lista[i].lista)
                    {
                        if (b.dir == dir)
                        {
                            index = i;
                            existe = true;
                            break;
                        }
                    }
                }
            }
            return existe;
        }
        public int NumeroDeListadeCVE(object cve)//checar funcion.
        {
            int aux=-1;
            string auxC="";
            int dato = -1;
            int indice = -1;
            if (tipodato == 'E')
            {
                aux = Convert.ToInt32(cve);
                dato = 1;
            }
            else
            {
                auxC = Convert.ToString(cve);
                dato = 2;
            }
            for (int i = 0; i < datosIndiceS.Count; i++)
            {
                if(dato == 1)
                {
                    if(datosIndiceS[i].entero == aux)
                    {
                        indice = i;
                        break;
                    }
                }
                else
                {
                    if (dato == 2)
                    {
                        if (datosIndiceS[i].cadena == auxC)
                        {
                            indice = i;
                            break;
                        }
                    }
                }
            }

            return indice;
        }
        public void EliminarDireccion(long dir)
        {
            for (int i = 0; i < datosIndiceS.Count; i++)
            {                 
                for (int j = 0; j < datosIndiceS[i].lista.Count; j++)
                {
                    if(dir == datosIndiceS[i].lista[j].dir)
                    {
                        datosIndiceS[i].lista.RemoveAt(j--);
                    }
                }
            }
           
        }
        public void AgregarRegistroEnSecundario(Entidad entidad, Atributo atributo,int ite,Registro registro)
        {
            if(atributo.tipodato == 'E')
            {
                if(ExisteCveEnSecundarioE(Convert.ToInt32(registro.registros[ite]))==true)
                {
                    //buscamos la direccion del bloque
                    AgregarBloque(Convert.ToInt32(registro.registros[ite]), registro.dirReG);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].lista,
                    atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].dir);
                }
                else
                {
                    //si no existe el bloque se crea uno nuevo y se agrega al bloque principal.
                    long DireccionBloque = Archivo.CrearBloque(entidad.Atributos[ite].rutaIndice, entidad.Atributos[ite].archivoIndice);
                    AgregarBloquePrincipalE(Convert.ToInt32(registro.registros[ite]), DireccionBloque, registro.dirReG);
                    Archivo.EscribirIndiceSecundarioBloquePrincipal(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS,
                    atributo.tipodato, atributo.longitud, registro.registros[ite], registro.dirReG, DireccionBloque);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].lista,
                            atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].dir);
                }

                atributo.dirIndice = 0;/*atributo.datosIndiceS[0].dir;*/
            }
            else
            {
                if (ExisteCveEnSecundarioC(Convert.ToString(registro.registros[ite])) == true)
                {
                    //buscamos la direccion del bloque
                    AgregarBloque(Convert.ToString(registro.registros[ite]), registro.dirReG);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS[entidad.Atributos[ite].NumeroDeListadeCVE(registro.registros[ite])].lista,
                    atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].dir);//excepcion.
                }
                else
                {
                    //si no existe el bloque se crea uno nuevo y se agrega al bloque principal.
                    long DireccionBloque = Archivo.CrearBloque(entidad.Atributos[ite].rutaIndice, entidad.Atributos[ite].archivoIndice);
                    AgregarBloquePrincipalC(Convert.ToString(registro.registros[ite]), DireccionBloque, registro.dirReG);
                    Archivo.EscribirIndiceSecundarioBloquePrincipal(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS,
                    atributo.tipodato, atributo.longitud, registro.registros[ite], registro.dirReG, DireccionBloque);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].lista,
                    atributo.datosIndiceS[atributo.NumeroDeListadeCVE(registro.registros[ite])].dir);
                }
                atributo.dirIndice = 0;/*atributo.datosIndiceS[0].dir;*/
            }
        }
        /// <summary>
        /// Funcion para borrar un dato en el archivo secundario.
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="atributo"></param>
        /// <param name="ite"></param>
        /// <param name="registro"></param>
        public void BorrarRegistroSecundario(Entidad entidad, Atributo atributo, int ite, Registro registro)
        {
            int indice, indicE;
            int i;
            if (atributo.tipodato == 'E')
            {
                int cve = Convert.ToInt32(registro.registros[ite]);
                if (ExisteDatoEntero(atributo.datosIndiceS, cve, out indice) == true)
                {
                    if (ExistedirREG(atributo.datosIndiceS[indice - 1].lista, registro.dirReG,out indicE) == true)
                    {
                        for (i = 0; i < atributo.datosIndiceS.Count ; i++)
                        {
                            if (atributo.datosIndiceS[i].entero == cve)
                            {
                                atributo.datosIndiceS[i].lista.RemoveAt(indicE-1);
                                //atributo.datosIndiceS[i].lista.Remove(new Ordenadas(registro.dirReG));
                                if(atributo.datosIndiceS[i].lista.Count == 0)
                                {
                                    for (int j = 0; j < atributo.datosIndiceS.Count; j++)
                                    {
                                        if (atributo.datosIndiceS[i].entero == cve)
                                        {
                                            //para borrar antes en el bloque principal.
                                            //Archivo.BorrarEnArchivo(atributo.rutaIndice, atributo.archivoIndice,atributo,cve,"",'E');
                                            atributo.datosIndiceS.RemoveAt(i--);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    atributo.datosIndiceS[i].lista = atributo.datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                                    break;
                                }
                            }                                
                        }
                    }
                    else
                    {
                        if (ExistedirREG(datosIndiceS[indice - 1].lista, registro.dirReG,out indicE) == false)
                        {
                            for (i = 0; i < atributo.datosIndiceS.Count; i++)
                            {
                                if (atributo.datosIndiceS[i].entero == cve)
                                {
                                    // datosIndiceS[i].lista.Add(new Ordenadas(registro.dirReG));
                                    //datosIndiceS[i].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                                    //atributo.datosIndiceS[i].lista.Remove(new Ordenadas(registro.dirReG));
                                    int index;
                                    if (ExistedirREGotraLista(datosIndiceS, registro.dirReG, i, out index))
                                    {
                                        for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                                        {
                                            if (datosIndiceS[index].lista[j].dir == registro.dirReG)
                                            {
                                                datosIndiceS[index].lista.RemoveAt(j);
                                                datosIndiceS[index].lista = datosIndiceS[index].lista.OrderBy(o => o.dir).ToList();
                                                if (datosIndiceS[index].lista.Count == 0)
                                                {
                                                    datosIndiceS.RemoveAt(index);
                                                    break;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {//esperarse........
                    for (int j = 0; j < atributo.datosIndiceS.Count; j++)
                    {
                        if (ExistedirREG(atributo.datosIndiceS[j].lista, registro.dirReG,out int n) == true)
                        {
                            for (int k = 0; k <atributo.datosIndiceS[j].lista.Count; k++)
                            {
                                if(atributo.datosIndiceS[j].lista[k].dir == registro.dirReG)
                                {
                                    atributo.datosIndiceS[j].lista.RemoveAt(k--);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string cve = Convert.ToString(registro.registros[ite]);
                if (ExisteDatoCadena(atributo.datosIndiceS, cve, out indice) == true)
                {
                    if (ExistedirREG(atributo.datosIndiceS[indice - 1].lista, registro.dirReG,out indicE) == true)
                    {
                        for (i = 0; i < atributo.datosIndiceS.Count; i++)
                        {
                            if (atributo.datosIndiceS[i].cadena == cve)
                            {
                                atributo.datosIndiceS[i].lista.RemoveAt(indicE);
                                //atributo.datosIndiceS[i].lista.Remove(new Ordenadas(registro.dirReG));
                                if (atributo.datosIndiceS[indice - 1].lista.Count == 0)
                                {
                                    for (int j = 0; j < atributo.datosIndiceS.Count; j++)
                                    {
                                        if (atributo.datosIndiceS[i].cadena == cve)
                                        {
                                            //Archivo.BorrarEnArchivo(atributo.rutaIndice, atributo.archivoIndice,atributo,0, cve, 'C');
                                            atributo.datosIndiceS.RemoveAt(i--);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    atributo.datosIndiceS[i].lista = atributo.datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ExistedirREG(datosIndiceS[indice - 1].lista, registro.dirReG,out indicE) == false)
                        {
                            for (i = 0; i < datosIndiceS.Count; i++)
                            {
                                if (datosIndiceS[i].cadena == cve)
                                {
                                    /*datosIndiceS[i].lista.Add(new Ordenadas(registro.dirReG));
                                    datosIndiceS[i].lista = datosIndiceS[i].lista.OrderBy(o => o.dir).ToList();*/
                                    //atributo.datosIndiceS[i].lista.Remove(new Ordenadas(registro.dirReG));
                                    int index;
                                    if (ExistedirREGotraLista(datosIndiceS, registro.dirReG, i, out index))
                                    {
                                        for (int j = 0; j < datosIndiceS[index].lista.Count; j++)
                                        {
                                            if (datosIndiceS[index].lista[j].dir == registro.dirReG)
                                            {
                                                datosIndiceS[index].lista.RemoveAt(j);
                                                datosIndiceS[index].lista = datosIndiceS[index].lista.OrderBy(o => o.dir).ToList();
                                                if (datosIndiceS[index].lista.Count == 0)
                                                {
                                                    datosIndiceS.RemoveAt(index);
                                                    break;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {//esperarse........
                    for (int j = 0; j < atributo.datosIndiceS.Count; j++)
                    {
                        if (ExistedirREG(atributo.datosIndiceS[j].lista, registro.dirReG,out int n) == true)
                        {
                            for (int k = 0; k < atributo.datosIndiceS[j].lista.Count; k++)
                            {
                                if (atributo.datosIndiceS[j].lista[k].dir == registro.dirReG)
                                {
                                    atributo.datosIndiceS[j].lista.RemoveAt(k--);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        #endregion

        #region Arbol Primario

        #region Insertar
        /// <summary>
        /// Se busca la hoja en la cual vamos a insertar <paramref name="cve"/>.
        /// </summary>
        /// <param name="cve">Parametro para buscar la hoja</param>
        /// <returns>Retorna la direccion de la hoja en donde se puede insertar</returns>
        public long BuscarHoja(List<Hoja> Arbol,int cve, bool recursivo ,Hoja hoja)
        {
            long DirNodoHoja =-1 ;
            if(recursivo == false)
            {
                //buscamos una hoja raiz si es que tiene si no buscamos la hoja solamente.
                if (Arbol.Count > 0)
                {
                    foreach (Hoja a in Arbol)
                    {
                        if (a.tipoHoja == 'R')
                        {
                            hoja = a;
                            break;
                        }
                        else
                        {
                            if (a.tipoHoja == 'H')
                            {
                                hoja = a;
                            }
                        }
                    }
                }
            }
            if(hoja.dirnodo != -1)
            {
                //checamos la cve entrante con los elementos que hay en el nodo hoja que esta.
                if(hoja.tipoHoja == 'R' || hoja.tipoHoja == 'I')
                {
                    if (hoja.ListPointKey.Count > 0)
                    {
                        for (int i = 0; i < hoja.ListPointKey.Count; i++)
                        {
                            if (i == 0)
                            {
                                if (cve < hoja.ListPointKey[i].entero)
                                {
                                    DirNodoHoja = hoja.ListPointKey[i].dirIZQ;
                                    Hoja aux = BuscarHojaDIR(DirNodoHoja, Arbol);
                                    if (aux.tipoHoja == 'I')
                                    {
                                        recursivo = true;
                                        DirNodoHoja = BuscarHoja(Arbol, cve, recursivo, aux);
                                        //break;
                                    }
                                    else
                                    {
                                        if (aux.tipoHoja == 'H')
                                        {
                                            DirNodoHoja = aux.dirnodo;
                                            //break;
                                        }
                                    }
                                }
                                else
                                {
                                    //DirNodoHoja = hoja.ListPointKey[i].dirDER;
                                    if (cve >= hoja.ListPointKey[i].entero)
                                    {
                                        DirNodoHoja = hoja.ListPointKey[i].dirDER;
                                        Hoja aux = BuscarHojaDIR(DirNodoHoja, Arbol);
                                        if (aux.tipoHoja == 'I')
                                        {
                                            recursivo = true;
                                            DirNodoHoja = BuscarHoja(Arbol, cve, recursivo, aux);
                                            //break;
                                        }
                                        else
                                        {
                                            if (aux.tipoHoja == 'H')
                                            {
                                                DirNodoHoja = aux.dirnodo;
                                                //break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (cve >= hoja.ListPointKey[i].entero && cve < hoja.ListPointKey[i + 1].entero)
                                    {
                                        DirNodoHoja = hoja.ListPointKey[i].dirDER;
                                        Hoja aux = BuscarHojaDIR(DirNodoHoja, Arbol);
                                        if (aux.tipoHoja == 'I')
                                        {
                                            recursivo = true;
                                            DirNodoHoja = BuscarHoja(Arbol, cve, recursivo, aux);
                                            //break;
                                        }
                                        else
                                        {
                                            if (aux.tipoHoja == 'H')
                                            {
                                                DirNodoHoja = aux.dirnodo;
                                                //break;
                                            }
                                        }
                                    }
                                }
                                catch (System.ArgumentOutOfRangeException) 
                                {
                                    if (cve >= hoja.ListPointKey[i].entero)
                                    {
                                        DirNodoHoja = hoja.ListPointKey[i].dirDER;
                                        Hoja aux = BuscarHojaDIR(DirNodoHoja, Arbol);
                                        if (aux.tipoHoja == 'I')
                                        {
                                            recursivo = true;
                                            DirNodoHoja = BuscarHoja(Arbol, cve, recursivo, aux);
                                            //break;
                                        }
                                        else
                                        {
                                            if (aux.tipoHoja == 'H')
                                            {
                                                DirNodoHoja = aux.dirnodo;
                                                //break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        DirNodoHoja = hoja.dirnodo;
                    }
                }
                else
                {
                    if(hoja.tipoHoja=='H')
                    {
                        DirNodoHoja = hoja.dirnodo;
                    }
                }
            }
            return DirNodoHoja;
        }
        public Hoja BuscarHojaDIR(long direccion,List<Hoja> Arbol)
        {
            Hoja hoja = new Hoja();
            foreach(Hoja a in Arbol)
            {
                if(a.dirnodo == direccion)
                {
                    hoja = a;
                    break;
                }
            }
            return hoja;
        }               
        public Hoja RegresaHoja(long DirHoja)
        {
            Hoja hoja = new Hoja();

            foreach (Hoja hoja1 in Arbol)
            {
                if(hoja1.dirnodo == DirHoja)
                {
                    hoja = hoja1;
                    break;
                }
            }
           
            return hoja;
        }
        /// <summary>
        /// Funcion del Arbol que inserta los elementos de un registro en el archivo.
        /// </summary>
        /// <param name="Clave">Clave que se va a guardar en el Arbol</param>
        /// <param name="Direccion">Direccion del registro</param>
        /// <param name="atributo">Objeto de tipo Atributo</param>
        public void Inserta(int Clave,long Direccion,Atributo atributo,string nombreArchivo,FileStream archivo)
        {//chercar si ya existe la clave en el arbol.
            Hoja hoja;
            int AP;
            if (Arbol.Count == 0)//si el arbol esta vacio.
            {
                hoja = new Hoja();
                hoja = CreaHojatipoH(hoja, atributo);
                Ordenadas PK = new Ordenadas(Clave, Direccion);
                hoja.ListPointKey.Add(PK);
                Arbol.Add(hoja);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
            }
            else
            {
                hoja = new Hoja();
                bool recursivo = false;
                long DirHoja = BuscarHoja(Arbol, Clave, recursivo, new Hoja());//Busca la direccion de la hoja
                hoja = RegresaHoja(DirHoja);//devuelve la hoja que estamos buscando.   
            }
            if ((AP = hoja.NoApuntadores()) < 4)
            {
                Inserta_En_Hoja(hoja,Clave,Direccion);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
            }
            else
            {
                //Ya tenemos la hoja que se esta verificando para ver si se puede insertar.
                Hoja nueva = new Hoja();
                nueva = CreaHojatipoH(nueva,atributo);//Creamos la hoja nueva en el archivo tambien.
                Hoja auxiliar = new Hoja();
                auxiliar.ListPointKey = hoja.ListPointKey;
                Inserta_En_Hoja(auxiliar, Clave, Direccion);
                hoja.dirsignodo = nueva.dirnodo;//asiganamos apuntadores
                hoja.ListPointKey = new List<Ordenadas>();//borramos la lista con todo.

                for (int i = 0; i < auxiliar.ListPointKey.Count; i++)//Copiar o asingar los valores del auxiliar a los nodos que estan ya en el archivo.
                {
                    if(i<2)
                    {
                        hoja.ListPointKey.Add(auxiliar.ListPointKey[i]);//copiamos 4+1/2 en este nodo.
                    }
                    else
                    {
                        nueva.ListPointKey.Add(auxiliar.ListPointKey[i]);//Copiamos del 2 al 5.
                    }
                }
                Arbol.Add(nueva);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, nueva);
                int valor_menor = nueva.ListPointKey[0].entero;
                Inserta_En_Raiz(hoja, valor_menor, nueva,atributo, nombreArchivo,archivo);
            }
            
        }       
        /// <summary>
        /// Inserta el valor directamente en la hoja sin checar si es menor o mayor, solo se inserta
        /// </summary>
        /// <param name="direccionHoja">Direccion en donde se encuentra la hoja</param>
        /// <param name="PK">Ordenada de un clave y un apuntador.</param>
        public void InsertaEnHoja(long direccionHoja ,Ordenadas PK)
        {
            for (int i = 0; i < Arbol.Count; i++)
            {
                if(Arbol[i].tipoHoja == 'H' && Arbol[i].dirnodo == direccionHoja)
                {
                    Arbol[i].ListPointKey.Add(PK);
                    Arbol[i].ListPointKey.OrderBy(o => o.entero).ToList();
                    break;
                }
            }
        }
        public void Inserta_En_Hoja(Hoja L, int cve, long direccion)
        {
            for (int i = 0; i < L.ListPointKey.Count; i++)
            { 
                if(cve < L.ListPointKey[i].entero)
                {
                    if (i == 0)
                    { L.ListPointKey.Insert(i, new Ordenadas(L.ListPointKey[i].dirIZQ, cve, direccion)); }
                    else
                    {
                        L.ListPointKey.Insert(i, new Ordenadas(-1,cve, direccion));
                    }
                    break;
                }else
                {
                    if(cve > L.ListPointKey[i].entero)
                    {
                        L.ListPointKey.Add(new Ordenadas(-1,cve, direccion));
                        break;
                    }
                    //i--;
                }
            }
            L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
        }
        public void Inserta_En_Raiz(Hoja N,int Clave,Hoja N1,Atributo atributo, string nombreArchivo, FileStream archivo)
        {
            Hoja Raiz;
            if (N.tipoHoja =='R')//Pregunta que si n es la raiz pero no con la letra si no , que si es el comienzo del arbol
            {
                if (ExisteRaiz() == true)
                {
                    Raiz = new Hoja();
                    N.tipoHoja = 'I';
                    Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice, N);
                    Raiz = CreaHojatipoR(Raiz, atributo);//creamos la raiz.
                    Raiz.ListPointKey.Add(new Ordenadas(N.dirnodo, Clave, N1.dirnodo));
                    //Raiz.ListPointKey.Add(new Ordenadas(Clave, N.dirnodo));
                    //Raiz.ListPointKey.Add(new Ordenadas(N1.dirnodo));
                    Arbol.Add(Raiz);
                    Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice, Raiz); 
                }
            }
            else
            {
                if(N.dirnodo == 0 && N.dirsignodo != -1)
                {
                    if (ExisteRaiz() == false)
                    {
                        Raiz = new Hoja();
                        Raiz = CreaHojatipoR(Raiz, atributo);//creamos la raiz.
                        Raiz.ListPointKey.Add(new Ordenadas(N.dirnodo, Clave, N1.dirnodo));
                        //Raiz.ListPointKey.Add(new Ordenadas(Clave, N.dirnodo));
                        //Raiz.ListPointKey.Add(new Ordenadas(-1,N1.dirnodo));
                        Arbol.Add(Raiz);
                        Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice, Raiz);
                    }
                }
            }
            Hoja P = new Hoja();
            P = Regresa_Raiz(N);//En ves de esta funcion hacer una que se llame buscar padre.
            int AP = P.NoApuntadores();
            atributo.dirIndice = P.dirnodo;
            Archivo.ModificaAtributo(atributo, nombreArchivo, archivo);
            if (AP < 5)
            {
                Inserta_En_Hoja(P, Clave, N1.dirnodo);
                Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice, P);
            }
            else
            {
                Hoja T = new Hoja();
                T.ListPointKey = P.ListPointKey;
                Inserta_En_Hoja(T, Clave, N1.dirnodo);
                P.ListPointKey = new List<Ordenadas>();
                T.ListPointKey = T.ListPointKey.OrderBy(o => o.entero).ToList();
                Hoja P1 = new Hoja();
                P1 = CreaHojatipoI(P1, atributo);
                int K2 =-1;
                Ordenadas k2;
                for (int i = 0; i < T.ListPointKey.Count; i++)//Copiar o asingar los valores del auxiliar a los nodos que estan ya en el archivo.
                {
                    if (i < 2)
                    {
                        P.ListPointKey.Add(T.ListPointKey[i]);//copiamos 4+1/2 en este nodo.
                    }
                    else
                    {
                        if(i==2)
                        {
                            //P.tipoHoja = 'I';
                            K2 = T.ListPointKey[i].entero;
                        }
                        else
                        {
                            if (i == 3)
                            {
                                k2 = new Ordenadas(T.ListPointKey[i - 1].dirDER, T.ListPointKey[i].entero, T.ListPointKey[i].dirDER);
                                T.ListPointKey[i].dirIZQ = k2.dirIZQ;
                                P1.ListPointKey.Add(T.ListPointKey[i]);//Copiamos del 2 al 5.
                            }
                            else { P1.ListPointKey.Add(T.ListPointKey[i]); }//Copiamos del 2 al 5.
                        }
                    }
                }
                Arbol.Add(P1);
                Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice, P);
                Archivo.EscribirEnRaiz(atributo.rutaIndice, atributo.archivoIndice,P1);
                Inserta_En_Raiz(P, K2, P1,atributo,nombreArchivo ,archivo);
                List<Hoja> Intermedios = RegresaIntermedios();
                if (Intermedios.Count != 2)
                { UNA = false; }
                if (ExistenIntermedios() == true && UNA == false)
                {
                    Hoja R = Regresa_Raiz();
                    Intermedios = RegresaIntermedios();
                    for (int i = 0; i < Intermedios.Count;)
                    {
                        if(Intermedios.Count == 2)
                        {
                            Hoja hoja = RegresaHoja(Intermedios[i].ListPointKey[Intermedios[i].ListPointKey.Count-1].dirDER);
                            i++;
                            Hoja hoja1 = RegresaHoja(Intermedios[i].ListPointKey[0].dirIZQ);
                            i++;
                            hoja.dirsignodo = hoja1.dirnodo;
                            foreach (Hoja h in Arbol)
                            {
                                if(h.dirnodo == hoja.dirnodo)
                                {
                                    h.dirsignodo = hoja.dirsignodo;
                                    break;
                                }
                            }
                            Archivo.ActualizarArbolSECU(atributo.rutaIndice, atributo.archivoIndice,Arbol);
                            UNA = true;
                        }else
                        {
                            if(i==0)
                            {
                                Hoja hoja = RegresaHoja(Intermedios[i].ListPointKey[Intermedios[i].ListPointKey.Count - 1].dirDER);
                                i++;
                                Hoja hoja1 = RegresaHoja(Intermedios[i].ListPointKey[0].dirIZQ);
                                i++;
                                hoja.dirsignodo = hoja1.dirnodo;
                                foreach (Hoja h in Arbol)
                                {
                                    if (h.dirnodo == hoja.dirnodo)
                                    {
                                        h.dirsignodo = hoja.dirsignodo;
                                    }
                                }
                                Archivo.ActualizarArbolSECU(atributo.rutaIndice, atributo.archivoIndice,Arbol);
                                UNA = true;
                            }
                            else
                            {
                                i--;
                                Hoja hoja = RegresaHoja(Intermedios[i].ListPointKey[Intermedios[i].ListPointKey.Count-1].dirDER);
                                i++;
                                Hoja hoja1 = RegresaHoja(Intermedios[i].ListPointKey[0].dirIZQ);
                                hoja.dirsignodo = hoja1.dirnodo;
                                foreach (Hoja h in Arbol)
                                {
                                    if (h.dirnodo == hoja.dirnodo)
                                    {
                                        h.dirsignodo = hoja.dirsignodo;
                                    }
                                }
                                Archivo.ActualizarArbolSECU(atributo.rutaIndice, atributo.archivoIndice, Arbol);
                                UNA = true;
                            }
                        }

                    }
                }
            }
        }
        public Hoja CreaHojatipoH(Hoja hoja,Atributo atributo)
        {
            long DirNodoHoja = Archivo.CreacionHojas(atributo.rutaIndice, atributo.archivoIndice);
            hoja.tipoHoja = 'H';
            hoja.dirnodo = DirNodoHoja;
            hoja.ListPointKey = new List<Ordenadas>();
            hoja.dirsignodo = -1;
            return hoja;
        }
        public Hoja CreaHojatipoR(Hoja hoja, Atributo atributo)
        {
            long DirNodoHoja = Archivo.CreacionHojas(atributo.rutaIndice, atributo.archivoIndice);
            hoja.tipoHoja = 'R';
            hoja.dirnodo = DirNodoHoja;
            hoja.ListPointKey = new List<Ordenadas>();
            return hoja;
        }
        public Hoja CreaHojatipoI(Hoja hoja, Atributo atributo)
        {
            long DirNodoHoja = Archivo.CreacionHojas(atributo.rutaIndice, atributo.archivoIndice);
            hoja.tipoHoja = 'I';
            hoja.dirnodo = DirNodoHoja;
            hoja.ListPointKey = new List<Ordenadas>();
            hoja.dirsignodo = -1;
            //Ordenadas PK = new Ordenadas(Clave, Direccion);
            //hoja.ListPointKey.Add(PK);
            //Arbol.Add(hoja);
            //Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
            return hoja;
        }
        public Hoja Regresa_Raiz(Hoja N)
        {
            Hoja hoja = new Hoja();
            bool encontrada = false;
            foreach (Hoja hoja1 in Arbol)
            {
                if (hoja1.tipoHoja == 'R' || hoja1.tipoHoja == 'I')
                {
                    for (int i = 0; i < hoja1.ListPointKey.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (hoja1.ListPointKey[i].dirIZQ == N.dirnodo || hoja1.ListPointKey[i].dirDER == N.dirnodo)
                            {
                                hoja = hoja1;
                                encontrada = true;
                                break;
                            }
                        }
                        else
                        {
                            if (hoja1.ListPointKey[i].dirDER == N.dirnodo)
                            {
                                hoja = hoja1;
                                encontrada = true;
                                break;
                            }
                        }
                    }
                }
                if (encontrada == true)
                { break; }
            }
            return hoja;
        }
        public bool ExisteRaiz()
        {
            bool existe = false;
            foreach (Hoja item in Arbol)
            {
                if(item.tipoHoja =='R')
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        public Hoja Regresa_Raiz()
        {
            Hoja hoja = new Hoja();
            bool encontrada = false;
            foreach (Hoja hoja1 in Arbol)
            {
               if(hoja1.tipoHoja == 'R')
                {
                    hoja = hoja1;
                    encontrada = true;
                }
                if (encontrada == true)
                { break; }
            }
            return hoja;
        }
        public List<Hoja> RegresaIntermedios()
        {
            List<Hoja> hojas = new List<Hoja>();            
            foreach (Hoja hoja1 in Arbol)
            {
                if (hoja1.tipoHoja == 'I')
                {
                    hojas.Add(hoja1);
                    
                }
            }
            return hojas;
        }
        #endregion

        #region Eliminar
        /// <summary>
        /// Funcion que busca el espacio en donde se va a insertar el dato.
        /// </summary>
        /// <returns></returns>
        public Hoja BuscarEspacio(int cve)
        {
            Hoja hoja1 = new Hoja();

            foreach (Hoja hoja in Arbol)
            {
                if(Arbol.Count > 3)//caso en donde ya sabemos que tenemos mas de una hoja en el arbol , intermedios y raiz.
                {

                }
                else
                {
                    if(Arbol.Count == 3)//Caso donde sabemos que tenemos nada mas raiz y dos hojas.
                    {

                    }
                    else
                    {
                        if(Arbol.Count == 1)//Caso donde tenemos nada mas un hoja.
                        {//aqui debemos de checar si hay espacio o no para poder insertar la clave.
                            hoja1 = hoja;
                        }
                    }
                }

            }


            return hoja1;
        }
        public bool ExistenIntermedios()
        {
            bool existe = false;
            foreach (Hoja hoja in Arbol)
            {
                if(hoja.tipoHoja == 'I')
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        public bool Hojallena()
        {
            bool llena = false;

            return llena;
        }
        public void Borrar(int valor , long direccion,List<Hoja> Arbol,Atributo atributo)
        {
            Hoja hoja = new Hoja();
            bool recursivo = false;
            long dir = BuscarHoja(Arbol, valor, recursivo, new Hoja());
            hoja = BuscarHojaDIR(dir, Arbol);
            if(ExisteClaveyDirEnHoja(hoja,valor,direccion) == true)
            {
                Borrar_Entrada(hoja, valor, direccion,Arbol,atributo);
            }

        }
        public void Borrar_Entrada(Hoja N, int valor, long direccion, List<Hoja> Arbol, Atributo atributo)
        {
            if(N.NoApuntadores() <= 2 && Arbol.Count == 1)
            {
                Borrar_En_Hoja(N, valor, direccion);
            }
            else
            { 
                if (N.NoApuntadores() > 2)//Borramos directo en la hoja.Hay que checar si los apuntadores son menores a dos y solo hay una hoja , que borre directo.
                {
                    Borrar_En_Hoja(N, valor, direccion);
                }
                else
            {
                Ordenadas K1 = new Ordenadas();
                if (N.NoApuntadores() <= 2)
                {
                    Hoja N1 = new Hoja();
                    Hoja P = new Hoja();
                    P = Regresa_Raiz(N);
                    Borrar_En_Hoja(N, valor, direccion);
                    //bool bandDER = HermanoDerecha(N, P, out N1);
                    bool bandPrestamo = false;

                    if (HermanoDerecha(N, P, out N1) == true)//condicional para saber si existe el hermano de la derecha.
                    {
                        K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                        if (N1.ListPointKey.Count > 2)//Prestamo de hermano derecha
                        {
                            //N.ListPointKey.Add(N1.ListPointKey[0]);
                            Inserta_En_Hoja(N, N1.ListPointKey[0].entero, N1.ListPointKey[0].dirDER);
                            N1.ListPointKey.RemoveAt(0);
                            bandPrestamo = true;
                            Actualiza_Raiz(P, N1);
                        }
                    }
                    if (HermanoIzquierda(N, P, out N1) == true && bandPrestamo == false)//condicional para saber si existe el hermano de la izquierda.
                    {
                        K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                        if (N1.ListPointKey.Count > 2)//Prestamo de hermano derecha
                        {
                            //N.ListPointKey.Add(N1.ListPointKey.Last());
                            Inserta_En_Hoja(N, N1.ListPointKey.Last().entero, N1.ListPointKey.Last().dirDER);
                            N1.ListPointKey.RemoveAt(N1.ListPointKey.Count-1);
                            bandPrestamo = true;
                            Actualiza_Raiz(P, N);
                        }
                        //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                        //apuntadores se tiene que hacer una restructuracion.
                    }
                    if(bandPrestamo ==false)
                    {
                        bool bandUnion = false;

                        if (HermanoDerecha(N, P, out N1) == true)//condicional para saber si existe el hermano de la derecha.
                        {
                            //K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                            if (N1.ListPointKey.Count <4)//union de hermano derecha con hermano actual.
                            {
                                foreach (Ordenadas ordenadas in N1.ListPointKey)
                                {
                                    //N.ListPointKey.Add(ordenadas);
                                    Inserta_En_Hoja(N,ordenadas.entero, ordenadas.dirDER);
                                    //Inserta_En_Hoja()
                                }
                                Borrar_En_Hoja(P, N1.ListPointKey[0].entero, N1.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                N.dirsignodo = N1.dirsignodo;
                                Borrar_Hoja(Arbol, N1.dirnodo);
                                bandUnion = true;
                                Actualiza_Raiz(P,N);//Borrar la direccion y cve del nodo que se elimino.
                            }

                        }
                        if (HermanoIzquierda(N, P, out N1) == true && bandUnion == false)//condicional para saber si existe el hermano de la izquierda.
                        {
                            K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                            if (N1.ListPointKey.Count < 4)//Prestamo de hermano derecha
                            {
                                foreach (Ordenadas ordenadas in N.ListPointKey)
                                {
                                    Inserta_En_Hoja(N1,ordenadas.entero, ordenadas.dirDER);
                                }
                                Borrar_En_Hoja(P, N.ListPointKey[0].entero, N.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                N1.dirsignodo = N.dirsignodo;
                                Borrar_Hoja(Arbol, N.dirnodo);
                                bandUnion = true;
                                Actualiza_Raiz(P, N1);//Borrar la direccion y cve del nodo que se elimino.
                                //Borrar_En_Hoja(P, N.ListPointKey[0].entero, N.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                            }
                            
                            //P.NoApuntadores();
                                //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                                //apuntadores se tiene que hacer una restructuracion.
                        }
                        int i = P.NoApuntadores();

                        if (P.NoApuntadores()<=2 && P.tipoHoja =='I')
                        {
                            bandPrestamo = false;
                            //intermedios con menos de dos apuntadores.
                            //si si hay hacer rotacion.
                            Hoja Inter = P;
                            P = Regresa_Raiz(P);
                            Hoja hermano = new Hoja();
                            if(HermanoDerecha(Inter,P,out hermano) == true)
                            {
                                //K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                if (hermano.NoApuntadores() >= 4)//Prestamo de hermano derecha
                                {
                                    //Inter.ListPointKey.Add(hermano.ListPointKey[0]);
                                    Inserta_En_Hoja(Inter, hermano.ListPointKey[0].entero, hermano.ListPointKey[0].dirDER);
                                    hermano.ListPointKey.RemoveAt(0);
                                    bandPrestamo = true;
                                    Actualiza_Raiz(P, hermano);
                                }
                            }
                            if (HermanoIzquierda(Inter, P, out hermano) == true && bandPrestamo == false)//condicional para saber si existe el hermano de la izquierda.
                            {
                               //K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                //si el hermano tiene al menos 3 elementos se puede hacer el prestamo que seria la rotacion de los elementos.
                                if (hermano.NoApuntadores() >= 4)//Prestamo de hermano izquierda
                                {
                                    //Inter.ListPointKey.Add(hermano.ListPointKey.Last());
                                    Inserta_En_Hoja(Inter, hermano.ListPointKey.Last().entero, hermano.ListPointKey.Last().dirDER);
                                    hermano.ListPointKey.RemoveAt(hermano.ListPointKey.Count - 1);
                                    bandPrestamo = true;
                                    Actualiza_Raiz(P, Inter);
                                }
                                //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                                //apuntadores se tiene que hacer una restructuracion.
                            }
                            if (bandPrestamo == false)
                            {
                                bandUnion = false;

                                if (HermanoDerecha(Inter, P, out hermano) == true)//condicional para saber si existe el hermano de la derecha.
                                {
                                    //K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                    if (hermano.ListPointKey.Count <= 2)//union de hermano derecha
                                    {
                                        Ordenadas ordenadas2 = new Ordenadas(P.ListPointKey[0].entero, hermano.ListPointKey[0].dirIZQ);
                                        Inserta_En_Hoja(Inter, ordenadas2.entero, ordenadas2.dirDER);
                                        foreach (Ordenadas ordenadas in hermano.ListPointKey)
                                        {
                                            Inserta_En_Hoja(Inter, ordenadas.entero, ordenadas.dirDER);
                                        }
                                        /*Ordenadas ordenadas1 = new Ordenadas(P.ListPointKey[0].entero, hermano.ListPointKey[0].dirIZQ);
                                        Inserta_En_Hoja(Inter, ordenadas1.entero, ordenadas1.dirDER);*/
                                        Inter.dirsignodo = hermano.dirsignodo;
                                        Borrar_Hoja(Arbol, hermano.dirnodo);
                                        bandUnion = true;
                                        //Actualiza_Raiz(P, Inter);//Borrar la direccion y cve del nodo que se elimino.

                                        if (ExistenIntermedios(Arbol) < 2)
                                        {
                                            if (ExistenIntermedios(Arbol, hermano.dirnodo) == false)
                                            {
                                                Borrar_Hoja(Arbol, P.dirnodo);
                                                Inter.tipoHoja = 'R';
                                            }
                                        }
                                    }

                                }
                                if (HermanoIzquierda(Inter, P, out hermano) == true && bandUnion == false)//condicional para saber si existe el hermano de la izquierda.
                                {
                                    K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                    if (hermano.ListPointKey.Count <= 2)//union de hermano izquierda
                                    {
                                        //se crea un elemento con el apuntador y el valor unico de la raiz.
                                        Ordenadas ordenadas1 = new Ordenadas(P.ListPointKey[0].entero, Inter.ListPointKey[0].dirIZQ);
                                        Inserta_En_Hoja(hermano, ordenadas1.entero, ordenadas1.dirDER);
                                        foreach (Ordenadas ordenadas in Inter.ListPointKey)
                                        {
                                            Inserta_En_Hoja(hermano, ordenadas.entero, ordenadas.dirDER);
                                        }
                                        //int i = 0;
                                        //hermano.ListPointKey[hermano.ListPointKey.Count - 1].dirDER = 65;
                                        hermano.dirsignodo = Inter.dirsignodo;
                                        Borrar_Hoja(Arbol, Inter.dirnodo);
                                        bandUnion = true;
                                        //Actualiza_Raiz(P, hermano);//Borrar la direccion y cve del nodo que se elimino.

                                        if (ExistenIntermedios(Arbol) <2)
                                        {
                                           if(ExistenIntermedios(Arbol,Inter.dirnodo) == false)
                                            {
                                                //copiar en una ordenada el elemento de la raiz;
                                                Borrar_Hoja(Arbol, P.dirnodo);
                                                hermano.tipoHoja = 'R';
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            }
        }
        public void Borrar_En_Hoja(Hoja L, int cve, long direccion)
        {
            if (L.tipoHoja == 'H')
            {
                for (int i = 0; i < L.ListPointKey.Count; i++)
                {
                    if (L.ListPointKey[i].entero == cve && L.ListPointKey[i].dirDER == direccion)
                    {
                        L.ListPointKey.RemoveAt(i--);
                        break;
                    }
                }
                L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
            }
            else
            {
                if (L.tipoHoja == 'I')
                {
                    for (int i = 0; i < L.ListPointKey.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (L.ListPointKey[i].dirDER == direccion)//En el primer elemento no se elimina la cve y el apuntador ya que contiene dos apuntadores y solo se queda el hermano izquierda.
                            {
                                //L.ListPointKey.RemoveAt(i--);
                                //L.ListPointKey[i].dirDER = -1;
                                L.ListPointKey[i + 1].dirIZQ = L.ListPointKey[i].dirIZQ;
                                L.ListPointKey.RemoveAt(i--);
                                break;
                            }
                        }
                        else
                        {
                            if (L.ListPointKey[i].dirDER == direccion)
                            {
                                L.ListPointKey.RemoveAt(i--);
                                break;
                                //i = -1;
                            }
                        }
                    }
                    L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
                }else
                {
                    if(L.tipoHoja == 'R')
                    {
                        for (int i = 0; i < L.ListPointKey.Count; i++)
                        {
                            if (i == 0)
                            {
                                if (L.ListPointKey[i].dirDER == direccion)//En el primer elemento no se elimina la cve y el apuntador ya que contiene dos apuntadores y solo se queda el hermano izquierda.
                                {
                                    //L.ListPointKey.RemoveAt(i--);
                                    //L.ListPointKey[i].dirDER = -1;
                                    try { 
                                            L.ListPointKey[i + 1].dirIZQ = L.ListPointKey[i].dirIZQ;
                                            L.ListPointKey.RemoveAt(i--);
                                            break;
                                    
                                    } catch (System.IndexOutOfRangeException) { L.ListPointKey.RemoveAt(i); break; }
                                }
                            }
                            else
                            {
                                if (L.ListPointKey[i].dirDER == direccion)
                                {
                                    L.ListPointKey.RemoveAt(i--);
                                    break;
                                    //i = -1;
                                }
                            }
                        }
                        L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
                    }
                }
            }
        }
        public bool HermanoDerecha(Hoja hoja,Hoja raiz,out Hoja hermanoDER)
        {
            bool tiene = false;
            hermanoDER = new Hoja();
            for (int i = 0; i < raiz.ListPointKey.Count ; i++)
            {
                if(i==0)
                {
                    if(raiz.ListPointKey[i].dirIZQ  == hoja.dirnodo)
                    {
                        if(raiz.ListPointKey[i].dirDER != -1)
                        {
                            tiene = true;
                            hermanoDER = RegresaHoja(raiz.ListPointKey[i].dirDER);
                        }
                    }
                }
                else
                {
                    if (raiz.ListPointKey[i-1].dirDER == hoja.dirnodo)
                    {
                        if (raiz.ListPointKey[i].dirDER != -1)
                        {
                            tiene = true;
                            hermanoDER = RegresaHoja(raiz.ListPointKey[i].dirDER);
                        }
                    }
                }
            }
            return tiene;
        }
        public bool HermanoIzquierda(Hoja hoja, Hoja raiz, out Hoja hermanoIZQ)
        {
            bool tiene = false;
            hermanoIZQ = new Hoja();
            for (int i = 0; i < raiz.ListPointKey.Count; i++)
            {
                if (i == 0)
                {
                    if (raiz.ListPointKey[i].dirDER == hoja.dirnodo)
                    {
                        if (raiz.ListPointKey[i].dirIZQ != -1)
                        {
                            tiene = true;
                            hermanoIZQ = RegresaHoja(raiz.ListPointKey[i].dirIZQ);
                        }
                    }
                }
                else
                {
                    if (raiz.ListPointKey[i].dirDER == hoja.dirnodo)
                    {
                        if (raiz.ListPointKey[i-1].dirDER != -1)
                        {
                            tiene = true;
                            hermanoIZQ = RegresaHoja(raiz.ListPointKey[i-1].dirDER);
                        }
                    }
                }
            }
            return tiene;
        }
        public void Actualiza_Raiz(Hoja raiz,Hoja hoja)
        {
            if (raiz.tipoHoja == 'I' || raiz.tipoHoja == 'R')
            {
                for (int i = 0; i < raiz.ListPointKey.Count; i++)
                {
                    if (raiz.ListPointKey[i].dirDER == hoja.dirnodo)
                    {
                        raiz.ListPointKey[i].entero = hoja.ListPointKey[0].entero;
                        raiz.ListPointKey[i].dirDER = hoja.dirnodo;
                        break;
                    }
                }
            }
            /*else
            {
                if (raiz.tipoHoja == 'R')
                {
                    for (int i = 0; i < raiz.ListPointKey.Count; i++)
                    {
                        if ( i==0 && (raiz.ListPointKey[i].dirDER == hoja.dirnodo || raiz.ListPointKey[i].dirIZQ == hoja.dirnodo))
                        {
                            raiz.ListPointKey[i].entero = hoja.ListPointKey[0].entero;
                            //break;
                        }
                        else
                        {
                            raiz.ListPointKey[i].entero = hoja.ListPointKey[0].entero;
                        }
                    }
                }
            }*/
        }
        public void Borrar_Hoja(List<Hoja> Arbol,long direccionHoja)
        {
            for (int i = 0; i < Arbol.Count; i++)
            {
                if(Arbol[i].dirnodo == direccionHoja)
                {
                    Arbol.RemoveAt(i--);
                    break;
                }
            }
        }
        public bool ExisteClaveyDirEnHoja(Hoja hoja, int clave,long direccion)
        {
            bool existe = false;

            foreach (Ordenadas or in hoja.ListPointKey)
            {
                if (or.entero == clave /*&& or.dirDER == direccion)*/)
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }
        public int ExistenIntermedios(List<Hoja> arbol)
        {
            int i = 0;
            foreach (Hoja hoja in arbol)
            {
                if(hoja.tipoHoja == 'I')
                {
                    i++;
                }
            }
            return i;
        }
        /// <summary>
        /// Funcion para verificar que el otro intermedio se haya eliminado.
        /// </summary>
        /// <param name="arbol"></param>
        /// <param name="direccion"></param>
        /// <returns></returns>
        public bool ExistenIntermedios(List<Hoja> arbol ,long direccion)
        {
            bool existe = false;
            foreach (Hoja hoja in arbol)
            {
                if (hoja.tipoHoja == 'I' && hoja.dirnodo == direccion)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }        
        #endregion
        public long RegresaRaiz()
        {
            //la restructuracion en el hermano Derecho no lo hace en su Raiz. checar despues de que se elimine la raiz ver si se elimina 
            //el hermano intermedio de la izquierda.
            long dir = -1;
            if (Arbol.Count == 1 && Arbol[0].tipoHoja == 'H')
            {
                dir = Arbol[0].dirnodo;
            }
            else
            {
                foreach(Hoja hoja in Arbol)
                {
                    if(hoja.tipoHoja == 'R')
                    {
                        dir = hoja.dirnodo;
                        break;
                    }
                }
            }
            return dir;
        }
        #endregion

        #region Arbol Secundario

        #region Insertar
        /// <summary>
        /// Funcion del Arbol que inserta los elementos de un registro en el archivo.
        /// La funcion busca si la clave ya esta en el arbol e inserta la direccion del
        /// Registro en el bloque de la clave.
        /// </summary>
        /// <param name="Clave">Clave que se va a guardar en el Arbol</param>
        /// <param name="Direccion">Direccion del registro</param>
        /// <param name="atributo">Objeto de tipo Atributo</param>
        public void InsertaSECU(int Clave, long Direccion, Atributo atributo, string nombreArchivo, FileStream archivo)
        {//chercar si ya existe la clave en el arbol.
            Hoja hoja;
            int AP;
             if (Arbol.Count == 0)//si el arbol esta vacio.
            {
                hoja = new Hoja();
                hoja = CreaHojatipoH(hoja, atributo);
                long DireccionBloque = Archivo.CrearBloque(atributo.rutaIndice, atributo.archivoIndice);
                Ordenadas PK = new Ordenadas(Clave, DireccionBloque);
                hoja.ListPointKey.Add(PK);
                Arbol.Add(hoja);
                AgregarBloqueSECU(hoja, Clave, Direccion);
                int index = IndiceDeClave(hoja, DireccionBloque);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
                Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, hoja.ListPointKey[index].lista, DireccionBloque);
                //AgregarBloqueSECU(hoja, Clave, Direccion);
            }
            else
            {
                hoja = new Hoja();
                bool recursivo = false;
                long DirHoja = BuscarHoja(Arbol, Clave, recursivo, new Hoja());//Busca la direccion de la hoja
                hoja = RegresaHoja(DirHoja);//devuelve la hoja que estamos buscando.   
            }
            if ((AP = hoja.NoApuntadores()) <= 4)
            {
                if (ExisteClaveEnHoja(hoja, Clave) == false)
                {
                    long DireccionBloque = Archivo.CrearBloque(atributo.rutaIndice, atributo.archivoIndice);
                    Inserta_En_HojaSECU(hoja, Clave, Direccion,DireccionBloque); 
                    AgregarBloqueSECU(hoja,Clave,Direccion);
                    int index = IndiceDeClave(hoja, DireccionBloque);
                    Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, hoja.ListPointKey[index].lista, DireccionBloque);
                }
                else
                {
                    //insertar en bloque de la clave
                    AgregarBloqueSECU(hoja, Clave, Direccion);
                    long DireccionBloque = RegresarDirBloque(hoja, Clave);
                    int index = IndiceDeClave(hoja, DireccionBloque);
                    Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
                    Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, hoja.ListPointKey[index].lista, DireccionBloque);
                    //hacer una funcion de buscar el elemento de la clave en la listpointkey para ver que lista se va a acceder y escribir
                }
                //Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
            }
            else
            {
                //Ya tenemos la hoja que se esta verificando para ver si se puede insertar.
                Hoja nueva = new Hoja();
                nueva = CreaHojatipoH(nueva, atributo);//Creamos la hoja nueva en el archivo tambien.
                Hoja auxiliar = new Hoja();
                auxiliar.ListPointKey = hoja.ListPointKey;
                auxiliar.tipoHoja = 'H';
                long DireccionBloque = Archivo.CrearBloque(atributo.rutaIndice, atributo.archivoIndice);
                Inserta_En_HojaSECU(auxiliar, Clave, 0, DireccionBloque);
                AgregarBloqueSECU(auxiliar, Clave, Direccion);
                int index = IndiceDeClave(hoja, DireccionBloque);
                Archivo.EscribirIndiceSecundarioBloques(atributo.rutaIndice, atributo.archivoIndice, hoja.ListPointKey[index].lista, DireccionBloque);
                hoja.dirsignodo = nueva.dirnodo;//asiganamos apuntadores
                hoja.ListPointKey = new List<Ordenadas>();//borramos la lista con todo.

                for (int i = 0; i < auxiliar.ListPointKey.Count; i++)//Copiar o asingar los valores del auxiliar a los nodos que estan ya en el archivo.
                {
                    if (i < 2)
                    {
                        hoja.ListPointKey.Add(auxiliar.ListPointKey[i]);//copiamos 4+1/2 en este nodo.
                    }
                    else
                    {
                        nueva.ListPointKey.Add(auxiliar.ListPointKey[i]);//Copiamos del 2 al 5.
                    }
                }
                Arbol.Add(nueva);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, hoja);
                Archivo.EscribirEnHoja(atributo.rutaIndice, atributo.archivoIndice, nueva);
                int valor_menor = nueva.ListPointKey[0].entero;
                Inserta_En_Raiz(hoja, valor_menor, nueva, atributo, nombreArchivo, archivo);
            }

        }
        public bool ExisteClaveEnHoja(Hoja hoja, int clave)
        {
            bool existe = false;

            foreach(Ordenadas or in hoja.ListPointKey)
            {
                if(or.entero == clave)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        /// <summary>
        /// En esta funcion se inserta en la hoja <paramref name="L"/> la direccion del bloque
        /// si es el caso de tipo de hoja == hoja , si es una raiz se manejan <paramref name="direccionREG"/> 
        /// como si fuera la direccion del nodo.
        /// </summary>
        /// <param name="L"></param>
        /// <param name="cve"></param>
        /// <param name="direccionREG"></param>
        /// <param name="direccionBloque"></param>
        public void Inserta_En_HojaSECU(Hoja L, int cve, long direccionREG,long direccionBloque)
        {
            if (L.tipoHoja == 'H')
            {
                L.ListPointKey.Add(new Ordenadas(-1, cve, direccionBloque));
                L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
            }
            else
            {
                for (int i = 0; i < L.ListPointKey.Count; i++)
                {
                    if (cve < L.ListPointKey[i].entero)
                    {
                        if (i == 0)
                        { L.ListPointKey.Insert(i, new Ordenadas(L.ListPointKey[i].dirIZQ, cve, direccionREG)); }
                        else
                        {
                            L.ListPointKey.Insert(i, new Ordenadas(-1, cve, direccionREG));
                        }
                        break;
                    }
                    else
                    {
                        if (cve > L.ListPointKey[i].entero)
                        {
                            L.ListPointKey.Add(new Ordenadas(-1, cve, direccionREG));
                            break;
                        }
                        //i--;
                    }
                }
                L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
            }
        }
        public void AgregarBloqueSECU(Hoja hoja,int cve, long dirReg)
        {
            int indice;
            if (ExisteDatoEntero(hoja.ListPointKey, cve, out indice) == true)
            {
                if (ExistedirREG(hoja.ListPointKey[indice - 1].lista, dirReg,out int n) == false)
                {
                    for (int i = 0; i < hoja.ListPointKey.Count; i++)
                    {
                        if (hoja.ListPointKey[i].entero == cve)
                        {
                            hoja.ListPointKey[i].lista.Add(new Ordenadas(dirReg));
                            hoja.ListPointKey[i].lista = hoja.ListPointKey[i].lista.OrderBy(o => o.dir).ToList();
                            break;
                        }
                    }
                }
            }

        }
        public long RegresarDirBloque(Hoja hoja,int clave)
        {
            long dir = -1;
            foreach(Ordenadas ordenadas in hoja.ListPointKey)
            {
                if(clave == ordenadas.entero)
                {
                    dir = ordenadas.dirDER;
                    break;
                }
            }
            return dir;
        }
        public int IndiceDeClave(Hoja hoja,long direccionBloque)
        {
            int index = 0;
            for (int i = 0; i < hoja.ListPointKey.Count; i++)
            {
                if(hoja.ListPointKey[i].dirDER == direccionBloque)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        #endregion

        #region Eliminar
        public void BorrarSECU(int valor, long direccion, List<Hoja> Arbol, Atributo atributo)
        {
            Hoja hoja = new Hoja();
            bool recursivo = false;
            long dir = BuscarHoja(Arbol, valor, recursivo, new Hoja());
            hoja = BuscarHojaDIR(dir, Arbol);
            if (ExisteClaveyDirEnHoja(hoja, valor, direccion) == true)
            {
                Borrar_EntradaSECU(hoja, valor, direccion, Arbol, atributo);
            }

        }
        public void Borrar_EntradaSECU(Hoja N, int valor, long direccion, List<Hoja> Arbol, Atributo atributo)
        {
            if (N.NoApuntadores() == 2  && N.ListPointKey[0].lista.Count != 1 || N.ListPointKey[1].lista.Count != 1 || Arbol.Count == 1)
            {
                Borrar_En_HojaSECU(N, valor, direccion);
            }
            else
            {
                if (N.NoApuntadores() > 2)//Borramos directo en la hoja.
                {
                    Borrar_En_HojaSECU(N, valor, direccion);
                }
                else
                {
                    Ordenadas K1 = new Ordenadas();
                    if (N.NoApuntadores() <= 2)
                    {
                        Hoja N1 = new Hoja();
                        Hoja P = new Hoja();
                        P = Regresa_Raiz(N);

                        bool borro = Borrar_En_HojaSECU(N, valor, direccion);
                        if (borro == true)
                        {
                            bool bandPrestamo = false;

                            if (HermanoDerecha(N, P, out N1) == true)//condicional para saber si existe el hermano de la derecha.
                            {
                                K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                if (N1.ListPointKey.Count > 2)//Prestamo de hermano derecha
                                {
                                    //N.ListPointKey.Add(N1.ListPointKey[0]);
                                    Inserta_En_HojaS(N, N1.ListPointKey[0].entero, N1.ListPointKey[0].dirDER,N1.ListPointKey[0].lista);
                                    N1.ListPointKey.RemoveAt(0);
                                    bandPrestamo = true;
                                    Actualiza_Raiz(P, N1);
                                }
                            }
                            if (HermanoIzquierda(N, P, out N1) == true && bandPrestamo == false)//condicional para saber si existe el hermano de la izquierda.
                            {
                                K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                if (N1.ListPointKey.Count > 2)//Prestamo de hermano derecha
                                {
                                    //N.ListPointKey.Add(N1.ListPointKey.Last());
                                    Inserta_En_HojaS(N, N1.ListPointKey.Last().entero, N1.ListPointKey.Last().dirDER, N1.ListPointKey.Last().lista);
                                    N1.ListPointKey.RemoveAt(N1.ListPointKey.Count - 1);
                                    bandPrestamo = true;
                                    Actualiza_Raiz(P, N);
                                }
                                //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                                //apuntadores se tiene que hacer una restructuracion.
                            }
                            if (bandPrestamo == false)
                            {
                                bool bandUnion = false;
                                /*
                                 *Tomar  en cuanta la consideracion en la cual sean solo 3 hojas (una raiz y dos hojas) 
                                 * y se tenga que hacer una union entonces la raiz se borra y se juntan los  nodos.
                                 */


                                if (HermanoDerecha(N, P, out N1) == true)//condicional para saber si existe el hermano de la derecha.
                                {
                                    //K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                    if (N1.ListPointKey.Count < 4)//union de hermano derecha con hermano actual.
                                    {
                                        foreach (Ordenadas ordenadas in N1.ListPointKey)
                                        {
                                            //N.ListPointKey.Add(ordenadas);
                                            Inserta_En_HojaS(N, ordenadas.entero, ordenadas.dirDER,ordenadas.lista);
                                            //Inserta_En_Hoja()
                                        }
                                        if(Arbol.Count != 3)
                                        {
                                            Borrar_En_Hoja(P, N1.ListPointKey[0].entero, N1.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                            N.dirsignodo = N1.dirsignodo;
                                            Borrar_Hoja(Arbol, N1.dirnodo);
                                            bandUnion = true;
                                            Actualiza_Raiz(P, N);//Borrar la direccion y cve del nodo que se elimino.
                                        }
                                        else
                                        {
                                            //para dejar solo un nodo cuando son tres hojas(una raiz y dos hojas)
                                            //Borrar_En_Hoja(P, N1.ListPointKey[0].entero, N1.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                            N.dirsignodo = -1;
                                            Borrar_Hoja(Arbol, N1.dirnodo);
                                            Borrar_Hoja(Arbol, P.dirnodo);
                                            bandUnion = true;
                                            //Actualiza_Raiz(P, N);//Borrar la direccion y cve del nodo que se elimino.
                                        }
                                    }

                                }
                                if (HermanoIzquierda(N, P, out N1) == true && bandUnion == false)//condicional para saber si existe el hermano de la izquierda.
                                {
                                    K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                    if (N1.ListPointKey.Count < 4)//Prestamo de hermano derecha
                                    {
                                        foreach (Ordenadas ordenadas in N.ListPointKey)
                                        {
                                            Inserta_En_HojaS(N1, ordenadas.entero, ordenadas.dirDER,ordenadas.lista);
                                        }
                                        if(Arbol.Count !=3)
                                        {
                                            Borrar_En_Hoja(P, N.ListPointKey[0].entero, N.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                            N1.dirsignodo = N.dirsignodo;
                                            Borrar_Hoja(Arbol, N.dirnodo);
                                            bandUnion = true;
                                            Actualiza_Raiz(P, N1);//Borrar la direccion y cve del nodo que se elimino.
                                                                  //Borrar_En_Hoja(P, N.ListPointKey[0].entero, N.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                        }
                                        else
                                        {
                                            //para dejar solo un nodo cuando son tres hojas(una raiz y dos hojas)
                                            //Borrar_En_Hoja(P, N.ListPointKey[0].entero, N.dirnodo);//checar si es un intermedio para borrar en la posicion 0 el apuntador izq o der.
                                            N1.dirsignodo = -1;
                                            Borrar_Hoja(Arbol, N.dirnodo);
                                            Borrar_Hoja(Arbol, P.dirnodo);
                                            bandUnion = true;
                                            //Actualiza_Raiz(P, N1);//Borrar la direccion y cve del nodo que se elimino.
                                        }
                                    }

                                    //P.NoApuntadores();
                                    //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                                    //apuntadores se tiene que hacer una restructuracion.
                                }
                                int i = P.NoApuntadores();

                                if (P.NoApuntadores() <= 2 && P.tipoHoja == 'I')
                                {
                                    bandPrestamo = false;
                                    //intermedios con menos de dos apuntadores.
                                    //si si hay hacer rotacion.
                                    Hoja Inter = P;
                                    P = Regresa_Raiz(P);
                                    Hoja hermano = new Hoja();
                                    if (HermanoDerecha(Inter, P, out hermano) == true)
                                    {
                                        K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                        if (hermano.NoApuntadores() >= 4)//Prestamo de hermano derecha
                                        {
                                            //Inter.ListPointKey.Add(hermano.ListPointKey[0]);
                                            Inserta_En_HojaS(Inter, hermano.ListPointKey[0].entero, hermano.ListPointKey[0].dirDER,hermano.ListPointKey[0].lista);
                                            hermano.ListPointKey.RemoveAt(0);
                                            bandPrestamo = true;
                                            Actualiza_Raiz(P, hermano);
                                        }
                                    }
                                    if (HermanoIzquierda(Inter, P, out hermano) == true && bandPrestamo == false)//condicional para saber si existe el hermano de la izquierda.
                                    {
                                        K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                        //si el hermano tiene al menos 3 elementos se puede hacer el prestamo que seria la rotacion de los elementos.
                                        if (hermano.NoApuntadores() >= 4)//Prestamo de hermano izquierda
                                        {
                                            //Inter.ListPointKey.Add(hermano.ListPointKey.Last());
                                            Inserta_En_HojaS(Inter, hermano.ListPointKey.Last().entero, hermano.ListPointKey.Last().dirDER, hermano.ListPointKey.Last().lista);
                                            hermano.ListPointKey.RemoveAt(hermano.ListPointKey.Count - 1);
                                            bandPrestamo = true;
                                            Actualiza_Raiz(P, Inter);
                                        }
                                        //checar si el padre no es un intermedio, si es un intermedio debe de tener al menos dos apuntadores, si no tiene al menos dos
                                        //apuntadores se tiene que hacer una restructuracion.
                                    }
                                    if (bandPrestamo == false)
                                    {
                                        bandUnion = false;

                                        if (HermanoDerecha(Inter, P, out hermano) == true)//condicional para saber si existe el hermano de la derecha.
                                        {
                                            //K1 = new Ordenadas(N.dirnodo, N1.ListPointKey[0].entero, N1.dirnodo);
                                            if (hermano.ListPointKey.Count <= 2)//union de hermano derecha
                                            {
                                                Ordenadas ordenadas2 = new Ordenadas(P.ListPointKey[0].entero, hermano.ListPointKey[0].dirIZQ);
                                                Inserta_En_HojaS(Inter, ordenadas2.entero, ordenadas2.dirDER,ordenadas2.lista);
                                                foreach (Ordenadas ordenadas in hermano.ListPointKey)
                                                {
                                                    Inserta_En_HojaS(Inter, ordenadas.entero, ordenadas.dirIZQ, ordenadas.lista);
                                                }
                                                Inter.dirsignodo = hermano.dirsignodo;
                                                Borrar_Hoja(Arbol, hermano.dirnodo);

                                                bandUnion = true;
                                                //Actualiza_Raiz(P, Inter);//Borrar la direccion y cve del nodo que se elimino.

                                                if (ExistenIntermedios(Arbol) < 2)
                                                {
                                                    if (ExistenIntermedios(Arbol, hermano.dirnodo) == false)
                                                    {
                                                        Borrar_Hoja(Arbol, P.dirnodo);
                                                        Inter.tipoHoja = 'R';
                                                    }
                                                }
                                            }

                                        }
                                        if (HermanoIzquierda(Inter, P, out hermano) == true && bandUnion == false)//condicional para saber si existe el hermano de la izquierda.
                                        {
                                            K1 = new Ordenadas(N1.dirnodo, N.ListPointKey[0].entero, N.dirnodo);
                                            if (hermano.ListPointKey.Count <= 2)//union de hermano izquierda
                                            {
                                                //se crea un elemento con el apuntador y el valor unico de la raiz.
                                                Ordenadas ordenadas1 = new Ordenadas(P.ListPointKey[0].entero, Inter.ListPointKey[0].dirIZQ);
                                                Inserta_En_HojaS(hermano, ordenadas1.entero, ordenadas1.dirDER,ordenadas1.lista);

                                                foreach (Ordenadas ordenadas in Inter.ListPointKey)
                                                {
                                                    Inserta_En_HojaS(hermano, ordenadas.entero, ordenadas.dirDER,ordenadas.lista);
                                                }
                                                //int i = 0;
                                                //hermano.ListPointKey[hermano.ListPointKey.Count - 1].dirDER = 65;
                                                hermano.dirsignodo = Inter.dirsignodo;
                                                Borrar_Hoja(Arbol, Inter.dirnodo);
                                                bandUnion = true;
                                                //Actualiza_Raiz(P, hermano);//Borrar la direccion y cve del nodo que se elimino.

                                                if (ExistenIntermedios(Arbol) < 2)
                                                {
                                                    if (ExistenIntermedios(Arbol, Inter.dirnodo) == false)
                                                    {
                                                        //copiar en una ordenada el elemento de la raiz;
                                                        Borrar_Hoja(Arbol, P.dirnodo);
                                                        hermano.tipoHoja = 'R';
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public bool Borrar_En_HojaSECU(Hoja L, int cve, long direccion)
        {
            bool borroenHoja = false;
            if (L.tipoHoja == 'H')
            {
                for (int i = 0; i < L.ListPointKey.Count; i++)
                {
                    if (L.ListPointKey[i].entero == cve /*&& L.ListPointKey[i].dirDER == direccion)*/)
                    {
                        if(BorrarEnBloque(L,cve,direccion,out int res) == true)
                        {
                            //BorrarEnBloque(L, cve, direccion, out int x);
                            if (res == 1)
                            {
                                L.ListPointKey.RemoveAt(i--);
                                borroenHoja = true;
                                break;
                            }
                        }else
                        {
                            break;
                        }
                    }
                    //L.ListPointKey.RemoveAt(i--);

                }
                L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
                //L.ListPointKey.RemoveAt(0);
            }
            else
            {
                for (int i = 0; i < L.ListPointKey.Count; i++)
                {
                    if (i == 0)
                    {
                        if (L.ListPointKey[i].dirDER == direccion)//En el primer elemento no se elimina la cve y el apuntador ya que contiene dos apuntadores y solo se queda el hermano izquierda.
                        {
                            //L.ListPointKey.RemoveAt(i--);
                            //L.ListPointKey[i].dirDER = -1;
                            L.ListPointKey[i + 1].dirIZQ = L.ListPointKey[i].dirIZQ;
                            L.ListPointKey.RemoveAt(i--);
                            break;
                        }
                    }
                    else
                    {
                        if (L.ListPointKey[i].dirDER == direccion)
                        {
                            L.ListPointKey.RemoveAt(i--);
                            break;
                            //i = -1;
                        }
                    }
                }
                L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
            }
            return borroenHoja;
        }
        public bool BorrarEnBloque(Hoja hoja, int cve, long dirReg, out int restantes)
        {
            bool borro = false;
            int indice;
            restantes = 0;
            if (ExisteDatoEntero(hoja.ListPointKey, cve, out indice) == true)
            {
                if (ExistedirREG(hoja.ListPointKey[indice - 1].lista, dirReg, out int n) == true)
                {
                    for (int i = 0; i < hoja.ListPointKey.Count; i++)
                    {
                        if (hoja.ListPointKey[i].entero == cve)
                        {
                            for (int j = 0; j < hoja.ListPointKey[i].lista.Count; j++)
                            {
                                if(hoja.ListPointKey[i].lista.Count >1)
                                {
                                    if (hoja.ListPointKey[i].lista[j].dir == dirReg)
                                    {
                                        hoja.ListPointKey[i].lista.RemoveAt(j);
                                        hoja.ListPointKey[i].lista = hoja.ListPointKey[i].lista.OrderBy(o => o.dir).ToList();
                                        borro = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    restantes = hoja.ListPointKey[i].lista.Count;
                                    hoja.ListPointKey[i].lista.RemoveAt(j);
                                    borro = true;
                                    break;
                                }
                            }   
                        }
                        if(borro == true)
                        {
                            break;
                        }
                    }
                }
            }

            return borro;
        }
        public void Inserta_En_HojaS(Hoja L, int cve, long direccion,List<Ordenadas> lista)
        {
            for (int i = 0; i < L.ListPointKey.Count; i++)
            {
                if (cve < L.ListPointKey[i].entero)
                {
                    if (i == 0)
                    { 
                        Ordenadas nueva = new Ordenadas(L.ListPointKey[i].dirIZQ, cve, direccion);
                        nueva.lista = new List<Ordenadas>();
                        nueva.lista = lista;
                        L.ListPointKey.Insert(i,nueva);
                    }
                    else
                    {
                        Ordenadas nueva = new Ordenadas(-1, cve, direccion);
                        nueva.lista = new List<Ordenadas>();
                        nueva.lista = lista;
                        L.ListPointKey.Insert(i,nueva);
                        
                    }
                    break;
                }
                else
                {
                    if (cve > L.ListPointKey[i].entero)
                    {
                        Ordenadas nueva = new Ordenadas(-1, cve, direccion);
                        nueva.lista = new List<Ordenadas>();
                        nueva.lista = lista;
                        L.ListPointKey.Add(nueva);
                        //L.ListPointKey[i].lista = lista;
                        break;
                    }
                    //i--;
                }
            }
            L.ListPointKey = L.ListPointKey.OrderBy(o => o.entero).ToList();
        }
        #endregion

        #endregion

    }
}
