using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoED1.DBContest;
using ProyectoED1.Models;
using ProyectoED1.Controllers;
using TDA;
using System.Net;
namespace ProyectoED1.Controllers
{
    public class FilmeController : Controller
    {
        DefaultConnection<Filme,string> db = DefaultConnection<Filme, string>.getInstance;
        
        string seleccionorden="nombre";
        // GET: Filme
        public ActionResult Index()
        {
            
           
            return View(db.filmes_lista.ToList());
          
        }

        public ActionResult Catalogo_user()
        {
            return View(db.filmes_lista.ToList());
        }
        //Funcion para buscar un usuario
        public ActionResult buscar(string buscado)
        {

            if (db.catalogonombre.buscar(buscado) != null) {
                db.filmes_lista.Clear();
                db.filmes_lista.Add(db.catalogonombre.buscar(buscado));
            }
          


            if (db.usuariologeado == null)
            {
                return RedirectToAction("Index");

            }
            else {

                return RedirectToAction("Catalogo_user");

            }
        }
        //Se agregan a una lista los filmes
        public void pasar_a_lista(elemento<Filme,string> actual)
        {
            db.filmes_lista.Add(actual.valor);
        }

       //Se pasan a una lista los valores por genero
        public void pasar_a_lista_gen(elemento<Filme, Filme> actual)
        {
            db.filmes_lista.Add(actual.valor);
        }
        //Se pasan a una lista los valores por año
        public void pasar_a_lista_int(elemento<Filme, Filme> actual)
        {
            db.filmes_lista.Add(actual.valor);
        }
        //Se le agrega una llave por nombre al filme
        public void asignar_comparador_nombre(elemento<Filme, string> actual)
        {
            actual.comparador = comparadornombres;
        }
        //Se le agrega una llave por genero
        public void asignar_comparador_genero(elemento<Filme, Filme> actual)
        {
            actual.comparador = comparadorgeneros;
        }
        //Se le agrega una llave por año
        public void asignar_comparador_anio(elemento<Filme, Filme> actual)
        {
            actual.comparador = comparadoranio;
        }
        //Se cambia el orden en el que se le es mostrada la watchlist al usuario
        public ActionResult cambiar_orden(string orden) {
            seleccionorden = orden;
            db.filmes_lista.Clear();
            if (orden == "genero")
            {

                db.catalogogenero.recorrer(pasar_a_lista_gen);

            }
            else if (orden == "nombre")
            {
                db.catalogonombre.recorrer(pasar_a_lista);

            }
            else if (orden == "año")
            {
                db.catalogoanio.recorrer(pasar_a_lista_int);

            }

            if (db.usuariologeado == null)
            {
                return RedirectToAction("Index");
            }
            else {


                return RedirectToAction("Catalogo_user");

            }

           
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
        public ActionResult Create([Bind(Include = "tipo,Nombre,anio,genero")]Filme filme)
        {
         

            //try
            //{
                if (!db.catalogonombre.existe(filme.Nombre))
                {
                    db.filmes_lista.Clear();
                    db.catalogonombre.recorrer(asignar_comparador_nombre);
                    db.catalogogenero.recorrer(asignar_comparador_genero);
                    db.catalogoanio.recorrer(asignar_comparador_anio);
                    elemento<Filme, string> nuevo_filme_no = new elemento<Filme, string>(filme, filme.Nombre, comparadornombres);
                    elemento<Filme, Filme> nuevo_filme_ge = new elemento<Filme, Filme>(filme, filme, comparadorgeneros);
                    elemento<Filme, Filme> nuevo_filme_an = new elemento<Filme, Filme>(filme, filme, comparadoranio);

                    db.catalogonombre.insertar(nuevo_filme_no.valor, nuevo_filme_no.valor.Nombre);
                    db.catalogogenero.insertar(nuevo_filme_ge.valor, nuevo_filme_ge.valor);
                    db.catalogoanio.insertar(nuevo_filme_an.valor, nuevo_filme_an.valor);
                    db.carga.CrearJsonNombre(filme);

                    if (seleccionorden == "genero")
                    {
                        db.catalogogenero.recorrer(pasar_a_lista_gen);

                    }
                    else if (seleccionorden == "nombre")
                    {
                        db.catalogonombre.recorrer(pasar_a_lista);

                    }
                    else if (seleccionorden == "anio")
                    {
                        db.catalogoanio.recorrer(pasar_a_lista_int);

                    }

                    return RedirectToAction("Index");
                }
                else {
                    
                    return RedirectToAction("Index");
                }
              
            //}
            //catch
            //{
            //    return View();
            //}
        }
        public  int comparadornombres(string actual,string Other) {
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
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filme filme_buscado = db.catalogonombre.buscar(id);

            if (filme_buscado == null)
            {

                return HttpNotFound();
            }
            return View(filme_buscado);
        }

        // POST: Filme/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {

            
            try
            {
                Filme filmebuscado = db.catalogonombre.buscar(id);
                // TODO: Add delete logic here
                db.catalogonombre.eliminar(filmebuscado.Nombre);
                db.catalogogenero.eliminar(filmebuscado);
                  db.catalogoanio.eliminar(filmebuscado);
                db.filmes_lista.Clear();
                db.catalogonombre.recorrer(asignar_comparador_nombre);
                db.catalogogenero.recorrer(asignar_comparador_genero);
                db.catalogoanio.recorrer(asignar_comparador_anio);
                if (seleccionorden == "genero")
                {
                    db.catalogogenero.recorrer(pasar_a_lista_gen);

                }
                else if (seleccionorden == "nombre")
                {
                    db.catalogonombre.recorrer(pasar_a_lista);

                }
                else if (seleccionorden == "anio")
                {
                    db.catalogoanio.recorrer(pasar_a_lista_int);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CargaJsonUsuario(HttpPostedFileBase archivo)
        {

            CargaArchivo<Usuario, string> carga = new CargaArchivo<Usuario, string>();
            db.usuarios = carga.CargajsonUsuario(archivo, Server);
            Response.Write("<script>alert('Arbol de Usuarios creado');</script>");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CargaJsonCatalogo(HttpPostedFileBase archivo)
        {
            db.filmes_lista.Clear();
            CargaArchivo<Usuario, string> carga = new CargaArchivo<Usuario, string>();
            db.catalogonombre = carga.CargajsonCatalogo(archivo, Server);
            db.catalogogenero = carga.CargajsonCatalogoGenero();
            db.catalogoanio = carga.CargajsonCatalogoAño();
            db.catalogonombre.recorrer(pasar_a_lista);
           
            Response.Write("<script>alert('Arbol de Filmes creado');</script>");

            return RedirectToAction("Index");
        }
    }
}
