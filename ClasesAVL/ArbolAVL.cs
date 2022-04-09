using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesAVL
{
    public class ArbolAVL <T>
    {
        public NodoAVL<T> Raiz;

        public delegate int Delegado(T valor1, T valor2);

        public Delegado Comparador;

        public ArbolAVL(Delegado unComparador)
        {
            this.Comparador = unComparador;
        }
    }
}
