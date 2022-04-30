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
            this.Raiz = null;
            this.Comparador = unComparador;
            this.Comparador2 = unComparador2;
        }

        /// <summary>
        /// Método para la inserción de valores que será llamado por el árbol 
        /// </summary>
        /// <param name="unValor">Valor que será insertado</param>
        public bool Insertar(T unValor)
        {
            bool insertado = false;
            Insertar(unValor, ref this.Raiz, ref insertado);
            return insertado;
        }

        /// <summary>
        /// Método interno de la inserción de un valor en el arbol
        /// </summary>
        /// <param name="unValor">Valor que será insertado</param>
        /// <param name="raizActual">Nodo utilizado para recorrer el árbol buscando la posición ideal de inserción</param>
        /// <param name="insertado">Variable de validación acerca de la inserción</param>
        void Insertar(T unValor, ref NodoAVL<T> raizActual, ref bool insertado)
        {
            if (raizActual == null) // Encontró la posición en la cual debe ir el valor y se inserta.
            {
                raizActual = new NodoAVL<T>(unValor);
                insertado = true;
            }
            else if (Comparador(unValor, raizActual.Valor) == 0) // Encontró un valor repetido, el valor no es insertado.
            {
                insertado = false;
            }
            else if (Comparador(unValor, raizActual.Valor) > 0) // El valor podría ser insertado en el subarbol DERECHO.
            {
                this.Insertar(unValor, ref raizActual.SubDerecho, ref insertado);
            }
            else // El valor podría ser insertado en el subarbol IZQUIERDO.
            {
                this.Insertar(unValor, ref raizActual.SubIzquierdo, ref insertado);
            }

            // Comienza proceso de verificación de alturas que se ejecuta por cada nodo que se recorrió durante la recursión para insertar el valor en una posición correcta
            // pero que el arbol podría no estar balanceado.

            if (Comparador(unValor, raizActual.Valor) != 0) // Validar que no se comience en una hoja.
            {
                //Procedimiento para el cálculo de la altura actual del subarbol, o nodo, luego de haber insertado.
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
                    raizActual.Altura = Math.Max(raizActual.SubIzquierdo.Altura, raizActual.SubDerecho.Altura) + 1;
                }

                // Cálculo del factor de equilibrio del subarbol, o nodo, actual basado en la altura previamente calculada de sus hijos
                int FE = this.ObtenerFactorEquilibrio(raizActual);

                if (FE > 1) // Existe un desequilibrio izquierdo, la altura del subarbol IZQUIERDO es mayor a la del subarbol DERECHO. ROTACIONES A LA DERECHA.
                {
                    if (this.ObtenerFactorEquilibrio(raizActual.SubIzquierdo) < 0) // Rotación doble hacia la derecha porque el subarbol IZQUIERDO cuenta con más valores a su derecha.
                    {
                        raizActual.SubIzquierdo = this.RotacionSimpleAIzquierda(raizActual.SubIzquierdo);
                        raizActual = this.RotacionSimpleADerecha(raizActual);
                    }
                    else // Rotación simple hacia la derecha poque el subarbol IZQUIERDO no cuenta con más valores a su derecha.
                    {
                        raizActual = this.RotacionSimpleADerecha(raizActual);
                    }
                }

                if (FE < -1) // Existe un desequilibrio derecho, la altura del subarbol DERECHO es mayor a la del subarbol IZQUIERDO. ROTACIONES A LA IZQUIERDA.
                {
                    if (this.ObtenerFactorEquilibrio(raizActual.SubDerecho) > 0) // Rotación doble hacia la Izquierda porque el subarbol DERECHO cuenta con más valores a su izquierda.
                    {
                        raizActual.SubDerecho = this.RotacionSimpleADerecha(raizActual.SubDerecho);
                        raizActual = this.RotacionSimpleAIzquierda(raizActual);
                    }
                    else // Rotación simple hacia la izquierda porque el subarbol DERECHO no cuenta con más valores a su izquierda.
                    {
                        raizActual = this.RotacionSimpleAIzquierda(raizActual);
                    }
                }
            }
        }

        /// <summary>
        /// Función interna que devuelve el valor del farctor de equilibrio para un nodo en específico que no es una hoja.
        /// </summary>
        /// <param name="raizActual">Nodo intermedio o raiz en el que se desea calcular el factor de equilibrio, no puede ser una hoja</param>
        /// <returns> Valor numérico del factor de equilibrio </returns>
        int ObtenerFactorEquilibrio(NodoAVL<T> raizActual)
        {
            if (raizActual.SubIzquierdo == null) // Si no hay subarbol IZQUIERDO el factor de equilibrio es 0 menos la altura del subarbol DERECHO.
            {
                return -raizActual.SubDerecho.Altura;
            }
            else if (raizActual.SubDerecho == null) // Si no hay dubarbol DERECHO el factor de equilibrio es la altura del subarbol IZQUIERDO.
            {
                return raizActual.SubIzquierdo.Altura;
            }
            else // Como tiene ambos subarboles entonces el facto de equilibrio es la resta del subarbol IZQUIERDO menos la del subarbol DERECHO.
            {
                return raizActual.SubIzquierdo.Altura - raizActual.SubDerecho.Altura;
            }
        }

        /// <summary>
        /// Función interna que representa una rotación simple hacia la derecha.
        /// </summary>
        /// <param name="raizActual">Raiz en la que se produjo el desequilibrio</param>
        /// <returns>Nodo resultante de la rotación simple hacia la derecha</returns>
        /// x: Simboliza la raiz actual, donde se produjo el desequilibrio.
        /// y: Simboliza el subarbol IZQUIERDO de la raiz actual.
        /// z: Simboliza la raiz del subarbol DERECHO del subarbol IZQUIERDO de la raiz actual.
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

            // Recálculo de las alturas de los nodos involucrados en la rotación
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

        /// <summary>
        /// Función interna que representa una rotación simple hacia la izquierda.
        /// </summary>
        /// <param name="raizActual">Nodo intermedio en el que se produjo el desequilibrio</param>
        /// <returns>El nodo resultante tras la rotación simple hacia la izquierda</returns>
        /// x: Simboliza la raiz actual, donde se produjo el desequilibrio.
        /// y: Simboliza el subarbol DERECHO de la raiz actual.
        /// z: Simboliza el subarbol IZQUIERDO del subarbol DERECHO de la raiz actual
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

            // Recálculo de las alturas de los árboles involucrados en la rotación
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

        // Método público que es invocado para remover un elemento del árbol por medio de una llave.
        public void Remover(string llave)
        {
            this.Remover(llave, ref this.Raiz);
        }

        /// <summary>
        /// Método interno que realiza el proceso de remover un elemento del árbol por medio de una llave.
        /// </summary>
        /// <param name="llave">Llave de búsqueda.</param>
        /// <param name="raizActual">Nodo auxiliar utilizado para ubicar la posición de la llave en el arbol.</param>
        void Remover(string llave, ref NodoAVL<T> raizActual)
        {
            if(this.Comparador2(llave, raizActual.Valor) == 0) // La llave se encuentra en raiz actual
            {
                if(raizActual.SubIzquierdo == null && raizActual.SubDerecho == null) // Caso en el que se remueve a una hoja
                {
                    raizActual = null;
                }
                else if(raizActual.SubIzquierdo != null && raizActual.SubDerecho != null) // Caso en el que el nodo a remover cuenta con los dos hijos, se realiza intercambio con el mayor de los menores.
                {
                    var Remplazo = this.MayorIzquierda(raizActual);
                    Remplazo.SubDerecho = raizActual.SubDerecho;
                    Remplazo.SubIzquierdo = raizActual.SubIzquierdo;
                    raizActual = Remplazo;
                }
                else // Caso en el que el nodo solo cuenta con un hijo que podría ser el derecho o izquierdo.
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
            else if(this.Comparador2(llave, raizActual.Valor) > 0) // La llave podría encontrarse en el sub arbol DERECHO.
            {
                this.Remover(llave, ref raizActual.SubDerecho);
            }
            else // La llave podría encontrarse en el subarbol IZQUIERDO.
            {
                this.Remover(llave, ref raizActual.SubIzquierdo);
            }
        }

        /// <summary>
        /// Función interna que funciona como el intercambio entre un nodo y el valor con mayor denominación que se encuentra en el subárbol IZQUIERDO.
        /// </summary>
        /// <param name="raizActual">Nodo que será removido sustituyéndose por el valor más grande del subarbol IZQUIERDO.</param>
        /// <returns></returns>
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
                bool validacion = true;
                while(Auxiliar!= null && validacion)
                {
                    if(Auxiliar.SubDerecho.SubDerecho == null)
                    {
                        if(Auxiliar.SubDerecho.SubIzquierdo != null)
                        {
                            Mayor = Auxiliar.SubDerecho;
                            Auxiliar.SubDerecho = Auxiliar.SubDerecho.SubIzquierdo;
                            validacion = false;
                        }
                        else
                        {
                            Mayor = Auxiliar.SubDerecho;
                            Auxiliar.SubDerecho = null;
                            validacion = false;
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

        //Función que se invoca para encontrar un elemento en el arbol a través de una llave.
        public T Encontrar(string llave)
        {
            return this.Encontrar(ref this.Raiz, llave);
        }

        /// <summary>
        /// Función que devuelve el elemento que corresponde a la llave solicitada para la búsqueda.
        /// </summary>
        /// <param name="raizActual">Nodo actual utilizado para recorrer el arbol es búsqueda del valor asociado a la llave.</param>
        /// <param name="llave">Valor asociado al elemento que se está buscando.</param>
        /// <returns></returns>
        T Encontrar(ref NodoAVL<T> raizActual, string llave)
        {
            if (raizActual == null) // No se encontró el valor correspondiente a la llave.
            {
                return default(T);
            }
            else if (this.Comparador2(llave, raizActual.Valor) == 0) // El elemento que corresponde a la llave se encontró en la raiz actual.
            {
                return raizActual.Valor;
            }
            else if (this.Comparador2(llave, raizActual.Valor) > 0) // EL elemento que corresponde a la llave podría encontrarse en el subarbol DERECHO.
            {
                return this.Encontrar(ref raizActual.SubDerecho, llave);
            }
            else // El elemento que corresponde a la llave podría encontrarse en el subarbol IZQUIERDO.
            {
                return this.Encontrar(ref raizActual.SubIzquierdo, llave);
            }
        }

        /// <summary>
        /// Método para la lectura del árbol que representa el recorrido INORDER.
        /// </summary>
        /// <param name="raizActual">Nodo auxiliar para realizar el recorrido del arbol.</param>
        /// <param name="cola">Cola donde se almacenan los datos de forma lineal para IEnumerable.</param>
        public void Leer(NodoAVL<T> raizActual, ref Cola<T> cola)
        {
            if (raizActual.SubDerecho == null && raizActual.SubIzquierdo == null) // Lee el valor de la raiz actual.
            {
                cola.Encolar(raizActual.Valor);
            }
            else if (raizActual.SubDerecho == null) // Se pasa al subarbol IZQUIERDO y luego lee el valor de la raiz actual.
            {
                this.Leer(raizActual.SubIzquierdo, ref cola);
                cola.Encolar(raizActual.Valor);
            }
            else if (raizActual.SubIzquierdo == null) // Lee el valor de la raiz actual y luego se para al subarbol DERECHO.
            {
                cola.Encolar(raizActual.Valor);
                this.Leer(raizActual.SubDerecho, ref cola);
            }
            else // Se pasa el subarbol IZQUIERO, lee el valor de la raiz actual y luego se pasa al subarbol derecho.
            {
                this.Leer(raizActual.SubIzquierdo, ref cola);
                cola.Encolar(Raiz.Valor);
                this.Leer(Raiz.SubDerecho, ref cola);
            }
        }

        // Función de invocación para verificación la cantidad de elementos que existen con cierta característica.
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
            if (raizActual.SubDerecho == null && raizActual.SubIzquierdo == null) // Verifica el valor de la raiz actual con la característica enviada.
            {
                if (unPredicado(raizActual.Valor)) // El valor de la raiz actual cumple con la característica.
                {
                    contador++;  
                }              
            }
            else if (raizActual.SubDerecho == null) // Se mueve al subarbol IZQUIERDO y verifica el valor de la raiz actual.
            {
                //verifica Izquierdo y luego Raiz
                Verificacion(unPredicado, raizActual.SubIzquierdo, ref contador); 
                if (unPredicado(raizActual.Valor)) // El valor de la raiz actual cumple con la característica.
                {
                    contador++;
                }
            }
            else if (raizActual.SubIzquierdo == null) // Verifica el valor de la raiz actual y se mueve al subarbol IZQUIERDO.
            {
                //verifica Raiz y luego derecho
                if (unPredicado(raizActual.Valor)) // EL valor de la raiz actual cumple con la característica.
                {
                    contador++;
                }
                Verificacion(unPredicado, raizActual.SubDerecho, ref contador);
            }
            else // Se mueve al subarbol IZQUIERDO, verifica valor de la raiz actual y se mueve al subarbol DERECHO
            {
                Verificacion(unPredicado, raizActual.SubIzquierdo, ref contador);
                if (unPredicado(raizActual.Valor)) // El valor de la raiz actual cumple con la característica.
                {
                    contador++;
                }
                Verificacion(unPredicado, raizActual.SubDerecho, ref contador);
            }
        }

        // Método para el Enumerable y que se puede visualizar el arbol.
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
