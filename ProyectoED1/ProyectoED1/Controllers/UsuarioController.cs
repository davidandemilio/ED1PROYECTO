using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
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
            return View(usuario_buscado);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {

            return View();
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
