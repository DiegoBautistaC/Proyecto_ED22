﻿using System;
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

        public void Insertar(T unValor)
        {
            Insertar(unValor, this.Raiz);
        }

        void Insertar(T unValor, NodoAVL<T> raizActual)
        {
            if (raizActual == null)
            {
                NodoAVL<T> nodo = new NodoAVL<T>(unValor);
                raizActual = nodo;
            }
            else if (Comparador(unValor, raizActual.Valor) == 0) 
            {
                
            }
            else if (Comparador(unValor, raizActual.Valor) > 0)
            {
                this.Insertar(unValor, raizActual.SubDerecho);
            }
            else
            {
                this.Insertar(unValor, raizActual.SubIzquierdo);
            }

            if (Comparador(unValor, raizActual.Valor) != 0)
            {
                if (raizActual.SubIzquierdo == null)
                {
                    raizActual.Altura = raizActual.SubDerecho.Altura + 1;
                }
                else if (raizActual.SubDerecho == null)
                {
                    raizActual.Altura = raizActual.SubIzquierdo.Altura + 1;
                }
                else
                {
                    raizActual.Altura = Math.Max(raizActual.SubIzquierdo.Altura, raizActual.SubDerecho.Altura) +1;
                }

                int FE = this.ObtenerFactorEquilibrio(raizActual);

                if (FE > 1)
                {
                    if (this.ObtenerFactorEquilibrio(raizActual.SubIzquierdo) < 0)
                    {
                        //Rotación doble
                        raizActual.SubIzquierdo = this.RotacionSimpleAIzquierda(raizActual.SubIzquierdo);
                        raizActual = this.RotacionSimpleADerecha(raizActual);
                    }
                    else
                    {
                        //Rotación simple
                        raizActual = this.RotacionSimpleADerecha(raizActual);
                    }
                }

                if (FE < -1)
                {
                    if (this.ObtenerFactorEquilibrio(raizActual.SubDerecho) > 0)
                    {
                        //Rotación doble
                        raizActual.SubDerecho = this.RotacionSimpleADerecha(raizActual.SubDerecho);
                        raizActual = this.RotacionSimpleAIzquierda(raizActual);
                    }
                    else
                    {
                        //Rotación simple
                        raizActual = this.RotacionSimpleAIzquierda(raizActual);
                    }
                }
            }
        }
        int ObtenerFactorEquilibrio(NodoAVL<T> raizActual)
        {
            if (raizActual.SubIzquierdo == null)
            {
                return -raizActual.SubDerecho.Altura;
            }
            else if (raizActual.SubDerecho == null)
            {
                return raizActual.SubIzquierdo.Altura;
            }
            else
            {
                return raizActual.SubIzquierdo.Altura - raizActual.SubDerecho.Altura;
            }
        }

        NodoAVL<T> RotacionSimpleADerecha(NodoAVL<T> raizActual)
        {
            NodoAVL<T> x = raizActual;
            NodoAVL<T> y = raizActual.SubIzquierdo;
            NodoAVL<T> z = raizActual.SubIzquierdo.SubDerecho;

            y.SubDerecho = x;

            if (z != null)
            {
                x.SubIzquierdo = z;
            }
            else
            {
                x.SubIzquierdo = null;
            }

            if (x.SubDerecho == null && x.SubIzquierdo == null)
            {
                x.Altura = 1;
            }
            else if (x.SubIzquierdo == null)
            {
                x.Altura = x.SubDerecho.Altura + 1;
            }
            else
            {
                x.Altura = Math.Max(x.SubIzquierdo.Altura, x.SubDerecho.Altura) + 1;
            }

            if (y.SubIzquierdo == null)
            {
                y.Altura = y.SubDerecho.Altura + 1;
            }
            else
            {
                y.Altura = Math.Max(y.SubIzquierdo.Altura, y.SubDerecho.Altura) + 1;
            }
            return y;
        }

        NodoAVL<T> RotacionSimpleAIzquierda(NodoAVL<T> raizActual)
        {
            NodoAVL<T> x = raizActual;
            NodoAVL<T> y = raizActual.SubDerecho;
            NodoAVL<T> z = raizActual.SubDerecho.SubIzquierdo;

            y.SubIzquierdo = x;

            if (z != null)
            {
                x.SubDerecho = z;
            }
            else
            {
                x.SubDerecho = null;
            }

            if (x.SubDerecho == null && x.SubIzquierdo == null)
            {
                x.Altura = 1;
            }
            else if (x.SubDerecho == null)
            {
                x.Altura = x.SubIzquierdo.Altura + 1;
            }
            else
            {
                x.Altura = Math.Max(x.SubIzquierdo.Altura, x.SubDerecho.Altura) + 1;
            }

            if (y.SubDerecho == null)
            {
                y.Altura = y.SubIzquierdo.Altura + 1;
            }
            else
            {
                y.Altura = Math.Max(y.SubIzquierdo.Altura, y.SubDerecho.Altura) + 1;
            }
            return y;
        }
    }
}
