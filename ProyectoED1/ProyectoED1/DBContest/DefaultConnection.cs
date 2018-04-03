using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoED1.Models;
using TDA;
namespace ProyectoED1.DBContest
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        public List<Usuario> usuarios = new List<Usuario>();
        public ArbolB<Filme, string> catalogo = new ArbolB<Filme, string>();

        public List<string> Ids = new List<string>();
        public int IDActual { get; set; }

     


        private DefaultConnection()
        {
            if (usuarios.Count == 0)
            {
                IDActual = 0;
            }else
            {
                IDActual = usuarios.Count - 1;
            }
        }

        public static DefaultConnection getInstance
        {

            get
            {

                if (Instance == null)
                {
                    lock (syncRoot)
                    {

                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }


    }
}
