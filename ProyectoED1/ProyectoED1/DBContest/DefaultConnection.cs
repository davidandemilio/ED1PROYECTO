using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoED1.Models;
using TDA;
using TDA.Interfaces;
using ProyectoED1.Controllers;

namespace ProyectoED1.DBContest
{
    public class DefaultConnection<T,K>
    {
        private static volatile DefaultConnection<T,K> Instance;
        private static object syncRoot = new Object();
    
       
        public ArbolB<Usuario, string> usuarios = new ArbolB<Usuario, string> (3,"", comparadorstring);
        public ArbolB<Filme, string> catalogonombre = new ArbolB<Filme, string>(3,"", comparadorstring);
        public ArbolB<Filme, Filme> catalogogenero = new ArbolB<Filme, Filme>(3, null, comparadorgeneros);
        public ArbolB<Filme, Filme> catalogoanio = new ArbolB<Filme, Filme>(3, null, comparadoranio);
        public CargaArchivo<Usuario, string> carga = new CargaArchivo<Usuario, string>();


        public List<string> Ids = new List<string>();
        public List<Filme> filmes_lista = new List<Filme>();
        public int IDActual { get; set; }
       public Usuario usuariologeado;

        public static int comparadorgeneros(Filme actual, Filme Other)
        {
            if (Other.genero.CompareTo(actual.genero) == 0)
            {

              return  Other.Nombre.CompareTo(actual.Nombre);
            }
            else
            {

                return Other.genero.CompareTo(actual.genero);
            }

        }
        public static int comparadoranio(Filme actual, Filme Other)
        {
            if (Other.anio.CompareTo(actual.anio) == 0)
            {

                return Other.Nombre.CompareTo(actual.Nombre);
            }
            else
            {

                return Other.anio.CompareTo(actual.anio);
            }
        }

        public static int comparadorstring(string actual, string other)
        {
            return other.CompareTo(actual);
        }



        public DefaultConnection()
        {
            if (usuarios.contar() == 0)
            {
                IDActual = 0;
            }else
            {
                IDActual = usuarios.contar() - 1;
            }
        }

        public static DefaultConnection<T,K> getInstance
        {

            get
            {

                if (Instance == null)
                {
                    lock (syncRoot)
                    {

                        if (Instance == null)
                        {
                            Instance = new DefaultConnection<T,K>();
                        }
                    }
                }
                return Instance;
            }
        }


    }
}
