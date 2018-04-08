using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
using ProyectoED1.Controllers;
namespace ProyectoED1.Controllers
{
    public class FilmeController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Filme
        public ActionResult Index()
        {
            return View();
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
