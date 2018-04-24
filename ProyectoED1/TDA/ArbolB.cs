using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;
namespace TDA
{
    public class ArbolB<T,K>
    {
        public Nodo<T, K> raiz { get; set; }
        public int grado { get; set; }
        public ComparadorNodosDelegate<K>  comparador_;
        private int noelementos;
        private T buscado;
        private Nodo<T, K> target;
        private K keynodotarget;
        private K key_target;

        List<elemento<T, K>> almacenador = new List<elemento<T, K>>();

        /// <summary>
        /// constructor arbol
        /// </summary>
        /// <param name="_grado"> grado del arbol</param>
        /// <param name="llave"> llave inicial</param>
        /// <param name="comparador"> delegate que comapara</param>
        public ArbolB(int _grado,K llave,ComparadorNodosDelegate<K> comparador) {
            grado = _grado;
            raiz = new Nodo<T, K>(llave);
            comparador_ = comparador;
            raiz.asignar_listas(comparador_);
            
           
          
         
        }
        /// <summary>
        /// inserta en raiz si se puede si no llama al metodo intsertar en hojas
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="llave"></param>
        public void insertar(T valor, K llave) {

            elemento<T, K> elemento_insertar = new elemento<T, K>(valor, llave,comparador_);

            if (raiz.hijos.Count==0) {

              
                raiz.elementos.Add(llave,elemento_insertar);
                raiz.llave = raiz.elementos.ElementAt(0).Key;

                if (raiz.elementos.Count == grado)
                {
                    separar(raiz);
                }
            }
            else
            {
                insertar_hojas(valor, llave,raiz);
            }
        }

        /// <summary>
        /// recorre el aorbol hasta que busca la llave a eliminar
        /// </summary>
        /// <param name="llave"> llave que se desea eliminar</param>
        public void eliminar(K llave)
        {

            almacenador.Clear();

            guardar(raiz, llave);
            Clear(raiz);

            regresar(raiz);

        }

        /// <summary>
        /// almacena la lista filtrada cuando se elimina
        /// </summary>
        /// <param name="nodo_start"> nodo desde donde se empezara a insertar</param>
        public void regresar(Nodo<T,K> nodo_start) {
            foreach (elemento<T,K> elemento in almacenador) {

                insertar(elemento.valor,elemento.llave);
            }
        }

        /// <summary>
        /// guarda una lista de elementos menos el que se dese aliminar
        /// </summary>
        /// <param name="nodo_start"> nodo desde don empezara  aevaluar</param>
        /// <param name="llave"> llave a filtrar</param>
        public void guardar(Nodo<T,K> nodo_start,K llave)
        {
            



            if (nodo_start != null)
            {
                for (int i = 0; i <= nodo_start.elementos.Count - 1; i++)
                {
                    if (nodo_start.hijos.Count != 0)
                    {
                        guardar(nodo_start.hijos.ElementAt(i).Value,llave);

                    }
                    if (nodo_start.elementos.ElementAt(i).Value.CompareTo(llave)!=0){

                        almacenador.Add(nodo_start.elementos.ElementAt(i).Value);
                    }
                 


                }
                if (nodo_start.hijos.Count != 0)
                {

                    guardar(nodo_start.hijos.ElementAt(nodo_start.hijos.Count - 1).Value,llave);
                }


            }

        }

        /// <summary>
        /// limipia todo el arbol
        /// </summary>
        /// <param name="nodo_start"></param>
        public  void Clear(Nodo<T,K> nodo_start) {

            raiz.elementos.Clear();
            raiz.hijos.Clear();
        }

       


     





        /// <summary>
        /// buca una llave mandando a llamar al buscador interno 
        /// </summary>
        /// <param name="llave">llave que se desea buscar</param>
        /// <returns></returns>
        public T  buscar(K llave)
        {
            buscado = default(T);
            target = null;
            key_target = default(K);
            keynodotarget = default(K);
            buscar_interno(raiz, llave);
          
            return buscado;
        }

        /// <summary>
        /// verifica si un elemento esta dentro del arbol
        /// </summary>
        /// <param name="llave"> llave a aevaluar si existe</param>
        /// <returns> true si existe false si no</returns>
        public bool existe(K llave)

        {
           
            buscar( llave);
            if (buscado == null)
            {
                return false;
            }
            else {
                return true;
            }
        }


        /// <summary>
        /// recorre el arbol recursivamente hasta que encuentra la llave deseada
        /// </summary>
        /// <param name="nodo_start">nodo desde donde se empezara a buscar</param>
        /// <param name="llave">llave que se desea encontrar</param>
        public void buscar_interno(Nodo<T, K> nodo_start, K llave)
        {
            for (int j = 0; j <= nodo_start.elementos.Count - 1; j++)
            {
                if (nodo_start.elementos.ElementAt(j).Value.CompareTo(llave)==0)
                {
                    buscado= nodo_start.elementos.ElementAt(j).Value.valor;

                    target = nodo_start;
                    key_target = nodo_start.elementos.ElementAt(j).Key;
                    if (nodo_start.padre != null)
                    {
                        keynodotarget = nodo_start.padre.hijos.ElementAt(nodo_start.padre.hijos.IndexOfValue(nodo_start)).Key;

                    }
                    else {
                        key_target= nodo_start.llave;
                    }
                    
                }
            }
          
            for (int j = 0; j <= nodo_start.hijos.Count - 1; j++)
            {
                buscar_interno(nodo_start.hijos.ElementAt(j).Value,llave);
               
            }

           

        }


        /// <summary>
        /// manda  allamar al recorrer interno
        /// </summary>
        /// <param name="recorrido">que hara con cada elemento del arbol</param>
        public void recorrer( RecorridoDelegate<T, K> recorrido)
        {
            recorrer_interno(raiz,recorrido);


        }
        /// <summary>
        /// recorre cada elmento del arbol recursivamente
        /// </summary>
        /// <param name="nodo_start">donde empezara el recorrido</param>
        /// <param name="recorrido">que hara con cada elemento del arbol</param>
        public void recorrer_interno(Nodo<T, K> nodo_start,RecorridoDelegate<T,K> recorrido)
        {
            if (nodo_start != null) {
                for (int i = 0; i <= nodo_start.elementos.Count-1 ; i++)
                {
                    if (nodo_start.hijos.Count != 0)
                    {
                        recorrer_interno(nodo_start.hijos.ElementAt(i).Value, recorrido);

                    }
                    
                    recorrido(nodo_start.elementos.ElementAt(i).Value);
                    

                }
                if (nodo_start.hijos.Count != 0) {

                    recorrer_interno(nodo_start.hijos.ElementAt(nodo_start.hijos.Count - 1).Value, recorrido);
                }


            }



        }

        private void contar_interno(Nodo<T,K> nodo_start)
        {
            noelementos = nodo_start.elementos.Count+noelementos;
            for (int j = 0; j <= nodo_start.hijos.Count - 1; j++) {
                contar_interno(nodo_start.hijos.ElementAt(j).Value);
            }
        }

        /// <summary>
        /// cuenta el numero de elementos del arbol
        /// </summary>
        /// <returns></returns>
        public int contar() {
            contar_interno(raiz);
            return noelementos;
        }


        /// <summary>
        /// metodo que separa el nodo cuando rebasa su limite
        /// </summary>
        /// <param name="actual"> nodo que se partira</param>
        public void separar(Nodo<T, K> actual)
        {

            Nodo<T, K> derecho;
           
          
            Nodo<T, K> izquierdo = new Nodo<T, K>(actual.elementos.ElementAt(0).Key);
            izquierdo.asignar_listas(comparador_);
            Nodo<T, K> padre_actual = actual.padre;
          

            if (grado % 2 == 0)
            {
                derecho = new Nodo<T, K>(actual.elementos.ElementAt((grado / 2)).Key);
                derecho.asignar_listas(comparador_);
                if (actual.hijos.Count > 0)
                {
                    for (int x = 0; x <= (grado / 2); x++)
                    {
                        if (x <= (grado / 2) - 1)
                        {
                            izquierdo.hijos.Add(actual.hijos.ElementAt(x).Key, actual.hijos.ElementAt(x).Value);
                            izquierdo.hijos.ElementAt(x).Value.padre = izquierdo;
                         

                        }
                        derecho.hijos.Add(actual.hijos.ElementAt((grado / 2) + x).Key, actual.hijos.ElementAt((grado / 2) + x).Value);
                        derecho.hijos.ElementAt(x).Value.padre = derecho;
                       

                    }
                    actual.hijos.Clear();
                }
                for (int x = 0; x <= (grado / 2) - 1; x++)
                {

                    izquierdo.elementos.Add(actual.elementos.ElementAt(x).Key, actual.elementos.ElementAt(x).Value);

                    
                    derecho.elementos.Add(actual.elementos.ElementAt((grado / 2) + x).Key, actual.elementos.ElementAt((grado / 2) + x).Value);
                    
                   

                }
                actual.elementos.Clear();

                
                
            }
            else
            {
                derecho = new Nodo<T, K>(actual.elementos.ElementAt((grado / 2) + 1).Key);
                derecho.asignar_listas(comparador_);

                if (actual.hijos.Count > 0)
                {
                    for (int x = 0; x <= (grado / 2); x++)
                    {

                        izquierdo.hijos.Add(actual.hijos.ElementAt(x).Key, actual.hijos.ElementAt(x).Value);
                        izquierdo.hijos.ElementAt(x).Value.padre = izquierdo;
                       

                        derecho.hijos.Add(actual.hijos.ElementAt((grado / 2) + x+1).Key, actual.hijos.ElementAt((grado / 2) + x+1).Value);
                        derecho.hijos.ElementAt(x).Value.padre = derecho;
                       


                    }

                    actual.hijos.Clear();
                }

                for (int x = 0; x <= (grado / 2); x++)
                {

                    izquierdo.elementos.Add(actual.elementos.ElementAt(x).Key, actual.elementos.ElementAt(x).Value);

                   
                    if (x <= (grado / 2) - 1)
                    {
                        derecho.elementos.Add(actual.elementos.ElementAt((grado / 2) + x + 1).Key, actual.elementos.ElementAt((grado / 2) + x + 1).Value);

                      
                    }

                }
                actual.elementos.Clear();

            }


            if (actual.padre != null)
            {


                if (grado % 2 == 0)
                {
                    padre_actual.elementos.Add(izquierdo.elementos.ElementAt((grado / 2) - 1).Key, izquierdo.elementos.ElementAt((grado / 2) - 1).Value);
                    izquierdo.elementos.RemoveAt((grado / 2) - 1);
                }
                else {

                    padre_actual.elementos.Add(izquierdo.elementos.ElementAt((grado / 2)).Key, izquierdo.elementos.ElementAt((grado / 2)).Value);
                    izquierdo.elementos.RemoveAt((grado / 2));

                }

                padre_actual.hijos.Remove(actual.llave);
                padre_actual.hijos.Add(izquierdo.llave, izquierdo);
                padre_actual.hijos.Add(derecho.llave, derecho);
                izquierdo.padre = padre_actual;
                derecho.padre = padre_actual;
               
                if (padre_actual.elementos.Count == grado) {
                    separar(padre_actual);
                }
            }
            else {

                if (grado % 2 == 0)
                {
                    actual.elementos.Add(izquierdo.elementos.ElementAt((grado / 2) - 1).Key, izquierdo.elementos.ElementAt((grado / 2) - 1).Value);
                    actual.llave = actual.elementos.ElementAt(0).Key;
                    izquierdo.elementos.RemoveAt((grado / 2) - 1);
                }
                else
                {

                    actual.elementos.Add(izquierdo.elementos.ElementAt((grado / 2)).Key, izquierdo.elementos.ElementAt((grado / 2)).Value);
                    actual.llave = actual.elementos.ElementAt(0).Key;
                    izquierdo.elementos.RemoveAt((grado / 2));

                }

                actual.hijos.Add(izquierdo.llave, izquierdo);
                actual.hijos.Add(derecho.llave, derecho);
                izquierdo.padre = actual;
                derecho.padre = actual;

                if (actual.elementos.Count == grado)
                {
                    separar(actual);
                }
            }
            


        }

        /// <summary>
        /// inserta elementos en los nodo hoja
        /// </summary>
        /// <param name="valor">elementoa  insertar</param>
        /// <param name="llave"> llave del elemnto</param>
        /// <param name="nod">nodo donde se emepezara a insertar</param>
        public void insertar_hojas(T valor, K llave,Nodo<T,K> nod)
        {
            if (nod.hijos.Count == 0)
            {
                elemento<T, K> elemento_aniadir = new elemento<T, K>(valor, llave, comparador_);
                nod.elementos.Add(llave, elemento_aniadir);
                Nodo<T, K> temp = nod;
               

                nod.padre.hijos.Remove(nod.llave);
                nod.padre.hijos.Add(nod.elementos.ElementAt(0).Key, temp);
              nod.llave = nod.elementos.ElementAt(0).Key;
              
                if (nod.elementos.Count == grado)
                {
                    separar(nod);
                }
            }
            else {
                if (nod.elementos.ElementAt(0).Value.CompareTo(llave) < 0)
                {
                    insertar_hojas(valor, llave, nod.hijos.ElementAt(0).Value);
                    return;
                }
                for (int j = 0; j <= nod.elementos.Count - 2; j++)
                {
                    if ((nod.elementos.ElementAt(j).Value.CompareTo(llave)) > 0 && (nod.elementos.ElementAt(j + 1).Value.CompareTo(llave) < 0))
                    {
                        insertar_hojas(valor, llave, (nod.hijos.ElementAt(j + 1).Value));
                        return;
                    }

                }
                if (nod.elementos.ElementAt(nod.elementos.Count - 1).Value.CompareTo(llave) > 0)
                {
                    insertar_hojas(valor, llave, nod.hijos.ElementAt(nod.hijos.Count - 1).Value);
                    return;
                }
            }

            

        }
    }
}
