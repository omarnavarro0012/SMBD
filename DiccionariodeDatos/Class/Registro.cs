using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariodeDatos
{
    class Registro
    {
        private List<object> Registros;
        public List<object> registros { get { return Registros; } set { Registros = value; } }
        private long DirSigR;          // Dirección del siguiente registro.
        public long dirSigR { get { return DirSigR; } set { DirSigR = value; } }
        private long DirReG;
        public long dirReG { get { return DirReG; } set { DirReG = value; } }

        public Registro()
        {
            registros = new List<object>();
            dirSigR = -1;
            dirReG = -1;
        }

    }
}
