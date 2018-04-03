using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
using ProyectoED1.Models;
namespace ProyectoED1.Models
{
    public class Usuario
    {

        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public ArbolB<Filme,string> WatchList{ get; set; }
    }
}