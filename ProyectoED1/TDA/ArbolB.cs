﻿using System;
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
     

       public ArbolB(int _grado,K llave,ComparadorNodosDelegate<K> comparador) {
            grado = _grado;
            raiz = new Nodo<T, K>(llave);
            comparador_ = comparador;
        }
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

        public void eliminar()
        {

        }
     

        public void separar(Nodo<T, K> actual)
        {

            Nodo<T, K> derecho;
          
            Nodo<T, K> izquierdo = new Nodo<T, K>(actual.elementos.ElementAt(0).Key);
            Nodo<T, K> padre_actual = actual.padre;

            if (grado % 2 == 0)
            {
                derecho = new Nodo<T, K>(actual.elementos.ElementAt((grado / 2)).Key);
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
