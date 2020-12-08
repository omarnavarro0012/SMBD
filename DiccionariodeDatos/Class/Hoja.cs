using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiccionariodeDatos
{
    class Hoja
    {
        char TIPOHOJA;
        public char tipoHoja { get { return TIPOHOJA; } set { TIPOHOJA = value; } }
        long DirNodo;
        public long dirnodo { get { return DirNodo; } set { DirNodo = value; } }

        List<Ordenadas> ArregloDeDirCve;
        public List<Ordenadas> ListPointKey { get { return ArregloDeDirCve; } set { ArregloDeDirCve = value; } }

        long DirSigNodo;
        public long dirsignodo { get { return DirSigNodo; } set { DirSigNodo = value; } }

        long P5;
        public long p5 { get { return P5; } set { P5 = value; } }

        public Hoja()
        {
        }

        public int NoApuntadores()
        {
            int numeroapu = 0;
            if (this.tipoHoja == 'R')
            {
                for (int i = 0; i < ListPointKey.Count; i++)
                {
                    if (i == 0)
                    {
                        if (ListPointKey[i].dirIZQ != -1)
                        {
                            numeroapu += 1;
                        }
                        if (ListPointKey[i].dirDER != -1)
                        {
                            numeroapu += 1;
                        }
                    }
                    else
                    {
                        numeroapu += 1;
                    }
                }
            }
            else
            {
                if (this.tipoHoja == 'I')
                {
                    for (int i = 0; i < ListPointKey.Count; i++)
                    {
                        if (i == 0)
                        {
                            if(ListPointKey[i].dirIZQ != -1)
                            {
                                numeroapu += 1;
                            }
                            if (ListPointKey[i].dirDER != -1)
                            {
                                numeroapu += 1;
                            }
                        }
                        else
                        {
                            numeroapu += 1;
                        }
                    }
                }
                else
                {
                    if (this.tipoHoja == 'H')
                    {
                        for (int i = 0; i < ListPointKey.Count; i++)
                        {
                            numeroapu += 1;
                        }
                    }
                }
            }
            return numeroapu;
        }        
        public bool Contiene(long direccion)
        {
            bool contiene = false;
            foreach (Ordenadas or in this.ListPointKey)
            {
                if(or.dirDER == direccion)
                {
                    contiene = true;
                }
            }
            return contiene;
        }
    }
}
