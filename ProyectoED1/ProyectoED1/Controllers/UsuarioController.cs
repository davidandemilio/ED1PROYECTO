using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
using TDA;
using System.Net;
namespace ProyectoED1.Controllers
{
    public class UsuarioController : Controller
    {
        DefaultConnection<Filme,string> db = DefaultConnection<Filme, string>.getInstance;
        // GET: Usuario

        
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
     
        public ActionResult Details(string id)
        {
            Usuario usuario_buscado = db.usuarios.buscar(id);
            db.usuariologeado = usuario_buscado;
            return View(usuario_buscado);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {

            return View();
        }
        public void asignar_comparador(elemento<Filme, string> actual)
        {
            actual.comparador = comparadorfilmes;
        }
        public ActionResult logout()
        {
            db.usuarios.buscar(db.usuariologeado.username).WatchList = db.usuariologeado.WatchList;
            db.usuarios.buscar(db.usuariologeado.username).WatchList_lista = db.usuariologeado.WatchList_lista;

            db.usuariologeado = null;

            return RedirectToAction("Index","Login");
        }
        public ActionResult Agregar(string id)
        {
            if (db.usuariologeado.WatchList.existe(id))
            {
                Response.Write("<script>alert('Ya tiene esta pelicula en su watchlist');</script>");
                return RedirectToAction("Catalogo_user", "Filme");
                
            }
            else {
                db.usuariologeado.WatchList_lista.Clear();
                db.usuariologeado.WatchList.recorrer(asignar_comparador);
                db.catalogonombre.recorrer(asignar_comparador);
                db.usuariologeado.WatchList.insertar(db.catalogonombre.buscar(id), db.catalogonombre.buscar(id).Nombre);
                db.usuariologeado.WatchList.recorrer(pasar_a_lista);
                db.carga.CrearJsonWatchlist(db.usuariologeado.WatchList_lista, db.usuariologeado.username);
                return RedirectToAction("Details", new { id = db.usuariologeado.username });
            }
           
        }
        public static int comparadorfilmes(string actual, string Other)
        {
            return Other.CompareTo(actual);
        }
        public void pasar_a_lista(elemento<Filme, string> actual)
        {
            db.usuariologeado.WatchList_lista.Add(actual.valor);
        }
        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "nombre,apellido,edad,username,password")]Usuario user)
        {
           
            try
            {
                if (db.usuarios.existe(user.username))
                {
                    Response.Write("<script>alert('usuario ya existe');</script>");
                    return View();
                }
                else {
                    db.usuarios.insertar(user,user.username);
                    db.carga.obtenerArbolB(db.usuarios);
                    db.carga.CrearJsonUsuarios(user, Server);
                    Response.Write("<script>alert('usuario creado');</script>");
                    return View();
                }

              
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        

 
        public ActionResult Deletewa(string id)
        {
            try
            {
                Filme filmebuscado = db.catalogonombre.buscar(id);
                // TODO: Add delete logic here
                db.usuariologeado.WatchList.eliminar(filmebuscado.Nombre);
                
                db.filmes_lista.Clear();
                
                db.usuariologeado.WatchList.recorrer(asignar_comparador);

                db.usuariologeado.WatchList.recorrer(pasar_a_lista);
                return RedirectToAction("Details", new { id = db.usuariologeado.username });
            }
            catch
            {
                return RedirectToAction("Details", new { id = db.usuariologeado.username });
            }
        }
    }
}
