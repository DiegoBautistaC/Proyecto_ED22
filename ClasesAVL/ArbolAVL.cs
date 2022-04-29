using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesAVL
{
    public class ArbolAVL <T> : IEnumerable<T>
    {
        public NodoAVL<T> Raiz;

        public delegate int Delegado(T valor1, T valor2);

        public delegate int Delegado2(string valor1, T valor2);

        public Delegado Comparador;

        public Delegado2 Comparador2;

        public ArbolAVL(Delegado unComparador, Delegado2 unComparador2)
        {
            this.Comparador = unComparador;
            this.Comparador2 = unComparador2;
        }

        public void Insertar(T unValor)
        {
            Insertar(unValor, ref this.Raiz);
        }

        void Insertar(T unValor, ref NodoAVL<T> raizActual)
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
                this.Insertar(unValor, ref raizActual.SubDerecho);
            }
            else
            {
                this.Insertar(unValor, ref raizActual.SubIzquierdo);
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

        public void Remover(string valor)
        {
            this.Remover(valor, ref this.Raiz);
        }

        void Remover(string valor, ref NodoAVL<T> raizActual)
        {
            if(this.Comparador2(valor, raizActual.Valor) == 0)
            {
                if(raizActual.SubIzquierdo == null && raizActual.SubDerecho == null)
                {
                    raizActual = null;
                }
                else if(raizActual.SubIzquierdo != null && raizActual.SubDerecho != null)
                {
                    var Remplazo = this.MayorIzquierda(raizActual);
                    Remplazo.SubDerecho = raizActual.SubDerecho;
                    Remplazo.SubIzquierdo = raizActual.SubIzquierdo;
                    raizActual = Remplazo;
                }
                else
                {
                    if(raizActual.SubDerecho != null)
                    {
                        raizActual = raizActual.SubDerecho;
                    }
                    else
                    {
                        raizActual = raizActual.SubIzquierdo;
                    }
                }
            }
            else if(this.Comparador2(valor, raizActual.Valor) > 0)
            {
                this.Remover(valor, ref raizActual.SubDerecho);
            }
            else
            {
                this.Remover(valor, ref raizActual.SubIzquierdo);
            }
        }

        NodoAVL<T> MayorIzquierda(NodoAVL<T> raizActual)
        {
            NodoAVL<T> Mayor = null;
            NodoAVL<T> Auxiliar = raizActual;
            if(raizActual.SubIzquierdo.SubDerecho == null)
            {
                Mayor = Auxiliar.SubIzquierdo;
                Auxiliar.SubIzquierdo = Auxiliar.SubIzquierdo.SubIzquierdo;
            }
            else
            {
                Auxiliar = raizActual.SubIzquierdo;
                bool Validacion = true;
                while(Auxiliar!= null && Validacion )
                {
                    if(Auxiliar.SubDerecho.SubDerecho == null)
                    {
                        if(Auxiliar.SubDerecho.SubIzquierdo != null)
                        {
                            Mayor = Auxiliar.SubDerecho;
                            Auxiliar.SubDerecho = Auxiliar.SubDerecho.SubIzquierdo;
                            Validacion = false;
                        }else
                        {
                            Mayor = Auxiliar.SubDerecho;
                            Auxiliar.SubDerecho = null;
                            Validacion = false;
                        }
                    }
                    else
                    {
                        Auxiliar = Auxiliar.SubDerecho;
                    } 
                        
                }
            }
            return Mayor;
        }

        public T Encontrar(string valor)
        {
            return this.Encontrar(ref this.Raiz, valor);
        }

        T Encontrar(ref NodoAVL<T> raizActual, string valor)
        {
            if (raizActual == null)
            {
                return default(T);
            }
            else if (this.Comparador2(valor, raizActual.Valor) == 0)
            {
                return raizActual.Valor;
            }
            else if (this.Comparador2(valor, raizActual.Valor) > 0)
            {
                return this.Encontrar(ref raizActual.SubDerecho, valor);
            }
            else
            {
                return this.Encontrar(ref raizActual.SubIzquierdo, valor);
            }
        }

        public void Leer(NodoAVL<T> raizActual, ref Cola<T> cola)
        {
            if (raizActual.SubDerecho == null && raizActual.SubIzquierdo == null)
            {
                cola.Encolar(raizActual.Valor);
            }
            else if (raizActual.SubDerecho == null)
            {
                //lee Izquierdo y luego Raiz
                this.Leer(raizActual.SubIzquierdo, ref cola);
                cola.Encolar(raizActual.Valor);
            }
            else if (raizActual.SubIzquierdo == null)
            {
                //lee Raiz y luego derecho
                cola.Encolar(raizActual.Valor);
                this.Leer(raizActual.SubDerecho, ref cola);
            }
            else
            {
                //lee Izquierdo, lee raiz y luego derecho
                this.Leer(raizActual.SubIzquierdo, ref cola);
                cola.Encolar(Raiz.Valor);
                this.Leer(Raiz.SubDerecho, ref cola);
            }
        }

        public bool Verificacion(Predicate<T> unPredicado)
        {
            int contador = 0;
            if (Raiz != null)
            {
                Verificacion(unPredicado, Raiz, ref contador);
            }
            if (contador < 8)
            {
                return true;
            }
            return false;
        }

        void Verificacion(Predicate<T> unPredicado, NodoAVL<T> raizActual, ref int contador)
        {
            if (raizActual.SubDerecho == null && raizActual.SubIzquierdo == null)
            {
                if (unPredicado(raizActual.Valor))
                {
                    contador++;  
                }              
            }
            else if (raizActual.SubDerecho == null)
            {
                //verifica Izquierdo y luego Raiz
                Verificacion(unPredicado, raizActual.SubIzquierdo, ref contador);
                if (unPredicado(raizActual.Valor))
                {
                    contador++;
                }
            }
            else if (raizActual.SubIzquierdo == null)
            {
                //verifica Raiz y luego derecho
                if (unPredicado(raizActual.Valor))
                {
                    contador++;
                }
                Verificacion(unPredicado, raizActual.SubDerecho, ref contador);
            }
            else
            {
                //verificar Izquierdo, verificar raiz y luego derecho
                Verificacion(unPredicado, raizActual.SubIzquierdo, ref contador);
                if (unPredicado(raizActual.Valor))
                {
                    contador++;
                }
                Verificacion(unPredicado, raizActual.SubDerecho, ref contador);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Cola<T> cola = new Cola<T>();
            if (this.Raiz != null)
            {
                this.Leer(this.Raiz, ref cola);
                while (!cola.EstaVacia())
                {
                    yield return cola.Desencolar();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
