using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesAVL
{
    public class NodoAVL <T>
    {
        public T Valor { get; set; }
        public int Altura { get; set; }

        public NodoAVL<T> SubIzquierdo;

        public NodoAVL<T> SubDerecho;

        public NodoAVL(T unValor)
        {
            this.Valor = unValor;
            this.Altura = 1;
            this.SubIzquierdo = null;
            this.SubDerecho = null;
        }
    }
}
