using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
using TDA;
using TDA.Interfaces;
using System.IO;
using Newtonsoft.Json;
using directorios = System.IO;
using System.Security.Permissions;

namespace ProyectoED1.Controllers
{
    public class CargaArchivo<T, K>
    {
        ArbolB<T, K> arbolB_Usuario;
        string JsonUsuarios;
        string JsonNombres;
        string JsonCatalogo;

        public static int comparadorusuarios(string actual, string other)
        {
            return other.CompareTo(actual);
        }

        public void obtenerArbolB(ArbolB<T, K> _ArbolB)
        {
            arbolB_Usuario = _ArbolB;
        }

        public void CrearJsonNombre(Filme filme)
        {
            string archivoNombre = JsonConvert.SerializeObject(filme);
            JsonNombres = JsonNombres + ";" + archivoNombre;
            string NombreArchivo = @"C:\Users\Public\catalogoNombre.json";
            string path = Path.GetPathRoot(NombreArchivo);
            FileIOPermission permiso = new FileIOPermission(FileIOPermissionAccess.Write, path);

            foreach (string f in Directory.GetFiles(@"C:\Users\Public"))
            {
                if (NombreArchivo == f)
                {
                    File.Delete(@"C:\Users\Public\catalogoNombre.json");
                }
            }
            try
            {
                permiso.Demand();
                StreamWriter escritor = new StreamWriter(NombreArchivo);
                escritor.WriteLine(JsonNombres);
                escritor.Close();
            }
            catch
            {
                throw new Exception("Acceso denegado para el disco");
            }
        }

        public void CrearJsonUsuarios(T _user, HttpServerUtilityBase SERVIDOR)
        {
            string archivoJson;
            archivoJson = JsonConvert.SerializeObject(_user);
            string[] arreglo = archivoJson.Split(',');
            archivoJson = "{";
            for (int i = 19; i < 23; i++)
            {
                archivoJson = archivoJson + arreglo[i] + ",";
            }
            archivoJson = archivoJson + arreglo[23] + "} ;";
            JsonUsuarios = JsonUsuarios + archivoJson;
            string NombreArchivo = @"C:\Users\Public\Usuarios.json";
            string path = Path.GetPathRoot(NombreArchivo);
            FileIOPermission permiso = new FileIOPermission(FileIOPermissionAccess.Write, path);

            foreach (string f in Directory.GetFiles(@"C:\Users\Public"))
            {
                if (NombreArchivo == f)
                {
                    File.Delete(@"C:\Users\Public\Usuarios");
                }
            }
            try
            {
                permiso.Demand();
                StreamWriter escritor = new StreamWriter(NombreArchivo);
                escritor.WriteLine(JsonUsuarios);
                escritor.Close();
            }
            catch
            {
                throw new Exception("Acceso denegado para el disco");
            }
        }

        public ArbolB<Usuario, string> CargajsonUsuario(HttpPostedFileBase archivo, HttpServerUtilityBase SERVIDOR)
        {
            ArbolB<Usuario, string> arbol_a_insertar = new ArbolB<Usuario, string>(3, "", comparadorusuarios);
            string pathArchivo = string.Empty;
            if (archivo != null)
            {

                string path = SERVIDOR.MapPath("~/Cargas/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                pathArchivo = path + Path.GetFileName(archivo.FileName);
                string extension = Path.GetExtension(archivo.FileName);
                archivo.SaveAs(pathArchivo);
                string userJSON = directorios.File.ReadAllText(pathArchivo);
                string[] _Usuarios = userJSON.Split(';');
                Usuario _user = new Usuario();
                for (int i = 0; i < _Usuarios.Length - 1; i++)
                {
                    _user = JsonConvert.DeserializeObject<Usuario>(_Usuarios[i]);
                    arbol_a_insertar.insertar(_user, _user.username);
                }
                return arbol_a_insertar;
            }
            return null;
        }

        public ArbolB<Filme, string> CargajsonCatalogo(HttpPostedFileBase archivo, HttpServerUtilityBase SERVIDOR)
        {
            ArbolB<Filme, string> arbol_a_insertar = new ArbolB<Filme, string>(3, "", comparadornombres);
            Filme filme;
            string pathArchivo = string.Empty;
            if (archivo != null)
            {
                string path = SERVIDOR.MapPath("~/Cargas/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                pathArchivo = path + Path.GetFileName(archivo.FileName);
                string extension = Path.GetExtension(archivo.FileName);
                archivo.SaveAs(pathArchivo);
                string archivoJSON = directorios.File.ReadAllText(pathArchivo);
                JsonCatalogo = archivoJSON;
                string[] filmes = archivoJSON.Split(';');
                for (int i = 1; i < filmes.Length; i++)
                {
                    filme = JsonConvert.DeserializeObject<Filme>(filmes[i]);
                    elemento<Filme, string> nuevo_filme_no = new elemento<Filme, string>(filme, filme.Nombre, comparadornombres);
                    arbol_a_insertar.recorrer(asignar_comparador_nombre);
                    arbol_a_insertar.insertar(nuevo_filme_no.valor, nuevo_filme_no.valor.Nombre);
                }

                return arbol_a_insertar;
            }
            return null;
        }

        public ArbolB<Filme, Filme> CargajsonCatalogoGenero()
        {
            ArbolB<Filme, Filme> arbol_a_insertar = new ArbolB<Filme, Filme>(3, null, comparadorgeneros);
            Filme filme;

            string[] filmes = JsonCatalogo.Split(';');
            for (int i = 1; i < filmes.Length; i++)
            {
                filme = JsonConvert.DeserializeObject<Filme>(filmes[i]);
                elemento<Filme, Filme> nuevo_filme_no = new elemento<Filme, Filme>(filme, filme, comparadorgeneros );
                arbol_a_insertar.recorrer(asignar_comparador_genero);
                arbol_a_insertar.insertar(nuevo_filme_no.valor, nuevo_filme_no.valor);
            }

            return arbol_a_insertar;
        }

        public ArbolB<Filme, Filme> CargajsonCatalogoAño()
        {
            ArbolB<Filme, Filme> arbol_a_insertar = new ArbolB<Filme, Filme>(3, null, comparadoranio);
            Filme filme;

            string[] filmes = JsonCatalogo.Split(';');
            for (int i = 1; i < filmes.Length; i++)
            {
                filme = JsonConvert.DeserializeObject<Filme>(filmes[i]);
                elemento<Filme, Filme> nuevo_filme_no = new elemento<Filme, Filme>(filme, filme, comparadoranio);
                arbol_a_insertar.recorrer(asignar_comparador_anio);
                arbol_a_insertar.insertar(nuevo_filme_no.valor, nuevo_filme_no.valor);
            }

            return arbol_a_insertar;
        }

        public void CrearJsonWatchlist(List<Filme> _watchlist, string usuario)
        {
            string UsuarioWatchlist = JsonConvert.SerializeObject(_watchlist);

            string NombreArchivo = @"C:\Users\Public\" + usuario + "_Watchlist.json";
            string path = Path.GetPathRoot(NombreArchivo);
            FileIOPermission permiso = new FileIOPermission(FileIOPermissionAccess.Write, path);

            foreach (string f in Directory.GetFiles(@"C:\Users\Public"))
            {
                if (NombreArchivo == f)
                {
                    File.Delete(@"C:\Users\Public\Watchlist.json");
                }
            }
            try
            {
                permiso.Demand();
                StreamWriter escritor = new StreamWriter(NombreArchivo);
                escritor.WriteLine(UsuarioWatchlist);
                escritor.Close();
            }
            catch
            {
                throw new Exception("Acceso denegado para el disco");
            }
        }

        public int comparadornombres(string actual, string Other)
        {
            return Other.CompareTo(actual);
        }
        public int comparadorgeneros(Filme actual, Filme Other)
        {

            if (Other.genero.CompareTo(actual.genero) == 0)
            {

                return Other.Nombre.CompareTo(actual.Nombre);
            }
            else
            {

                return Other.genero.CompareTo(actual.genero);
            }

        }
        public int comparadoranio(Filme actual, Filme Other)
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
        public void asignar_comparador_nombre(elemento<Filme, string> actual)
        {
            actual.comparador = comparadornombres;
        }
        public void asignar_comparador_genero(elemento<Filme, Filme> actual)
        {
            actual.comparador = comparadorgeneros;
        }
        public void asignar_comparador_anio(elemento<Filme, Filme> actual)
        {
            actual.comparador = comparadoranio;
        }
    }
}