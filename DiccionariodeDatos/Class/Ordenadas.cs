using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariodeDatos
{
    class Ordenadas
    {
        private int Entero;
        public int entero { get { return Entero; } set { Entero = value; } }
        private string Cadena;
        public string cadena { get { return Cadena; } set { Cadena = value; } }
        public long dir;
        public long dirIZQ;
        public long dirDER;
        public long dirSig;
        private List<Ordenadas> Lista = new List<Ordenadas>();
        public List<Ordenadas> lista { get { return Lista; } set { Lista = value; } }
        public Ordenadas()
        {

        }
        public Ordenadas(long direccion)
        {
            dir = direccion;
        }
        public Ordenadas(int clave)
        {
            entero = clave;
        }
        /// <summary>
        /// Se asigna la direccion a el dir y a direccion Derecha
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="direccion"></param>
        public Ordenadas(int valor , long direccion)
        {
            entero = valor;
            dir = direccion;
            dirDER = direccion;
            //lista = new List<Ordenadas>();
        }
        /// <summary>
        /// Crear el primer nodo del arbol que es la raiz. Solo se usa
        /// cuando se va a crear la raiz, ya para cuando sea el segundo valor 
        /// en la lista se tiene que crear una Ordenada con solo un apuntador
        /// y el valor de la clave.
        /// </summary>
        /// <param name="direccion1">Direccion del nodo de lado Izquierdo</param>
        /// <param name="valor">Clave con la que se va a comparar la clave entrante</param>
        /// <param name="direccion2">Direccion del nodo de lado Derecho</param>
        public Ordenadas(long direccion1 ,int valor, long direccion2)
        {
            entero = valor;
            dirIZQ = direccion1;
            dirDER = direccion2;
            //lista = new List<Ordenadas>();
        }
        public Ordenadas(string valor, long direccion)
        {
            cadena = valor;
            dir = direccion;
            //lista = new List<Ordenadas>();
        }
        public static void AgregarEnteros(int valor,List<int> ordenadaE)
       {
            ordenadaE.Add(valor);
            ordenadaE = ordenadaE.OrderBy(o => o).ToList();
       }
        public static void AgregarStrings(string a, List<string> ordenadaC)
        {
            ordenadaC.Add(a);
            ordenadaC = ordenadaC.OrderBy(o => o).ToList();
        }

    }
}
