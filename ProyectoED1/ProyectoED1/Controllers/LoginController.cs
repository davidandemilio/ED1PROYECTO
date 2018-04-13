using System;
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
        DefaultConnection<Filme,string> db = DefaultConnection<Filme, string>.getInstance;




        public ActionResult verificar(string user, string pass)
        {
            if(user=="admin"&& user == "admin")
            {
                return RedirectToAction("Index", "Filme");

            } else {
                Usuario usuario = null;
                Usuario buscado = db.usuarios.buscar(user);
                if (buscado != null)
                {
                    if (buscado.username == user && buscado.password == pass)
                    {
                        usuario = buscado;
                    }
                }

                if (usuario != null)
                {
                    UsuarioController uscontro = new UsuarioController();



                    return RedirectToAction("Details", "Usuario", new { id = usuario.username });

                }
                else
                {

                    return View("Index");
                }
            }
           
        }


    }
}
