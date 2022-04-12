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
        public static Nodo<T> Coola;
        public static int Tamaño;

        public Cola()
        {
            Cabeza = null;
            Coola = null;
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
                Coola.Siguiente = nuevoNodo;
            }
            Coola = nuevoNodo;
            Tamaño++;
            return true;
        }
        
        public T Desencolar()
        {
            T valor;
            valor = Cabeza.Valor;
            if(Tamaño == 1)
            {
                Cabeza = null;
                Coola = null;
            }else
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
