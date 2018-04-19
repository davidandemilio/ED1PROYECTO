using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
using ProyectoED1.Controllers;
using TDA;
namespace ProyectoED1.Controllers
{
    public class FilmeController : Controller
    {
        DefaultConnection<Filme,string> db = DefaultConnection<Filme, string>.getInstance;
        // GET: Filme
        public ActionResult Index()
        {
            return View(db.filmes_lista.ToList());
        }

        public ActionResult Catalogo_user()
        {
            return View(db.filmes_lista.ToList());
        }

        public void pasar_a_lista(elemento<Filme,string> actual)
        {
            db.filmes_lista.Add(actual.valor);
        }

        public void asignar_comparador(elemento<Filme, string> actual)
        {
            actual.comparador = comparadorfilmes;
        }

        // GET: Filme/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Filme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "tipo,Nombre,anio,genero")]Filme partido)
        {
         

            try
            {
                db.filmes_lista.Clear();
                db.catalogo.recorrer(asignar_comparador);

                elemento<Filme, string> nuevo_filme = new elemento<Filme, string>(partido,partido.Nombre,comparadorfilmes);
               
                db.catalogo.insertar(nuevo_filme.valor,nuevo_filme.valor.Nombre);

                
               
                db.catalogo.recorrer(pasar_a_lista);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public  int comparadorfilmes(string actual,string Other) {
           return Other.CompareTo(actual);
        }

        // GET: Filme/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Filme/Edit/5
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

        // GET: Filme/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Filme/Delete/5
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
