﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
namespace ProyectoED1.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        DefaultConnection db = DefaultConnection.getInstance;


    

        public ActionResult verificar(string user,string pass)
        {
            Usuario usuario = db.usuarios.Find(x => x.username.Equals(user) && x.password.Equals(pass));
            if (usuario != null)
            {
                UsuarioController uscontro = new UsuarioController();



                return RedirectToAction("Details", "Usuario", new { id = usuario.username });
   
            }
            else {

                return View("Index");
            }
          
           
        }


    }
}