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



        //Se le pasa la contraseña y usuario al metodo para verificar
        public ActionResult verificar(string user, string pass)
        {
            //Si la contraseña y usuario son admin, se logea como administrador para agregar los filmes
            if(user=="admin"&& pass == "admin")
            {
                db.usuariologeado = null;
                return RedirectToAction("Index", "Filme");
            

            } else {
                //Se verifica si el usuario que se ingreso existe.
                Usuario usuario = null;
                Usuario buscado = db.usuarios.buscar(user);
                if (buscado != null)
                {
                    if (buscado.username == user && buscado.password == pass)
                    {
                        usuario = buscado;
                    }
                }
                //EL usuario fue encontrado y se muestran los detalles de ese usuario
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
