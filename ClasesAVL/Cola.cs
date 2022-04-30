using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesAVL
{
    public class Cola<T>
    {
        public class Nodo<T>
        {
            public T Valor { get; set; }
            public Nodo<T> Siguiente;

            public Nodo(T valor)
            {
                this.Valor = valor;
                this.Siguiente = null;
            }
        }

        public static Nodo<T> Cabeza;
        public static Nodo<T> Queue;
        public static int Tamaño;

        public Cola()
        {
            Cabeza = null;
            Queue = null;
            Tamaño = 0;
        }

        public bool Encolar(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(valor);
            if (Tamaño == 0)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                Queue.Siguiente = nuevoNodo;
            }
            Queue = nuevoNodo;
            Tamaño++;
            return true;
        }
        
        public T Desencolar()
        {
            T valor = Cabeza.Valor;
            if(Tamaño == 1)
            {
                Cabeza = null;
                Queue = null;
            }
            else
            {
                Cabeza = Cabeza.Siguiente;
            }
            Tamaño--;
            return valor;
        }

        public bool EstaVacia()
        {
            return Tamaño == 0;
        }
    }
}
